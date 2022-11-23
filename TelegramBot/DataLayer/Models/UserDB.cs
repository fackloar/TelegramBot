namespace TelegramBot.DataLayer.Models
{
    public class UserDB
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
    }
}
