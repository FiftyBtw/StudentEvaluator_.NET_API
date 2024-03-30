using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EF_Entities;


/// <summary>
/// Represents a teacher entity, inheriting from the base UserEntity class.
/// </summary>
public class TeacherEntity : IdentityUser
{
    public ICollection<TemplateEntity> Templates { get; set; }
    
    public ICollection<EvaluationEntity> Evaluations { get; set; }

    public ICollection<LessonEntity> Lessons { get; set; } = new List<LessonEntity>();
}