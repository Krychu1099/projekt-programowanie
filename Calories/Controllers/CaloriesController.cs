using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calories.Models;
using Microsoft.AspNetCore.Http;

namespace Calories.Controllers
{
    public class CaloriesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.userId = HttpContext.Session.GetString("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");

            return View();
        }

        [HttpPost]
        public IActionResult Index(CaloriesModel calories)
        {
            calories.CalorieRequirement = (int)Math.Round(calories.calculate(calories.Gender, calories.Weight, calories.Height, calories.Age, calories.Activity, calories.Goal, calories.Somatotype));
            calories.Date = DateTime.Now.ToString("dd-MM-yyyy");

            
            if (ModelState.IsValid && HttpContext.Session.GetString("UserId") != null)
            {
                using (var db = new MyDbContext())
                {
                    db.Add(calories);
                    db.SaveChanges();
                }
            }

            ViewBag.calories = calories;

            return View("Result");
        }
    }
}
