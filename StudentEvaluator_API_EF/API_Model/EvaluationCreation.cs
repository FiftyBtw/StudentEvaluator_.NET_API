using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Model
{
    public class EvaluationCreation
    {
        public DateTime Date { get; set; }

        public string CourseName { get; set; }

        public long Grade { get; set; }
        public string? PairName { get; set; }
        public long TeacherId { get; set; }

        public long TemplateId { get; set; }

        public long StudentId { get; set; }

        public EvaluationCreation(DateTime date, string courseName, long grade, string? pairName, long teacherId, long templateId, long studentId)
        {
            Date = date;
            CourseName = courseName;
            Grade = grade;
            PairName = pairName;
            TeacherId = teacherId;
            TemplateId = templateId;
            StudentId = studentId;
        }
    }
}
