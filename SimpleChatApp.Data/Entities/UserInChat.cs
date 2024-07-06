using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Data.Entities
{
    public class UserInChat
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public User User { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
    }
}
