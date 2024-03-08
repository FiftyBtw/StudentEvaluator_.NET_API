namespace Shared;

/// <summary>
/// Represents a service interface for template-related operations.
/// </summary>
/// <typeparam name="TTemplate">The type representing a template.</typeparam>
public interface ITemplateService<TTemplate> where TTemplate : class
{
    public Task<PageReponse<TTemplate>> GetTemplatesByUserId(long userId, int index, int count);
    public Task<PageReponse<TTemplate>> GetEmptyTemplatesByUserId(long userId, int index, int count);
    public Task<TTemplate?> GetTemplateById(long userId, long templateId);
    public Task<TTemplate?> PostTemplate(long userId, TTemplate template);
    public Task<TTemplate?> PutTemplate(long templateId, TTemplate template);
    public Task<bool> DeleteTemplate( long templateId);
}