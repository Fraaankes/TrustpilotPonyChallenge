using Newtonsoft.Json;

namespace Trustpilot.Pony.Core.Model
{
    public class MazeMove
    {
        [JsonProperty("direction")]
        public Direction Direction { get; set; }
    }
}