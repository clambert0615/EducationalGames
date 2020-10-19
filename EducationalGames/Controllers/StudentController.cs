using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EducationalGames.Controllers
{
    public class StudentController : Controller
    {
        private readonly EducationDbContext _context;
  
        public StudentController(EducationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.FirstName = _context.AspNetUsers.Find(id).FirstName;
            ViewBag.LastName = _context.AspNetUsers.Find(id).LastName;
            return View();
        }
        
        public IActionResult Statistics()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string firstName = _context.AspNetUsers.Find(id).FirstName;
            string lastName = _context.AspNetUsers.Find(id).LastName;
            Students st = _context.Students.FirstOrDefault(x => x.UserId == id);
            StudentTeacher stteach = _context.StudentTeacher.FirstOrDefault(x => x.StudentId == st.StudentId);
            Teacher teach = _context.Teacher.FirstOrDefault(x => x.TeacherId == stteach.TeacherId);
            AspNetUsers teachUser = _context.AspNetUsers.Find(teach.UserId);
            string teachUserId = teachUser.Id;
            var routeValues = new RouteValueDictionary
            {
                {"studentfirst", firstName },
                {"studentlast", lastName },
                {"teachuserId", teachUserId }
            };
 
            return RedirectToAction("StudentProgress", "Teacher", routeValues);
        }
    }
}
