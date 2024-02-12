using API_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities2Dto
{
    public class DbDataManager : IStudentService
    {
        public Task<bool> DeleteStudent(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDto>> GetStudentById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDto>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> PostStudent(StudentDto book)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto?> PutStudent(long id, StudentDto book)
        {
            throw new NotImplementedException();
        }
    }
}
