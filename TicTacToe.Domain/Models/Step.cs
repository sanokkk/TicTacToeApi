using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Domain.Models
{
    public class Step
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public Player Player { get; set; }
    }
}
