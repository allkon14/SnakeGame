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
using ORMDal;

namespace SnakeGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //private LoginContext db;
        //public HomeController(LoginContext context)
        //{
        //    db = context;
        //}



        [Authorize(Roles = "Admin")]
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
            return Json(new { Id = 1, Name = "Andrey" });
        }

        public IActionResult Test()
        {
            return View("Test");
        }

        //public async Task<IActionResult> Show()
        //{
        //    return View(await db.Logins.ToListAsync()); //получаем объект из БД, создаем список
        //}
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(LoginModel user) //добавляем новый объект
        //{

        //    db.Logins.Add(user);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
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
    }
}
