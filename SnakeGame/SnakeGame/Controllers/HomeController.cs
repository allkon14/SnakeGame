using SnakeGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SnakeGame.Models.Users;
//using ORMDal;
using Interfaces;
using Entities;
using SnakeGame.Models.Games;

namespace SnakeGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUsersBL _userBL;
        private IGameBL _gameBL;

        public HomeController(ILogger<HomeController> logger, IUsersBL userBL, IGameBL gameBL)
        {
            _logger = logger;
            _userBL = userBL;
            _gameBL = gameBL;
        }

        //private LoginContext db;
        //public HomeController(LoginContext context)
        //{
        //    db = context;
        //}



        //[Authorize(Roles = "Admin")]
        [Authorize]
        public IActionResult Index(int id, string name)
        {
            TempData["Success"] = "Success";
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
            }
            return View("Index");
           // return View("Login");

        }

        public IActionResult Privacy()
        {
            return Json(new { Id = 1, Name = "Alena" });
        }

        //public IActionResult Test()
        //{
        //    return View("Test");
        //}

        //public IActionResult Create()
        //{
        //    return View();
        //}


        public IActionResult Info()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpGet]
        //[Authorize]
        public IActionResult Game()///////////
        {
            Game newGame = new Game();
            GameModel gameModel = new GameModel()
            {
                Score = 0,
                GameDate = DateTime.Now,
                UserId = _userBL.GetByLogin(User.Identity.Name).Id
            };
            return View(gameModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult SaveGame(int score)
        {
            Game game = new Game()
            {
                Score = score,
                GameDate = DateTime.Now,
                UserId = _userBL.GetByLogin(User.Identity.Name).Id
            };
            _gameBL.PutGame(game);

            return RedirectToAction("Game", "Home");
        }
    }
}
