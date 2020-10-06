using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class Parent
    {
        public int ParentId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? StudentId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
