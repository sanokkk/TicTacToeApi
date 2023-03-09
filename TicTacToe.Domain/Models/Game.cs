﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Domain.Models
{
    public class Game
    {
        public int Id { get; set; }

        public Player[] Table { get; set; }

        public Player Winner { get; set; }

        public Player LastStep { get; set; }


    }
}