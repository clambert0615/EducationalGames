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
            AspNetUsers studentUser = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == studentfirst) && (x.LastName == studentlast));
            if (studentUser != null)
            {
                Students student = _context.Students.FirstOrDefault(x => x.UserId == studentUser.Id);
                if (student != null)
                {
                    AspNetUsers teacherUser = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == teacherfirst) && (x.LastName == teacherlast));
                    if (teacherUser != null)
                    {
                        Teacher teacher = _context.Teacher.FirstOrDefault(x => x.UserId == teacherUser.Id);
                        if (teacher != null)
                        {
                            StudentTeacher st = new StudentTeacher { StudentId = student.StudentId, TeacherId = teacher.TeacherId };
                            _context.StudentTeacher.Add(st);
                            _context.SaveChanges();

                            TempData["Message"] = $"The student, {studentfirst} {studentlast}, has been assigned to {teacherfirst} {teacherlast}.";
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            return RedirectToAction("Index");
        }
        public IActionResult GetParenttoStud()
        {
            return View();
        }
        public IActionResult AssignParenttoStud(string parentfirst, string parentlast, string studentfirst, string studentlast)
        {
            AspNetUsers studentUser = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == studentfirst) && (x.LastName == studentlast));
            if (studentUser != null)
            {
                Students student = _context.Students.FirstOrDefault(x => x.UserId == studentUser.Id);
                if (student != null)
                {
                    AspNetUsers parentUser = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == parentfirst) && (x.LastName == parentlast));
                    if (parentUser != null)
                    {
                        Parent parent = _context.Parent.FirstOrDefault(x => x.UserId == parentUser.Id);
                        if (parent != null)
                        {
                            StudentParent sp = new StudentParent { StudentId = student.StudentId, ParentId = parent.ParentId };
                            _context.StudentParent.Add(sp);
                            _context.SaveChanges();
                            TempData["Message"] = $"The parent, {parentfirst} {parentlast}, has been assigned to {studentfirst} {studentlast}.";
                        }
                    }
                }

            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            return RedirectToAction("Index");
        }
        public IActionResult GetLookUp()
        {
            return View();
        }
        public IActionResult LookUp(string type, string firstName, string lastName, int id)
        {
            AspNetUsers user = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == firstName) && (x.LastName == lastName));
            if (user != null)
            {
                if (type == "Student" && id == 0)
                {
                    Students student = _context.Students.FirstOrDefault(x => x.UserId == user.Id);
                    if (student != null)
                    {
                        ViewBag.Id = student.StudentId;
                        ViewBag.FirstName = firstName;
                        ViewBag.LastName = lastName;
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage");
                    }
                }
                else if (type == "Teacher" && id == 0)
                {
                    Teacher teacher = _context.Teacher.FirstOrDefault(x => x.UserId == user.Id);
                    if (teacher != null)
                    {
                        ViewBag.Id = teacher.TeacherId;
                        ViewBag.FirstName = firstName;
                        ViewBag.LastName = lastName;
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage");
                    }
                }
                else if (type == "Parent" && id == 0)
                {
                    Parent parent = _context.Parent.FirstOrDefault(x => x.UserId == user.Id);
                    if (parent != null)
                    {
                        ViewBag.Id = parent.ParentId;
                        ViewBag.FirstName = firstName;
                        ViewBag.LastName = lastName;
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage");
                    }
                }
                else if (type == "Student" && firstName == null && lastName == null)
                {
                    Students student = _context.Students.FirstOrDefault(x => x.StudentId == id);
                    if (student != null)
                    {
                        AspNetUsers stUser = _context.AspNetUsers.FirstOrDefault(x => x.Id == student.UserId);
                        if (stUser != null)
                        {
                            ViewBag.FirstName = stUser.FirstName;
                            ViewBag.LastName = stUser.LastName;
                            ViewBag.Id = id;
                        }
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage");
                    }
                }
                else if (type == "Parent" && firstName == null && lastName == null)
                {
                    Parent parent = _context.Parent.FirstOrDefault(x => x.ParentId == id);
                    if (parent != null)
                    {
                        AspNetUsers paUser = _context.AspNetUsers.FirstOrDefault(x => x.Id == parent.UserId);
                        if (paUser != null)
                        {
                            ViewBag.FirstName = paUser.FirstName;
                            ViewBag.LastName = paUser.LastName;
                            ViewBag.Id = id;
                        }
                        else
                        {
                            return RedirectToAction("ErrorPage");
                        }
                    }
                }
                else if (type == "Teacher" && firstName == null && lastName == null)
                {
                    Teacher teacher = _context.Teacher.FirstOrDefault(x => x.TeacherId == id);
                    if (teacher != null)
                    {
                        AspNetUsers teUser = _context.AspNetUsers.FirstOrDefault(x => x.Id == teacher.UserId);
                        if (teUser != null)
                        {
                            ViewBag.FirstName = teUser.FirstName;
                            ViewBag.LastName = teUser.LastName;
                            ViewBag.Id = id;
                        }
                        else
                        {
                            return RedirectToAction("ErrorPage");
                        }
                    }
                }
               
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            ViewBag.Type = type;
            return View();

        }
        [HttpGet]
        public IActionResult RemoveStudFromTeacher()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RemoveStudFromTeacher(string studentfirst, string studentlast, string teacherfirst, string teacherlast)
        {
            AspNetUsers studentUser = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == studentfirst) && (x.LastName == studentlast));
            if (studentUser != null)
            {
                Students student = _context.Students.FirstOrDefault(x => x.UserId == studentUser.Id);
                if (student != null)
                {
                    AspNetUsers teacherUser = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == teacherfirst) && (x.LastName == teacherlast));
                    if (teacherUser != null)
                    {
                        Teacher teacher = _context.Teacher.FirstOrDefault(x => x.UserId == teacherUser.Id);
                        if (teacher != null)
                        {
                            StudentTeacher st = _context.StudentTeacher.FirstOrDefault(x => (x.StudentId == student.StudentId) && (x.TeacherId == teacher.TeacherId));
                            _context.StudentTeacher.Remove(st);
                            _context.SaveChanges();

                            TempData["Message"] = $"The student, {studentfirst} {studentlast}, has been removed from {teacherfirst} {teacherlast}.";
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            return RedirectToAction("Index");
        }

    public IActionResult GetTeachStudList()
        {
            return View();
        }
        public IActionResult StudentTeacherList(string firstName, string lastName)
        {
            List<AspNetUsers> stList = new List<AspNetUsers>();
            AspNetUsers teacherUser = _context.AspNetUsers.FirstOrDefault(x => (x.FirstName == firstName) && (x.LastName == lastName));
           if (teacherUser != null)
            {
                Teacher teacher = _context.Teacher.FirstOrDefault(x => x.UserId == teacherUser.Id);
                if(teacher != null)
                {
                    List<StudentTeacher> stIdList = _context.StudentTeacher.Where(x => x.TeacherId == teacher.TeacherId).ToList();
                    foreach(StudentTeacher st in stIdList)
                    {
                        Students stud = _context.Students.FirstOrDefault(x => x.StudentId == st.StudentId);
                        AspNetUsers user = _context.AspNetUsers.FirstOrDefault(x => x.Id == stud.UserId);
                        stList.Add(user);

                    }
                }
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
           else
            {
                return RedirectToAction("ErrorPage");
            }

            return View(stList);
        }



        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
