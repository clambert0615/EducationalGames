using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class StudentTeacher
    {
        public int StudTeachId { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }

        public virtual Students Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
