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
    public class TeacherDto 
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public IEnumerable<TemplateDto> Templates { get; set; }
    }
}
