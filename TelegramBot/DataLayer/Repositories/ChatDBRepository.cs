using Microsoft.EntityFrameworkCore;
using TelegramBot.DataLayer.Interfaces;
using TelegramBot.DataLayer.Models;

namespace TelegramBot.DataLayer.Repositories
{
    public class ChatDBRepository : IRepository<ChatDB>
    {
        DataContext _dataContext;

        public ChatDBRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task Create(ChatDB entity)
        {
            using (_dataContext)
            {
                if (entity != GetById(entity.Id).Result)
                {
                    await _dataContext.AddAsync(entity);
                    await _dataContext.SaveChangesAsync();
                }
            }
        }

        public async Task Delete(long id)
        {
            using (_dataContext)
            {
                var chatToDelete = _dataContext.Chat.Find(id);

                if (chatToDelete != null)
                {
                    _dataContext.Remove(chatToDelete);
                }
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IList<ChatDB>> GetAll()
        {
            return await _dataContext.Chat.OrderBy(dc => dc.Id).ToListAsync();
        }

        public async Task<ChatDB> GetById(long id)
        {
            var result = await _dataContext.Chat
                    .Where(chat => chat.Id == id)
                    .SingleOrDefaultAsync();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("No chat with this ID");
            }
        }

        public async Task<IList<ChatDB>> GetByName(string title)
        {
            var result = await _dataContext.Chat
                    .Where(chat => chat.Title == title)
                    .ToListAsync();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("No chat with this chatname");
            }
        }

        public async Task Update(long id, ChatDB entity)
        {
            using (_dataContext)
            {
                var chatToChange = _dataContext.Chat.Find(id);
                if (chatToChange != null)
                {
                    chatToChange.Id = entity.Id;
                    chatToChange.Title = entity.Title;
                    chatToChange.Type = entity.Type;
                }
                _dataContext.Update(chatToChange);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
