namespace TelegramBot.BusinessLayer.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
    }
}
