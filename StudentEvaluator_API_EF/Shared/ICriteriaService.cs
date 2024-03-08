namespace Shared;

public interface ICriteriaService<TCriteria,TText,TSlider,TRadio> where TRadio : class where TText : class where TSlider : class where TCriteria : class
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