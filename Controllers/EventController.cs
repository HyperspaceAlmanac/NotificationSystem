using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        //async Task<IActionResult>
        public IActionResult GetSupervisors()
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
        [HttpPost("supervisor/new")]
        public IActionResult NewSupervisor()
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
        [HttpPost("user/new")]
        public IActionResult NewUser()
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
        [HttpPost("sendNotification")]
        public IActionResult SendNotification(int id)
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
        [HttpPost("supervisor/signin")]
        public IActionResult SupervisorSignIn()
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
        public IActionResult UserSignIn()
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
    }
}
