using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Game;
using Connect4_Web_Project.Models.Players;

namespace Connect4_Web_Project.Controllers
{
    public static class GroupManager
    {
        public class Lobby
        {
            public Guid id;
            public string lobbyName { get
                {
                    return id.ToString();
                }
            }

            public Game game;
        }

        public static List<Lobby> lobbies { get; set; } = new List<Lobby>();

        public static Lobby FindOpenLobby(int pieceKey)
        {
            foreach (Lobby l in lobbies)
            {
                if (l.game.GetPlayerSize() < 2 && l.game.GetPlayer(0).PlayerNum != pieceKey)
                {
                    return l;
                }
            }

            //IF no lobbies are open
            Lobby lobby = new Lobby();
            lobby.id = Guid.NewGuid();
            lobby.game = new Game();

            lobbies.Add(lobby);
            return lobby;
        }

        public static Lobby CreateLobbyWithPlayer(Player player)
        {
            Lobby lobby = new Lobby();
            lobby.id = Guid.NewGuid();
            lobby.game = new Game();

            lobby.game.AddPlayer(player);

            lobbies.Add(lobby);
            return lobby;
        }

        public static string GetGroupName(Guid id)
        {
            foreach (Lobby l in lobbies)
            {
                if (l.id == id)
                {
                    return l.lobbyName;
                }
            }

            return null;
        }

        public static Lobby FindLobbyViaGuid(Guid id)
        {
            foreach (Lobby l in lobbies)
            {
                if (l.id == id)
                {
                    return l;
                }
            }

            return null;
        }

        public static Lobby FindLobbyViaConnectionID(string connectionID)
        {
            foreach (Lobby l in lobbies)
            {
                if (l.game.hasPlayer(connectionID))
                {
                    return l;
                }
            }

            return null;
        }

    }
}