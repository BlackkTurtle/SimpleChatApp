using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleChatApp.BLL.Services.Contracts;
using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.UserDTOs;
using SimpleChatApp.Data.Responses;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService Service { get; }

        public UserController(IUserService service)
        {
            Service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<List<GetUserDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetUserDto>>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<List<GetUserDto>>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var response = await Service.GetAsync();

            return response.StatusCode switch
            {
                SimpleChatApp.Data.Responses.Enums.StatusCode.Ok => Ok(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.NotFound => NotFound(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.BadRequest => BadRequest(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.InternalServerError => StatusCode(500, response),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BaseResponse<GetUserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<GetUserDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<GetUserDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<GetUserDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await Service.GetByIdAsync(id);

            return response.StatusCode switch
            {
                SimpleChatApp.Data.Responses.Enums.StatusCode.Ok => Ok(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.NotFound => NotFound(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.BadRequest => BadRequest(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.InternalServerError => StatusCode(500, response),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<AddUserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<AddUserDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AddUserDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(AddUserDto entity)
        {
            var response = await Service.InsertAsync(entity);

            return response.StatusCode switch
            {
                SimpleChatApp.Data.Responses.Enums.StatusCode.Ok => Ok(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.NotFound => NotFound(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.BadRequest => BadRequest(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.InternalServerError => StatusCode(500, response),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<UpdateUserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<UpdateUserDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<UpdateUserDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UpdateUserDto entity)
        {
            var response = await Service.UpdateAsync(entity);
            
            return response.StatusCode switch
            {
                SimpleChatApp.Data.Responses.Enums.StatusCode.Ok => Ok(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.NotFound => NotFound(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.BadRequest => BadRequest(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.InternalServerError => StatusCode(500, response),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Service.DeleteAsync(id);
            
            return response.StatusCode switch
            {
                SimpleChatApp.Data.Responses.Enums.StatusCode.Ok => Ok(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.NotFound => NotFound(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.BadRequest => BadRequest(response),
                SimpleChatApp.Data.Responses.Enums.StatusCode.InternalServerError => StatusCode(500, response),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}