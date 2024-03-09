namespace API_Dto;


/// <summary>
/// Data transfer object for template responses.
/// </summary>
public class TemplateResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }  
    public List<CriteriaDto> Criterias  { get; set; }
}