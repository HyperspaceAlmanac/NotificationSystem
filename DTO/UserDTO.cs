using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static UserDTO ToDTO(User user)
        {
            return new UserDTO() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName };
        }
    }
}
