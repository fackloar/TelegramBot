using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Bot;

var botClient = new TelegramBotClient("5944999248:AAFO45MEAB0uLCMAjpZAExwxY7MMpy8vBrg");

using var cts = new CancellationTokenSource();


// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;


    var chatId = message.Chat.Id;



    if (message.Text == Commands.Start)
    {

    }

    if (message.Text == "/register")
    {
        User newUser = new User()
        {
            Id = message.From.Id,
            FirstName = message.From.FirstName,
            LastName = message.From.LastName,
            Username = message.From.Username
        };

        Console.WriteLine($"id: {newUser.Id}\nFirstName: {newUser.FirstName}\nLastName: {newUser.LastName}\nUsername: {newUser.Username}");

        users.Add(newUser);
    }

    if (message.Text == "/pidor")
    {
        Random random = new Random();
        var choice = random.Next(0, users.Count());
        Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: $"The gay today: @{users[choice].Username}",
        cancellationToken: cancellationToken);
    }
    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
