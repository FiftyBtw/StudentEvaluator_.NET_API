namespace API_Dto;

public class TemplateResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }  
    public List<CriteriaDto> Criterias  { get; set; }
}