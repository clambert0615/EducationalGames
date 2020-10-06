using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class MathStudents
    {
        public int MathStudId { get; set; }
        public int? Game { get; set; }
        public int? Student { get; set; }

        public virtual Math GameNavigation { get; set; }
        public virtual Students StudentNavigation { get; set; }
    }
}
