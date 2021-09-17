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

namespace NotificationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private ApplicationDbContext _context;
        private TwilioSend _twilio;
        public EventController(ApplicationDbContext context, TwilioSend twilio)
        {
            _context = context;
            _twilio = twilio;
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
                else
                {
                    User user = new User()
                    {
                        UserName = request.UserName,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        HashedPassword = request.HashedPassword,
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
        public IActionResult SubsribeToSupervisor()
        {
            try
            {
                // Grab list of supervisors and their ID
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("sendNotifications")]
        public IActionResult SendNotifications(int id)
        {
            try
            {
                // Grab list of supervisors and their ID
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost("user/signin")]
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

        [HttpPost("user/subscriptions")]
        public IActionResult GetUserSubscriptions()
        {
            try
            {
                // Grab list of supervisors and their ID
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("user/removeSubscription")]
        public IActionResult RemoveSubscription()
        {
            try
            {
                // Grab list of supervisors and their ID
                return Ok();
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
            return "";
        }

        private bool PhoneNumberValidation(string phoneNumber)
        {
            Regex rx = new Regex(@"+1\d{10}");
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
