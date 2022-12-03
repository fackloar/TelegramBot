using TelegramBot.BusinessLayer.DTOs;
using TelegramBot.DataLayer.Models;

namespace TelegramBot.BusinessLayer.Interfaces
{
    public interface IChatService : IService<ChatDTO>
    {
        Task Delete(long id);
        Task Update(long id, ChatDTO entity);
        Task<ChatDTO> GetById(long id);

    }
}
