using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    /// <summary>
    /// Data transfer object for evaluation.
    /// </summary>
    public class EvaluationDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string CourseName { get; set; }

        public long Grade { get; set; }
        public string? PairName { get; set; }
        public string TeacherId { get; set; }

        public long TemplateId { get; set; }

        public long StudentId { get; set; }

    }
}
