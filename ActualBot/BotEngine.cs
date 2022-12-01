using ActualBot.BotAPI;
using Quartz.Impl;
using Quartz;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Bot;
using ActualBot.Info;

namespace ActualBot
{
    internal class BotEngine
    {
        private readonly TelegramBotClient _botClient;
        private static IBotAPI? _botAPI;
        private bool gameOn { get; set; } = false;

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
                case Commands.StartAt:
                    var response = await commandOperator.StartAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Created a chat {message.Chat.Title} in the database");
                    }
                    break;
                case Commands.Register:
                case Commands.RegisterAt:
                    var registerResponse = await commandOperator.Register();
                    if (registerResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Created a user {message.From.Username} from the chat {message.Chat.Title} in the database");
                    }
                    break;
                case Commands.SecretSanta:
                case Commands.SecretSantaAt:
                    commandOperator.SecretSanta();
                    break;
                case Commands.GameOn:
                case Commands.GameOnAt:
                    if (gameOn == false)
                    {
                        await CreateAndStartAJob(commandOperator);
                    }
                    else
                    {
                        commandOperator.AlreadyOn();
                    }
                    break;
                case Commands.Winner:
                case Commands.WinnerAt:
                    commandOperator.WinnerCheck();
                    break;
                case Commands.Help:
                case Commands.HelpAt:
                    commandOperator.Help();
                    break;
            }
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

        private async Task CreateAndStartAJob(CommandOperator commandOperator)
        {
            gameOn = true;
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ScheduleJob>()
                .WithIdentity("job1", "group1")
                .Build();

            job.JobDataMap.Put("commandOperator", commandOperator);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            if (await scheduler.CheckExists(job.Key))
            {
                await scheduler.DeleteJob(job.Key);
            }

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}

