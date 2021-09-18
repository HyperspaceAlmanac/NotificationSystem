using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.DTO
{
    public class SubscribeRequest
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int SupervisorId { get; set; }
    }
}
