namespace Client_Model
{
    public class EvaluationCreation(
        DateTime date,
        string courseName,
        long grade,
        string? pairName,
        string teacherId,
        long templateId,
        long studentId)
    {
        public DateTime Date { get; } = date;

        public string CourseName { get; set; } = courseName;

        public long Grade { get; } = grade;
        public string? PairName { get;  } = pairName;
        public string TeacherId { get;  } = teacherId;

        public long TemplateId { get;  } = templateId;

        public long StudentId { get;  } = studentId;
    }
}
