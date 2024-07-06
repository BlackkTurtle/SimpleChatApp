using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.DAL.Repositories.Contracts;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly SimpleChatDbContext _Context;

    public IChatRepository chatRepository { get; }
    public IMessageRepository messageRepository { get; }
    public IUserRepository userRepository { get; }
    public IUserInChatRepository userInChatRepository { get; }

    public UnitOfWork
        (SimpleChatDbContext Context, IChatRepository chatRepository, IMessageRepository messageRepository, IUserRepository userRepository, IUserInChatRepository userInChatRepository)
    {
        _Context = Context;
        this.chatRepository = chatRepository;
        this.messageRepository = messageRepository;
        this.userRepository = userRepository;
        this.userInChatRepository = userInChatRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _Context.SaveChangesAsync();
    }
}