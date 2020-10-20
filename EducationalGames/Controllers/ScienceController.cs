using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationalGames.Controllers
{
    public class ScienceController : Controller
    {
        private readonly EducationDbContext _context;
        private readonly PeriodicTableDAL pt = new PeriodicTableDAL();
        private readonly PlanetsDAL pd = new PlanetsDAL();
        public ScienceController(EducationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PeriodicTableQuestion(string type)
        {
            PeriodicTable table = await pt.GetElements();
            Random random = new Random();
            int index = random.Next(118);
            if (type == "numbertoname")
            {
                TempData["atomicNumber"] = table.records[index].fields.atomicnumber;
                TempData["name"] = table.records[index].fields.name;
                return RedirectToAction("NumberToName");
            }
            else if (type == "nametonumber")
            {
                TempData["atomicNumber"] = table.records[index].fields.atomicnumber;
                TempData["name"] = table.records[index].fields.name;
                return RedirectToAction("NameToNumber");
            }
            else if (type == "nametosymbol")
            {
                TempData["name"] = table.records[index].fields.name;
                TempData["symbol"] = table.records[index].fields.symbol;
                return RedirectToAction("NameToSymbol");
            }
            else if (type == "symboltoname")
            {
                TempData["name"] = table.records[index].fields.name;
                TempData["symbol"] = table.records[index].fields.symbol;
                return RedirectToAction("SymbolToName");
            }
           else
            {
                return RedirectToAction("ErrorPage", "Home");
            }
        }
        public IActionResult NumberToName()
        {
            ViewBag.AtomicNumber = TempData["atomicNumber"];
            ViewBag.Name = TempData["name"];
            ViewBag.Type = "numbertoname";
            return View();
        }
        public IActionResult NameToNumber()
        {
            ViewBag.AtomicNumber = TempData["atomicNumber"];
            ViewBag.Name = TempData["name"];
            ViewBag.Type = "nametonumber";
            return View();
        }
        public IActionResult NameToSymbol()
        {
            ViewBag.Name = TempData["name"];
            ViewBag.Symbol = TempData["symbol"];
            ViewBag.Type = "nametosymbol";
            return View();
        }
        public IActionResult SymbolToName()
        {
            ViewBag.Name = TempData["name"];
            ViewBag.Symbol = TempData["symbol"];
            ViewBag.Type = "symboltoname";
            return View();
        }
     
        public IActionResult PerTableStringAnswer(string answer, string correctAnswer, string type)
        {
           
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string titleCaseAnswer = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(answer);
           
            if(titleCaseAnswer == correctAnswer)
            {
                Science science = new Science { UserId = id, Correct = 1, Type = type };
                if (ModelState.IsValid)
                {
                    _context.Science.Add(science);
                    _context.SaveChanges();
                }
                ViewBag.message = "Correct Answer!";
            }
            else
            {
                Science science = new Science { UserId = id, Incorrect = 1, Type = type };
                if (ModelState.IsValid)
                {
                    _context.Science.Add(science);
                    _context.SaveChanges();
                }
                ViewBag.message = $"That is incorrect. The correct answer is {correctAnswer}";
            }
            ViewBag.Type = type;
            return View();
        }

        public IActionResult PerTableIntAnswer(int answer, int correctAnswer, string type)
        {

            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
          
            if (answer == correctAnswer)
            {
                Science science = new Science { UserId = id, Correct = 1, Type = type };
                if (ModelState.IsValid)
                {
                    _context.Science.Add(science);
                    _context.SaveChanges();
                }
                ViewBag.message = "Correct Answer!";
            }
            else
            {
                Science science = new Science { UserId = id, Incorrect = 1, Type = type };
                if (ModelState.IsValid)
                {
                    _context.Science.Add(science);
                    _context.SaveChanges();
                }
                ViewBag.message = $"That is incorrect. The correct answer is {correctAnswer}";
            }
            ViewBag.Type = type;
            return View();
        }

        public async Task<IActionResult> PlanetQuestions()
        {
            List<Planets> planetList = await pd.GetPlanets();
            Random random = new Random();
            int index = random.Next(30);
            ViewBag.Question = planetList[index].Question;
            ViewBag.Answer = planetList[index].Answer;
            ViewBag.Type = planetList[index].Type;
            return View(); 
        }
        public IActionResult PlanetAnswer(string userAnswer, string correctAnswer, string type)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string titleCaseAnswer = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userAnswer);

            if (titleCaseAnswer == correctAnswer)
            {
                Science science = new Science { UserId = id, Correct = 1, Type = type };
                if (ModelState.IsValid)
                {
                    _context.Science.Add(science);
                    _context.SaveChanges();
                }
                ViewBag.message = "Correct Answer!";
            }
            else
            {
                Science science = new Science { UserId = id, Incorrect = 1, Type = type };
                if (ModelState.IsValid)
                {
                    _context.Science.Add(science);
                    _context.SaveChanges();
                }
                ViewBag.message = $"That is incorrect. The correct answer is {correctAnswer}";
            }
            ViewBag.Type = type;
            return View();
        }
     
    }
}
