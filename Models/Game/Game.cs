using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Board;
using Connect4_Web_Project.Models.Players;
using Connect4_Web_Project.Models.Misc;

namespace Connect4_Web_Project.Models.Game
{
    public class Game
    {
        List<Player> players = new List<Player>();
        Player human1, human2, computer1, computer2, currentPlayer;

        Board.Board board = new Board.Board();

        Guid ID { get; set; }

        int turnIndex = 0;

        public Game() { }

        public void RunGame()
        {
            bool quit = false;
            while (!quit)
            {
                board.GetBoard();

                human1 = new Human();
                computer1 = new Computer();
                players.Add(human1);
                players.Add(computer1);

                PlayGame();
            }
        }

        public void PlayGame()
        {
            currentPlayer = players[0];
            bool gameOver = false;
            while (!gameOver)
            {
                currentPlayer.MakeMove(board.GetBoard());
                
                //instead of 0, 0.. need to get player input from webpage or start from first filled row/col
                gameOver = Utilties.FindConnect4Win(board.GetBoard(), 0, 0, currentPlayer.PlayerNum);

                GameTurn();
            }
            System.Console.WriteLine("I won");
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