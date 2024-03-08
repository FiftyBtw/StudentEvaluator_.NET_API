using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class LessonDto
   {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string CourseName { get; set; }
        public string Classroom { get; set; }
        public long TeacherId { get; set; } 
        public int GroupNumber { get; set; }
        public int GroupYear { get; set; }
   }
}
