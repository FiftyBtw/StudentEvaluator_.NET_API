namespace API_Dto;

public interface ITemplateService
{
    public Task<PageReponseDto<TemplateDto>> GetTemplatesByUserId(long userId, int index, int count);
    public Task<PageReponseDto<TemplateDto>> GetEmptyTemplatesByUserId(long userId, int index, int count);
    public Task<TemplateDto?> GetTemplateById(long userId, long templateId);
    public Task<TemplateDto?> PostTemplate(long userId, TemplateDto template);
    public Task<TemplateDto?> PutTemplate(long userId, long templateId, TemplateDto template);
    public Task<bool> DeleteTemplate(long userId, long templateId);
}