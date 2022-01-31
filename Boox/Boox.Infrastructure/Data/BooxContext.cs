using Boox.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boox.Infrastructure.Data
{
    public class BooxContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //Place db-file at solution level
            options.UseSqlite($"Data Source=../BooxDatabase.db");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>()
        //        .Property(x => x.Id)
        //        .HasComputedColumnSql("B")

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
