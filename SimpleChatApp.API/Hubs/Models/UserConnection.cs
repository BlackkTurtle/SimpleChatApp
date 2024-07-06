using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.API.Hubs.Models
{
    public class UserConnection
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
