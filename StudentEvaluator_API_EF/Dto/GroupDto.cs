using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto

{
    public class GroupDto
    {
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }
        public IEnumerable<StudentDto> Students { get; set; } = new List<StudentDto>();
        public GroupDto() { }
        public GroupDto(int groupYear, int groupNumber, IEnumerable<StudentDto> students)
        {
            GroupYear = groupYear;
            GroupNumber = groupNumber;
            Students = students;
        }
    }

}
