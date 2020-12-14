using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Trustpilot.Pony.Core.Model
{
    public class GameState
    {
        [JsonProperty("state")]
        public CurrentState State { get; set; }

        [JsonProperty("state-result")]
        public string StateResult { get; set; }

        [JsonProperty("hidden-url")]
        public string HiddenUrl { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum CurrentState
        {
            [EnumMember(Value = "active")]
            Active,

            [EnumMember(Value = "over")]
            Over,

            [EnumMember(Value = "won")]
            Won
        }
    }
}