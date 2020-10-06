using System;
using System.Collections.Generic;

namespace EducationalGames.Models
{
    public partial class Math
    {
        public int GameId { get; set; }
        public int? StudId { get; set; }
        public int? GameLevel { get; set; }
        public string UserId { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public string Type { get; set; }

        public virtual Students Stud { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
