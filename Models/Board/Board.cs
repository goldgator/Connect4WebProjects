using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Misc;

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

        public int[,] GetBoard()
        {
            return board;
        }

        public bool PlacePiece(int col, int value)
        {
            int row = Utilties.FindEmptySpot(board, col);
            board[row, col] = value;

            return Utilties.FindConnect4Win(board, row, col, value);
        }

        public int CheckSquare(int row, int col)
        {
            return board[row, col];
        }

        public override string ToString()
        {
            string boardString = "";
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    //if (board[r, c] == 1)
                    //{
                    //    board[r, c] = "~/Images/redChip.png";
                    //}

                    boardString += board[r, c] + "";
                }
                boardString += "\n";
            }

            return boardString;
        }
    }
}