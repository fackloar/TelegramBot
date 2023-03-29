using Telegram.Bot.Types;
using Telegram.Bot;
using ActualBot.Models;
using ActualBot.BotAPI;
using ActualBot.Info;
using System.Text;
using System;
using System.Runtime.InteropServices;
using ActualBot.Extensions;

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
            UserDTO user = new UserDTO()
            {
                TelegramId = _sender.Id,
                ChatId = _chatId,
                FirstName = _sender.FirstName,
                Username = _sender.Username,
            };

            var registeredResponse = await _botApi.CreateUserAsync(user);
            if (registeredResponse.IsSuccessStatusCode)
            {
                Message registeredMessage = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: Messages.Registered,
                cancellationToken: _cts);
            }
            else
            {
                Message notRegisteredMessage = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: Messages.NotRegistered,
                    cancellationToken: _cts);
            }
            return registeredResponse;
        }

        private async Task OfTheDay()
        {
            var chat = await _botApi.GetChatByIdAsync(_chatId);
            var users = await _botApi.GetUsersOfChatAsync(_chatId);

            var chosenUser = RandomPickExtension.PickRandom(users);

            chosenUser.WinsNumber++;
            var name = UsernameOrFirstname(chosenUser);
            chat.Winner = name;
            if (chat.GameOnSwitch == false)
            {
                chat.GameOnSwitch = true;
            }
            await _botApi.UpdateChat(_chatId, chat);
            await _botApi.UpdateUser(chosenUser.Id, chosenUser);

            var catStickerList = new CatStickers();
            var randomSticker = RandomPickExtension.PickRandom(catStickerList.CatStickersList);

            Message sticker = await _botClient.SendStickerAsync(
                chatId: _chatId,
                sticker: randomSticker,
                cancellationToken: _cts);

            Message winner = await _botClient.SendTextMessageAsync(
            chatId: _chatId,
            text: $"Котик дня: {name}",
            cancellationToken: _cts);
        }

        public async void RunOfTheDayManually()
        {
            await OfTheDay();
        }

        public async void WinnerCheck()
        {
            var chat = await _botApi.GetChatByIdAsync(_chatId);
            if (chat != null)
            {
                if (chat.Winner != null)
                {
                    Message winner = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"Котик дня: {chat.Winner}",
                cancellationToken: _cts);
                }
                else
                {
                    Message winner = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: Messages.GamesOff,
                cancellationToken: _cts);
                }
            }
            else
            {
                await SendErrorMessage(Messages.NoChat);
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

        public async Task<bool> CheckIfGameIsOn(long chatId)
        {
            var chat = await _botApi.GetChatByIdAsync(chatId);
            return chat.GameOnSwitch;
        }

        public async Task UpdateMessages(long chatId, long userId)
        {
            var user = await _botApi.GetUserOfChatAsync(chatId, userId);
            user.Messages++;
            await _botApi.UpdateUser(user.Id, user);
        }

        public async Task KarmaUp(long userId)
        {
            var user = await _botApi.GetUserOfChatAsync(_chatId, userId);
            if (user != null)
            {
                if (user.KarmaSwitch == true)
                {
                    user.Karma++;
                    KarmaMilestoneMessage(user);
                    await _botApi.UpdateUser(user.Id, user);
                }
                else
                {
                    await KarmaIsOffMessage();
                }
            }
            else
            {
                await SendErrorMessage(Messages.NoUser);
            }
        }

        public async Task KarmaDown(long userId)
        {
            var user = await _botApi.GetUserOfChatAsync(_chatId, userId);
            if (user != null)
            {
                if (user.KarmaSwitch == true)
                {
                    user.Karma--;
                    KarmaMilestoneMessage(user);
                    await _botApi.UpdateUser(user.Id, user);
                }
                else
                {
                    await KarmaIsOffMessage();
                }
            }
            else
            {
                await SendErrorMessage(Messages.NoUser);
            }
        }

        private async void KarmaMilestoneMessage(UserDTO user)
        {
            var karmaMilestoneMessage = KarmaMilestoneChecker.Check(user.Karma);
            if (karmaMilestoneMessage != null)
            {
                var username = UsernameOrFirstname(user);
                Message achievement = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"{username}! {karmaMilestoneMessage}",
                cancellationToken: _cts);
            }
        }
        public async Task KarmaSwitch(long userId)
        {
            var user = await _botApi.GetUserOfChatAsync(_chatId, userId);
            if (user != null)
            {
                if (user.KarmaSwitch == false)
                {
                    user.KarmaSwitch = true;
                    await _botApi.UpdateUser(user.Id, user);
                    Message turnOn = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: $"{UsernameOrFirstname(user)} включил счетчик кармы.",
                    cancellationToken: _cts);
                }
                else if (user.KarmaSwitch == true)
                {
                    user.KarmaSwitch = false;
                    if (user.Karma > 0)
                    {
                        user.Karma = 0;

                    }
                    await _botApi.UpdateUser(user.Id, user);
                    Message turnOn = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: $"{UsernameOrFirstname(user)} выключил счетчик кармы. Если она была положительная, то я сбросил счетчик.",
                    cancellationToken: _cts);
                }
            }
            else
            {
                await SendErrorMessage(Messages.NoUser);
            }
        }

        public async Task CantManipulateYourKarma()
        {
            Message dontCheat = await _botClient.SendTextMessageAsync(
            chatId: _chatId,
            text: Messages.DontCheat,
            cancellationToken: _cts);
        }

        public async Task GetWinners()
        {
            var winners = await _botApi.GetTopWinners(_chatId);
            var catEmoji = char.ConvertFromUtf32(0x1F63C);
            if (winners != null)
            {
                var sb = new StringBuilder();
                foreach (UserDTO winner in winners)
                {
                    sb.Append($"{UsernameOrFirstname(winner)} {catEmoji}: {winner.WinsNumber}\n");
                }
                var result = sb.ToString();

                Message winnerList = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: result,
                    cancellationToken: _cts);
            }
            else
            {
                await SendErrorMessage(Messages.NoChat);
            }
        }
        public async Task GetKarma()
        {
            var winners = await _botApi.GetTopKarma(_chatId);
            if (winners != null)
            {
                var sb = new StringBuilder();
                foreach (UserDTO winner in winners)
                {
                    var karmaEmoji = KarmaEmojiSelector.EmojiString(winner.Karma);
                    sb.Append($"{UsernameOrFirstname(winner)}: {winner.Karma} кармы {karmaEmoji}\n");
                }
                var result = sb.ToString();

                Message winnerList = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: result,
                    cancellationToken: _cts);
            }
            else
            {
                await SendErrorMessage(Messages.NoChat);
            }
        }

        public async Task GetMessages()
        {
            var users = await _botApi.GetUsersOfChatAsync(_chatId);
            var textEmoji = char.ConvertFromUtf32(0x1F4AC);
            if (users != null)
            {
                var sb = new StringBuilder();
                List<UserDTO> sortedUsers = users.OrderBy(u => u.Messages).ToList();
                sortedUsers.Reverse();
                foreach (UserDTO user in sortedUsers)
                {
                    sb.Append($"{UsernameOrFirstname(user)} {textEmoji}: {user.Messages}\n");
                }
                var result = sb.ToString();

                Message messagesList = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: result,
                    cancellationToken: _cts);
            }
            else
            {
                await SendErrorMessage(Messages.NoChat);
            }
        }

        private async Task SendErrorMessage(string wrong)
        {
            Message error = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: $"{Messages.Error} {wrong}",
                    cancellationToken: _cts);
        }

        private async Task KarmaIsOffMessage()
        {
            Message karmaOff = await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: Messages.KarmaOff,
                    cancellationToken: _cts);
        }

        public async Task SendCustomMessage()
        {
            var chats = _botApi.GetAllChats().Result;
            var groupChats = new List<ChatDTO>();
            foreach (ChatDTO chat in chats)
            {
                if (chat.Type != "Private")
                {
                    groupChats.Add(chat);
                }
            }

            foreach (ChatDTO groupChat in groupChats)
            {
                Message announcment = await _botClient.SendTextMessageAsync(
                chatId: groupChat.Id,
                text: "Привет! Я немного обновился, но для того, чтобы я продолжал правильно работать, придется перезапустить игру. Пропишите /gameOn в чат!",
                cancellationToken: _cts);
            }
        }
    }
}
