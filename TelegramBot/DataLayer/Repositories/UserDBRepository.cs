using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;
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
                UserDB tryFind = await _dataContext.ChatUser.Where(user => user.TelegramId == entity.TelegramId)
                    .Where(user => user.ChatId == entity.ChatId).FirstOrDefaultAsync();

                if (tryFind == null)
                {
                    await _dataContext.AddAsync(entity);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("This user already exists");
                 
                }

            }
        }

        public async Task Delete(int id)
        {
            using (_dataContext)
            {
                var userToDelete = _dataContext.ChatUser.Find(id);

                if (userToDelete != null)
                {
                    _dataContext.Remove(userToDelete);
                }
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IList<UserDB>> GetAll()
        {
            return await _dataContext.ChatUser.OrderBy(dc => dc.Id).ToListAsync();
        }

        public async Task<IList<UserDB>> GetByTelegramId(long telegramId)
        {
            var result = await _dataContext.ChatUser
                    .Where(user => user.Id == telegramId)
                    .ToListAsync();
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
            var result = await _dataContext.ChatUser
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

        public async Task Update(int id, UserDB entity)
        {
            using (_dataContext)
            {
                var userToChange = _dataContext.ChatUser.Find(id);
                if (userToChange != null)
                {
                    userToChange.Id = entity.Id;
                    userToChange.Username = entity.Username;
                    userToChange.FirstName = entity.FirstName;
                    userToChange.WinsNumber = entity.WinsNumber;
                    userToChange.Karma = entity.Karma;
                    userToChange.KarmaSwitch = entity.KarmaSwitch;
                    userToChange.Messages = entity.Messages;
                }
                _dataContext.Update(userToChange);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IList<UserDB>> GetUsersOfChat(long chatId)
        {
            using (_dataContext)
            {
                var result = await _dataContext.ChatUser
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

        public async Task<UserDB> GetByIdWithChat(long chatId, long telegramId)
        {
            using (_dataContext)
            {
                var result = await _dataContext.ChatUser
                    .Where(user => user.TelegramId == telegramId && user.ChatId == chatId)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new Exception("No user with this ID");
                }
            }
        }

        public async Task<IList<UserDB>> GetTopWinners(long chatId)
        {
            using (_dataContext)
            {
                var result = await _dataContext.ChatUser
                    .Where(user => user.ChatId == chatId)
                    .OrderBy(order => order.WinsNumber)
                    .ToListAsync();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new Exception("No user with this ID");
                }
            }
        }

        public async Task<IList<UserDB>> GetTopKarma(long chatId)
        {
            using (_dataContext)
            {
                var result = await _dataContext.ChatUser
                    .Where(user => user.ChatId == chatId)
                    .OrderBy(order => order.Karma)
                    .ToListAsync();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new Exception("No user with this ID");
                }
            }
        }

        public async Task<UserDB> GetById(int id)
        {
            using (_dataContext)
            {
                var result = await _dataContext.ChatUser.FindAsync(id);
                return result;
            }
        }
    }
}
