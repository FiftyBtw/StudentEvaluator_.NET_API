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
    public class DbDataManager : IStudentService , IGroupService
    {
        private readonly LibraryContext _libraryContext;

        public DbDataManager(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public Task<bool> DeleteGroup(long id)
        {
            throw new NotImplementedException();
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

        public Task<GroupDto?> GetGroupByIds(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PageReponseDto<GroupDto>> GetGroups(int index, int count)
        {
            var groups = _libraryContext.GroupSet.ToDtos();

            return await Task.FromResult(new PageReponseDto<GroupDto>(groups.Count(), groups.Skip(index * count).Take(count)));
        }

        public async Task<StudentDto?> GetStudentById(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            return await Task.FromResult(student);
        }

        public async Task<PageReponseDto<StudentDto>> GetStudents(int? index = null, int? count = null)
        {
            var students = _libraryContext.StudentSet.ToDtos();

            if (index != null && count != null)
            {
                return new PageReponseDto<StudentDto>(students.Count(), students.Skip((int)index).Take((int)count));
            }
            else
            {
                return new PageReponseDto<StudentDto>(students.Count(), students);
            }
        }

        public Task<GroupDto?> PostGroup(GroupDto book)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto?> PostStudent(StudentDto student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(student);

        }

        public Task<GroupDto?> Putgroup(long id, GroupDto book)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto?> PutStudent(long id, StudentDto student)
        {
            var oldStudent = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);
            if(oldStudent == null) return Task.FromResult<StudentDto?>(null);
            oldStudent.Name=student.Name;
            oldStudent.Lastname=student.Lastname;
            oldStudent.UrlPhoto=student.UrlPhoto;
            oldStudent.GroupNumber = student.Group.GroupNumber;
            oldStudent.GroupYear = student.Group.GroupYear;
            var nbModif = _libraryContext.SaveChanges();
            return Task.FromResult(student);

        }
    }   
}
