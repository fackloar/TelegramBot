using ActualBot;
using ActualBot.BotAPI;
using Quartz.Impl;
using Quartz;
using Telegram.Bot;

var botApi = new BotAPI();
var token = File.ReadAllText("token.txt");
var botClient = new TelegramBotClient(token);

// Create a bot

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler = await factory.GetScheduler();

await scheduler.Start();
await scheduler.ResumeAll();


var chatBot = new BotEngine(botClient, botApi, scheduler);

await chatBot.ListenForMessagesAsync();
