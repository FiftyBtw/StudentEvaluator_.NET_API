using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;

[Table("Teacher")]
public class TeacherEntity : UserEntity
{
    public ICollection<TemplateEntity> Templates { get; set; }
}