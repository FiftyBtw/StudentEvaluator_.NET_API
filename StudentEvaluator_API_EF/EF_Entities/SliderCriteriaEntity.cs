using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;

[Table("SliderCriteria")]
public class SliderCriteriaEntity : CriteriaEntity
{
    public long Value { get; set; }
}