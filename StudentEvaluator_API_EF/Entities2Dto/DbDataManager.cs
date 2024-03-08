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
using EF_Entities;
using Shared;

namespace Entities2Dto
{
    public class DbDataManager : IStudentService<StudentDto>, IGroupService<GroupDto>, ICriteriaService<CriteriaDto,TextCriteriaDto,SliderCriteriaDto,RadioCriteriaDto>,
        IUserService<UserDto,LoginRequestDto, LoginResponseDto>, ITemplateService<TemplateDto>, ILessonService<LessonDto,LessonReponseDto>, IEvaluationService<EvaluationDto,EvaluationReponseDto>
    {
        private readonly LibraryContext _libraryContext;

        public DbDataManager(StubbedContext context)
        {
            _libraryContext = context;
        }

        //Student
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

        public async Task<StudentDto?> GetStudentById(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            Translator.StudentMapper.Reset();
            return await Task.FromResult(student);
        }

        public async Task<PageReponse<StudentDto>> GetStudents(int index, int count )
        {
            var students = _libraryContext.StudentSet.ToDtos();
            Translator.StudentMapper.Reset();
            return await Task.FromResult(new PageReponse<StudentDto>(students.Count(), students.Skip(index * count).Take(count)));
        }

        public Task<StudentDto?> PostStudent(StudentDto student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            Translator.StudentMapper.Reset();
            return Task.FromResult(_libraryContext.StudentSet.FirstOrDefault(s => s.Name.Equals(student.Name) && s.Lastname.Equals(student.Lastname))?.ToDto());

        }

     
        public Task<StudentDto?> PutStudent(long id, StudentDto student)
        {
            var oldStudent = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);
            if (oldStudent == null) {
                Translator.StudentMapper.Reset();
                return Task.FromResult<StudentDto?>(null);
            }
            oldStudent.Name = student.Name;
            oldStudent.Lastname = student.Lastname;
            oldStudent.UrlPhoto = student.UrlPhoto;
            oldStudent.GroupNumber = student.GroupNumber;
            oldStudent.GroupYear = student.GroupYear;
            _libraryContext.SaveChanges();
            Translator.StudentMapper.Reset();
            return Task.FromResult(oldStudent.ToDto());

        }

        //Group
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

