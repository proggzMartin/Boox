using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boox.Infrastructure.Data
{
    public class BooxContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={Environment.CurrentDirectory}\\BooxDatabase.db");
        }
    }
}
