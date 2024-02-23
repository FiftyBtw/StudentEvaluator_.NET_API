using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;

public class TeacherEntity : UserEntity
{
    public IEnumerable<TemplateEntity> Templates { get; set; }
    
    public ICollection<EvaluationEntity> Evaluations { get; set; }
}