using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class EvaluationDto
    {
        public long Id { get; set; }

        public DateOnly Date { get; set; }

        public string CourseName { get; set; }

        public long Grade { get; set; }
        public string? PairName { get; set; }
        public TeacherDto Teacher { get; set; }

        public TemplateDto Template { get; set; } // TODO - Voir si Ajout dans TemplateEntity de ICollection<Evaluation> Evaluations { get; set; } est nécessaire

        public StudentDto Student { get; set; } // TODO - Voir si Ajout dans StudentEntity de ICollection<Evaluation> Evaluations { get; set; } est nécessaire

    }
}
