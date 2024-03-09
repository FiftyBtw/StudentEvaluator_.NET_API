using API_Dto;
using EF_Entities;

namespace Entities2Dto;

/// <summary>
/// Static class for converting between entity objects and DTOs for criteria.
/// </summary>
public static class CriteriaDtoConverter
{
    private static readonly Dictionary<Type, Func<CriteriaEntity, CriteriaDto>> _entityToDtoConverters;
    private static readonly Dictionary<Type, Func<CriteriaDto, CriteriaEntity>> _dtoToEntityConverters;


    /// <summary>
    /// Static constructor to initialize converters dictionaries.
    /// </summary>
    static CriteriaDtoConverter()
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


    /// <summary>
    /// Converts a criteria entity object to its corresponding DTO.
    /// </summary>
    /// <param name="entity">Criteria entity object.</param>
    /// <returns>Corresponding DTO object.</returns>
    public static CriteriaDto ConvertToDto(CriteriaEntity entity)
    {
        if (entity == null) return null;

        var entityType = entity.GetType();
        if (_entityToDtoConverters.TryGetValue(entityType, out var converter))
        {
            return converter(entity);
        }

        throw new ArgumentException($"No converter available for {entityType.Name}");
    }


    /// <summary>
    /// Converts a criteria DTO object to its corresponding entity.
    /// </summary>
    /// <param name="dto">Criteria DTO object.</param>
    /// <returns>Corresponding entity object.</returns>
    public static CriteriaEntity ConvertToEntity(CriteriaDto dto)
    {
        if (dto == null) return null;

        var dtoType = dto.GetType();
        if (_dtoToEntityConverters.TryGetValue(dtoType, out var converter))
        {
            return converter(dto);
        }

        throw new ArgumentException($"No converter available for {dtoType.Name}");
    }


    /// <summary>
    /// Converts a SliderCriteriaEntity object to its corresponding SliderCriteriaDto.
    /// </summary>
    /// <param name="entity">The SliderCriteriaEntity object to convert.</param>
    /// <returns>The corresponding SliderCriteriaDto.</returns>
    private static SliderCriteriaDto ConvertSliderToDto(SliderCriteriaEntity entity)
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


    /// <summary>
    /// Converts a RadioCriteriaEntity object to its corresponding RadioCriteriaDto.
    /// </summary>
    /// <param name="entity">The RadioCriteriaEntity object to convert.</param>
    /// <returns>The corresponding RadioCriteriaDto.</returns>
    private static RadioCriteriaDto ConvertRadioToDto(RadioCriteriaEntity entity)
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


    /// <summary>
    /// Converts a TextCriteriaEntity object to its corresponding TextCriteriaDto.
    /// </summary>
    /// <param name="entity">The TextCriteriaEntity object to convert.</param>
    /// <returns>The corresponding TextCriteriaDto.</returns>
    private static TextCriteriaDto ConvertTextToDto(TextCriteriaEntity entity)
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


    /// <summary>
    /// Converts a SliderCriteriaDto object to its corresponding SliderCriteriaEntity.
    /// </summary>
    /// <param name="dto">The SliderCriteriaDto object to convert.</param>
    /// <returns>The corresponding SliderCriteriaEntity.</returns>
    private static SliderCriteriaEntity ConvertSliderToEntity(SliderCriteriaDto dto)
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


    /// <summary>
    /// Converts a RadioCriteriaDto object to its corresponding RadioCriteriaEntity.
    /// </summary>
    /// <param name="dto">The RadioCriteriaDto object to convert.</param>
    /// <returns>The corresponding RadioCriteriaEntity.</returns>
    private static RadioCriteriaEntity ConvertRadioToEntity(RadioCriteriaDto dto)
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


    /// <summary>
    /// Converts a TextCriteriaDto object to its corresponding TextCriteriaEntity.
    /// </summary>
    /// <param name="dto">The TextCriteriaDto object to convert.</param>
    /// <returns>The corresponding TextCriteriaEntity.</returns>
    private static TextCriteriaEntity ConvertTextToEntity(TextCriteriaDto dto)
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
