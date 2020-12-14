using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Trustpilot.Pony.Core.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Direction
    {
        [EnumMember(Value = "stay")]
        Stay,

        [EnumMember(Value = "north")]
        North,

        [EnumMember(Value = "east")]
        East,

        [EnumMember(Value = "west")]
        West,

        [EnumMember(Value = "south")]
        South
    }
}