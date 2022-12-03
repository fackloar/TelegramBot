using TelegramBot.DataLayer.Models;

namespace TelegramBot.DataLayer.Interfaces
{
    public interface IChatDBRepository : IRepository<ChatDB>
    {
        Task<ChatDB> GetById(long id);
        Task Delete(long id);
        Task Update(long id, ChatDB entity);
    }
}
