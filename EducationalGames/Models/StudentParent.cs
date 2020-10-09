using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class StudentParent
    {
        public int StudParentId { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }

        public virtual Parent Parent { get; set; }
        public virtual Students Student { get; set; }
    }
}
