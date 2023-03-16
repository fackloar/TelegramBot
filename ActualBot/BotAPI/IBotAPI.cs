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
        Task<List<UserDTO>> GetUserByIdAsync(long id);
        Task<HttpResponseMessage> CreateChatAsync(ChatDTO chat);
        Task<ChatDTO> GetChatByIdAsync(long id);
        Task<HttpResponseMessage> UpdateUser(int id, UserDTO user);
        Task<HttpResponseMessage> UpdateChat(long id, ChatDTO chat);
        Task<UserDTO> GetUserOfChatAsync(long chatId, long userId);
        Task<List<UserDTO>> GetTopWinners(long chatId);
        Task<List<UserDTO>> GetTopKarma(long chatId);
        Task<List<ChatDTO>> GetAllChats();
    }
}
