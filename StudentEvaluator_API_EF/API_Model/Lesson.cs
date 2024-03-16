namespace Client_Model
{
    public class Lesson
    {
        private readonly long _id;
        public long Id { get => _id;  }
        public DateTime Start { get; }
        public DateTime End { get;  }
        public string CourseName { get; } = "";
        public string Classroom { get;  } = "";
        public Teacher Teacher { get;  } = new();
        public Group Group { get; } = new();

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
            string lesson = "Lesson : " + Id + ", " + CourseName + ","+ Classroom + ", "+Start+"-"+End+"\n";
            lesson += "\tTeacher : " + Teacher.Username + "\n";
            lesson += "\tGroup  :" + Group.GroupYear + "A G" + Group.GroupNumber + "\n";
            return lesson;
        }
    }
}
