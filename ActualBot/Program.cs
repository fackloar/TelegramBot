using ActualBot;
using ActualBot.BotAPI;
using Quartz.Impl;
using Quartz;
using Telegram.Bot;

var botApi = new BotAPI();
var botClient = new TelegramBotClient("5944999248:AAFO45MEAB0uLCMAjpZAExwxY7MMpy8vBrg");

// Create a bot

var chatBot = new BotEngine(botClient, botApi);

await chatBot.ListenForMessagesAsync();