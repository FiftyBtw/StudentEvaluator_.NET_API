using API_Dto;
using Client_Model;

namespace Dto2Model
{
    public static class CriteriaModelConverter
    {
        private static readonly Dictionary<Type, Func<Criteria, CriteriaDto>> _modelToDtoConverters;
        private static readonly Dictionary<Type, Func<CriteriaDto, Criteria>> _dtoToModelConverters;


        /// <summary>
        /// Static constructor to initialize converters dictionaries.
        /// </summary>
        static CriteriaModelConverter()
        {
            _modelToDtoConverters = new Dictionary<Type, Func<Criteria, CriteriaDto>>
        {
            { typeof(SliderCriteriaDto), model => ConvertSliderToDto(model as SliderCriteria) },
            { typeof(RadioCriteriaDto), model => ConvertRadioToDto(model as RadioCriteria) },
            { typeof(TextCriteriaDto), model => ConvertTextToDto(model as TextCriteria) },
        };

            _dtoToModelConverters = new Dictionary<Type, Func<CriteriaDto,Criteria >>
        {
            { typeof(SliderCriteriaDto), dto => ConvertSliderToModel(dto as SliderCriteriaDto) },
            { typeof(RadioCriteriaDto), dto => ConvertRadioToModel(dto as RadioCriteriaDto) },
            { typeof(TextCriteriaDto), dto => ConvertTextToModel(dto as TextCriteriaDto) },
        };
        }


        /// <summary>
        /// Converts a criteria entity object to its corresponding DTO.
        /// </summary>
        /// <param name="entity">Criteria model object.</param>
        /// <returns>Corresponding DTO object.</returns>
        public static CriteriaDto ConvertToDto(Criteria model)
        {
            if (model == null) return null;

            var entityType = model.GetType();
            if (_modelToDtoConverters.TryGetValue(entityType, out var converter))
            {
                return converter(model);
            }

            throw new ArgumentException($"No converter available for {entityType.Name}");
        }


        /// <summary>
        /// Converts a criteria DTO object to its corresponding model.
        /// </summary>
        /// <param name="dto">Criteria DTO object.</param>
        /// <returns>Corresponding model object.</returns>
        public static Criteria ConvertToModel(CriteriaDto dto)
        {
            if (dto == null) return null;

            var dtoType = dto.GetType();
            if (_dtoToModelConverters.TryGetValue(dtoType, out var converter))
            {
                return converter(dto);
            }

            throw new ArgumentException($"No converter available for {dtoType.Name}");
        }


        /// <summary>
        /// Converts a SliderCriteria object to its corresponding SliderCriteriaDto.
        /// </summary>
        /// <param name="entity">The SliderCriteria object to convert.</param>
        /// <returns>The corresponding SliderCriteriaDto.</returns>
        private static SliderCriteriaDto ConvertSliderToDto(SliderCriteria model)
        {
            return new SliderCriteriaDto
            {
                Id = model.Id,
                Name = model.Name,
                ValueEvaluation = model.ValueEvaluation,
                TemplateId = model.TemplateId,
                Value = model.Value
            };
        }


        /// <summary>
        /// Converts a RadioCriteria object to its corresponding RadioCriteriaDto.
        /// </summary>
        /// <param name="entity">The RadioCriteria object to convert.</param>
        /// <returns>The corresponding RadioCriteriaDto.</returns>
        private static RadioCriteriaDto ConvertRadioToDto(RadioCriteria model)
        {
            return new RadioCriteriaDto
            {
                Id = model.Id,
                Name = model.Name,
                ValueEvaluation = model.ValueEvaluation,
                TemplateId = model.TemplateId,
                Options = model.Options,
                SelectedOption = model.SelectedOption
            };
        }


        /// <summary>
        /// Converts a TextCriteriaEntity object to its corresponding TextCriteriaDto.
        /// </summary>
        /// <param name="entity">The TextCriteriaEntity object to convert.</param>
        /// <returns>The corresponding TextCriteriaDto.</returns>
        private static TextCriteriaDto ConvertTextToDto(TextCriteria model)
        {
            return new TextCriteriaDto
            {
                Id = model.Id,
                Name = model.Name,
                ValueEvaluation = model.ValueEvaluation,
                TemplateId = model.TemplateId,
                Text = model.Text,
            };
        }


        /// <summary>
        /// Converts a SliderCriteriaDto object to its corresponding SliderCriteria .
        /// </summary>
        /// <param name="dto">The SliderCriteriaDto object to convert.</param>
        /// <returns>The corresponding SliderCriteria.</returns>
        private static SliderCriteria ConvertSliderToModel(SliderCriteriaDto dto)
        {
            return new SliderCriteria(dto.Id, dto.Name, dto.ValueEvaluation, dto.TemplateId,dto.Value);
           
        }


        /// <summary>
        /// Converts a RadioCriteriaDto object to its corresponding RadioCriteria.
        /// </summary>
        /// <param name="dto">The RadioCriteriaDto object to convert.</param>
        /// <returns>The corresponding RadioCriteria.</returns>
        private static RadioCriteria ConvertRadioToModel(RadioCriteriaDto dto)
        {
            return new RadioCriteria(dto.Id, dto.Name, dto.ValueEvaluation, dto.TemplateId, dto.SelectedOption, dto.Options);
          
        }


        /// <summary>
        /// Converts a TextCriteriaDto object to its corresponding TextCriteria.
        /// </summary>
        /// <param name="dto">The TextCriteriaDto object to convert.</param>
        /// <returns>The corresponding TextCriteria.</returns>
        private static TextCriteria ConvertTextToModel(TextCriteriaDto dto)
        {
            return new TextCriteria(dto.Id, dto.Name, dto.ValueEvaluation,dto.TemplateId, dto.Text);
          
        }
    }

}
