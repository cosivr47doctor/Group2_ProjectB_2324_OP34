/*
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

class CustomReservationsConverter : JsonConverter<Dictionary<int, Dictionary<string, object>[]>>
{
    public override Dictionary<int, Dictionary<string, object>[]> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var dictionary = new Dictionary<int, Dictionary<string, object>[]>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return dictionary;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (!int.TryParse(propertyName, out int key))
            {
                throw new JsonException($"Invalid key format: {propertyName}");
            }

            reader.Read(); // Move to the start of the array
            var reservations = JsonSerializer.Deserialize<Dictionary<string, object>[]>(ref reader, options);
            dictionary.Add(key, reservations);
        }

        throw new JsonException("Unexpected end of JSON input");
    }


    public override void Write(Utf8JsonWriter writer, Dictionary<int, Dictionary<string, object>[]> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var pair in value)
        {
            writer.WritePropertyName(pair.Key.ToString());
            JsonSerializer.Serialize(writer, pair.Value, options);
        }

        writer.WriteEndObject();
    }
}
*/