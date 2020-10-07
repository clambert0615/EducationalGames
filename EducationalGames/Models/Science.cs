using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class Science
    {
        public int ScienceId { get; set; }
        public string UserId { get; set; }
        public int? Correct { get; set; }
        public int? Incorrect { get; set; }
        public string Type { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
