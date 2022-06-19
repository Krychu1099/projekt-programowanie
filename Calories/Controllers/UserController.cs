using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calories.Models;
using Microsoft.AspNetCore.Http;

namespace Calories.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel, string PasswordRep)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("UserInfo");
            }

            if (userModel.checkPassword(userModel.Password, PasswordRep) == false)
            {
                ViewBag.errorMessage = "Podane hasła nie są takie same!";
                return View();
            }

            bool checkUserInDb = userModel.checkUserInDatabsa(userModel);
            if (checkUserInDb)
            {
                ViewBag.userExist = "Użytkownik o takim loginie lub mailu już istnieje";
                return View();
            }

            if (ModelState.IsValid)
            {
                userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

                using (var db = new MyDbContext())
                {
                    db.Add(userModel);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.successfulLoginMessage = "Brawo " + userModel.Username + ", udało Ci się zarejestrować!";
            }
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("UserInfo");
            }
            
            using (var db = new MyDbContext())
            {
                

                var usr = db.Users.Single(u => u.Login == user.Login);

                bool verify = BCrypt.Net.BCrypt.Verify(user.Password, usr.Password);

                if (usr != null && verify)
                {
                    HttpContext.Session.SetString("UserId", usr.Id.ToString());
                    HttpContext.Session.SetString("Username", usr.Username.ToString());

                    ViewBag.username = HttpContext.Session.GetString("Username");
                    ViewBag.successfulMessage = "Udało Ci się zalogować " + usr.Username;
                    return RedirectToAction("UserInfo");
                }
                else
                {
                    ModelState.AddModelError("", "Login lub hasło są nieprawidłowe");
                    return View();
                }
            }
        }

        public IActionResult UserInfo()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int userId = Int32.Parse(HttpContext.Session.GetString("UserId"));

                ViewBag.userId = userId;
                ViewBag.username = HttpContext.Session.GetString("Username");

                List<CaloriesModel> userCalories = new List<CaloriesModel>();
                
                using (var db = new MyDbContext())
                {
                    userCalories = db.Calories.Where(u => u.UserId == userId).ToList();

                    ViewBag.userCalories = userCalories;
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
