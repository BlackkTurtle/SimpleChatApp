using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Data.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreatorId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = null!;
        public ICollection<UserInChat> UserInChats { get; set; } = null!;
    }
}
