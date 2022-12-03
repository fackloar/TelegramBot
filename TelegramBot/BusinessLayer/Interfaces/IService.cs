namespace TelegramBot.BusinessLayer.Interfaces
{
    public interface IService<T> where T : class
    {
        Task Create(T entity);
        Task<IList<T>> GetAll();
        Task<IList<T>> GetByName(string name);

    }
}
