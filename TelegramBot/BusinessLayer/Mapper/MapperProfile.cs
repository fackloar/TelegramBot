using AutoMapper;
using TelegramBot.BusinessLayer.DTOs;
using TelegramBot.DataLayer.Models;

namespace TelegramBot.BusinessLayer.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ChatDB, ChatDTO>();
            CreateMap<UserDB, UserDTO>();
            CreateMap<ChatDTO, ChatDB>();
            CreateMap<UserDTO, UserDB>();
        }
    }
}
