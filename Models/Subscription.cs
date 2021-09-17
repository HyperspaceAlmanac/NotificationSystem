using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.Models
{
    public class Subscription
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Supervisor")]
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
    }
}
