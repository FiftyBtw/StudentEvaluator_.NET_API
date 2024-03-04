using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class LessonDto
   {
        public long Id { get; set; }
        //public DateOnly Date { get; set; }
        //public TimeOnly Start { get; set; }
        public DateTime Start { get; set; }
        //public TimeOnly End { get; set; }
        public DateTime End { get; set; }
        public string CourseName { get; set; }
        public string Classroom { get; set; }
        public TeacherDto Teacher { get; set; } 
        public GroupDto Group { get; set; }
   }
}
