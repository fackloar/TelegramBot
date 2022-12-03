namespace TelegramBot.BusinessLayer.DTOs
{
    public class ChatDTO
    {
        public long Id { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public bool GameOnSwitch { get; set; }
        public string? Winner { get; set; }
    }
}
