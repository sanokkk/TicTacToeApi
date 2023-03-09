using TicTacToe.Domain.Models;
using TicTacToe.Service.Helpers;

namespace TicTacToe.Service.Interfaces;

public interface IGameService
{
    Task<Game[]> SelectAsync();
    Task AddAsync();
    Task<StepStatus> StepAsync(int gameId, Step step);
    Task<CurrGame> SelectByIdAsync(int id);
}
