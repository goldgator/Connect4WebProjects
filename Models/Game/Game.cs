using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Board;
using Connect4_Web_Project.Models.Players;

namespace Connect4_Web_Project.Models.Game
{
    public class Game
    {
        List<Player> players = new List<Player>();
        Board.Board board = new Board.Board();

        Guid ID { get; set; }

        public Game() { }

        public void StartGame()
        {
            bool gameOver = false;
            while (!gameOver)
            {

            }
        }

        public void GameTurn()
        {
            int turn = 0;

            int choice = players[turn].MakeMove(board.GetBoard());
            if (choice >= 1 && choice <= 7)
            {
                board.PlacePiece(choice, players[turn].PlayerNum);

                turn = (turn + 1) % players.Count;
            }
            
        }
    }
}