using Boox.Core.Seed;
using Boox.Infrastructure.Data;

namespace Boox.Infrastructure.Extensions
{
    public static class BooxContextExtensions
    {
        public static void SeedBooks(this BooxContext ctx)
        {
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            if (!ctx.Books.Any())
            {
                ctx.Books.AddRange(BookSeed.Books);
                ctx.SaveChanges();
            }
        }
    }
}
