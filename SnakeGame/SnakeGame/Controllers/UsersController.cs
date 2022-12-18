using BL;

using Interfaces;
using SnakeGame.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;


namespace SnakeGame.Controllers
{
    public class UsersController : Controller
    {
        private IUsersBL _bl;
        public UsersController(IUsersBL bl)
        {
            _bl = bl;
        }

        public IActionResult Index()
        {
            //var users = _bl.GetAllUsers();

            //if (users != null)
            //{
            //    return View(users.ToString());
            //}
            //else
            //{
            //    return View();
            //}
            //return View(users);
            return View();
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Reg(RegModel regModel)
        {
            User newUser = new User()
            {
                Name = regModel.Login,
                Password = regModel.Password,
            };
            _bl.PutUser(newUser);
            var identity = new CustomUserIdentity(newUser);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
           // return RedirectToAction("Test", "Home");
            return RedirectToAction("Game", "Home");

        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = _bl.GetByLogin(loginModel.Login);

            if (user != null && user.Password.TrimEnd() == loginModel.Password.TrimEnd())
            {
                var identity = new CustomUserIdentity(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("Game", "Home"); ////

            }
            return RedirectToAction("Login", "Users");

        }


        public IActionResult Get(int id)
        {
            var user = _bl.GetById(id);

            if (user != null)
            {
                return View(new UserModel() { Id = user.Id, Name = $"{user.Name}" });
            }
            else
            {
                return View();
            }

        }


        public IActionResult GetAllUsers()
        {
            var users = _bl.GetAllUsers();

            if (users != null)
            {
                return View(users.ToString());
            }
            else
            {
                return View();
            }

        }
    }
}
