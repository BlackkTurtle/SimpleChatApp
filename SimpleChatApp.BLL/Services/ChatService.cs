using AutoMapper;
using SimpleChatApp.BLL.Helpers;
using SimpleChatApp.BLL.Services.Contracts;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.DAL.Repositories.Contracts;
using SimpleChatApp.Data.DTOs.ChatDTOs;
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
    public class ChatService:IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseCreator _responseCreator;

        private IChatRepository Repository => _unitOfWork.chatRepository;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseCreator = new ResponseCreator();
        }

        public async Task<IBaseResponse<List<GetChatDto>>> GetAsync()
        {
            try
            {
                var entitiesFromDatabase = await Repository.GetAsync();

                if (entitiesFromDatabase.Count == 0)
                    return _responseCreator.CreateBaseNotFound<List<GetChatDto>>("No entities found.");

                var entityDto = _mapper.Map<List<GetChatDto>>(entitiesFromDatabase);

                return _responseCreator.CreateBaseOk(entityDto, entityDto.Count);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<List<GetChatDto>>(e.Message);
            }
        }

        public async Task<IBaseResponse<GetChatDto>> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return _responseCreator.CreateBaseBadRequest<GetChatDto>("Id is empty.");

                var entityDto = _mapper.Map<GetChatDto>(await Repository.GetByIdAsync(id));

                if (entityDto == null)
                    return _responseCreator.CreateBaseNotFound<GetChatDto>($"Entity with id {id} not found.");

                return _responseCreator.CreateBaseOk(entityDto, 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<GetChatDto>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> InsertAsync(AddChatDto entity)
        {
            try
            {
                if (entity == null)
                    return _responseCreator.CreateBaseBadRequest<string>("Entity is empty.");

                await Repository.InsertAsync(_mapper.Map<Chat>(entity));
                await _unitOfWork.SaveChangesAsync();

                return _responseCreator.CreateBaseOk($"Entity added.", 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<string>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> UpdateAsync(UpdateChatDto entity)
        {
            try
            {
                if (entity == null)
                    return _responseCreator.CreateBaseBadRequest<string>("Entity is empty.");

                await Repository.UpdateAsync(_mapper.Map<Chat>(entity));
                await _unitOfWork.SaveChangesAsync();

                return _responseCreator.CreateBaseOk("Entity updated.", 1);
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
