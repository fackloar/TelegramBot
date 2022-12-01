using ActualBot;
using ActualBot.BotAPI;
using Quartz.Impl;
using Quartz;
using Telegram.Bot;

var botApi = new BotAPI();
var token = File.ReadAllText("token.txt");
var botClient = new TelegramBotClient(token);

// Create a bot

var chatBot = new BotEngine(botClient, botApi);

await chatBot.ListenForMessagesAsync();