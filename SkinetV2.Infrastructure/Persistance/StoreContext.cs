using Microsoft.EntityFrameworkCore;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance
{
    public class StoreContext : DbContext
    {
        // -s: -startup-project
        // -p: -project
        // RUN MIGRATION: dotnet ef migrations add InitialCreate -p .\SkinetV2.Infrastructure\ -s .\SkinetV2.Api\
        // UPDATE DATABASE: dotnet ef database update -p .\SkinetV2.Infrastructure\ -s .\SkinetV2.Api\

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        // Db Sets
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Products
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .HasConversion
                (
                    id => id.Value, // way in
                    value => new ProductId(value)
                );
        }
    }
}