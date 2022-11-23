using TelegramBot.BusinessLayer.DTOs;

namespace TelegramBot.API.Responses
{
    public class GetUsersResponse
    {
        public List<UserDTO> Users { get; set; }
    }
}
