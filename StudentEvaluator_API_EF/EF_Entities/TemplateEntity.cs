namespace EF_Entities;

public class TemplateEntity
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public ICollection<CriteriaEntity>? Criteria { get; set; }
    
    public long? TeacherId { get; set; }
    public TeacherEntity? Teacher { get; set; }
}