using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSystem.Models
{
    public class Subscription
    {
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public User Publisher { get; set; }
        [ForeignKey("Subscriber")]
        public int SubscriberId { get; set; }
        public User Subscriber { get; set; }
    }
}
