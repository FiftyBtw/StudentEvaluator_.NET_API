using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public abstract class CriteriaDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ValueEvaluation { get; set; }

        public TemplateDto? Template { get; set; }
    }
}
