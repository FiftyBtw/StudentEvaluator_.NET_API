using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class CriteriaDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ValueEvaluation { get; set; }

        public long TemplateId { get; set; }
    }
}
