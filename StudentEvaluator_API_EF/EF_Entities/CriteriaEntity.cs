namespace EF_Entities;

/// <summary>
/// Represents a criteria entity in the database.
/// </summary>
public abstract class  CriteriaEntity
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public long ValueEvaluation { get; set; }
    
    public long TemplateId { get; set; }
    public TemplateEntity Template { get; set; }
}