using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public interface IStudentService
    {
        public Task<PageReponseDto<StudentDto>> GetStudents(int index, int countl);
        public Task<StudentDto?> GetStudentById(long id);
        public Task<StudentDto?> PostStudent(StudentDto book);

        public Task<StudentDto?> PutStudent(long id, StudentDto book);

        public Task<bool> DeleteStudent(long id);
    }

}
