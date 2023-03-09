using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain.Models;

namespace TicTacToe.Service.Services
{
    public interface IResultService
    {
        Tuple<bool, Result> isGameFinished(Player[][] table);
    }

    public class ResultService : IResultService
    {
        public Tuple<bool, Result> isGameFinished(Player[][] table)
        {
            var variants = GetVariants(table);
            bool hasZero = false;
            foreach(var el in variants)
            {
                if (el.All(x => x == Player.X))
                    return new Tuple<bool, Result>(true, Result.X);
                if (el.All(x => x == Player.O))
                    return new Tuple<bool, Result>(true, Result.O);
                if (el.Contains(Player.None))
                    hasZero = true;
            }
            return (hasZero) ? new Tuple<bool, Result>(false, Result.NotFinished) : new Tuple<bool, Result>(true, Result.Draw);
        }

        private static List<Player[]> GetVariants(Player[][] table)
        {
            var variants = new List<Player[]>();

            //строки
            variants.Add(new Player[] { table[0][0], table[0][1], table[0][2] });
            variants.Add(new Player[] { table[1][0], table[1][1], table[1][2] });
            variants.Add(new Player[] { table[2][0], table[2][1], table[2][2] });

            //столбцы
            variants.Add(new Player[] { table[0][0], table[1][0], table[2][0] });
            variants.Add(new Player[] { table[0][1], table[1][1], table[2][1] });
            variants.Add(new Player[] { table[0][2], table[1][2], table[2][2] });

            //диагонали
            variants.Add(new Player[] { table[0][0], table[1][1], table[2][2] });
            variants.Add(new Player[] { table[0][2], table[1][1], table[2][0] });

            return variants;

        }

        
    }
}
