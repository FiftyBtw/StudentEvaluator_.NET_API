namespace API_Dto;

/// <summary>
/// Data transfer object for radio button criteria.
/// </summary>
public class RadioCriteriaDto : CriteriaDto
{
    public string SelectedOption { get; set; }
    public string[] Options { get; set; }
}