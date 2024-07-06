using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Data.DTOs.ChatDTOs
{
    public class AddChatDto
    {
        public string Name { get; set; }
        public Guid CreatorId { get; set; }
    }
}
