namespace EF_Entities;


/// <summary>
/// Represents a lesson entity in the database.
/// </summary>
public class LessonEntity
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string CourseName { get; set; }
    public string Classroom { get; set; }
    
    public string TeacherEntityId { get; set; }
    public TeacherEntity Teacher { get; set; } 
    
    public int GroupYear { get; set; }
    public int GroupNumber { get; set; }
    public GroupEntity Group { get; set; } 
}