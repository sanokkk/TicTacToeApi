using TicTacToe.Domain.Models;

namespace TicTacToe.Service.Helpers;

public class CurrGame
{
    public int Id { get; set; }

    public Player[][] Table { get; set; }

    public Result Winner { get; set; }

    public Player LastStep { get; set; }
}
