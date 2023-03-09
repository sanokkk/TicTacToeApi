using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Service.Services;

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _game;
        public GamesController(IGameService game)
        {
            _game = game;
        }

        [HttpGet]
        public async Task<IActionResult> SelectAsync()
        {
            var games = await _game.SelectAsync();
            return Ok(games);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync()
        {
            await _game.AddAsync();
            return Ok();
        }
    }
}
