using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading;
using ActualBot.Models;
using ActualBot.BotAPI;
using System.Runtime.InteropServices;
using Telegram.Bot.Types.Enums;

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
            var chosenUser = users[choice];
            var name = UsernameOrFirstname(chosenUser);
            if (chosenUser.Username != null)
            {
                Message sentMessage = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"Random user today: @{name}",
                cancellationToken: _cts);
            }
            else if(chosenUser.FirstName != null)
            {
                Message sentMessage = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"Random user today: {name}",
                cancellationToken: _cts);
            }
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

        public async void SecretSanta()
        {
            var santas = await _botApi.GetUsersOfChatAsync(_chatId);
            var usersToGift = await _botApi.GetUsersOfChatAsync(_chatId);
            Random random = new Random();
            foreach (UserDTO santa in santas)
            {
                List<int> possibleChoices = new List<int>();
                for (int i = 0; i < usersToGift.Count; i++)
                {
                    if (usersToGift[i].Id != santa.Id)
                    {
                        possibleChoices.Add(i);
                    }
                }
                var choice = possibleChoices[random.Next(0, possibleChoices.Count)];

                var userToGift = usersToGift[choice];

                var name = UsernameOrFirstname(userToGift);

                if (CheckIfUserHasAPrivateChatWithBot(santa.Id).Result == true)
                {
                    Message stickerMessage = await _botClient.SendStickerAsync(
                    chatId: santa.Id,
                    sticker: "CAACAgIAAxkBAAEaT7tjf-2UlQR748jqPTARgQABpHXVwKgAAmYSAAIrEOhJ3To7U-US8LYrBA",
                    cancellationToken: _cts);

                    Message sentMessage = await _botClient.SendTextMessageAsync(
                    chatId: santa.Id,
                    text: $"Hi! You give a gift to {name}",
                    cancellationToken: _cts);
                    usersToGift.Remove(userToGift);
                }
            }
        }

        public async Task OfTheDay()
        {
            Random random = new Random();
            var users = await _botApi.GetUsersOfChatAsync(_chatId);
            var choice = random.Next(0, users.Count());
            var chosenUser = users[choice];
            var name = UsernameOrFirstname(chosenUser);
            if (chosenUser.Username != null)
            {
                Message sentMessage = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"Киса дня: {name}",
                cancellationToken: _cts);
            }
            else if (chosenUser.FirstName != null)
            {
                Message sentMessage = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"Киса дня: {name}",
                cancellationToken: _cts);
            }
        }

        private async Task<bool> CheckIfUserHasAPrivateChatWithBot(long userId)
        {
            var chat = await _botApi.GetChatByIdAsync(userId);
            if (chat == null)
                return false;
            else return true;
        }

        private string UsernameOrFirstname(UserDTO user)
        {
            if (user.Username == null)
            {
                return user.FirstName;
            }
            else
            {
                return $"@{user.Username}";
            }
        }
    }
}
