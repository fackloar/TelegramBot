﻿namespace TelegramBot.DataLayer.Models
{
    public class UserDB
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public long ChatId { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
        public bool KarmaSwitch { get; set; }
        public int Karma { get; set; }
        public int WinsNumber { get; set; }
    }
}
