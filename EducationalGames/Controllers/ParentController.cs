using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
