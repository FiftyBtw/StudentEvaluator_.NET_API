using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto

{
    /// <summary>
    /// Data transfer object for group.
    /// </summary>
    public class GroupDto
    {
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }
        public IEnumerable<StudentDto> Students { get; set; } = new List<StudentDto>();
        public GroupDto() { }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="groupYear">The group year.</param>
        /// <param name="groupNumber">The group number.</param>
        /// <param name="students">The students belonging to the group.</param>
        public GroupDto(int groupYear, int groupNumber, IEnumerable<StudentDto> students)
        {
            GroupYear = groupYear;
            GroupNumber = groupNumber;
            Students = students;
        }
    }

}
