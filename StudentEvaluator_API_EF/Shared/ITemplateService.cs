namespace Shared;

public interface ITemplateService<TTemplate> where TTemplate : class
{
    public Task<PageReponse<TTemplate>> GetTemplatesByUserId(long userId, int index, int count);
    public Task<PageReponse<TTemplate>> GetEmptyTemplatesByUserId(long userId, int index, int count);
    public Task<TTemplate?> GetTemplateById(long userId, long templateId);
    public Task<TTemplate?> PostTemplate(long userId, TTemplate template);
    public Task<TTemplate?> PutTemplate(long templateId, TTemplate template);
    public Task<bool> DeleteTemplate( long templateId);
}