using Microsoft.EntityFrameworkCore;
using TelegramBot.DataLayer.Interfaces;
using TelegramBot.DataLayer.Models;

namespace TelegramBot.DataLayer.Repositories
{
    public class UserDBRepository : IUserDBRepository
    {
        DataContext _dataContext;

        public UserDBRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task Create(UserDB entity)
        {
            using (_dataContext)
            {
                await _dataContext.AddAsync(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            using (_dataContext)
            {
                var userToDelete = _dataContext.User.Find(id);

                if (userToDelete != null)
                {
                    _dataContext.Remove(userToDelete);
                }
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IList<UserDB>> GetAll()
        {
            return await _dataContext.User.OrderBy(dc => dc.Id).ToListAsync();
        }

        public async Task<UserDB> GetById(long id)
        {
            var result = await _dataContext.User
                    .Where(user => user.Id == id)
                    .SingleOrDefaultAsync();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("No user with this ID");
            }
        }

        public async Task<IList<UserDB>> GetByName(string username)
        {
            var result = await _dataContext.User
                    .Where(user => user.Username == username)
                    .ToListAsync();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("No user with this username");
            }
        }

        public async Task Update(long id, UserDB entity)
        {
            using (_dataContext)
            {
                var userToChange = _dataContext.User.Find(id);
                if (userToChange != null)
                {
                    userToChange.Id = entity.Id;
                    userToChange.Username = entity.Username;
                    userToChange.FirstName = entity.FirstName;
                }
                _dataContext.Update(userToChange);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IList<UserDB>> GetUsersOfChat(long chatId)
        {
            using (_dataContext)
            {
                var result = await _dataContext.User
                        .Where(users => users.ChatId == chatId)
                        .ToListAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new Exception("No users in this chat");
                }
            }
        }
    }
}
