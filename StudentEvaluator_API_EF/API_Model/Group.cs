using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API_Model
{
    /// <summary>
    /// Represents a group of students.
    /// </summary>
    public class Group
    {
        private readonly int _groupeyear;
        public int GroupYear { get { return _groupeyear; } }
        private readonly int _groupenumber;
        public int GroupNumber { get { return _groupenumber; } }

        /// <summary>
        /// Gets the collection of students in the group.
        /// </summary>
        public IReadOnlyCollection<Student> Students 
        { 
            get;
            private set;
        } 
        List<Student> _students =new List<Student>();
            

        public Group() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class with the specified year and number.
        /// </summary>
        /// <param name="groupYear">The year of the group.</param>
        /// <param name="groupNumber">The number of the group.</param>
        public Group(int groupYear, int groupNumber)
        {
            _groupeyear = groupYear;
            _groupenumber = groupNumber;
            Students = new ReadOnlyCollection<Student>(_students);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class with the specified year, number, and students.
        /// </summary>
        /// <param name="groupYear">The year of the group.</param>
        /// <param name="groupNumber">The number of the group.</param>
        /// <param name="students">The students in the group.</param>
        public Group(int groupYear, int groupNumber, IEnumerable<Student> students)
        {
            _groupeyear = groupYear;
            _groupenumber = groupNumber;
            Students = new ReadOnlyCollection<Student>(_students);
            _students.AddRange(students);
        }

        /// <summary>
        /// Returns a string that represents the current group.
        /// </summary>
        /// <returns>A string that represents the current group.</returns>
        public override string ToString()
        {
            string group = "Group : " + GroupYear + "A G" +GroupNumber +" :\n";
            foreach(var Student in _students)
            {
                group += Student.ToString()+"\n";
            }
            return group;
        }

    }
    
}