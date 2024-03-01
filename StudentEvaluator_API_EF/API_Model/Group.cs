using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Model
{
    public class Group
    {
        private readonly int _groupeyear;
        public int GroupYear { get { return _groupeyear; } }
        private readonly int _groupenumber;
        public int GroupNumber { get { return _groupenumber; } }
        public IReadOnlyCollection<Student> Students 
        { 
            get;
           private set;
        } 
        List<Student> _students =new List<Student>();
            

        public Group() { }
        public Group(int groupYear, int groupNumber, IEnumerable<Student> students)
        {
            _groupenumber = groupYear;
            _groupenumber = groupNumber;
            Students = new ReadOnlyCollection<Student>(_students);
            _students.AddRange(students);
        }
    }
    }
}
