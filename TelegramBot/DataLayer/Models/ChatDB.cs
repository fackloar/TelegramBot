using System.ComponentModel.DataAnnotations;

namespace TelegramBot.DataLayer.Models
{
    public class ChatDB
    {
        public long Id { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public bool GameOnSwitch { get; set; }
        public string? Winner { get; set; }
    }
}
