using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;


/// <summary>
/// Represents a text criteria entity, inheriting from the base CriteriaEntity class.
/// </summary>
public class TextCriteriaEntity : CriteriaEntity
{
    public string Text { get; set; }
}