using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DAL.Interfaces;
using TicTacToe.Domain.Models;

namespace TicTacToe.Service.Services
{
    public interface IGameService
    {
        Task<Game[]> SelectAsync();
        Task AddAsync();
        Task<Tuple<bool, Result>> StepAsync(int gameId, Step step);
        Task<CurrGame> SelectByIdAsync(int id);
    }
    public class GameService:IGameService
    {
        private readonly IGameRepo _game;
        private readonly IResultService _result;
        public GameService(IGameRepo game, IResultService result)
        {
            _game = game;
            _result = result;
        }

        public async Task AddAsync()
        {
            //var game = entity.MapToGame();
            var table = new Player[9];
            for (int i = 0; i < 9; i++)
                table[i] = Player.None;
            var game = new Game()
            {
                Table = table
            };
            await _game.AddAsync(game);
        }

        public async Task<Game[]> SelectAsync()
        {
            return await _game.SelectAsync();
        }

        public async Task<Tuple<bool, Result>> StepAsync(int gameId, Step step)
        {
            var game = await _game.GetByIdAsync(gameId);
            if (game.LastStep == step.Player)
                return new Tuple<bool, Result>(false, Result.AnotherPlayer);
            var currGame = MapToCurrGame(game);
            if (_result.isGameFinished(currGame.Table).Item1 == true)
            {
                game.Winner = game.LastStep;
                game.LastStep = step.Player;
                await _game.SetWinner(game, game.Winner);
                return new Tuple<bool, Result>(false, _result.isGameFinished(currGame.Table).Item2);
            }
                
            var table = currGame.Table;
            if (table[step.Row][step.Column] != Player.None)
                return new Tuple<bool, Result>(false, Result.Filled);
            table[step.Row][step.Column] = step.Player;
            if (_result.isGameFinished(table).Item1 == true)
            {
                var comTable = GetArray(table);
                game.LastStep = step.Player;
                game.Winner = game.LastStep;
                await _game.StepAsync(game, comTable);
                await _game.SetWinner(game, game.Winner);
                return new Tuple<bool, Result>(true, _result.isGameFinished(table).Item2);
            }

            var newTable = GetArray(table);
            game.LastStep = step.Player;
            await _game.StepAsync(game, newTable);
            return new Tuple<bool, Result>(true, Result.NotFinished);
        }

        public async Task<CurrGame> SelectByIdAsync(int id)
        {
            var game = await _game.GetByIdAsync(id);
            var currGame = MapToCurrGame(game);
            return currGame;
        }

        private static Player[][] GetDoublyArray(Player[] arr)
        {
            int position = 0;
            var gameTable = new Player[3][];
            for (int i = 0; i < 3; i++)
            {
                gameTable[i] = new Player[3];
                for (int j = 0; j < 3; j++)
                {
                    gameTable[i][j] = arr[position];
                    position++;
                }
            }
            return gameTable;
        }
        private static Player[] GetArray(Player[][] arr)
        {
            var GameArray = new Player[9];
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
        private CurrGame MapToCurrGame(Game game)
        {
            var arr = GetDoublyArray(game.Table);
            var currGame = new CurrGame()
            {
                Id = game.Id,
                Winner = game.Winner,
                Table = arr,
                LastStep = game.LastStep,
            };
            return currGame;
            

        }

        
    }
}
