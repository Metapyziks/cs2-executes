using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Modules.Utils;

namespace ExecutesPlugin.Models.JsonConverters;

public class QAngleJsonConverter : JsonConverter<QAngle>
{
    public override QAngle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            Console.WriteLine("Error! Expected a string value for QAngle JSON Deserialisation.");
            return new QAngle(float.NaN, float.NaN, float.NaN);
        }

        var stringValue = reader.GetString();
        if (stringValue == null)
        {
            Console.WriteLine("Error! String value is null for QAngle JSON Deserialisation.");
            return new QAngle(float.NaN, float.NaN, float.NaN);
        }

        stringValue = stringValue.Trim(); 

        var values = stringValue.Split(' '); // Split by space

        Console.WriteLine($"[Executes] QAngle values: {stringValue}");

        if (values.Length != 3)
        {
            Console.WriteLine($"String value '{stringValue}' is not in the correct format (X Y Z).");
            return new QAngle(float.NaN, float.NaN, float.NaN);
        }

        if (!float.TryParse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var x) ||
            !float.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var y) ||
            !float.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var z))
        {
            Console.WriteLine($"[Executes] Unable to parse QAngle float values for: '{stringValue}'"); 
            return new QAngle(float.NaN, float.NaN, float.NaN);
        }

        return new QAngle(x, y, z);
    }

    public override void Write(Utf8JsonWriter writer, QAngle value, JsonSerializerOptions options)
    {
        // Convert Vector object to string representation (example assumes ToString() returns desired format)
        var qangleString = value.ToString();
        writer.WriteStringValue(qangleString);
    }
}