using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public ICollection<Message> Messages { get; set; } = null!;
        public ICollection<UserInChat> UserInChats { get; set; } = null!;
        public ICollection<Chat> Chats { get; set; } = null!;
    }
}
