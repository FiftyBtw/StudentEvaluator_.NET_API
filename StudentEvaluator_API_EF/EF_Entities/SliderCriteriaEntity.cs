using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;

public class SliderCriteriaEntity : CriteriaEntity
{
    public long Value { get; set; }
}