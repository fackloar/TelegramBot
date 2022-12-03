using TelegramBot.DataLayer.Models;

namespace TelegramBot.DataLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task<IList<T>> GetAll();
        Task<IList<T>> GetByName(string name);
    }
}
