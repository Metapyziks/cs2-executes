using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Modules.Utils;

namespace ExecutesPlugin.Models.JsonConverters;

public class VectorJsonConverter : JsonConverter<Vector>
{
    public override Vector Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            Console.WriteLine("Error! Expected a string value for vector JSON Deserialisation.");
            return new Vector(float.NaN, float.NaN, float.NaN);
        }

        var stringValue = reader.GetString();
        if (stringValue == null)
        {
            Console.WriteLine("Error! String value is null for vector JSON Deserialisation.");
            return new Vector(float.NaN, float.NaN, float.NaN);
        }

        stringValue = stringValue.Trim();

        var values = stringValue.Split(' '); // Split by space

        Console.WriteLine($"[Executes] Vector values: {stringValue}");

        if (values.Length != 3)
        {
            Console.WriteLine($"Error! String value '{stringValue}' is not in the correct format (X Y Z).");
            return new Vector(float.NaN, float.NaN, float.NaN);
        }

        if (!float.TryParse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var x) ||
            !float.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var y) ||
            !float.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var z))
        {
            Console.WriteLine($"Error! Unable to parse Vector float values for: '{stringValue}'");
            return new Vector(float.NaN, float.NaN, float.NaN);
        }

        return new Vector(x, y, z);
    }

    public override void Write(Utf8JsonWriter writer, Vector value, JsonSerializerOptions options)
    {
        // Convert Vector object to string representation (example assumes ToString() returns desired format)
        var vectorString = value.ToString();
        writer.WriteStringValue(vectorString);
    }
}