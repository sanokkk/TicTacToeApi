using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Domain.Models
{
    public class CurrGame
    {
        public int Id { get; set; }

        public Player[][] Table { get; set; }

        public string FirstPlayer { get; set; }

        public string SecondPlayer { get; set; }

        public string Winner { get; set; }
    }
}
