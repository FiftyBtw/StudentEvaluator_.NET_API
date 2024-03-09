using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    /// <summary>
    /// Data transfer object for teachers.
    /// </summary>
    public class TeacherDto : UserDto
    {
        public IEnumerable<TemplateDto> Templates { get; set; }
    }
}
