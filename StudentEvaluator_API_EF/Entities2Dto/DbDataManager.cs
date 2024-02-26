using API_Dto;
using EF_DbContextLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Dto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Entities2Dto
{
    public class DbDataManager : IStudentService , IGroupService, ICriteriaService, ILessonService
    {
        private readonly LibraryContext _libraryContext;

        public DbDataManager(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public Task<bool> DeleteGroup(int gyear,int gnumber)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);
            if(group == null) return Task.FromResult(false);

            _libraryContext.GroupSet.Where(g => g.GroupYear == gyear && g.GroupNumber == gnumber).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);

            if (group == null) return Task.FromResult(true);
         
            else return Task.FromResult(false);
        }

        public Task<bool> DeleteStudent(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);
            if (student == null) return Task.FromResult(false);

            _libraryContext.StudentSet.Where(b => b.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            student = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);

            if (student == null) return Task.FromResult(true);
            else return Task.FromResult(false);
        }

        public async Task<GroupDto?> GetGroupByIds(int gyear,int gnumber)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber== gnumber)?.ToDto();
            return await Task.FromResult(group);
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

        public Task<GroupDto?> PostGroup(GroupDto group)
        {
            _libraryContext.GroupSet.AddAsync(group.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(group);
        }

        public Task<StudentDto?> PostStudent(StudentDto student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(student);

        }

        public Task<GroupDto?> PutGroup(int gyear, int gnumber, GroupDto newGroup)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);
            if (group == null) return Task.FromResult<GroupDto?>(null);
            group.GroupNumber = newGroup.GroupNumber;
            group.GroupYear = newGroup.GroupYear;
            //group.Students = newGroup.Students;
            _libraryContext.SaveChanges();
            return Task.FromResult(newGroup);
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
            _libraryContext.SaveChanges();
            return Task.FromResult(student);

        }
        
        public Task<PageReponseDto<TextCriteriaDto>> GetTextCriterions(int? index, int? count)
        {
            var criterions = _libraryContext.TextCriteriaSet.ToDtos();
            
            if (index != null && count != null)
            {
                return Task.FromResult(new PageReponseDto<TextCriteriaDto>(criterions.Count(), criterions.Skip((int)index).Take((int)count)));
            }

            return Task.FromResult(new PageReponseDto<TextCriteriaDto>(criterions.Count(), criterions));
        }
        
        public Task<TextCriteriaDto?> GetTextCriterionByIds(long id)
        {
            var criterion = _libraryContext.TextCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            return Task.FromResult(criterion);
        }


        //Lessons

        public Task<PageReponseDto<LessonDto>> GetLessons(int index, int count)
        {
            var lessons = _libraryContext.LessonSet.ToDtos();
            return Task.FromResult(new PageReponseDto<LessonDto>(lessons.Count(), lessons.Skip(index * count).Take(count)));
        }

        public Task<LessonDto?> GetLessonById(long id)
        {
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id)?.ToDto();
            return Task.FromResult(lesson);
        }


        public Task<LessonDto?> PutLesson(long id, LessonDto newLesson)
        {
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id==id);
            if (lesson == null) return Task.FromResult<LessonDto?>(null);
            lesson.Id = newLesson.Id;
            lesson.CourseName = newLesson.CourseName;
            lesson.Classroom = newLesson.Classroom;
            lesson.Date = newLesson.Date;
            lesson.Start = newLesson.Start;
            lesson.End = newLesson.End;
            lesson.Teacher = newLesson.Teacher.ToEntity();
            
            _libraryContext.SaveChanges();
            return Task.FromResult(newLesson);
        }

        public Task<bool> DeleteLesson(long id)
        {
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id);
            if (lesson == null) return Task.FromResult(false);

            _libraryContext.LessonSet.Where(l => l.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id);

            if (lesson == null) return Task.FromResult(true);
            else return Task.FromResult(false);
        }

        public Task<LessonDto?> PostLesson(LessonDto lesson)
        {
            _libraryContext.LessonSet.AddAsync(lesson.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(lesson);
        }
    }   
}
