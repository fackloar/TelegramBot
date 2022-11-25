using ActualBot.BotAPI;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Bot;

namespace ActualBot
{
    internal class BotEngine
    {
        private readonly TelegramBotClient _botClient;
        private static IBotAPI? _botAPI;

        public BotEngine(TelegramBotClient botClient, IBotAPI botAPI)
        {
            _botClient = botClient;
            _botAPI = botAPI;
        }

        public async Task ListenForMessagesAsync()
        {
            using var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };
            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await _botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
            {
                return;
            }

            // Only process text messages
            if (message.Text is not { } messageText)
            {
                return;
            }

            var chatId = message.Chat.Id;
            var sender = message.From;

            CommandOperator commandOperator = new CommandOperator(_botClient, cancellationToken, chatId, _botAPI, sender);


            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
            
            switch (message.Text)
            {
                case Commands.Start:
                    var response = await commandOperator.StartAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Created a chat {message.Chat.Title} in the database");
                    }
                    break;
                case Commands.Register:
                    var registerResponse = await commandOperator.Register();
                    if (registerResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Created a user {message.From.Username} from the chat {message.Chat.Title} in the database");
                    }
                    break;
                case Commands.Random:
                    commandOperator.Random();
                    break;
                case Commands.Id:
                    commandOperator.Id(sender.Id);
                    break;
                case Commands.SecretSanta:
                    commandOperator.SecretSanta();
                    break;
            }

            /*var periodicTime = new PeriodicTimer(TimeSpan.FromSeconds(10));
            while (await periodicTime.WaitForNextTickAsync())
            {
                await commandOperator.OfTheDay();
            }*/
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}

