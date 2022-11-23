using Microsoft.EntityFrameworkCore;
using TelegramBot.DataLayer.Models;
using TelegramBot.BusinessLayer.DTOs;

namespace TelegramBot.DataLayer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserDB> User { get; set; }
        public DbSet<ChatDB> Chat { get; set; }
    }
}
