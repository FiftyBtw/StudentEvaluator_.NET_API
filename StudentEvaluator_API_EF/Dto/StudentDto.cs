using System.Text.RegularExpressions;

namespace API_Dto
{
    /// <summary>
    /// Data transfer object for students.
    /// </summary>
    public class StudentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string UrlPhoto { get; set; }
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }

        public override string ToString()
        {
            return "Student : " + Id + ", " + Name + " " + Lastname + ", " + GroupYear + "A G" + GroupNumber + ".";
        }
    }

}
