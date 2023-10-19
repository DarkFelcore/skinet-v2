using System.Text.Json;
using System.Text.Json.Serialization;
using SkinetV2.Domain.Products.ProductTypes.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.JsonConverters
{
    public class ProductTypeIdConverter : JsonConverter<ProductTypeId>
    {
        public override ProductTypeId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && Guid.TryParse(reader.GetString(), out var guid))
            {
                return new ProductTypeId(guid);
            }

            throw new JsonException("Invalid format for ProductTypeId");
        }

        public override void Write(Utf8JsonWriter writer, ProductTypeId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value.ToString());
        }
    }
}