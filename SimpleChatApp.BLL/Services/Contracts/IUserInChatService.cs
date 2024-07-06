using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.UserDTOs;
using SimpleChatApp.Data.DTOs.UserInChatDTOs;
using SimpleChatApp.Data.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChatApp.BLL.Services.Contracts
{
    public interface IUserInChatService
    {
        Task<IBaseResponse<List<GetUserInChatDto>>> GetAsync();
        Task<IBaseResponse<GetUserInChatDto>> GetByIdAsync(Guid id);
        Task<IBaseResponse<string>> InsertAsync(AddUserInChatDto entity,Guid adminId);
        Task<IBaseResponse<string>> DeleteAsync(Guid id);
    }
}