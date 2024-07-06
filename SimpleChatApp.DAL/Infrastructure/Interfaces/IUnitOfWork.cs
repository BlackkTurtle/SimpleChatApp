
using SimpleChatApp.DAL.Repositories.Contracts;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IChatRepository chatRepository { get; }
    IMessageRepository messageRepository { get; }
    IUserRepository userRepository { get; }
    IUserInChatRepository userInChatRepository { get; }

    Task SaveChangesAsync();
}