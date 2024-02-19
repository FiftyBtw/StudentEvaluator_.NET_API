namespace EF_Entities;

public class EvaluationEntity
{
    public long Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public string CourseName { get; set; }
    
    public long Grade { get; set; }
    
    public long TeacherId { get; set; }
    public TeacherEntity Teacher { get; set; }
    
    public long TemplateId { get; set; }
    public TemplateEntity Template { get; set; } // TODO - Voir si Ajout dans TemplateEntity de ICollection<Evaluation> Evaluations { get; set; } est nécessaire
    
    public long StudentId { get; set; }
    public StudentEntity Student { get; set; } // TODO - Voir si Ajout dans StudentEntity de ICollection<Evaluation> Evaluations { get; set; } est nécessaire
}