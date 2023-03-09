using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain.Models;

namespace TicTacToe.Service.Interfaces
{
    public interface IResultService
    {
        Tuple<bool, Result> IsGameFinished(Player[][] table);
    }
}
