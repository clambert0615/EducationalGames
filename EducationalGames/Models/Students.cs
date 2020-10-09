using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class Students
    {
        public Students()
        {
            Math = new HashSet<Math>();
            StudentParent = new HashSet<StudentParent>();
            StudentTeacher = new HashSet<StudentTeacher>();
            Teacher = new HashSet<Teacher>();
        }

        public int StudentId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? ParentId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Math> Math { get; set; }
        public virtual ICollection<StudentParent> StudentParent { get; set; }
        public virtual ICollection<StudentTeacher> StudentTeacher { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
