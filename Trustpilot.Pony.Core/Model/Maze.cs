using System.Linq;
using Newtonsoft.Json;

namespace Trustpilot.Pony.Core.Model
{
    public class Maze
    {
        [JsonProperty("pony")]
        public int[] PonyLocations { get; set; }
        public int PonyLocation
        {
            get => PonyLocations.Single();
            set => PonyLocations[0] = value;
        }

        [JsonProperty("domokun")]
        public int[] DomokunLocations { get; set; }
        public int DomokunLocation => DomokunLocations.Single();

        [JsonProperty("end-point")]
        public int[] Endpoints { get; set; }
        public int Endpoint => Endpoints.Single();

        [JsonProperty("size")]
        public Size Size { get; set; }

        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }

        [JsonProperty("data")]
        public Direction[][] Data { get; set; }

        [JsonProperty("maze_id")]
        public MazeIdentifier MazeIdentifier { get; set; }

        [JsonProperty("game-state")]
        public GameState GameState { get; set; }
    }
}