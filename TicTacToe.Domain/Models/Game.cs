namespace TicTacToe.Domain.Models;

public class Game
{
    public int Id { get; set; }

    public Player[] Table { get; set; }

    public Result Winner { get; set; }

    public Player LastStep { get; set; }

    public static Game CreateEmpty()
    {
        var table = new Player[9];
        for (int i = 0; i < 9; i++)
            table[i] = Player.None;
        var game = new Game()
        {
            Table = table,
            Winner = Result.NotFinished
        };
        return game;
    }

}
