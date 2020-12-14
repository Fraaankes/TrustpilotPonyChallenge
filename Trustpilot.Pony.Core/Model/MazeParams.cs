using System;
using System.Linq;
using Newtonsoft.Json;

namespace Trustpilot.Pony.Core.Model
{
    public class MazeParams
    {
        [JsonProperty("maze-width")]
        public int Width { get; set; }

        [JsonProperty("maze-height")]
        public int Height { get; set; }

        [JsonProperty("maze-player-name")]
        public string PlayerName { get; set; }

        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }

        public void ValidateParams()
        {
            if (Difficulty < 0 || Difficulty > 10)
            {
                throw new ArgumentException("Difficulty should be between 0 and 10");
            }

            if (Width < 15 || Width > 25 || Height < 15 || Height > 25)
            {
                throw new ArgumentException("Maze dimensions should be between 15 and 25");
            }

            var validNames = new[]
            {
                "Twilight Sparkle",
                "Pinkie Pie",
                "Applejack",
                "Rainbow Dash",
                "Fluttershy",
                "Rarity",
                "Spike"
            };
            if (string.IsNullOrEmpty(PlayerName) || !validNames.Contains(PlayerName))
            {
                throw new ArgumentException("Only ponies can play");
            }
        }
    }
}