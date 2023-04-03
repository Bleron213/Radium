using Microsoft.EntityFrameworkCore;
using Radium.Products.Infrastructure.Persistence;
using Radium.Products.Rest.Seed;

namespace Radium.Products.Rest.Extensions
{
    public static class DatabaseExtensions
    {
        public static void MigrateDatabase(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ProductsDbContext>();
            dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
        }

        public static void SeedDatabase(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ProductsDbContext>();
            dbContext.AddRangeAsync(ProductsData.Categories);
            dbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
