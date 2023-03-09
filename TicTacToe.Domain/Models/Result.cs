using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Domain.Models
{
    public enum Result
    {
        NotFinished = -1,
        Draw = 0,
        X = 1,
        O = 2,
        Filled = 3,
        AnotherPlayer = 4
    }
}
