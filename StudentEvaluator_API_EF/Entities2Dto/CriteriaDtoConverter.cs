using API_Dto;
using EF_Entities;

namespace Entities2Dto;

public class CriteriaDtoConverter
{
    private readonly Dictionary<Type, Func<CriteriaEntity, CriteriaDto>> _entityToDtoConverters;
    private readonly Dictionary<Type, Func<CriteriaDto, CriteriaEntity>> _dtoToEntityConverters;

    public CriteriaDtoConverter()
    {
        _entityToDtoConverters = new Dictionary<Type, Func<CriteriaEntity, CriteriaDto>>
        {
            { typeof(SliderCriteriaEntity), entity => ConvertSliderToDto(entity as SliderCriteriaEntity) },
            { typeof(RadioCriteriaEntity), entity => ConvertRadioToDto(entity as RadioCriteriaEntity) },
            { typeof(TextCriteriaEntity), entity => ConvertTextToDto(entity as TextCriteriaEntity) },
        };

        _dtoToEntityConverters = new Dictionary<Type, Func<CriteriaDto, CriteriaEntity>>
        {
            { typeof(SliderCriteriaDto), dto => ConvertSliderToEntity(dto as SliderCriteriaDto) },
            { typeof(RadioCriteriaDto), dto => ConvertRadioToEntity(dto as RadioCriteriaDto) },
            { typeof(TextCriteriaDto), dto => ConvertTextToEntity(dto as TextCriteriaDto) },
        };
    }
    
    public CriteriaDto ConvertToDto(CriteriaEntity entity)
    {
        if (entity == null) return null;

        var entityType = entity.GetType();
        if (_entityToDtoConverters.TryGetValue(entityType, out var converter))
        {
            return converter(entity);
        }

        throw new ArgumentException($"No converter available for {entityType.Name}");
    }

    public CriteriaEntity ConvertToEntity(CriteriaDto dto)
    {
        if (dto == null) return null;

        var dtoType = dto.GetType();
        if (_dtoToEntityConverters.TryGetValue(dtoType, out var converter))
        {
            return converter(dto);
        }

        throw new ArgumentException($"No converter available for {dtoType.Name}");
    }

    private SliderCriteriaDto ConvertSliderToDto(SliderCriteriaEntity entity)
    {
        return new SliderCriteriaDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ValueEvaluation = entity.ValueEvaluation,
            TemplateId = entity.TemplateId,
            Value = entity.Value
        };
    }

    private RadioCriteriaDto ConvertRadioToDto(RadioCriteriaEntity entity)
    {
        return new RadioCriteriaDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ValueEvaluation = entity.ValueEvaluation,
            TemplateId = entity.TemplateId,
            Options = entity.Options,
            SelectedOption = entity.SelectedOption
        };
    }

    private TextCriteriaDto ConvertTextToDto(TextCriteriaEntity entity)
    {
        return new TextCriteriaDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ValueEvaluation = entity.ValueEvaluation,
            TemplateId = entity.TemplateId,
            Text = entity.Text,
        };
    }
    
    private SliderCriteriaEntity ConvertSliderToEntity(SliderCriteriaDto dto)
    {
        return new SliderCriteriaEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            ValueEvaluation = dto.ValueEvaluation,
            TemplateId = dto.TemplateId,
            Value = dto.Value
        };
    }
    
    private RadioCriteriaEntity ConvertRadioToEntity(RadioCriteriaDto dto)
    {
        return new RadioCriteriaEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            ValueEvaluation = dto.ValueEvaluation,
            TemplateId = dto.TemplateId,
            Options = dto.Options,
            SelectedOption = dto.SelectedOption
        };
    }
    
    private TextCriteriaEntity ConvertTextToEntity(TextCriteriaDto dto)
    {
        return new TextCriteriaEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            ValueEvaluation = dto.ValueEvaluation,
            TemplateId = dto.TemplateId,
            Text = dto.Text,
        };
    }
}
    
    