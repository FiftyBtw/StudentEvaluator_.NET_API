using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Dto
{
    /// <summary>
    /// Abstract base class for criteria data transfer objects.
    /// </summary>

    [JsonPolymorphic(
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
    [JsonDerivedType(typeof(CriteriaDto), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(SliderCriteriaDto), typeDiscriminator: "slider")]
    [JsonDerivedType(typeof(RadioCriteriaDto), typeDiscriminator: "radio")]
    [JsonDerivedType(typeof(TextCriteriaDto), typeDiscriminator: "text")]
    public abstract class CriteriaDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ValueEvaluation { get; set; }

        public long TemplateId { get; set; }
    }
}
