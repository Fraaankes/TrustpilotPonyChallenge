using System;
using Newtonsoft.Json;

namespace Trustpilot.Pony.Core.Model
{
    [JsonConverter(typeof(MazeIdentifierJsonConverter))]
    public class MazeIdentifier
    {
        [JsonProperty("maze_id")]
        public string MazeId { get; set; }

        [JsonConstructor]
        public MazeIdentifier(string mazeId)
        {
            MazeId = mazeId;
        }

        public static implicit operator MazeIdentifier(string s)
        {
            return new MazeIdentifier(s);
        }

        /// <summary>
        /// Converts a <code>MazeIdentifier</code> into a string when serializing to json
        /// </summary>
        public class MazeIdentifierJsonConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is MazeIdentifier identifier)
                {
                    writer.WriteValue(identifier.MazeId);
                }
                else
                {
                    writer.WriteNull();
                }
            }

            // When reading there is no need to convert due to the implicit conversion
            public override bool CanRead => false;
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
            
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(MazeIdentifier);
            }
        }
    }
}