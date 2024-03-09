using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;


/// <summary>
/// Represents a radio criteria entity, inheriting from the base CriteriaEntity class.
/// </summary>
public class RadioCriteriaEntity : CriteriaEntity
{
    public string SelectedOption { get; set; } 
    
    public string[] Options { get; set; }
}