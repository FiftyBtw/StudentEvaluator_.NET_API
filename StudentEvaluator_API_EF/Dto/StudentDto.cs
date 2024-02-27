using System.Text.RegularExpressions;

namespace API_Dto
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string UrlPhoto { get; set; }

        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }
    }

}
