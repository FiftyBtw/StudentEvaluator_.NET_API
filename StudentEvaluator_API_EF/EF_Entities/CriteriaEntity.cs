namespace EF_Entities;

public class CriteriaEntity
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public long ValueEvaluation { get; set; }
    
    public long TemplateId { get; set; }
    public TemplateEntity Template { get; set; }
}