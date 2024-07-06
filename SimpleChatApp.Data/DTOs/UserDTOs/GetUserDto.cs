using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Data.DTOs.UserDTOs
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
