using Connect4_Web_Project.Models.Board;
using Connect4_Web_Project.Models.Players;
using Connect4_Web_Project.Models.Players.Difficulties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Connect4_Web_Project.Controllers
{
    public class GameController : Controller
    {


        // GET: Game
        public ActionResult Index()
        {
            ViewBag.MyBoard = new Board().GetBoard();

            return View();
        }

        public ActionResult UpdateBoard(string column, string pieceKey, string connectionID)
        {
            bool parsed = int.TryParse(column, out int colNumber);
            bool pieceParsed = int.TryParse(pieceKey, out int pieceNum);

            colNumber -= 1;
            ViewBag.ColumnNumber = colNumber;

            GroupManager.Lobby lobby = GroupManager.FindLobbyViaConnectionID(connectionID);
            Board board = lobby.game.GetBoardInstance();

            
            
            //Computer computer1 = new Computer(2, new Hard(), board.GetBoard());
            //board.PlacePiece(computer1.MakeMove(board.GetBoard()), computer1.pieceKey);

            ViewBag.MyBoard = board.GetBoard();

            return PartialView("Grid", board);
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
