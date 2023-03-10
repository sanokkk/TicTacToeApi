using TicTacToe.DAL.Interfaces;
using TicTacToe.Domain.Models;
using TicTacToe.Service.Helpers;
using TicTacToe.Service.Interfaces;

namespace TicTacToe.Service.Services;


public class GameService:IGameService
{
    const int ARRAY_SIZE = 9;
    const int DOUBLY_ARRAY_SIZE = 3;
    private readonly IGameRepo _game;
    private readonly IResultService _result;
    public GameService(IGameRepo game, IResultService result)
    {
        _game = game;
        _result = result;
    }

    public async Task AddAsync() => await _game.AddAsync(Game.CreateEmpty());
    
    public async Task<Game[]> SelectAsync() => await _game.SelectAsync();
    

    
    public async Task<StepStatus> StepAsync(int gameId, Step step)
    {
        var game = await _game.GetByIdAsync(gameId);
        var table = GetDoublyArray(game.Table);
        if (_result.IsGameFinished(table).Item1)
        {
            return await IfStepIsFinally(table, game, false);
        }

        if (table[step.Row][step.Column] != Player.None)
        {
            return new StepStatus() { IsDone = false, Result = Result.Filled };
        }

        if (game.LastStep == step.Player)
        {
            return new StepStatus() { IsDone = false, Result = Result.AnotherPlayer };
        }

        game.LastStep = step.Player;
        table[step.Row][step.Column] = step.Player;
        game.Winner = Result.NotFinished;
        if (_result.IsGameFinished(table).Item1)
        {
            return await IfStepIsFinally(table, game);
        }
        await _game.StepAsync(game, GetArray(table));

        return new StepStatus() { IsDone = true, Result = Result.NotFinished };
    }

    public async Task<CurrGame> SelectByIdAsync(int id) => MapToCurrGame(await _game.GetByIdAsync(id));

    private static Player[][] GetDoublyArray(Player[] arr)
    {
        int position = 0;
        var gameTable = new Player[DOUBLY_ARRAY_SIZE][];
        for (int i = 0; i < DOUBLY_ARRAY_SIZE; i++)
        {
            gameTable[i] = new Player[DOUBLY_ARRAY_SIZE];
            for (int j = 0; j < DOUBLY_ARRAY_SIZE; j++)
            {
                gameTable[i][j] = arr[position];
                position++;
            }
        }
        return gameTable;
    }
    private static Player[] GetArray(Player[][] arr)
    {
        var GameArray = new Player[ARRAY_SIZE];
        int position = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                GameArray[position] = arr[i][j];
                position++;
            }
        }
        return GameArray;
    }
    private CurrGame MapToCurrGame(Game game) => 
        new CurrGame() { Id = game.Id, Winner = game.Winner, LastStep = game.LastStep, Table = GetDoublyArray(game.Table) };
    
    private async Task<StepStatus> IfStepIsFinally(Player[][] table, Game game, bool scenario = true)
    {
        var res = _result.IsGameFinished(table).Item2;
        var Winner = (res == Result.X) ? Result.X : (res == Result.O) ? Result.O : Result.Draw;
        var newTable = GetArray(table);
        await _game.StepAsync(game, newTable);
        await _game.SetWinner(game, Winner);

        return ReturnStatus(scenario, res);
    }

    private static StepStatus ReturnStatus(bool success, Result result)
    {
        return new StepStatus()
        {
            IsDone = success,
            Result = (result != Result.X && result != Result.O)
                ? Result.Draw 
                : result,    
        };
    }


}
