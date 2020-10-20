using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalGames.Controllers
{
  
    public class TeacherController : Controller
    {
        private readonly EducationDbContext _context;
      
        public TeacherController(EducationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult ListStudents()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<AspNetUsers> stList = new List<AspNetUsers>();
            Teacher teacherId = _context.Teacher.FirstOrDefault(x => x.UserId == id);
            if (teacherId != null)
            {
                List<StudentTeacher> stIdList = _context.StudentTeacher.Where(x => x.TeacherId == teacherId.TeacherId).ToList();
                foreach (StudentTeacher st in stIdList)
                {
                    Students stud = _context.Students.FirstOrDefault(x => x.StudentId == st.StudentId);
                    AspNetUsers user = _context.AspNetUsers.FirstOrDefault(x => x.Id == stud.UserId);
                    stList.Add(user);

                }
            }
            else
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            return View(stList);
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult DetailedInformation(string id)
        {
            AspNetUsers user = _context.AspNetUsers.Find(id);
            ViewBag.StudentEmail = user.Email;
            ViewBag.StudentFirst = user.FirstName;
            ViewBag.StudentLast = user.LastName;
            Students stud = _context.Students.FirstOrDefault(x => x.UserId == user.Id);
            ViewBag.StudentId = stud.StudentId;
            StudentParent sp = _context.StudentParent.FirstOrDefault(x => x.StudentId == stud.StudentId);
            Parent parentId = _context.Parent.FirstOrDefault(x => x.ParentId == sp.ParentId);
            AspNetUsers parentUser = _context.AspNetUsers.FirstOrDefault(x => x.Id == parentId.UserId);
            ViewBag.ParentFirst = parentUser.FirstName;
            ViewBag.ParentLast = parentUser.LastName;
            ViewBag.ParentEmail = parentUser.Email;
            return View();
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult GetStudentProg()
        {
            return View();
        }
    
        public IActionResult StudentProgress(string studentfirst, string studentlast, string teachuserId)
        {            
            AspNetUsers userId = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == studentfirst) && (x.LastName == studentlast));
            string id = userId.Id;
            ViewBag.FirstName = studentfirst;
            ViewBag.LastName = studentlast;
            
            SetMath(id, "addition", 1);
            SetMath(id, "addition", 2);
            SetMath(id, "addition", 3);
            SetMath(id, "subtraction", 1);
            SetMath(id, "subtraction", 2);
            SetMath(id, "subtraction", 3);
            SetMath(id, "multiplication", 1);
            SetMath(id, "multiplication", 2);
            SetMath(id, "multiplication", 3);
            SetMath(id, "division", 1);
            SetMath(id, "division", 2);
            SetMath(id, "division", 3);
            SetScience(id, "numbertoname");
            SetScience(id, "nametonumber");
            SetScience(id, "nametosymbol");
            SetScience(id, "symboltoname");
            SetScience(id, "shortanswer");
            SetScience(id, "trueorfalse");
            SetMathAverage("addition", 1, teachuserId);
            SetMathAverage("addition", 2, teachuserId);
            SetMathAverage("addition", 3, teachuserId);
            SetMathAverage("subtraction", 1, teachuserId);
            SetMathAverage("subtraction", 2 , teachuserId);
            SetMathAverage("subtraction", 3 , teachuserId);
            SetMathAverage("multiplication", 1 , teachuserId);
            SetMathAverage("multiplication", 2, teachuserId);
            SetMathAverage("multiplication", 3, teachuserId);
            SetMathAverage("division", 1, teachuserId);
            SetMathAverage("division", 2, teachuserId);
            SetMathAverage("division", 3, teachuserId);
            SetScienceAverage("numbertoname", teachuserId);
            SetScienceAverage("nametonumber", teachuserId);
            SetScienceAverage("nametosymbol", teachuserId);
            SetScienceAverage("symboltoname", teachuserId);
            SetScienceAverage("shortanswer", teachuserId);
            SetScienceAverage("trueorfalse", teachuserId);


            return View();
        }


        public void SetMath(string id, string type, int gameLevel)
        {
            List<Models.Math> stats = _context.Math.Where(x => (x.UserId == id) && (x.Type == type) && (x.GameLevel == gameLevel)).ToList();
            foreach(Models.Math s in stats)
            {
                string correct = $"{type}Level{gameLevel}Correct";
                string incorrect = $"{type}Level{gameLevel}InCorrect";
                string percent = $"{type}Level{gameLevel}Percent";
                ViewData[correct] = ((decimal?) ViewData[correct] ?? 0) + (s.Wins ?? 0);
                ViewData[incorrect] = ((decimal?) ViewData[incorrect] ?? 0) + (s.Losses ?? 0);
                ViewData[percent] = System.Math.Round((((decimal?)ViewData[correct] ?? 0) / (((decimal?)ViewData[correct] ?? 0) + ((decimal?)ViewData[incorrect] ?? 0))) * 100, 1);
            }
        }
        public void SetScience(string id, string type)
        {
            List<Science> stats = _context.Science.Where(x => (x.UserId == id) && (x.Type == type)).ToList();
            foreach (Science s in stats)
            {
                string correct = $"{type}Correct";
                string incorrect = $"{type}InCorrect";
                string percent = $"{type}Percent";
                ViewData[correct] = ((decimal?)ViewData[correct] ?? 0) + (s.Correct ?? 0);
                ViewData[incorrect] = ((decimal?)ViewData[incorrect] ?? 0) + (s.Incorrect ?? 0);
                ViewData[percent] = System.Math.Round((((decimal?)ViewData[correct] ?? 0) / (((decimal?)ViewData[correct] ?? 0) + ((decimal?)ViewData[incorrect] ?? 0))) * 100, 1);

            }
        }
        public void SetMathAverage(string type, int gameLevel, string teachUserId)
        {
            string id;
            if (teachUserId == null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                id = teachUserId;
            }
            List<AspNetUsers> stList = new List<AspNetUsers>();
            decimal correctAverage = 0;
            decimal incorrectAverage = 0;
            Teacher teacherId = _context.Teacher.FirstOrDefault(x => x.UserId == id);
            if (teacherId != null)
            {
                List<StudentTeacher> stIdList = _context.StudentTeacher.Where(x => x.TeacherId == teacherId.TeacherId).ToList();
                foreach (StudentTeacher st in stIdList)
                {
                    Students stud = _context.Students.FirstOrDefault(x => x.StudentId == st.StudentId);
                    AspNetUsers user = _context.AspNetUsers.FirstOrDefault(x => x.Id == stud.UserId);
                    List<Models.Math> stats = _context.Math.Where(x => (x.UserId == user.Id) && (x.Type == type) && (x.GameLevel == gameLevel)).ToList();
                    foreach(Models.Math s in stats)
                    {
                        correctAverage += s.Wins ?? 0;
                        incorrectAverage += s.Losses ?? 0;

                    }

                }
            }
            string percent = $"{type}Level{gameLevel}AveragePercent";
            if (correctAverage + incorrectAverage == 0)
            {
                ViewData[percent] = 0;
            }
            else
            {
                ViewData[percent] = System.Math.Round((correctAverage) / ((correctAverage + incorrectAverage)) * 100, 1);
            }
        }
        public void SetScienceAverage(string type, string teachUserId)
        {
            string id;
            if (teachUserId == null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                id = teachUserId;
            }
          
            List<AspNetUsers> stList = new List<AspNetUsers>();
            decimal correctAverage = 0;
            decimal incorrectAverage = 0;
            Teacher teacherId = _context.Teacher.FirstOrDefault(x => x.UserId == id);
            if (teacherId != null)
            {
                List<StudentTeacher> stIdList = _context.StudentTeacher.Where(x => x.TeacherId == teacherId.TeacherId).ToList();
                foreach (StudentTeacher st in stIdList)
                {
                    Students stud = _context.Students.FirstOrDefault(x => x.StudentId == st.StudentId);
                    AspNetUsers user = _context.AspNetUsers.FirstOrDefault(x => x.Id == stud.UserId);
                    List<Science> stats = _context.Science.Where(x => (x.UserId == user.Id) && (x.Type == type)).ToList();
                    foreach (Science s in stats)
                    {
                        correctAverage += s.Correct ?? 0;
                        incorrectAverage += s.Incorrect ?? 0;

                    }

                }
            }
            string percent = $"{type}Average";
            if (correctAverage + incorrectAverage == 0)
            {
                ViewData[percent] = 0;
            }
            else
            {
                ViewData[percent] = System.Math.Round((correctAverage) / ((correctAverage + incorrectAverage)) * 100, 1);
            }
        }
     
    }

}
