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
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public string CourseName { get; set; }
        public string Classroom { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }

        //public IEnumerable<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();

        public Lesson() { }

        public Lesson(long id,DateOnly date ,TimeOnly start, TimeOnly end, string coursename,string classroom,Teacher teacher,Group group)
        {
            _id = id;
            Date= date;
            Start= start;
            End= end;
            CourseName= coursename;
            Classroom= classroom;
            Teacher= teacher;
            Group= group;
        }

    }
}
