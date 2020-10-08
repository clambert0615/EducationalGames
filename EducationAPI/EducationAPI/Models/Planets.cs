using System;
using System.Collections.Generic;

namespace EducationAPI.Models
{
    public partial class Planets
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
    }
}
