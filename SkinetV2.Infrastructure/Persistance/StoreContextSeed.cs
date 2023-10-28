using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Logging;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ProductBrands;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;
using SkinetV2.Domain.Products.ProductTypes;
using SkinetV2.Domain.Users;
using SkinetV2.Domain.Users.ValueObjects;
using SkinetV2.Infrastructure.Persistance.JsonConverters;

namespace SkinetV2.Infrastructure.Persistance
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var optionsBrandId = new JsonSerializerOptions();
                    optionsBrandId.Converters.Add(new ProductBrandIdConverter());
                    var brandsData = File.ReadAllText("../SkinetV2.Infrastructure/Persistance/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData, optionsBrandId)!;

                    foreach (var item in brands)
                    {
                        await context.ProductBrands.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var optionsTypeId = new JsonSerializerOptions();
                    optionsTypeId.Converters.Add(new ProductTypeIdConverter());
                    var typesData = File.ReadAllText("../SkinetV2.Infrastructure/Persistance/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData, optionsTypeId)!;

                    foreach (var item in types)
                    {
                        await context.ProductTypes.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var optionsProductId = new JsonSerializerOptions();
                    optionsProductId.Converters.Add(new ProductIdConverter());
                    optionsProductId.Converters.Add(new ProductBrandIdConverter());
                    optionsProductId.Converters.Add(new ProductTypeIdConverter());
                    var productsData = File.ReadAllText("../SkinetV2.Infrastructure/Persistance/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData, optionsProductId)!;

                    foreach (var item in products)
                    {
                        await context.Products.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Users.Any())
                {
                    var user = new User
                    {
                        UserId = new UserId(Guid.NewGuid()),
                        FirstName = "Anthony",
                        LastName = "Deville",
                        Email = "test@test.be",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                        Address = new Address
                        {
                            Street = "Sellaerstraat 40",
                            PostalCode = "1820",
                            City = "Steenokkerzeel",
                            Provice = "Vlaams-Brabant",
                            Country = "Belgium"
                        }
                    };

                    await context.AddAsync(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}