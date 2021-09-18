using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationSystem.Data;
using NotificationSystem.DTO;
using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace NotificationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private ApplicationDbContext _context;
        private TwilioSend _twilio;
        private char[] _alphaNumerical;
        public EventController(ApplicationDbContext context, TwilioSend twilio)
        {
            _context = context;
            _twilio = twilio;
            _alphaNumerical = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIKLMNOPQRSTUVWXYZ".ToCharArray();
        }

        [HttpPut("Supervisors")]
        public async Task<IActionResult> GetSupervisors([FromBody] GetSupervisorsRequest request)
        {
            GetSupervisorsResponse response = new GetSupervisorsResponse() { Result = "Success" }; 
            try
            {
                if (await TokenValid(request.UserName, request.Token))
                {
                    response.Supervisors = await _context.Users.Where(u => u.Supervisor).Select(u => UserDTO.ToDTO(u)).ToListAsync<UserDTO>();
                }
                else
                {
                    response.Result = "Invalid or Expired Token";
                }
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("supervisor/new")]
        public async Task<IActionResult> NewSupervisor([FromBody] NewUserRequest request)
        {
            TokenResponse response = new TokenResponse() { Result = "Success", Token = "", Message = "" };
            try
            {
                int count = await _context.Users.Where(s => s.UserName == request.UserName).CountAsync();
                if (count > 0)
                {
                    response.Result = "Error";
                    response.Message = "Username is already in use";
                }
                else
                {
                    User supervisor = new User()
                    {
                        UserName = request.UserName,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        HashedPassword = request.HashedPassword,
                        PhoneNumber = request.PhoneNumber,
                        Supervisor = true
                    };
                    await _context.AddAsync(supervisor);
                    await _context.SaveChangesAsync();
                    UserToken token = new UserToken()
                    {
                        UserId = supervisor.Id,
                        Token = GenerateToken(),
                        Expiration = DateTime.Now.AddHours(1)
                    };
                    await _context.AddAsync(token);
                    await _context.SaveChangesAsync();
                    response.Token = token.Token;
                }
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("user/new")]
        public async Task<IActionResult> NewUser([FromBody] NewUserRequest request)
        {
            TokenResponse response = new TokenResponse() { Result = "Success", Token = "", Message = "" };
            try
            {
                int count = await _context.Users.Where(s => s.UserName == request.UserName).CountAsync();
                if (count > 0)
                {
                    response.Result = "Error";
                    response.Message = "Username is already in use";
                }
                else if (!PhoneNumberValidation(request.PhoneNumber))
                {
                    response.Result = "Error";
                    response.Message = "Invalid phone number. Please ues the +1xxxyyyzzzz format";
                } else {
                    User user = new User()
                    {
                        UserName = request.UserName,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        HashedPassword = request.HashedPassword,
                        PhoneNumber = request.PhoneNumber,
                        Supervisor = false
                    };
                    await _context.AddAsync(user);
                    await _context.SaveChangesAsync();
                    UserToken token = new UserToken()
                    {
                        UserId = user.Id,
                        Token = GenerateToken(),
                        Expiration = DateTime.Now.AddHours(1)
                    };
                    await _context.AddAsync(token);
                    await _context.SaveChangesAsync();
                    response.Token = token.Token;
                }
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubsribeToSupervisor([FromBody] SubscribeRequest request)
        {
            SimpleResponse response = new SimpleResponse() { Result = "Error", Message = "" };
            try
            {
                if (await TokenValid(request.UserName, request.Token))
                {
                    User supervisor = await _context.Users.Where(u => u.Id == request.SupervisorId && u.Supervisor == true).SingleOrDefaultAsync();
                    User user = await _context.Users.Where(u => u.UserName == request.UserName).SingleOrDefaultAsync();
                    if (supervisor != null && user != null)
                    {

                        Subscription sub = new Subscription()
                        {
                            PublisherId = supervisor.Id,
                            SubscriberId = user.Id
                        };
                        await _context.Subscriptions.AddAsync(sub);
                        await _context.SaveChangesAsync();
                        response.Result = "Success";
                        response.Message = "Successfully Subscribed";
                    }
                    else
                    {
                        response.Message = "Invalid UserName or Supervisor Id";
                    }
                }
                else
                {
                    response.Message = "The Token has either invalid or expired";
                }
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("sendNotifications")]
        public async Task<IActionResult> SendNotifications([FromBody] NotifyRequest request)
        {
            SimpleResponse response = new SimpleResponse()
            {
                Result = "Error",
                Message = ""
            };
            try
            {
                if (await TokenValid(request.UserName, request.Token) == true)
                {
                    List<String> numbers = await _context.Subscriptions.Include(u => u.Subscriber).Include(u => u.Publisher)
                        .Where(u => u.Publisher.UserName == request.UserName).Select(u => u.Subscriber.PhoneNumber).ToListAsync();
                    if (numbers.Count == 0)
                    {
                        response.Message = "No subscribers found";
                    }
                    else
                    {
                        _twilio.SendNotifications(numbers, request.Message);
                        response.Result = "Succes";
                        response.Message = String.Format("Sent notifications to {0} {1}", numbers.Count, numbers.Count == 1 ? "user" : "users");
                    }
                }
                else
                {
                    response.Message = "Invalid Token";
                }
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost("user/SignIn")]
        public async Task<IActionResult> UserSignIn([FromBody] SignInRequest request)
        {
            TokenResponse response = new TokenResponse()
            {
                Result = "Success",
                Message = ""
            };
            try
            {
                UserToken token = await _context.UserTokens.Include(u => u.User)
                    .Where(u => u.User.UserName == request.UserName && u.User.HashedPassword == request.HashedPassword)
                    .FirstOrDefaultAsync();
                if (token != null)
                {
                    token.Expiration = DateTime.Now.AddHours(1);
                    token.Token = GenerateToken();
                    _context.Update(token);
                    await _context.SaveChangesAsync();
                    response.Token = token.Token;
                }
                else
                {
                    response.Result = "Error";
                    response.Message = "Invalid UserName or Incorrect Password";
                }
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // utility functions
        private async Task<bool> TokenValid(string userName, string Token)
        {
            DateTime now = DateTime.Now;
            var matches = await _context.UserTokens.Include(s => s.User)
                    .Where(t => t.User.UserName == userName && t.Token == Token && now < t.Expiration).ToListAsync();
            return matches.Count >= 1;    
        }

        private string GenerateToken()
        {
            Random rng = new Random();
            char[] token = new char[20];
            for (int i = 0; i < 20; i++)
            {
                token[i] = _alphaNumerical[rng.Next(_alphaNumerical.Length)];
            }
            return String.Join("", token);
        }

        private bool PhoneNumberValidation(string phoneNumber)
        {
            Regex rx = new Regex(@"\+1\d{10}");
            if (phoneNumber.Length != 12)
            {
                return false;
            }
            else
            {
                return rx.IsMatch(phoneNumber);
            }
        }
    }
}
