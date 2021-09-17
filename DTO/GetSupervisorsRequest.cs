using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.DTO
{
    public class GetSupervisorsRequest
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool Supervisor { get; set; }
    }
}
