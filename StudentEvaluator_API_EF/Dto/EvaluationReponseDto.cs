using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class EvaluationReponseDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string CourseName { get; set; }

        public long Grade { get; set; }
        public string? PairName { get; set; }
        public TeacherDto Teacher { get; set; }

        public TemplateDto? Template { get; set; }

        public StudentDto Student { get; set; }
    }
}
