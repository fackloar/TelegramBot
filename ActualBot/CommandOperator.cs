using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading;
using ActualBot.Models;
using ActualBot.BotAPI;

namespace TelegramBot.Bot
{
    public class CommandOperator
    {
        private readonly TelegramBotClient _botClient;
        private readonly CancellationToken _cts;
        private readonly long _chatId;
        private static IBotAPI? _botApi;
        private User _sender;

        public CommandOperator(TelegramBotClient botClient, CancellationToken cts, long chatId, IBotAPI botAPI, User sender)
        {
            _botClient = botClient;
            _cts = cts;
            _chatId = chatId;
            _botApi = botAPI;
            _sender = sender;
        }

        public async Task<HttpResponseMessage> StartAsync()
        {
            Message sentMessage = await _botClient.SendTextMessageAsync(
            chatId: _chatId,
            text: $"Hello!",
            cancellationToken: _cts);
            ChatDTO chat = new ChatDTO()
            {
                Id = _chatId,
                Type = sentMessage.Chat.Type.ToString(),
                Title = sentMessage.Chat.Title
            };
            return await _botApi.CreateChatAsync(chat);
        }

        public async Task<HttpResponseMessage> Register()
        {
            Message sentMessage = await _botClient.SendTextMessageAsync(
            chatId: _chatId,
            text: $"Hello!!!",
            cancellationToken: _cts);

            UserDTO user = new UserDTO()
            {
                Id = _sender.Id,
                ChatId = _chatId,
                FirstName = _sender.FirstName,
                Username = _sender.Username,
            };

            return await _botApi.CreateUserAsync(user);
        }

        public async void Random()
        {
            Random random = new Random();
            var users = await _botApi.GetUsersOfChatAsync(_chatId);
            var choice = random.Next(0, users.Count());
            Message sentMessage = await _botClient.SendTextMessageAsync(
            chatId: _chatId,
            text: $"Random user today: @{users[choice].Username}",
            cancellationToken: _cts);
        }

        public async void Id(long id)
        {
            var user = await _botApi.GetUserByIdAsync(id);
            if (user != null)
            {
                Message sentMessage = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"Your Id is {user.Id}",
                cancellationToken: _cts);
            }
        }
    }
}
