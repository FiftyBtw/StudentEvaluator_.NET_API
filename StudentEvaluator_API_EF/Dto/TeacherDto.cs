using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class TeacherDto : UserDto
    {
        public IEnumerable<TemplateDto> Templates { get; set; }
    }
}
