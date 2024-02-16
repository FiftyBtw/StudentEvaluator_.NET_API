using System;
namespace EF_Entities
{
    public class StudentEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string UrlPhoto { get; set; }
        
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }

        public GroupEntity Group { get; set; }

    }
}
