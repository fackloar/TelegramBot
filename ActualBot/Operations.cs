using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading;


namespace TelegramBot.Bot
{
    public static class Operations
    {
        public void Start(Update update, CancellationToken cancellationToken, long chatId)
        {
            Message sentMessage = await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Hello!!!",
            cancellationToken: cancellationToken);
            ChatDTO chat = new ChatDTO()
            {
                Id = chatId,
                Type = sentMessage.Chat.Type.ToString(),
                Title = sentMessage.Chat.Title
            };
            
            await _chatService.Create(chat);
        }

        public async void Register(Update update, CancellationToken cancellationToken, long chatId)
        {
            Message sentMessage = await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Hello!!!",
            cancellationToken: cancellationToken);
            UserDTO user = new UserDTO()
            {
                Id = sentMessage.From.Id,
                ChatId = chatId,
                FirstName = sentMessage.From.FirstName,
                Username = sentMessage.From.Username,
            };

            await _userService.Create(user);
        }

        public async void Random(Update update, CancellationToken cancellationToken, long chatId)
        {
            Random random = new Random();
            var users = await _userService.GetUsersOfChat(chatId);
            var choice = random.Next(0, users.Count());
            Message sentMessage = await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Random user today: @{users[choice].Username}",
            cancellationToken: cancellationToken);
        }
    }
}
