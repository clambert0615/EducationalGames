using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationalGames.Controllers
{
    public class MathController : Controller
    {
        private readonly EducationDbContext _context;
     
        public MathController(EducationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Addition(int level)
        {
            if(level != 0)
            {
                GetNumbers(level);
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            ViewBag.Level = level;
            return View();
        }
        public IActionResult AddAnswer(int answer, int num1, int num2, int level)
        {
            string type = "addition";
            GetResult(answer, num1, num2, level, type);
            ViewBag.Level = level;
            return View();
        }
     
    
        public IActionResult Subtraction(int level)
        {
            if (level != 0)
            {
                GetNumbers(level);
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            ViewBag.Level = level;
            return View();
        }
        public IActionResult SubAnswer(int answer, int num1, int num2, int level)
        {
            string type = "subtraction";
            GetResult(answer, num1, num2, level, type);
            ViewBag.Level = level;
            return View();
        }

        public IActionResult Multiplication(int level)
        {
            if (level != 0)
            {
                GetNumbers(level);
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            ViewBag.Level = level;
            return View();
        }

        public IActionResult MultAnswer(int answer, int num1, int num2, int level)
        {
            string type = "multiplication";
            GetResult(answer, num1, num2, level, type);
            ViewBag.Level = level;
            return View();
        }

        public IActionResult Division(int level)
        {
            Random random = new Random();
            int divisor = 0;
            int dividend = 0;
            if(level == 1)
            {
                 divisor = random.Next(1, 11);
                 dividend = divisor * random.Next(1, 11);
              }
            else if(level == 2)
            {
                 divisor = random.Next(10, 100);
                 dividend = divisor * random.Next(1, 100);
            }
            else if(level == 3)
            {
                 divisor = random.Next(1000 + 1000) - 1000;
                 dividend = divisor * (random.Next(1000 + 1000) -1000);
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            ViewBag.Divisor = divisor;
            ViewBag.Dividend = dividend;
            ViewBag.Level = level;
            return View();
        }
        public IActionResult DivAnswer(int answer, int dividend, int divisor, int level)
        {
            string type = "division";
            GetResult(answer, dividend, divisor, level, type);
            ViewBag.Level = level;
            return View();
        }

        public void GetNumbers(int level)
        {
            Random random = new Random();
            if (level == 1)
            {
                ViewBag.num1 = random.Next(1, 11);
                ViewBag.num2 = random.Next(1, 11);
            }
            else if (level == 2)
            {
                ViewBag.num1 = random.Next(10, 100);
                ViewBag.num2 = random.Next(1, 100);
            }
            else if (level == 3)
            {
                ViewBag.num1 = random.Next(1000 + 1000) - 1000;
                ViewBag.num2 = random.Next(1000 + 1000) - 1000;
            }
        }
        
        public void GetResult(int answer, int num1, int num2, int level, string type)
        {
            int correctAnswer = 0;
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (type == "subtraction")
            {
                correctAnswer = num1 - num2;
            }
            else if (type == "addition")
            {
                correctAnswer = num1 + num2;
            }
            else if (type == "multiplication")
            {
                correctAnswer = num1 * num2;
            }
            else if(type == "division")
            {
                correctAnswer = num1 / num2;
            }
            if (answer == correctAnswer)
            {
                Models.Math result = new Models.Math { UserId = id, GameLevel = level, Wins = 1, Type = type};
                if (ModelState.IsValid)
                {
                    _context.Math.Add(result);
                    _context.SaveChanges();
                }
                ViewBag.message = "Correct Answer!";

            }
            else
            {
                Models.Math result = new Models.Math { UserId = id, GameLevel = level, Losses = 1, Type = type };
                if (ModelState.IsValid)
                {
                    _context.Math.Add(result);
                    _context.SaveChanges();
                }
                ViewBag.message = $"Sorry, that is incorrect.  The correct answer is {correctAnswer}";

            }
        }
        
     }
}
