using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalGames.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly EducationDbContext _context;
        public AdminController(EducationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SetUpAcct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetUpAcct(string type, string firstName, string lastName)
        {
            AspNetUsers user = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == firstName) && (x.LastName == lastName));
            if (user != null)
            {
                if (type == "Student")
                {
                    Students s = new Students { UserId = user.Id };
                    _context.Students.Add(s);
                    _context.SaveChanges();
                }
                else if (type == "Teacher")
                {
                    Teacher t = new Teacher { UserId = user.Id };
                    _context.Teacher.Add(t);
                    _context.SaveChanges();
                }
                else if (type == "Parent")
                {
                    Parent p = new Parent { UserId = user.Id };
                    _context.Parent.Add(p);
                    _context.SaveChanges();

                }
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            TempData["Message"] = $"Success! A new Id for {type} has been automatically created.";
            return RedirectToAction("Index");
        }
        public IActionResult GetStudtoTeacher()
        {
            return View();
        }
        public IActionResult AssignStudtoTeacher(string studentfirst, string studentlast, string teacherfirst, string teacherlast)
        {
            var student = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == studentfirst) && (x.LastName == studentlast));
            return View();
        }
    }
}
