using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Entities
{
    /// <summary>
    /// Represents a group entity in the database.
    /// </summary>
    public class GroupEntity
    {
        public int GroupYear { get; set; }
        public int GroupNumber { get; set; }
        
        public ICollection<StudentEntity> Students { get; set; }

        public ICollection<LessonEntity> Lessons { get; set; } = new List<LessonEntity>();
        
    }
}
