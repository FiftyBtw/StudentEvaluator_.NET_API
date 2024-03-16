namespace Client_Model;

public class LessonCreation
{
    public DateTime Start { get;  }
    public DateTime End { get; }
    public string CourseName { get; } = "";
    public string Classroom { get; set; }  = "";
    public long TeacherId { get;  }
    public int GroupYear { get; }
    public int GroupNumber { get;  }
        
    public LessonCreation() { }

    public LessonCreation( DateTime start, DateTime end, string coursename, string classroom, long teacherid, int gyear, int gnumber)
    {
        Start = start;
        End = end;
        CourseName = coursename;
        Classroom = classroom;
        TeacherId = teacherid;
        GroupYear = gyear;
        GroupNumber = gnumber;
    }
}