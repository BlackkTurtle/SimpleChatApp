using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Repositories.Contracts
{
    public interface IMessageRepository:IGenericRepository<Message>
    {
    }
}
