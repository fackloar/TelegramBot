using TelegramBot.DataLayer.Models;

namespace TelegramBot.DataLayer.Interfaces
{
    public interface IUserDBRepository : IRepository<UserDB>
    {
        Task<IList<UserDB>> GetUsersOfChat(long chatId);
    }
}
