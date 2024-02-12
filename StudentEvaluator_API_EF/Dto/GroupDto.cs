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

        public GroupDto() { }
        public GroupDto(int groupYear, int groupNumber)
        {
            GroupYear = groupYear;
            GroupNumber = groupNumber;
        }
    }

}
