namespace TicTacToe.Domain.Models;

public class Step
{
    public int Row { get; init; }

    public int Column { get; init; }

    public Player Player { get; init; }
}
