using TicTacToe.Domain.Models;

namespace TicTacToe.DAL.Interfaces;

public interface IGameRepo: IBaseRepo<Game>
{
    Task<Game> StepAsync(Game game, Player[] table);
    Task<Game> SetWinner(Game game, Result winner);
}
