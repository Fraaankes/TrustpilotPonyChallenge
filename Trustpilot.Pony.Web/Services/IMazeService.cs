using System.Threading.Tasks;
using Trustpilot.Pony.Core.Model;

namespace Trustpilot.Pony.Web.Services
{
    public interface IMazeService
    {
        Task<MazeIdentifier> CreateNewMaze(MazeParams mazeParams);
        Task<Maze> GetMaze(string id);
        Task<GameState> MakeMove(string id, string direction);
    }
}