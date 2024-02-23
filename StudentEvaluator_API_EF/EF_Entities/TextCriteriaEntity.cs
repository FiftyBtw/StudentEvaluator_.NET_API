using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Entities;

public class TextCriteriaEntity : CriteriaEntity
{
    public string Text { get; set; }
}