        public async Task<GroupDto?> GetGroupByIds(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.Include(g => g.Students).FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber)
                ?.ToDto();
            Translator.GroupMapper.Reset();
            return await Task.FromResult(group);
        }

        public async Task<PageReponse<GroupDto>> GetGroups(int index, int count)
        {
            var groups = _libraryContext.GroupSet.Include(g => g.Students).ToDtos();
            Translator.GroupMapper.Reset();
            return await Task.FromResult(new PageReponse<GroupDto>(groups.Count(), groups.Skip(index * count).Take(count)));
        }
        public Task<GroupDto?> PostGroup(GroupDto group)
        {
            var groupTest = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber);
            if (groupTest != null) return Task.FromResult(groupTest.ToDto());
            _libraryContext.GroupSet.AddAsync(group.ToEntity());
            _libraryContext.SaveChanges();
            Translator.GroupMapper.Reset();
            return Task.FromResult(_libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber)?.ToDto());
        }

        public Task<GroupDto?> PutGroup(int gyear, int gnumber, GroupDto newGroup)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);
            if (group == null) return Task.FromResult<GroupDto?>(null);
            group.GroupNumber = newGroup.GroupNumber;
            group.GroupYear = newGroup.GroupYear;
            //group.Students = newGroup.Students;
            _libraryContext.SaveChanges();
            var groupDto = group.ToDto();
            Translator.GroupMapper.Reset();
            return Task.FromResult(groupDto);
        }

        // Criteria

        public Task<PageReponse<CriteriaDto>> GetCriterionsByTemplateId(long id)
        {
            var criterions = _libraryContext.TemplateSet
                .Include(t => t.Criteria)
                .FirstOrDefault(t => t.Id == id)
                ?.Criteria
                .Select(CriteriaDtoConverter.ConvertToDto)
                .ToList();

            return Task.FromResult(new PageReponse<CriteriaDto>(criterions.Count(), criterions));
        }
        
        public Task<bool> DeleteCriteria(long id)
        {
            var criterion = _libraryContext.CriteriaSet.FirstOrDefault(c => c.Id == id);
            if (criterion == null) return Task.FromResult(false);
            _libraryContext.CriteriaSet.Where(c => c.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();
            criterion = _libraryContext.CriteriaSet.FirstOrDefault(c => c.Id == id);
            if (criterion == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }

        // TextCriteria

        public Task<PageReponse<TextCriteriaDto>> GetTextCriterions(int index, int count)
        {
            var criterions = _libraryContext.TextCriteriaSet.ToDtos();
            Translator.TextCriteriaMapper.Reset();
            return Task.FromResult(new PageReponse<TextCriteriaDto>(criterions.Count(),
                criterions.Skip(index * count).Take(count)));
        }

        public Task<TextCriteriaDto?> GetTextCriterionByIds(long id)
        {
            var criterion = _libraryContext.TextCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            Translator.TextCriteriaMapper.Reset();
            return Task.FromResult(criterion);
        }
        
        public Task<TextCriteriaDto?> PostTextCriterion(long templateId, TextCriteriaDto text)
        {
            var template = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            if (template == null) return Task.FromResult<TextCriteriaDto?>(null);
            text.TemplateId = templateId;
            _libraryContext.TextCriteriaSet.AddAsync(text.ToEntity());
            _libraryContext.SaveChanges();
            Translator.TextCriteriaMapper.Reset();
            return Task.FromResult(text);
        }
        
        public Task<TextCriteriaDto?> PutTextCriterion(long id, TextCriteriaDto text)
        {
            var oldText = _libraryContext.TextCriteriaSet.FirstOrDefault(t => t.Id == id);
            if (oldText == null) return Task.FromResult<TextCriteriaDto?>(null);
            oldText.Name = text.Name;
            oldText.TemplateId = text.TemplateId == 0 ? oldText.TemplateId : text.TemplateId;
            oldText.ValueEvaluation = text.ValueEvaluation;
            oldText.Text = text.Text;
            _libraryContext.SaveChanges();
            return Task.FromResult(text);
        }
        
        public Task<bool> DeleteTextCriterion(long id)
        {
            var text = _libraryContext.TextCriteriaSet.FirstOrDefault(t => t.Id == id);
            if (text == null) return Task.FromResult(false);
            _libraryContext.TextCriteriaSet.Where(t => t.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();
            text = _libraryContext.TextCriteriaSet.FirstOrDefault(t => t.Id == id);
            if (text == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }
        
        // SliderCriteria
        
        public Task<PageReponse<SliderCriteriaDto>> GetSliderCriterions(int index, int count)
        {
            var criterions = _libraryContext.SliderCriteriaSet.ToDtos();
            Translator.SliderCriteriaMapper.Reset();
            return Task.FromResult(new PageReponse<SliderCriteriaDto>(criterions.Count(),
                criterions.Skip(index * count).Take(count)));
        }
        
        public Task<SliderCriteriaDto?> GetSliderCriterionByIds(long id)
        {   
            var criterion = _libraryContext.SliderCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            Translator.SliderCriteriaMapper.Reset();
            return Task.FromResult(criterion);
        }
        
        public Task<SliderCriteriaDto?> PostSliderCriterion(long templateId, SliderCriteriaDto slider)
        {
            var template = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            if (template == null) return Task.FromResult<SliderCriteriaDto?>(null);
            slider.TemplateId = templateId;
            _libraryContext.SliderCriteriaSet.AddAsync(slider.ToEntity());
            _libraryContext.SaveChanges();
            Translator.SliderCriteriaMapper.Reset();
            return Task.FromResult(slider);
        }
        
        public Task<SliderCriteriaDto?> PutSliderCriterion(long id, SliderCriteriaDto slider)
        {
            var oldSlider = _libraryContext.SliderCriteriaSet.FirstOrDefault(s => s.Id == id);
            if (oldSlider == null) return Task.FromResult<SliderCriteriaDto?>(null);
            oldSlider.Name = slider.Name;
            oldSlider.TemplateId = slider.TemplateId == 0 ? oldSlider.TemplateId : slider.TemplateId;
            oldSlider.ValueEvaluation = slider.ValueEvaluation;
            oldSlider.Value = slider.Value;
            _libraryContext.SaveChanges();
            return Task.FromResult(slider);
        }
        
        public Task<bool> DeleteSliderCriterion(long id)
        {
            var slider = _libraryContext.SliderCriteriaSet.FirstOrDefault(s => s.Id == id);
            if (slider == null) return Task.FromResult(false);
            _libraryContext.SliderCriteriaSet.Where(s => s.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();
            slider = _libraryContext.SliderCriteriaSet.FirstOrDefault(s => s.Id == id);
            if (slider == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }
        
        // RadioCriteria
        
        public Task<PageReponse<RadioCriteriaDto>> GetRadioCriterions(int index, int count)
        {
            var criterions = _libraryContext.RadioCriteriaSet.ToDtos();
            
            return Task.FromResult(new PageReponse<RadioCriteriaDto>(criterions.Count(),
                criterions.Skip((int)index).Take(count)));
        }
    
        public Task<RadioCriteriaDto?> GetRadioCriterionByIds(long id)
        {
            var criterion = _libraryContext.RadioCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            return Task.FromResult(criterion);
        }

        public Task<RadioCriteriaDto?> PostRadioCriterion(long templateId, RadioCriteriaDto radio)
        {
            var template = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            if (template == null) return Task.FromResult<RadioCriteriaDto?>(null);
            radio.TemplateId = templateId;
            _libraryContext.RadioCriteriaSet.AddAsync(radio.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(radio);
        }
        
        public Task<RadioCriteriaDto?> PutRadioCriterion(long id, RadioCriteriaDto radio)
        {
            var oldRadio = _libraryContext.RadioCriteriaSet.FirstOrDefault(r => r.Id == id);
            if (oldRadio == null) return Task.FromResult<RadioCriteriaDto?>(null);
            oldRadio.Name = radio.Name;
            oldRadio.TemplateId = radio.TemplateId == 0 ? oldRadio.TemplateId : radio.TemplateId;
            oldRadio.ValueEvaluation = radio.ValueEvaluation;
            oldRadio.Options = radio.Options;
            oldRadio.SelectedOption = radio.SelectedOption;
            _libraryContext.SaveChanges();
            return Task.FromResult(radio);
        }
        
        public Task<bool> DeleteRadioCriterion(long id)
        {
            var radio = _libraryContext.RadioCriteriaSet.FirstOrDefault(r => r.Id == id);
            if (radio == null) return Task.FromResult(false);
            _libraryContext.RadioCriteriaSet.Where(r => r.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();
            radio = _libraryContext.RadioCriteriaSet.FirstOrDefault(r => r.Id == id);
            if (radio == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }

        // User
        public Task<PageReponse<UserDto>> GetUsers(int index, int count)
        {
            var users = _libraryContext.UserSet.ToDtos();
            Translator.UserMapper.Reset();
            return Task.FromResult(new PageReponse<UserDto>(users.Count(), users.Skip(index * count).Take(count)));
        }

        public Task<UserDto> GetUserById(long id)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id)?.ToDto();
            Translator.UserMapper.Reset();
            return Task.FromResult(user);
        }

        public Task<UserDto?> PostUser(UserDto user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _libraryContext.UserSet.AddAsync(user.ToEntity());
            _libraryContext.SaveChanges();
            Translator.UserMapper.Reset();
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
            oldUser.Password = user.Password == null ? oldUser.Password : BCrypt.Net.BCrypt.HashPassword(user.Password);
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

        public Task<PageReponse<TemplateDto>> GetTemplatesByUserId(long userId, int index, int count)
        {
            var templates = _libraryContext.TemplateSet.Include(c => c.Criteria).Where(t => t.TeacherId == userId).ToDtos();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(new PageReponse<TemplateDto>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }
        
        // Récuère les templates non utilisé dans une évaluation, ils sont considèrés comme les modèles 
        public Task<PageReponse<TemplateDto>> GetEmptyTemplatesByUserId(long userId, int index, int count)
        {
            var templates = _libraryContext.TemplateSet
                .Include(c => c.Criteria)
                .Where(t => t.TeacherId == userId)
                .Where(t => !_libraryContext.EvaluationSet.Any(e => e.TemplateId == t.Id))
                .ToDtos();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(new PageReponse<TemplateDto>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }
        
        public Task<TemplateDto?> GetTemplateById(long userId, long templateId)
        {
            var template = _libraryContext.TemplateSet
                .Include(t => t.Criteria)
                .Where(t => t.TeacherId == userId && t.Id == templateId)
                .ToDtos()
                .FirstOrDefault();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(template);
        }
        
        public Task<TemplateDto?> PostTemplate(long userId, TemplateDto template)
        {
            var newTemplate = template.ToEntity();
            newTemplate.TeacherId = userId;
            _libraryContext.TemplateSet.AddAsync(newTemplate);
            _libraryContext.SaveChanges();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(template);
        }
        
        public Task<TemplateDto?> PutTemplate(long templateId, TemplateDto template)
        {
            var oldTemplate = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            if (oldTemplate == null) return Task.FromResult<TemplateDto?>(null);
            oldTemplate.Name = template.Name;
            oldTemplate.Criteria = template.Criteria.Select(CriteriaDtoConverter.ConvertToEntity).ToList();
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

        public Task<PageReponse<LessonReponseDto>> GetLessons(int index, int count)
        {
            var lessons = _libraryContext.LessonSet.Include(l => l.Teacher).Include(l => l.Group).ToReponseDtos();
            Translator.LessonMapper.Reset();
            return Task.FromResult(new PageReponse<LessonReponseDto>(lessons.Count(), lessons.Skip(index * count).Take(count)));
        }

        public Task<LessonReponseDto?> GetLessonById(long id)
        {
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id)?.ToReponseDto();
            Translator.LessonMapper.Reset();
            return Task.FromResult(lesson);
        }

        public Task<LessonReponseDto?> PutLesson(long id, LessonDto newLesson)
        {
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id);
            if (lesson == null) return Task.FromResult<LessonReponseDto?>(null);
            lesson.Id = newLesson.Id;
            lesson.CourseName = newLesson.CourseName;
            lesson.Classroom = newLesson.Classroom;
            //lesson.Date = newLesson.Date;
            //lesson.Start = newLesson.Start;
            //lesson.End = newLesson.End;
            lesson.TeacherEntityId = newLesson.TeacherId;

            _libraryContext.SaveChanges();
            
            Translator.LessonMapper.Reset();
            return Task.FromResult(_libraryContext.LessonSet.FirstOrDefault(l => l.Id == id)?.ToReponseDto());
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
        public Task<LessonReponseDto?> PostLesson(LessonDto lesson)
        {
            var lessonEntity = lesson.ToEntity();
            _libraryContext.LessonSet.AddAsync(lessonEntity);
            _libraryContext.SaveChanges();
            Translator.LessonMapper.Reset();
            return Task.FromResult(lessonEntity?.ToReponseDto());
        }

        public Task<PageReponse<LessonReponseDto>> GetLessonsByTeacherId(long id, int index, int count)
        {
            var lessons = _libraryContext.LessonSet.Where(l => l.TeacherEntityId == id).ToReponseDtos();
            Translator.LessonMapper.Reset();
            return Task.FromResult(new PageReponse<LessonReponseDto>(lessons.Count(),lessons.Skip(count*index).Take(count)));
        }


        //Evaluations
        public Task<PageReponse<EvaluationReponseDto>> GetEvaluations(int index, int count)
        {
            var evals = _libraryContext.EvaluationSet.Include(e => e.Template).Include(e => e.Student).Include(e => e.Teacher).ToReponseDtos();
            Translator.EvaluationReponseMapper.Reset();
            return Task.FromResult(new PageReponse<EvaluationReponseDto>(evals.Count(), evals.Skip(count * index).Take(count)));
        }

        public Task<EvaluationReponseDto?> GetEvaluationById(long id)
        {
            var eval = _libraryContext.EvaluationSet.Include(e => e.Template).Include(e => e.Student).Include(e => e.Teacher).FirstOrDefault(e => e.Id == id)?.ToReponseDto();
            Translator.EvaluationReponseMapper.Reset();
            return Task.FromResult(eval);
        }

        public Task<PageReponse<EvaluationReponseDto>> GetEvaluationsByTeacherId(long id, int index, int count)
        {
            var evals = _libraryContext.EvaluationSet.Include(e => e.Template).Include(e => e.Student).Include(e => e.Teacher).Where(e => e.TeacherId == id).ToReponseDtos();
            Translator.EvaluationMapper.Reset();
            return Task.FromResult(new PageReponse<EvaluationReponseDto>(evals.Count(), evals.Skip(count * index).Take(count)));
        }

        public Task<EvaluationReponseDto?> PostEvaluation(EvaluationDto eval)
        {
            var evalEntity = eval.ToEntity();
            _libraryContext.EvaluationSet.AddAsync(evalEntity);
            _libraryContext.SaveChanges();
            Translator.EvaluationMapper.Reset();
            return Task.FromResult(evalEntity?.ToReponseDto());
        }

        public Task<EvaluationReponseDto?> PutEvaluation(long id, EvaluationDto newEval)
        {
            var eval = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id);
            if (eval == null) return Task.FromResult<EvaluationReponseDto?>(null);
            eval.Id = newEval.Id;
            eval.CourseName = newEval.CourseName;
            eval.PairName = newEval.PairName;
            eval.Grade = newEval.Grade;
            eval.Date = newEval.Date;

            eval.TeacherId = newEval.TeacherId;

            _libraryContext.SaveChanges();
            Translator.EvaluationMapper.Reset();
            return Task.FromResult(_libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id)?.ToReponseDto());
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
