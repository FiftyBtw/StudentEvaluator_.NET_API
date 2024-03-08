namespace Shared;

/// <summary>
/// Represents a service interface for template-related operations.
/// </summary>
/// <typeparam name="TTemplate">The type representing a template.</typeparam>
public interface ITemplateService<TTemplate, TResponse> where TTemplate : class where TResponse : class
{
    public Task<PageReponse<TResponse>> GetTemplatesByUserId(long userId, int index, int count);
    public Task<PageReponse<TResponse>> GetEmptyTemplatesByUserId(long userId, int index, int count);
    public Task<TResponse?> GetTemplateById(long userId, long templateId);
    public Task<TResponse?> PostTemplate(long userId, TTemplate template);
    public Task<TResponse?> PutTemplate(long templateId, TTemplate template);
    public Task<bool> DeleteTemplate(long templateId);

}