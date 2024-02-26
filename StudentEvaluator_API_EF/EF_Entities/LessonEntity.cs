namespace EF_Entities;

public class LessonEntity
{
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
    public string CourseName { get; set; }
    public string Classroom { get; set; }
    
    public long TeacherEntityId { get; set; }
    public TeacherEntity Teacher { get; set; } 
    
    public int GroupYear { get; set; }
    public int GroupNumber { get; set; }
    public GroupEntity Group { get; set; } 
}