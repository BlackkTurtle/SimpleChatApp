using SimpleChatApp.DAL.Infrastructure;
using SimpleChatApp.DAL.Repositories.Contracts;
using SimpleChatApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Repositories
{
    public class MessageRepository:GenericRepository<Message>,IMessageRepository
    {
        public MessageRepository(SimpleChatDbContext context) : base(context)
        {
        }
    }
}
