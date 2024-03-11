using Client_Model;
using EF_Entities;

namespace Model2Entities;

public static class CriteriaEntityConverter
{
    private static readonly Dictionary<Type, Func<Criteria, CriteriaEntity>> _modelToEntityConverters;
    private static readonly Dictionary<Type, Func<CriteriaEntity, Criteria>> _entityToModelConverters;
    
    static CriteriaEntityConverter()
    {
        _modelToEntityConverters = new Dictionary<Type, Func<Criteria, CriteriaEntity>>
        {
            { typeof(SliderCriteria), model => ConvertSliderToEntity(model as SliderCriteria) },
            { typeof(RadioCriteria), model => ConvertRadioToEntity(model as RadioCriteria) },
            { typeof(TextCriteria), model => ConvertTextToEntity(model as TextCriteria) },
        };
        
        _entityToModelConverters = new Dictionary<Type, Func<CriteriaEntity, Criteria>>
        {
            { typeof(SliderCriteriaEntity), entity => ConvertSliderToModel(entity as SliderCriteriaEntity) },
            { typeof(RadioCriteriaEntity), entity => ConvertRadioToModel(entity as RadioCriteriaEntity) },
            { typeof(TextCriteriaEntity), entity => ConvertTextToModel(entity as TextCriteriaEntity) },
        };
    }
    
    public static CriteriaEntity ConvertToEntity(Criteria model)
    {
        if (model == null) return null;
        
        var modelType = model.GetType();
        if (_modelToEntityConverters.TryGetValue(modelType, out var converter))
        {
            return converter(model);
        }
        
        throw new ArgumentException($"No converter available for {modelType.Name}");
    }
    
    public static Criteria ConvertToModel(CriteriaEntity entity)
    {
        if (entity == null) return null;
        
        var entityType = entity.GetType();
        if (_entityToModelConverters.TryGetValue(entityType, out var converter))
        {
            return converter(entity);
        }
        
        throw new ArgumentException($"No converter available for {entityType.Name}");
    }
    
    private static SliderCriteriaEntity ConvertSliderToEntity(SliderCriteria model)
    {
        return new SliderCriteriaEntity
        {
            Id = model.Id,
            Name = model.Name,
            ValueEvaluation = model.ValueEvaluation,
            TemplateId = model.TemplateId,
            Value = model.Value
        };
    }
    
    private static RadioCriteriaEntity ConvertRadioToEntity(RadioCriteria model)
    {
        return new RadioCriteriaEntity
        {
            Id = model.Id,
            Name = model.Name,
            ValueEvaluation = model.ValueEvaluation,
            TemplateId = model.TemplateId,
            Options = model.Options,
            SelectedOption = model.SelectedOption
        };
    }
    
    private static TextCriteriaEntity ConvertTextToEntity(TextCriteria model)
    {
        return new TextCriteriaEntity
        {
            Id = model.Id,
            Name = model.Name,
            ValueEvaluation = model.ValueEvaluation,
            TemplateId = model.TemplateId,
            Text = model.Text
        };
    }
    
    private static SliderCriteria ConvertSliderToModel(SliderCriteriaEntity entity)
    {
        return new SliderCriteria(entity.Id, entity.Name, entity.ValueEvaluation, entity.TemplateId, entity.Value);
    }
    
    private static RadioCriteria ConvertRadioToModel(RadioCriteriaEntity entity)
    {
        return new RadioCriteria(entity.Id, entity.Name, entity.ValueEvaluation, entity.TemplateId, entity.SelectedOption, entity.Options);
    }
    
    private static TextCriteria ConvertTextToModel(TextCriteriaEntity entity)
    {
        return new TextCriteria(entity.Id, entity.Name, entity.ValueEvaluation, entity.TemplateId, entity.Text);
    }
    
}