using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;

public class RadioCriteriaEntity : CriteriaEntity
{
    public string SelectedOption { get; set; } 
    
    public string[] Options { get; set; }
}