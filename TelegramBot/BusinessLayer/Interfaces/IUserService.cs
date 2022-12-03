using TelegramBot.BusinessLayer.DTOs;
using TelegramBot.DataLayer.Models;

namespace TelegramBot.BusinessLayer.Interfaces
{
    public interface IUserService : IService<UserDTO>
    {
        Task<IList<UserDTO>> GetUsersOfChat(long chatId);
        Task<UserDTO> GetByIdWithChat(long chatId, long telegramId);
        Task<IList<UserDTO>> GetByTelegramId(long id);
        Task<IList<UserDTO>> GetTopWinners(long chatId);
        Task<IList<UserDTO>> GetTopKarma(long chatId);
        Task Delete(int id);
        Task Update(int id, UserDTO entity);
        Task<UserDTO> GetById(int id);

    }
}
