using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class Parent
    {
        public Parent()
        {
            StudentParent = new HashSet<StudentParent>();
        }

        public int ParentId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? StudentId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<StudentParent> StudentParent { get; set; }
    }
}
