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
        private List<Player> players = new List<Player>();
        private Board.Board board = new Board.Board();

        public Board.Board GetBoardInstance()
        {
            return board;
        }

        public bool hasPlayer(string connectionID)
        {
            foreach (Player p in players)
            {
                if (p.connectionID == connectionID)
                {
                    return true;
                }
            }

            return false;
        }

        Guid ID { get; set; }

        public Game() { }

        public int GetPlayerSize()
        {
            return players.Count;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

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