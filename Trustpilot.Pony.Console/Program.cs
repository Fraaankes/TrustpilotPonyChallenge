using System;
using System.Threading.Tasks;
using Trustpilot.Pony.Core;
using Trustpilot.Pony.Core.Api;
using Trustpilot.Pony.Core.Model;
using static System.Console;

namespace Trustpilot.Pony.Console
{
    class Program
    {
        private static readonly PonyApiClient PonyApiClient = new PonyApiClient("https://ponychallenge.trustpilot.com/");

        static async Task Main(string[] args)
        {
            Clear();
            WindowHeight = 40;
            WindowWidth = 120;

            var mazeId = await PonyApiClient.CreateNewMaze(new MazeParams
            {
                Width = 25,
                Height = 15,
                Difficulty = 10,
                PlayerName = "Spike"
            });

            var mazeSolver = new MazeSolver();
            
            await PrintMaze(mazeId);

            await foreach (var move in mazeSolver.Solve(async () => await PonyApiClient.GetMaze(mazeId)))
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                
                WriteLine($"Lets go {move.Direction} !");
                
                var state = await PonyApiClient.MakeMove(mazeId, move);

                await PrintMaze(mazeId);
                
                if (state.State != GameState.CurrentState.Active)
                {
                    WriteLine($"Game {state.State} - {state.StateResult}");
                    WriteLine("Check this out: " + PonyApiClient.BaseUri.AbsoluteUri + state.HiddenUrl);
                    ReadLine();
                    break;
                }
            }
        }

        private static async Task PrintMaze(MazeIdentifier mazeId)
        {
            SetCursorPosition(0, 0);
            var mazePrintString = await PonyApiClient.GetMazePrintString(mazeId);
            WriteLine(mazePrintString);
        }
    }
}
