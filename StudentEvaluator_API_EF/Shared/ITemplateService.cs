namespace Shared;

/// <summary>
/// Represents a service interface for template-related operations.
/// </summary>
/// <typeparam name="TTemplate">The type representing a template.</typeparam>
public interface ITemplateService<TTemplate> where TTemplate : class
{
    public Task<PageReponse<TTemplate>> GetTemplatesByUserId(string userId, int index = 0, int count = 10);
    public Task<PageReponse<TTemplate>> GetEmptyTemplatesByUserId(string userId, int index = 0, int count = 10);
    public Task<TTemplate?> GetTemplateById(long templateId);
    public Task<TTemplate?> PostTemplate(string userId, TTemplate template);
    public Task<TTemplate?> PutTemplate(long templateId, TTemplate template);
    public Task<bool> DeleteTemplate(long templateId);

}