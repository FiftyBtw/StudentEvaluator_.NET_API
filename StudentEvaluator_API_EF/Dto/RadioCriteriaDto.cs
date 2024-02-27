namespace API_Dto;

public class RadioCriteriaDto : CriteriaDto
{
    public string SelectedOption { get; set; }
    public string[] Options { get; set; }
}