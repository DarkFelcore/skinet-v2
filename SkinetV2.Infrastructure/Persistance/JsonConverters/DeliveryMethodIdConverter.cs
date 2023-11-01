using System.Text.Json;
using System.Text.Json.Serialization;
using SkinetV2.Domain.Orders.Entities.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.JsonConverters
{
    public class DeliveryMethodIdConverter : JsonConverter<DeliveryMethodId>
    {
        public override DeliveryMethodId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && Guid.TryParse(reader.GetString(), out var guid))
            {
                return new DeliveryMethodId(guid);
            }

            throw new JsonException("Invalid format for DeliveryMethodId");
        }

        public override void Write(Utf8JsonWriter writer, DeliveryMethodId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value.ToString());
        }
    }
}