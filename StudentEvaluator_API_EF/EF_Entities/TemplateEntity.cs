namespace EF_Entities;


/// <summary>
/// Represents a template entity in the database.
/// </summary>
public class TemplateEntity
{
    public long Id { get; set; }

    public string Name { get; set; }

    public ICollection<CriteriaEntity>? Criteria { get; set; }

    public string TeacherId { get; set; }
    public TeacherEntity? Teacher { get; set; }
    
    public long? EvaluationId { get; set; }
    public EvaluationEntity? Evaluation { get; set; }
}