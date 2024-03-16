namespace Client_Model
{
    public class Evaluation
    {
        private readonly long _id;
        public long Id { get { return _id; } }
        public DateTime Date { get; set; }
        public string CourseName { get; set; }

        public long Grade { get; set; }
        public string? PairName { get; set; }
        public Teacher Teacher { get; set; }

        public Template Template { get; set; }

        public Student Student { get; set; }

        public Evaluation(long id, DateTime date, string courseName, long grade, string? pairName, Teacher teacher, Template? template, Student student)
        {
            _id = id;
            Date = date;
            CourseName = courseName;
            Grade = grade;
            PairName = pairName;
            Teacher = teacher;
            Template = template;
            Student = student;
        }

        public override string ToString()
        {
            string eval = "Evaluation : " + Id + ", " + CourseName + ", " + Date + ", " + Grade + ", " + PairName+"\n";
            eval += "\tTeacher : " + Teacher.Username + "\n";
            eval += "\tStudent : " + Student.Name + " " + Student.Lastname + "\n";
            eval += "\tTemplate : " + Template?.Name + "\n";

            return eval;
        }
    }
}
