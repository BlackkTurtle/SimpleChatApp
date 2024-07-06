using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Data.DTOs.MessageDTOs
{
    public class GetMessageDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public bool Updated { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
