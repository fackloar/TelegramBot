using TelegramBot.BusinessLayer.DTOs;

namespace TelegramBot.BusinessLayer.Interfaces
{
    public interface IUserService : IService<UserDTO>
    {
        Task<IList<UserDTO>> GetUsersOfChat(long chatId);
    }
}
