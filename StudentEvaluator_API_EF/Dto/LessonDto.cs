using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    /// <summary>
    /// Data transfer object for lesson.
    /// </summary>
    public class LessonDto
   {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string CourseName { get; set; }
        public string Classroom { get; set; }
        public string TeacherId { get; set; } 
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }

   }
}
