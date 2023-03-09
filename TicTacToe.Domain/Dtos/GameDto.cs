using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain.Models;

namespace TicTacToe.Domain.Dtos
{
    public class GameDto
    {
        public string FirstPlayer { get; set; }

        public string SecondPlayer { get; set; }

        public Game MapToGame()
        {
            var game = new Game()
            {
                FirstPlayer = this.FirstPlayer,
                SecondPlayer = this.SecondPlayer,
                Table = new Player[9]
            };
            for (int i = 0; i < game.Table.Length; i++)
            {
                game.Table[i] = Player.None;
            }
            return game;
        }
    }
}
