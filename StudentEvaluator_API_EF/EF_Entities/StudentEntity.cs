using System;
namespace EF_Entities
{

    /// <summary>
    /// Represents a student entity in the database.
    /// </summary>
    public class StudentEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string UrlPhoto { get; set; }
        
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }

        public GroupEntity Group { get; set; }
        
        public ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();

    }
}
