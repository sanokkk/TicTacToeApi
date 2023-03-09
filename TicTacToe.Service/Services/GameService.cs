using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DAL.Interfaces;
using TicTacToe.Domain.Dtos;
using TicTacToe.Domain.Models;

namespace TicTacToe.Service.Services
{
    public interface IGameService
    {
        Task<Game[]> SelectAsync();
        Task AddAsync(GameDto entity);
        Task<bool> StepAsync(int gameId, Step step);
        Task<CurrGame> SelectByIdAsync(int id);
    }
    public class GameService:IGameService
    {
        private readonly IGameRepo _game;
        public GameService(IGameRepo game)
        {
            _game = game;
        }

        public async Task AddAsync(GameDto entity)
        {
            var game = entity.MapToGame();
            await _game.AddAsync(game);
        }

        public async Task<Game[]> SelectAsync()
        {
            return await _game.SelectAsync();
        }

        public async Task<bool> StepAsync(int gameId, Step step)
        {
            var game = await _game.GetByIdAsync(gameId);
            if (game is null) 
                return false;
            var GameTable = GetDoublyArray(game.Table);
            if (GameTable[step.Row][step.Column] != Player.None)
                return false;
            GameTable[step.Row][step.Column] = step.Player;
            game.Table = GetArray(GameTable);
            await _game.StepAsync(game, game.Table);
            return true;
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
                FirstPlayer = game.FirstPlayer,
                SecondPlayer = game.SecondPlayer,
                Winner = game.Winner,
                Table = arr
            };
            return currGame;
            

        }

        
    }
}
