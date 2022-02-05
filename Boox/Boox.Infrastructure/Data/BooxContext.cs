using Boox.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boox.Infrastructure.Data
{
    public class BooxContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        //Constructor for DI
        public BooxContext()
        { }

        //Constructor for tests
        public BooxContext(DbContextOptions<BooxContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //Place db-file at solution level
            options.UseSqlite($"Data Source=../BooxDatabase.db");
        }
    }
}
