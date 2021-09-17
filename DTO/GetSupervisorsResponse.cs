using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.DTO
{
    public class GetSupervisorsResponse
    {
        public string Result { get; set; }
        public List<SupervisorDTO> Supervisors { get; set; }
    }
}
