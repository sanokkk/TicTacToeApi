using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.Models;
using TicTacToe.Service.Interfaces;
using TicTacToe.Service.Services;

namespace TicTacToe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _game;
    private readonly IResultService _result;
    public GameController(IGameService game, IResultService result)
    {
        _game = game;
        _result = result;
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
    public async Task<IActionResult> StepAsync([FromRoute] int id, [FromBody] Step step)
    {
        var response = await _game.StepAsync(id, step);
        if (response.IsDone)
        {
            return Ok(response.Result);
        }
        else
        {
            return BadRequest(response.Result);
        } 
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
