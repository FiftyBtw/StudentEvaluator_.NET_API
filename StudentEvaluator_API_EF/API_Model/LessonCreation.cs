using API_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Model
{
    public class LessonCreation
    {
        private readonly long _id;
        public long Id { get { return _id; } }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string CourseName { get; set; }
        public string Classroom { get; set; }
        public long TeacherId { get; set; }
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }

        //public IEnumerable<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();

        public LessonCreation() { }

        public LessonCreation(long id, DateTime start, DateTime end, string coursename, string classroom, long teacherid, int gyear, int gnumber)
        {
            _id = id;
            Start = start;
            End = end;
            CourseName = coursename;
            Classroom = classroom;
            TeacherId = teacherid;
            GroupYear = gyear;
            GroupNumber = gnumber;
        }
    }
}
