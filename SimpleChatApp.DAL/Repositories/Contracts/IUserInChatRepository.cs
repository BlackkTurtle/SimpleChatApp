using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Repositories.Contracts
{
    public interface IUserInChatRepository:IGenericRepository<UserInChat>
    {
        public Task<UserInChat> GetUserInChatByIds(Guid userid, Guid chatid);
    }
}
