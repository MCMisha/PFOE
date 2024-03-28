using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi;


//workaround ze stackoverflow do "System.NotSupportedException: Serialization and deserialization of 'System.DateOnly' instances are not supported"
public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("O");
        writer.WriteStringValue(isoDate);
    }
}