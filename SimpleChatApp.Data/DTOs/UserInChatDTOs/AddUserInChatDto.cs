using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Data.DTOs.UserInChatDTOs
{
    public class AddUserInChatDto
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
