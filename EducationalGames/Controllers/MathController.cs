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
        public IActionResult EasyAddition()
        {
            Random random = new Random();
          
            ViewBag.num1 = random.Next(1, 11);
            ViewBag.num2 = random.Next(1, 11);
            return View();
        }
        public IActionResult EasyAddAnswer(int answer, int num1, int num2)
        {
            int level = 1;
            string type = "addition";
            GetAddResult(answer, num1, num2, level, type);
            return View();
        }
        public IActionResult MediumAddition()
        {
            Random random = new Random();

            ViewBag.num1 = random.Next(1, 100);
            ViewBag.num2 = random.Next(1, 100);
            return View();
        }
        public IActionResult MediumAddAnswer(int answer, int num1, int num2)
        {
            int level = 2;
            string type = "addition";
            GetAddResult(answer, num1, num2, level, type);
            return View();
        }

        public IActionResult HardAddition()
        {
            Random random = new Random();
            ViewBag.num1 = random.Next(1000 + 1000) - 1000;
            ViewBag.num2 = random.Next(1000 + 1000) - 1000;
            return View();
        }
        public IActionResult HardAddAnswer(int answer, int num1, int num2)
        {
            int level = 3;
            string type = "addition";
            GetAddResult(answer, num1, num2, level, type);
            return View();
        }
        public IActionResult EasySubtraction()
        {
            Random random = new Random();

            ViewBag.num1 = random.Next(1, 11);
            ViewBag.num2 = random.Next(1, 11);
            return View();
        }
        public IActionResult EasySubAnswer(int answer, int num1, int num2)
        {
            int level = 1;
            string type = "subtraction";
            GetSubtractResult(answer, num1, num2, level, type);
            return View();
        }
        public IActionResult MediumSubtraction()
        {
            Random random = new Random();

            ViewBag.num1 = random.Next(1, 100);
            ViewBag.num2 = random.Next(1, 100);
            return View();
        }
        public IActionResult MediumSubAnswer(int answer, int num1, int num2)
        {
            int level = 2;
            string type = "subtraction";
            GetSubtractResult(answer, num1, num2, level, type);
            return View();
        }
        public IActionResult HardSubtraction()
        {
            Random random = new Random();
            ViewBag.num1 = random.Next(1000 + 1000) - 1000;
            ViewBag.num2 = random.Next(1000 + 1000) - 1000;
            return View();
        }
        public IActionResult HardSubAnswer(int answer, int num1, int num2)
        {
            int level = 3;
            string type = "subtraction";
            GetSubtractResult(answer, num1, num2, level, type);
            return View();
        }
        public void GetAddResult(int answer, int num1, int num2, int level, string type)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int correctAnswer = num1 + num2;
            if (answer == correctAnswer)
            {
                Models.Math result = new Models.Math { UserId = id, GameLevel = level, Wins = 1, Type = type };
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
        public void GetSubtractResult(int answer, int num1, int num2, int level, string type)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int correctAnswer = num1 - num2;
            if (answer == correctAnswer)
            {
                Models.Math result = new Models.Math { UserId = id, GameLevel = level, Wins = 1, Type = type };
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
