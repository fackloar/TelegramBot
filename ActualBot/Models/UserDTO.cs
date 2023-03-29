using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualBot.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public long ChatId { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
        public bool KarmaSwitch { get; set; }
        public int Karma { get; set; }
        public int WinsNumber { get; set; }
        public int Messages { get; set; }
    }
}
