using System.Text.Json;
using System.Text.Json.Serialization;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.JsonConverters
{
    public class ProductBrandIdConverter : JsonConverter<ProductBrandId>
    {
        public override ProductBrandId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && Guid.TryParse(reader.GetString(), out var guid))
            {
                return new ProductBrandId(guid);
            }

            throw new JsonException("Invalid format for ProductBrandId");
        }

        public override void Write(Utf8JsonWriter writer, ProductBrandId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value.ToString());
        }
    }
}