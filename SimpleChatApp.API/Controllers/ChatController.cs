using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleChatApp.BLL.Services.Contracts;
using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.UserInChatDTOs;
using SimpleChatApp.Data.Responses;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInChatController : ControllerBase
    {
        private IUserInChatService Service { get; }

        public UserInChatController(IUserInChatService service)
        {
            Service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<List<GetUserInChatDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetUserInChatDto>>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<List<GetUserInChatDto>>), (int)HttpStatusCode.InternalServerError)]
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
        [ProducesResponseType(typeof(BaseResponse<GetUserInChatDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<GetUserInChatDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<GetUserInChatDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<GetUserInChatDto>), (int)HttpStatusCode.InternalServerError)]
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
        [ProducesResponseType(typeof(BaseResponse<AddUserInChatDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<AddUserInChatDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AddUserInChatDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(AddUserInChatDto entity,Guid adminId)
        {
            var response = await Service.InsertAsync(entity,adminId);

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