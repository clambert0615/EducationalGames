using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EducationalGames.Controllers
{
    [Authorize(Roles = "Parent")]
    public class ParentController : Controller
    {
        private readonly EducationDbContext _context;
        public ParentController(EducationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListStudent()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<AspNetUsers> stList = new List<AspNetUsers>();
            Parent parent = _context.Parent.FirstOrDefault(x => x.UserId == id);
            if (parent != null)
            {
                List<StudentParent> spList = _context.StudentParent.Where(x => x.ParentId == parent.ParentId).ToList();
                foreach(StudentParent sp in spList)
                {
                    Students student = _context.Students.FirstOrDefault(x => x.StudentId == sp.StudentId);
                    AspNetUsers user = _context.AspNetUsers.FirstOrDefault(x => x.Id == student.UserId);
                    stList.Add(user);
                }
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            return View(stList);
        }
        public IActionResult StudDetails(string id)
        {
            ViewBag.StudentEmail = _context.AspNetUsers.Find(id).Email;
            ViewBag.StudentFirstName = _context.AspNetUsers.Find(id).FirstName;
            ViewBag.StudentLastName = _context.AspNetUsers.Find(id).LastName;
            Students st = _context.Students.FirstOrDefault(x => x.UserId == id);
            StudentTeacher stteach = _context.StudentTeacher.FirstOrDefault(x => x.StudentId == st.StudentId);
            Teacher teach = _context.Teacher.FirstOrDefault(x => x.TeacherId == stteach.TeacherId);
            AspNetUsers teachUser = _context.AspNetUsers.Find(teach.UserId);
            ViewBag.TeacherEmail = teachUser.Email;
            ViewBag.TeacherFirstName = teachUser.FirstName;
            ViewBag.TeacherLastName = teachUser.LastName;
            ViewBag.TeachUserId = teachUser.Id;
            return View();
        }
        public IActionResult ProgressReport(string firstName, string lastName, string teachUserId)
        {
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
