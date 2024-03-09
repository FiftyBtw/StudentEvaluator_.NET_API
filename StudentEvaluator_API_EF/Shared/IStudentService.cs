using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

    /// <summary>
    /// Represents a service interface for student-related operations.
    /// </summary>
    /// <typeparam name="TStudent">The type representing a student.</typeparam>
    public interface IStudentService<TStudent> where TStudent : class
    {
        public Task<PageReponse<TStudent>> GetStudents(int index, int count);
        public Task<TStudent?> GetStudentById(long id);
        public Task<TStudent?> PostStudent(TStudent student);

        public Task<TStudent?> PutStudent(long id, TStudent student);

        public Task<bool> DeleteStudent(long id);
    }

