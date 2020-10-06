using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            Parent = new HashSet<Parent>();
            Students = new HashSet<Students>();
            Teacher = new HashSet<Teacher>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<Parent> Parent { get; set; }
        public virtual ICollection<Students> Students { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
