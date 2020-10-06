using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class Students
    {
        public Students()
        {
            Math = new HashSet<Math>();
            Teacher = new HashSet<Teacher>();
        }

        public int StudentId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? ParentId { get; set; }
        public int? MathLevel { get; set; }
        public int? MathWins { get; set; }
        public int? MathLosses { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Math> Math { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
