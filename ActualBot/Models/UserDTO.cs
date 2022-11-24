using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualBot.Models
{
    public class UserDTO
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
    }
}
