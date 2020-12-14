using System;
using Newtonsoft.Json;

namespace Trustpilot.Pony.Core.Model
{
    [JsonConverter(typeof(SizeJsonConverter))]
    public class Size
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// Custom JSON converter as the sizes are coming in / going out as an array of longs, and they should be in a nice object
        /// </summary>
        public class SizeJsonConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (!(value is Size size))
                {
                    writer.WriteNull();
                }
                else
                {
                    writer.WriteStartArray();
                    writer.WriteValue(size.Width);
                    writer.WriteValue(size.Height);
                    writer.WriteEndArray();
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.StartArray)
                {
                    reader.Read(); // Move to next token

                    var dimensions = new int[2];
                    int i = 0;
                    do
                    {
                        dimensions[i++] = (int) (long) reader.Value;
                        reader.Read();
                    } while (reader.TokenType != JsonToken.EndArray);
                    
                    return new Size
                    {
                        Width = dimensions[0],
                        Height = dimensions[1]
                    };
                }

                throw new ArgumentException($"Unknown start token {reader.TokenType}");
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Size);
            }
        }
    }
}