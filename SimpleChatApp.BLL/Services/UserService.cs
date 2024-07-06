using AutoMapper;
using SimpleChatApp.BLL.Helpers;
using SimpleChatApp.BLL.Services.Contracts;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.DAL.Repositories.Contracts;
using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.UserDTOs;
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
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseCreator _responseCreator;

        private IUserRepository Repository => _unitOfWork.userRepository;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseCreator = new ResponseCreator();
        }

        public async Task<IBaseResponse<List<GetUserDto>>> GetAsync()
        {
            try
            {
                var entitiesFromDatabase = await Repository.GetAsync();

                if (entitiesFromDatabase.Count == 0)
                    return _responseCreator.CreateBaseNotFound<List<GetUserDto>>("No entities found.");

                var entityDto = _mapper.Map<List<GetUserDto>>(entitiesFromDatabase);

                return _responseCreator.CreateBaseOk(entityDto, entityDto.Count);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<List<GetUserDto>>(e.Message);
            }
        }

        public async Task<IBaseResponse<GetUserDto>> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return _responseCreator.CreateBaseBadRequest<GetUserDto>("Id is empty.");

                var entityDto = _mapper.Map<GetUserDto>(await Repository.GetByIdAsync(id));

                if (entityDto == null)
                    return _responseCreator.CreateBaseNotFound<GetUserDto>($"Entity with id {id} not found.");

                return _responseCreator.CreateBaseOk(entityDto, 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<GetUserDto>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> InsertAsync(AddUserDto entity)
        {
            try
            {
                if (entity == null)
                    return _responseCreator.CreateBaseBadRequest<string>("Entity is empty.");

                await Repository.InsertAsync(_mapper.Map<User>(entity));
                await _unitOfWork.SaveChangesAsync();

                return _responseCreator.CreateBaseOk($"Entity added.", 1);
            }
            catch (Exception e)
            {
                return _responseCreator.CreateBaseServerError<string>(e.Message);
            }
        }

        public async Task<IBaseResponse<string>> UpdateAsync(UpdateUserDto entity)
        {
            try
            {
                if (entity == null)
                    return _responseCreator.CreateBaseBadRequest<string>("Entity is empty.");

                await Repository.UpdateAsync(_mapper.Map<User>(entity));
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
