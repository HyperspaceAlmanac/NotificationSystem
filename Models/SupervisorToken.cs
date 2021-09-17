using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.Models
{
    public class SupervisorToken
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Supervisor")]
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
