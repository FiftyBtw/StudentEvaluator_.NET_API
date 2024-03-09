namespace EF_Entities;

/// <summary>
/// Represents an evaluation entity in the database.
/// </summary>
public class EvaluationEntity
{
    public long Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string CourseName { get; set; }
    
    public long Grade { get; set; }

    public string? PairName { get; set; }

    public long TeacherId { get; set; }
    public TeacherEntity Teacher { get; set; }
    
    public long TemplateId { get; set; }
    public TemplateEntity Template { get; set; } 
    
    public long StudentId { get; set; }
    public StudentEntity Student { get; set; } 
}