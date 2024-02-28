namespace API_Dto;

public interface ICriteriaService
{
    public Task<PageReponseDto<TextCriteriaDto>> GetTextCriterions(int index, int count);
    public Task<TextCriteriaDto> GetTextCriterionByIds(long id);
    
    public Task<PageReponseDto<SliderCriteriaDto>> GetSliderCriterions(int index, int count);
    public Task<SliderCriteriaDto> GetSliderCriterionByIds(long id);
    
    public Task<PageReponseDto<RadioCriteriaDto>> GetRadioCriterions(int index, int count);
    public Task<RadioCriteriaDto> GetRadioCriterionByIds(long id);
    public Task<PageReponseDto<CriteriaDto>> GetCriterionsByTemplateId(long id);
    
    // Post
    public Task<TextCriteriaDto> PostTextCriterion(long templateId, TextCriteriaDto text);
    public Task<SliderCriteriaDto> PostSliderCriterion(long templateId, SliderCriteriaDto slider);
    public Task<RadioCriteriaDto> PostRadioCriterion(long templateId, RadioCriteriaDto radio);
    
    // Put
    public Task<TextCriteriaDto> PutTextCriterion(long id, TextCriteriaDto text);
    public Task<SliderCriteriaDto> PutSliderCriterion(long id, SliderCriteriaDto slider);
    public Task<RadioCriteriaDto> PutRadioCriterion(long id, RadioCriteriaDto radio);
    
    // Delete
    public Task<bool> DeleteCriteria(long id);
    
    
}