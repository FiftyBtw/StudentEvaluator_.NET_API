using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public interface IStudentService
    {
        public Task<PageReponseDto<StudentDto>> GetStudents(int index, int count);
        public Task<StudentDto?> GetStudentById(long id);
        public Task<StudentDto?> PostStudent(StudentDto student);

        public Task<StudentDto?> PutStudent(long id, StudentDto student);

        public Task<bool> DeleteStudent(long id);
    }

}
