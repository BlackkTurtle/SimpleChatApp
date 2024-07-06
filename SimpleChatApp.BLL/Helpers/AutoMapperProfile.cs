using System.Linq;
using AutoMapper;
using SimpleChatApp.Data.DTOs.ChatDTOs;
using SimpleChatApp.Data.DTOs.MessageDTOs;
using SimpleChatApp.Data.DTOs.UserDTOs;
using SimpleChatApp.Data.DTOs.UserInChatDTOs;
using SimpleChatApp.Data.Entities;

namespace SimpleChatApp.BLL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Chat, AddChatDto>().ReverseMap();
            CreateMap<Chat, GetChatDto>().ReverseMap();
            CreateMap<Chat, UpdateChatDto>().ReverseMap();

            CreateMap<Message, AddMessageDto>().ReverseMap();
            CreateMap<Message, GetMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();

            CreateMap<User, AddUserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();

            CreateMap<UserInChat, AddUserInChatDto>().ReverseMap();
            CreateMap<UserInChat, GetUserInChatDto>().ReverseMap();
        }
    }
}