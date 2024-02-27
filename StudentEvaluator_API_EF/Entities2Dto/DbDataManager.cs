using API_Dto;
using EF_DbContextLib;
using EF_StubbedContextLib;
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
    public class DbDataManager : IStudentService, IGroupService, ICriteriaService, IUserService, ITemplateService, ILessonService, IEvaluationService
    {
        private readonly LibraryContext _libraryContext;

        public DbDataManager(StubbedContext context)
        {
            _libraryContext = context;
        }

        public Task<bool> DeleteGroup(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);
            if (group == null) return Task.FromResult(false);

            _libraryContext.GroupSet.Where(g => g.GroupYear == gyear && g.GroupNumber == gnumber).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);

            if (group == null) return Task.FromResult(true);

            return Task.FromResult(false);
        }

        public Task<bool> DeleteStudent(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);
            if (student == null) return Task.FromResult(false);

            _libraryContext.StudentSet.Where(b => b.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            student = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);

            if (student == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public async Task<GroupDto?> GetGroupByIds(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber)
                ?.ToDto();
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

            return new PageReponseDto<StudentDto>(students.Count(), students);
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
            if (oldStudent == null) return Task.FromResult<StudentDto?>(null);
            oldStudent.Name = student.Name;
            oldStudent.Lastname = student.Lastname;
            oldStudent.UrlPhoto = student.UrlPhoto;
            oldStudent.GroupNumber = student.GroupNumber;
            oldStudent.GroupYear = student.GroupYear;
            _libraryContext.SaveChanges();
            return Task.FromResult(student);

        }

        // TextCriteria

        public Task<PageReponseDto<TextCriteriaDto>> GetTextCriterions(int? index, int? count)
        {
            var criterions = _libraryContext.TextCriteriaSet.ToDtos();

            if (index != null && count != null)
            {
                return Task.FromResult(new PageReponseDto<TextCriteriaDto>(criterions.Count(),
                    criterions.Skip((int)index).Take((int)count)));
            }

            return Task.FromResult(new PageReponseDto<TextCriteriaDto>(criterions.Count(), criterions));
        }

        public Task<TextCriteriaDto?> GetTextCriterionByIds(long id)
        {
            var criterion = _libraryContext.TextCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            return Task.FromResult(criterion);
        }

        // User
        public Task<PageReponseDto<UserDto>> GetUsers(int? index, int? count)
        {
            var users = _libraryContext.UserSet.ToDtos();
            if (index != null && count != null)
            {
                return Task.FromResult(new PageReponseDto<UserDto>(users.Count(),
                    users.Skip((int)index).Take((int)count)));
            }

            return Task.FromResult(new PageReponseDto<UserDto>(users.Count(), users));
        }

        public Task<UserDto> GetUserById(long id)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id)?.ToDto();
            return Task.FromResult(user);
        }

        public Task<UserDto?> PostUser(UserDto user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _libraryContext.UserSet.AddAsync(user.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(user);
        }

        public Task<LoginResponseDto?> Login(LoginRequestDto loginRequest)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Username == loginRequest.Username);
            if (user == null) return Task.FromResult<LoginResponseDto?>(null);
            if (BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return Task.FromResult(new LoginResponseDto
                {
                    Username = user.Username,
                    Roles = user.roles,
                    Id = user.Id
                });
            }

            return Task.FromResult<LoginResponseDto?>(null);
        }

        public Task<UserDto?> PutUser(long id, UserDto user)
        {
            var oldUser = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id);
            if (oldUser == null) return Task.FromResult<UserDto?>(null);
            oldUser.Username = user.Username;
            oldUser.Password = user.Password;
            oldUser.roles = user.roles;
            _libraryContext.SaveChanges();
            return Task.FromResult(user);
        }

        public Task<bool> DeleteUser(long id)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id);
            if (user == null) return Task.FromResult(false);
            _libraryContext.UserSet.Where(u => u.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();
            user = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id);
            if (user == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }


        // Template

        public Task<PageReponseDto<TemplateDto>> GetTemplatesByUserId(long userId, int index, int count)
        {
            var templates = _libraryContext.TemplateSet.Include(c => c.Criteria).Where(t => t.TeacherId == userId).ToDtos();
            return Task.FromResult(new PageReponseDto<TemplateDto>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }
        
        public Task<PageReponseDto<TemplateDto>> GetEmptyTemplatesByUserId(long userId, int index, int count)
        {
            // Récupérer les templates qui n'ont pas de critères ou qui n'ont jamais été utilisé pour une évaluation
            var templates = _libraryContext.TemplateSet
                .Where(t => t.TeacherId == userId && 
                            !t.Criteria.Any())  
                .ToDtos();            
            return Task.FromResult(new PageReponseDto<TemplateDto>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }
        
        public Task<TemplateDto?> GetTemplateById(long userId, long templateId)
        {
            var template = _libraryContext.TemplateSet
                .Where(t => t.TeacherId == userId && t.Id == templateId)
                .ToDtos()
                .FirstOrDefault();
            return Task.FromResult(template);
        }
        
        public Task<TemplateDto?> PostTemplate(long userId, TemplateDto template)
        {
            template.TeacherId = userId;
            _libraryContext.TemplateSet.AddAsync(template.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(template);
        }
        
        public Task<TemplateDto?> PutTemplate(long templateId, TemplateDto template)
        {
            var converter = new CriteriaDtoConverter();
            var oldTemplate = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            if (oldTemplate == null) return Task.FromResult<TemplateDto?>(null);
            oldTemplate.Name = template.Name;
            oldTemplate.Criteria = template.Criteria.Select(converter.ConvertToEntity).ToList();
            _libraryContext.SaveChanges();
            return Task.FromResult(template);
        }
        
        public Task<bool> DeleteTemplate(long templateId)
        {
            var template = _libraryContext.TemplateSet.Include(t => t.Criteria).FirstOrDefault(t => t.Id == templateId);
            if (template == null) return Task.FromResult(false);
            foreach(var criteria in template.Criteria)
            {
                _libraryContext.CriteriaSet.Where(c => c.Id == criteria.Id).ExecuteDelete();
            }
            
            _libraryContext.TemplateSet.Where(t => t.Id == templateId).ExecuteDelete();
            _libraryContext.SaveChangesAsync();
            template = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            if (template == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }

        //Lesson

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
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id);
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

        public Task<PageReponseDto<LessonDto>> GetLessonsByTeacherId(long id, int index, int count)
        {
            var lessons = _libraryContext.LessonSet.Where(l => l.TeacherEntityId == id).ToDtos();
            return Task.FromResult(new PageReponseDto<LessonDto>(lessons.Count(),lessons.Skip(count*index).Take(count)));
        }


        //Evaluations
        public Task<PageReponseDto<EvaluationDto>> GetEvaluations(int index, int count)
        {
            var evals = _libraryContext.EvaluationSet.ToDtos();
            return Task.FromResult(new PageReponseDto<EvaluationDto>(evals.Count(), evals.Skip(count * index).Take(count)));
        }

        public Task<EvaluationDto?> GetEvaluationById(long id)
        {
            var eval = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id)?.ToDto();
            return Task.FromResult(eval);
        }

        public Task<PageReponseDto<EvaluationDto>> GetEvaluationsByTeacherId(long id, int index, int count)
        {
            var evals = _libraryContext.EvaluationSet.Where(e => e.TeacherId == id).ToDtos();
            return Task.FromResult(new PageReponseDto<EvaluationDto>(evals.Count(), evals.Skip(count * index).Take(count)));
        }

        public Task<EvaluationDto?> PostEvaluation(EvaluationDto eval)
        {
            _libraryContext.EvaluationSet.AddAsync(eval.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(eval);
        }

        public Task<EvaluationDto?> PutEvaluation(long id, EvaluationDto newEval)
        {
            var eval = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id);
            if (eval == null) return Task.FromResult<EvaluationDto?>(null);
            eval.Id = newEval.Id;
            eval.CourseName = newEval.CourseName;
            eval.PairName = newEval.PairName;
            eval.Grade = newEval.Grade;
            eval.Date = newEval.Date;

            eval.Teacher = newEval.Teacher.ToEntity();

            _libraryContext.SaveChanges();
            return Task.FromResult(newEval);
        }

        public Task<bool> DeleteEvaluation(long id)
        {
            var eval = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id);
            if (eval == null) return Task.FromResult(false);

            _libraryContext.EvaluationSet.Where(e => e.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            eval = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id);

            if (eval == null) return Task.FromResult(true);
            else return Task.FromResult(false);
        }
    }

}
