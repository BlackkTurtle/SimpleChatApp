using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChatApp.BLL.Services.Contracts
{
    public interface IChatService
    {
        Task<IBaseResponse<List<GetChatDto>>> GetAsync();
        Task<IBaseResponse<GetChatDto>> GetByIdAsync(Guid id);
        Task<IBaseResponse<string>> InsertAsync(AddChatDto entity);
        Task<IBaseResponse<string>> UpdateAsync(UpdateChatDto entity);
        Task<IBaseResponse<string>> DeleteAsync(Guid id);
    }
}