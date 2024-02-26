using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class TemplateDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CriteriaDto>? Criteria { get; set; }

        public long TeacherId { get; set; }
    }
}
