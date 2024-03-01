using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Model
{
    public interface IDataManager
    {
        public Task<PageReponseModel<Student>> GetStudents(int index, int count);
        public Task<Student?> GetStudentById(long id);
        public Task<Student?> PostStudent(Student student);
        public Task<Student?> PutStudent(long id, Student student);
        public Task<bool> DeleteStudent(long id);
    }
}
