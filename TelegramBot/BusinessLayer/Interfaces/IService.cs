namespace TelegramBot.BusinessLayer.Interfaces
{
    public interface IService<T> where T : class
    {
        Task Create(T entity);
        Task<T> GetById(long id);
        Task<IList<T>> GetAll();
        Task<IList<T>> GetByName(string name);
        Task Delete(long id);
        Task Update(long id, T entity);
    }
}
