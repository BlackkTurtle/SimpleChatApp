using AutoMapper;
using SimpleChatApp.BLL.Helpers;
using SimpleChatApp.BLL.Services.Contracts;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.DAL.Repositories.Contracts;
using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.MessageDTOs;
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
    public class MessageService:IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseCreator _responseCreator;

        private IMessageRepository Repository => _unitOfWork.messageRepository;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseCreator = new ResponseCreator();
        }

        public async Task<IBaseResponse<List<GetMessageDto>>> GetAsync()
        {
            try
            {
                var entitiesFromDatabase = await Repository.GetAsync();

                if (entitiesFromDatabase.Count == 0)
                    return _responseCreator.CreateBaseNotFound<List<GetMessageDto>>("No entities found.");

                var entityDto = _mapper.Map<List<GetMessageDto>>(entitiesFromDatabase);

                return _responseCreator.CreateBaseOk(entityDto, entityDto.Count);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<List<GetMessageDto>>(e.Message);
            }
        }

        public async Task<IBaseResponse<GetMessageDto>> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return _responseCreator.CreateBaseBadRequest<GetMessageDto>("Id is empty.");

                var entityDto = _mapper.Map<GetMessageDto>(await Repository.GetByIdAsync(id));

                if (entityDto == null)
                    return _responseCreator.CreateBaseNotFound<GetMessageDto>($"Entity with id {id} not found.");

                return _responseCreator.CreateBaseOk(entityDto, 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<GetMessageDto>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> InsertAsync(AddMessageDto entity)
        {
            try
            {
                if (entity == null)
                    return _responseCreator.CreateBaseBadRequest<string>("Entity is empty.");

                var entityForDatabase = _mapper.Map<Message>(entity);
                entityForDatabase.Updated = false;
                entityForDatabase.Created=DateTime.Now;

                await Repository.InsertAsync(entityForDatabase);
                await _unitOfWork.SaveChangesAsync();

                return _responseCreator.CreateBaseOk($"Entity added.", 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<string>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> UpdateAsync(UpdateMessageDto entity)
        {
            try
            {
                if (entity == null)
                    return _responseCreator.CreateBaseBadRequest<string>("Entity is empty.");

                var entityFromDatabase = await Repository.GetByIdAsync(entity.Id);
                if(entityFromDatabase == null)
                {
                    return _responseCreator.CreateBaseBadRequest<string>("Entity with provided Id does not exist.");
                }
                entityFromDatabase.Updated=true;
                entityFromDatabase.Text= entity.Text;

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
