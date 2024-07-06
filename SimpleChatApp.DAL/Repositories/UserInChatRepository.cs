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
    public class UserInChatRepository:GenericRepository<UserInChat>,IUserInChatRepository
    {
        private SimpleChatDbContext _context;
        public UserInChatRepository(SimpleChatDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<UserInChat> GetUserInChatByIds(Guid userid,Guid chatid)
        {
            return await _context.UsersInChat.FindAsync(userid, chatid);
        }
    }
}
