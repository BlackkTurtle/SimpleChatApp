using AutoMapper;
using SimpleChatApp.BLL.Helpers;
using SimpleChatApp.BLL.Services.Contracts;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.DAL.Repositories.Contracts;
using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.UserDTOs;
using SimpleChatApp.Data.DTOs.UserInChatDTOs;
using SimpleChatApp.Data.Entities;
using SimpleChatApp.Data.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.BLL.Services
{
    public class UserInChatService:IUserInChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseCreator _responseCreator;

        private IUserInChatRepository Repository => _unitOfWork.userInChatRepository;

        public UserInChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseCreator = new ResponseCreator();
        }

        public async Task<IBaseResponse<List<GetUserInChatDto>>> GetAsync()
        {
            try
            {
                var entitiesFromDatabase = await Repository.GetAsync();

                if (entitiesFromDatabase.Count == 0)
                    return _responseCreator.CreateBaseNotFound<List<GetUserInChatDto>>("No entities found.");

                var entityDto = _mapper.Map<List<GetUserInChatDto>>(entitiesFromDatabase);

                return _responseCreator.CreateBaseOk(entityDto, entityDto.Count);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<List<GetUserInChatDto>>(e.Message);
            }
        }

        public async Task<IBaseResponse<GetUserInChatDto>> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return _responseCreator.CreateBaseBadRequest<GetUserInChatDto>("Id is empty.");

                var entityDto = _mapper.Map<GetUserInChatDto>(await Repository.GetByIdAsync(id));

                if (entityDto == null)
                    return _responseCreator.CreateBaseNotFound<GetUserInChatDto>($"Entity with id {id} not found.");

                return _responseCreator.CreateBaseOk(entityDto, 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<GetUserInChatDto>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> InsertAsync(AddUserInChatDto entity,Guid adminId)
        {
            try
            {
                if (entity == null)
                    return _responseCreator.CreateBaseBadRequest<string>("Entity is empty.");

                var chat = await _unitOfWork.chatRepository.GetByIdAsync(entity.ChatId);

                if (chat.CreatorId!=adminId)
                {
                    return _responseCreator.CreateBaseBadRequest<string>("You do not have Permision");
                }

                if (await Repository.GetUserInChatByIds(entity.UserId, entity.ChatId) == null)
                {
                    return _responseCreator.CreateBaseBadRequest<string>("User is already in chat!");
                }

                await Repository.InsertAsync(_mapper.Map<UserInChat>(entity));
                await _unitOfWork.SaveChangesAsync();

                return _responseCreator.CreateBaseOk($"Entity added.", 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<string>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return _responseCreator.CreateBaseBadRequest<string>("Id is empty.");

                await Repository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return _responseCreator.CreateBaseOk("Entity deleted.", 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<string>(e.Message);
            }
        }
    }
}
