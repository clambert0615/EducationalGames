using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationalGames.Controllers
{
    public class StudentController : Controller
    {
        private readonly EducationDbContext _context;
        public Statistics stats = new Statistics();
        public StudentController(EducationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Statistics()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addLevel1stats = _context.Math.Where(x => (x.UserId == id)&&(x.Type == "addition")&&(x.GameLevel == 1)).ToList();
            foreach( var s in addLevel1stats)
            {
                ViewBag.AddLevel1Wins = (ViewBag.AddLevel1Wins ?? 0) + (s.Wins ?? 0);
                ViewBag.AddLevel1Losses = (ViewBag.AddLevel1Losses ?? 0) + (s.Losses ?? 0);
            }
            var addLevel2stats = _context.Math.Where(x => (x.UserId == id) && (x.Type == "addition") && (x.GameLevel == 2)).ToList();
            foreach (var s in addLevel2stats)
            {
                ViewBag.AddLevel2Wins = (ViewBag.AddLevel2Wins ?? 0) + (s.Wins ?? 0);
                ViewBag.AddLevel2Losses = (ViewBag.AddLevel2Losses ?? 0) + (s.Losses ?? 0);
            }
            var addLevel3stats = _context.Math.Where(x => (x.UserId == id) && (x.Type == "addition") && (x.GameLevel == 3)).ToList();
            foreach (var s in addLevel3stats)
            {
                ViewBag.AddLevel3Wins = (ViewBag.AddLevel3Wins ?? 0) + (s.Wins ?? 0);
                ViewBag.AddLevel3Losses = (ViewBag.AddLevel3Losses ?? 0) + (s.Losses ?? 0);
            }
            var subLevel1stats = _context.Math.Where(x => (x.UserId == id) && (x.Type == "subtraction") && (x.GameLevel == 1)).ToList();
            foreach (var s in subLevel1stats)
            {
                ViewBag.SubLevel1Wins = (ViewBag.SubLevel1Wins ?? 0) + (s.Wins ?? 0);
                ViewBag.SubLevel1Losses = (ViewBag.SubLevel1Losses ?? 0) + (s.Losses ?? 0);
            }
            var subLevel2stats = _context.Math.Where(x => (x.UserId == id) && (x.Type == "subtraction") && (x.GameLevel == 2)).ToList();
            foreach (var s in subLevel2stats)
            {
                ViewBag.SubLevel2Wins = (ViewBag.SubLevel2Wins ?? 0) + (s.Wins ?? 0);
                ViewBag.SubLevel2Losses = (ViewBag.SubLevel2Losses ?? 0) + (s.Losses ?? 0);
            }
            var subLevel3stats = _context.Math.Where(x => (x.UserId == id) && (x.Type == "subtraction") && (x.GameLevel == 3)).ToList();
            foreach (var s in subLevel3stats)
            {
                ViewBag.SubLevel3Wins = (ViewBag.SubLevel3Wins ?? 0) + (s.Wins ?? 0);
                ViewBag.SubLevel3Losses = (ViewBag.SubLevel3Losses ?? 0) + (s.Losses ?? 0);
            }
            return View();
        }
    }
}
