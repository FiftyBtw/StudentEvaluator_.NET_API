using API_Dto;
using EF_DbContextLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities2Dto
{
    public class DbDataManager : IStudentService
    {
        private readonly LibraryContext _libraryContext;

        public DbDataManager(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }


        public Task<bool> DeleteStudent(long id)
        {
            _libraryContext.StudentSet.Where(b => b.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();
            var student = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);
            if (student == null)
            {
                return Task.FromResult(true);
            }
            else return Task.FromResult(false);

        }

        public async Task<StudentDto?> GetStudentById(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            return await Task.FromResult(student);
        }

        public async Task<PageReponseDto<StudentDto>> GetStudents(int index, int count)
        {
            var students = _libraryContext.StudentSet.ToDtos();

            return await Task.FromResult(new PageReponseDto<StudentDto>(students.Count(),students.Skip(index*count).Take(count)));
        }

        public Task<StudentDto?> PostStudent(StudentDto student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(student);

        }

        public Task<StudentDto?> PutStudent(long id, StudentDto student)
        {
            var oldStudent = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);
            oldStudent = student.ToEntity();
            _libraryContext.SaveChanges();
            return Task.FromResult(student);

        }
    }   
}
