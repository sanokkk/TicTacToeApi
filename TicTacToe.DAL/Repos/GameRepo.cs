using Microsoft.EntityFrameworkCore;
using TicTacToe.DAL.Interfaces;
using TicTacToe.Domain.Models;

namespace TicTacToe.DAL.Repos;

public class GameRepo : IGameRepo
{
    private readonly ApplicationContext _db;
    public GameRepo(ApplicationContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Game entity)
    {
        await _db.Games.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Game> GetByIdAsync(int id)
    {
        return await _db.Games.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Game[]> SelectAsync()
    {
        return await _db.Games.ToArrayAsync();
    }

    public async Task<Game> SetWinner(Game game, Result winner)
    {
        game.Winner = winner;
        await _db.SaveChangesAsync();
        return game;
    }

    public async Task<Game> StepAsync(Game game, Player[] table)
    {
        game.Table = table;
        await _db.SaveChangesAsync();
        return game;
    }
}
