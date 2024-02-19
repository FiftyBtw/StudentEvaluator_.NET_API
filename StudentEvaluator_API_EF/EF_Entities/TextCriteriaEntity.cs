using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;

[Table("TextCriteria")]
public class TextCriteriaEntity : CriteriaEntity
{
    public string content { get; set; }
}