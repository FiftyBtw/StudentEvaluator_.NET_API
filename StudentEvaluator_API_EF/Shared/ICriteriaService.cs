namespace Shared;


/// <summary>
/// Represents a service interface for criteria-related operations.
/// </summary>
/// <typeparam name="TCriteria">The type representing a criterion.</typeparam>
/// <typeparam name="TText">The type representing a text-based criterion.</typeparam>
/// <typeparam name="TSlider">The type representing a slider-based criterion.</typeparam>
/// <typeparam name="TRadio">The type representing a radio-based criterion.</typeparam>
public interface ICriteriaService<TCriteria, TText,TSlider,TRadio> where TCriteria : class  where TRadio : class where TText : class where TSlider : class
{
    public Task<PageReponse<TText>> GetTextCriterions(int index, int count);
    public Task<TText> GetTextCriterionByIds(long id);
    
    public Task<PageReponse<TSlider>> GetSliderCriterions(int index, int count);
    public Task<TSlider> GetSliderCriterionByIds(long id);
    
    public Task<PageReponse<TRadio>> GetRadioCriterions(int index, int count);
    public Task<TRadio> GetRadioCriterionByIds(long id);
    public Task<PageReponse<TCriteria>> GetCriterionsByTemplateId(long id);
    
    // Post
    public Task<TText> PostTextCriterion(long templateId, TText text);
    public Task<TSlider> PostSliderCriterion(long templateId, TSlider slider);
    public Task<TRadio> PostRadioCriterion(long templateId, TRadio radio);
    
    // Put
    public Task<TText> PutTextCriterion(long id, TText text);
    public Task<TSlider> PutSliderCriterion(long id, TSlider slider);
    public Task<TRadio> PutRadioCriterion(long id, TRadio radio);
    
    // Delete
    public Task<bool> DeleteCriteria(long id);
    
    
}