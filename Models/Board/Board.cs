using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Board
{
    public class Board
    {
        public int Columns { get; set; } = 7;
        public int Rows { get; set; } = 6;

        int[,] board;

        public Board() 
        { 
            board = new int[Rows, Columns];
        }
    }
}