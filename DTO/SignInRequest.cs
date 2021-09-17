using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.DTO
{
    public class SignInRequest
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
}
