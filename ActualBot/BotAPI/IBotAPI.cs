using ActualBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ActualBot.BotAPI
{
    public interface IBotAPI
    {
        Task<HttpResponseMessage> CreateUserAsync(UserDTO user);
        Task<List<UserDTO>> GetUsersOfChatAsync(long chatId);
        Task<UserDTO> GetUserByIdAsync(long id);
        Task<HttpResponseMessage> CreateChatAsync(ChatDTO chat);
        Task<ChatDTO> GetChatByIdAsync(long id);


    }
}
