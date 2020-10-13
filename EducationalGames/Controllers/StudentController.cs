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
            return View();
        }
        
        public IActionResult Statistics()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string firstName = _context.AspNetUsers.Find(id).FirstName;
            string lastName = _context.AspNetUsers.Find(id).LastName;
            var routeValues = new RouteValueDictionary
            {
                {"studentfirst", firstName },
                {"studentlast", lastName }
            };
 
            return RedirectToAction("StudentProgress", "Teacher", routeValues);
        }
    }
}
