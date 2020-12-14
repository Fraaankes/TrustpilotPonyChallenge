using System.IO;
using Newtonsoft.Json;
using Trustpilot.Pony.Core.Model;

namespace Trustpilot.Pony.Tests
{
    public class TestMazeData
    {
        public static Maze LoadCompleteTestMaze()
        {
            var json = File.ReadAllText("TestData/completemaze.json");
            return JsonConvert.DeserializeObject<Maze>(json);
        }

    }
}
