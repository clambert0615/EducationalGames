using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? StudId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual Students Stud { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
