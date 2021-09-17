using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.DTO
{
    public class SupervisorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static SupervisorDTO ToDTO(Supervisor supervisor)
        {
            return new SupervisorDTO() { Id = supervisor.Id, FirstName = supervisor.FirstName, LastName = supervisor.LastName };
        }
    }
}
