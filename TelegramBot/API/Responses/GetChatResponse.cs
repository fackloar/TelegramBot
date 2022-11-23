using TelegramBot.BusinessLayer.DTOs;

namespace TelegramBot.API.Responses
{
    public class GetChatsResponse
    {
        public List<ChatDTO> Chats { get; set; }
    }
}
