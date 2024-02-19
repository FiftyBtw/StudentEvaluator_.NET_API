namespace EF_Entities;

public class Lesson
{
    public long id { get; set; }
    public DateOnly date { get; set; }
    public TimeOnly start { get; set; }
    public TimeOnly end { get; set; }
    public string courseName { get; set; }
    public string classroom { get; set; }
    
    public long teacherId { get; set; }
    public TeacherEntity teacher { get; set; } // TODO - Voir si Ajout dans TeacherEntity de ICollection<Lesson> Lessons { get; set; } est nécessaire
    
    public int groupYear { get; set; }
    public int groupNumber { get; set; }
    public GroupEntity group { get; set; } // TODO - Voir si Ajout dans GroupEntity de ICollection<Lesson> Lessons { get; set; } est nécessaire
}