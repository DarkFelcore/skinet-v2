using Microsoft.EntityFrameworkCore;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ProductBrands;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;
using SkinetV2.Domain.Products.ProductTypes;
using SkinetV2.Domain.Products.ProductTypes.ValueObjects;
using SkinetV2.Domain.Products.ValueObjects;
using SkinetV2.Domain.Users;
using SkinetV2.Domain.Users.ValueObjects;

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
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Products
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .HasConversion
                (
                    id => id.Value, // way in
                    value => new ProductId(value) // way out
                );
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            // Product Brands
            modelBuilder.Entity<ProductBrand>()
                .HasKey(p => p.ProductBrandId);
            modelBuilder.Entity<ProductBrand>()
                .Property(pb => pb.ProductBrandId)
                .HasConversion
                (
                    id => id.Value,
                    value => new ProductBrandId(value)
                );

            // Product Types
            modelBuilder.Entity<ProductType>()
                .HasKey(p => p.ProductTypeId);
            modelBuilder.Entity<ProductType>()
                .Property(pb => pb.ProductTypeId)
                .HasConversion
                (
                    id => id.Value,
                    value => new ProductTypeId(value)
                );

            // Users
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
                
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .HasConversion
                (
                    id => id.Value,
                    value => new UserId(value)
                );

            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Address);

            // Realtions //

            // Product/ProductBrands - one-to-many
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.ProductBrandId);

            // Product/ProductType - one-to-many
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);
        }
    }
}