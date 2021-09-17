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
        public ApplicationDbContext _context;
        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("supervisors")]
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
        [HttpPost("supervisor")]
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
        [HttpPost("user")]
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
        [HttpPost("subscribe/{id}")]
        public IActionResult SubsribeToSupervisor(int id)
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
