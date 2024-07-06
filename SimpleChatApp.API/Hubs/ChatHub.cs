using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SimpleChatApp.API.Hubs.Models;
using SimpleChatApp.BLL.Helpers;
using SimpleChatApp.DAL.Infrastructure;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.Data.DTOs.MessageDTOs;
using SimpleChatApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.API.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private IUnitOfWork unitOfWork;
        private ResponseCreator _responseCreator;
        private IMapper _mapper;

        public ChatHub(IUnitOfWork unitOfWork, ResponseCreator responseCreator, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _responseCreator = responseCreator;
            _mapper = mapper;
        }

        public async Task JoinChat(UserConnection userConnection)
        {
            var result=unitOfWork.userInChatRepository.GetUserInChatByIds(userConnection.UserId,userConnection.ChatId);
            if (result == null)
            {
                await Clients.Client(Context.ConnectionId)
                        .ReceiveMessageAsync(403, "", "You do not have Permision!");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatId.ToString());
            }
        }

        public async Task SendMessage(AddMessageDto entity)
        {
            try
            {
                if (entity == null)
                    await Clients.Client(Context.ConnectionId)
                        .ReceiveMessageAsync(400,"","");

                var entityForDatabase = _mapper.Map<Message>(entity);
                entityForDatabase.Updated = false;
                entityForDatabase.Created = DateTime.Now;

                await unitOfWork.messageRepository.InsertAsync(entityForDatabase);
                await unitOfWork.SaveChangesAsync();

                await Clients.Group(entity.ChatId.ToString())
                    .ReceiveMessageAsync(200,entity.UserId.ToString(),entity.Text);
            }
            catch (Exception e)
            {
                await Clients.Client(Context.ConnectionId)
                        .ReceiveMessageAsync(500, "", e.Message);
            }
        }
    }
}
