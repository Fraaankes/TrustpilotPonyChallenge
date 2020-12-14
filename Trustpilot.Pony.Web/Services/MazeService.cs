using System;
using System.Threading.Tasks;
using Trustpilot.Pony.Core.Api;
using Trustpilot.Pony.Core.Model;

namespace Trustpilot.Pony.Web.Services
{
    public class MazeService : IMazeService
    {
        private readonly PonyApiClient _apiClient;

        public MazeService(PonyApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public Task<MazeIdentifier> CreateNewMaze(MazeParams mazeParams)
        {
            return _apiClient.CreateNewMaze(mazeParams);
        }

        public Task<Maze> GetMaze(string id)
        {
            return _apiClient.GetMaze(new MazeIdentifier(id));
        }

        public async Task<GameState> MakeMove(string id, string direction)
        {
            var dir = direction switch
            {
                "w" => Direction.West,
                "e" => Direction.East,
                "n" => Direction.North,
                "s" => Direction.South,
                "stay" => Direction.Stay,
                _ => throw new ArgumentException(nameof(direction))
            };

            var move = await _apiClient.MakeMove(new MazeIdentifier(id), new MazeMove {Direction = dir});
            
            if (!string.IsNullOrEmpty(move.HiddenUrl))
            {
                move.HiddenUrl = new Uri(_apiClient.BaseUri, move.HiddenUrl).AbsoluteUri;
            }

            return move;
        }
    }
}
