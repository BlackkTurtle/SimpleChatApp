using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.API.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessageAsync(int statusCode,string UserId, string message);
    }
}
