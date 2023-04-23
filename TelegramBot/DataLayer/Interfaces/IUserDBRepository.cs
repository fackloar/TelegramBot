using TelegramBot.DataLayer.Models;

namespace TelegramBot.DataLayer.Interfaces
{
    public interface IUserDBRepository : IRepository<UserDB>
    {
        Task<IList<UserDB>> GetUsersOfChat(long chatId);
        Task<UserDB> GetByIdWithChat(long chatId, long telegramId);
        Task<IList<UserDB>> GetTopWinners(long chatId);
        Task<IList<UserDB>> GetTopKarma(long chatId);
        Task<IList<UserDB>> GetByTelegramId(long telegramId);
        Task<UserDB> GetById(int id);
        Task Delete(int id);
        Task Update(int id, UserDB entity);
        Task UpdateMessages(int id);
    }
}
