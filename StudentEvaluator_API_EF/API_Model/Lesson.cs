using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Model
{
    public class Lesson
    {
        private readonly long _id;
        public long Id { get { return _id; } }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string CourseName { get; set; }
        public string Classroom { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }

        //public IEnumerable<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();

        public Lesson() { }

        public Lesson(long id,DateTime start, DateTime end, string coursename,string classroom,Teacher teacher,Group group)
        {
            _id = id;
            Start= start;
            End= end;
            CourseName= coursename;
            Classroom= classroom;
            Teacher= teacher;
            Group= group;
        }

        public override string ToString()
        {
            string lesson = "Lesson : " + Id + ", " + CourseName + ","+ Classroom + ", "+Start.ToString()+"-"+End.ToString()+"\n";
            lesson += "\tTeacher : " + Teacher.Username + "\n";
            lesson += "\tGroup  :" + Group.GroupYear + "A G" + Group.GroupNumber + "\n";
            return lesson;
        }
    }
}
