using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.UserDTOs;
using SimpleChatApp.Data.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChatApp.BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<IBaseResponse<List<GetUserDto>>> GetAsync();
        Task<IBaseResponse<GetUserDto>> GetByIdAsync(Guid id);
        Task<IBaseResponse<string>> InsertAsync(AddUserDto entity);
        Task<IBaseResponse<string>> UpdateAsync(UpdateUserDto entity);
        Task<IBaseResponse<string>> DeleteAsync(Guid id);
    }
}