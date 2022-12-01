using Telegram.Bot.Types;
using Telegram.Bot;
using ActualBot.Models;
using ActualBot.BotAPI;
using ActualBot.Info;

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
            text: Messages.Hello,
            cancellationToken: _cts) ;
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
            text: Messages.Registered,
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
                    sticker: Messages.SecretSantaSticker,
                    cancellationToken: _cts);

                    Message sentMessage = await _botClient.SendTextMessageAsync(
                    chatId: santa.Id,
                    text: $"Привет! Ты даришь подарок {name}. Постарайся порадовать своего друга! :3",
                    cancellationToken: _cts);
                    usersToGift.Remove(userToGift);
                }
            }
        }

        private async Task OfTheDay()
        {
            Random random = new Random();
            var users = await _botApi.GetUsersOfChatAsync(_chatId);
            var choice = random.Next(0, users.Count());
            var chosenUser = users[choice];
            var name = UsernameOrFirstname(chosenUser);
            Messages.Winner = $"Котик дня: {name}";

            Message sticker = await _botClient.SendStickerAsync(
                chatId: _chatId,
                sticker: Messages.OfTheDaySticker,
                cancellationToken: _cts);

            Message winner = await _botClient.SendTextMessageAsync(
            chatId: _chatId,
            text: Messages.Winner,
            cancellationToken: _cts);
        }

        public async void WinnerCheck()
        {
            if (Messages.Winner == null)
            {
                Message noWinner = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: Messages.GamesOff,
                cancellationToken: _cts);
            }
            else
            {
                Message repeatWinner = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: Messages.Winner,
                cancellationToken: _cts);
            }
        }

        public async void AlreadyOn()
        {
            Message alreadyOn = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: Messages.AlreadyOn,
                cancellationToken: _cts);
        }

        public async void Help()
        {
            Message helpMessage = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: Messages.Help,
                cancellationToken: _cts);
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

        public async Task EverydayJob()
        {
            await OfTheDay();
        }
    }
}
