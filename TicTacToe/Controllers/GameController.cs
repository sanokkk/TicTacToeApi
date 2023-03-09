using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.Models;
using TicTacToe.Service.Services;

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _game;
        public GameController(IGameService game)
        {
            _game = game;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            var game = await _game.SelectByIdAsync(id);
            if (game is not null)
                return Ok(game);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> StepAsync([FromRoute]int id, [FromBody]Step step)
        {
            var stepSuccess = await _game.StepAsync(id, step);
            if (stepSuccess)
            {
                var gameTable = await _game.SelectByIdAsync(id);
                return Ok(gameTable);
            }
            return BadRequest();
        }
    }
}
