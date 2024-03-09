using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;


/// <summary>
/// Represents a slider criteria entity, inheriting from the base CriteriaEntity class.
/// </summary>
public class SliderCriteriaEntity : CriteriaEntity
{
    public long Value { get; set; }
}