using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trustpilot.Pony.Core.Model;
using Trustpilot.Pony.Web.Model;
using Trustpilot.Pony.Web.Services;

namespace Trustpilot.Pony.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MazeController : ControllerBase
    {
        private readonly IMazeService _mazeService;

        public MazeController(IMazeService mazeService)
        {
            _mazeService = mazeService;
        }

        [HttpGet]
        public async Task<Maze> Get(string id)
        {
            return await _mazeService.GetMaze(id);
        }

        [HttpPost("")]
        public async Task<Maze> StartNew([FromBody] NewMazeRequest request)
        {
            var id = await _mazeService.CreateNewMaze(new MazeParams
            {
                PlayerName = request.Name,
                Difficulty = 10,
                Height = 15,
                Width = 15
            });
            return await _mazeService.GetMaze(id.MazeId);
        }

        [HttpPost("/move")]
        public async Task<GameState> Move(string id, string direction)
        {
            return await _mazeService.MakeMove(id, direction);
        }
    }
}
