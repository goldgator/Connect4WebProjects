using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Board;

namespace Connect4_Web_Project.Models.Game
{
    public class Game
    {
        List<Turns> turns = new List<Turns>();
        Board.Board board = new Board.Board();

        Guid ID { get; set; }

        public void StartGame()
        {
            bool gameOver = false;
            while (!gameOver)
            {

            }
        }
    }
}