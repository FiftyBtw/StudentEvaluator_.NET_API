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
    /// <summary>
    /// Provides data management functionalities for interacting with the database.
    /// Implements various service interfaces for managing students, groups, criteria, users, templates, lessons, and evaluations.
    /// </summary>
    public class DbDataManager : IStudentService<StudentDto>, IGroupService<GroupDto>, ICriteriaService<CriteriaDto,TextCriteriaDto,SliderCriteriaDto,RadioCriteriaDto>,
        IUserService<UserDto,LoginRequestDto, LoginResponseDto>, ITemplateService<TemplateDto, TemplateResponseDto>, ILessonService<LessonDto,LessonReponseDto>, IEvaluationService<EvaluationDto,EvaluationReponseDto>
    {
        private readonly LibraryContext _libraryContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataManager"/> class with the provided database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DbDataManager(StubbedContext context)
        {
            _libraryContext = context;
        }

        //Student


        /// <summary>
        /// Deletes a student with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A task representing the asynchronous operation with a boolean indicating whether the deletion was successful.</returns>
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


        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>A task representing the asynchronous operation with a nullable <see cref="StudentDto"/>.</returns>
        public async Task<StudentDto?> GetStudentById(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            Translator.StudentMapper.Reset();
            return await Task.FromResult(student);
        }


        /// <summary>
        /// Retrieves a page of students.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of students per page.</param>
        /// <returns>A task representing the asynchronous operation with a <see cref="PageReponse{T}"/> of <see cref="StudentDto"/>.</returns>
        public async Task<PageReponse<StudentDto>> GetStudents(int index, int count )
        {
            var students = _libraryContext.StudentSet.ToDtos();
            Translator.StudentMapper.Reset();
            return await Task.FromResult(new PageReponse<StudentDto>(students.Count(), students.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="student">The student to add.</param>
        /// <returns>A task representing the asynchronous operation with a nullable <see cref="StudentDto"/>.</returns>
        public Task<StudentDto?> PostStudent(StudentDto student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            Translator.StudentMapper.Reset();
            return Task.FromResult(_libraryContext.StudentSet.FirstOrDefault(s => s.Name.Equals(student.Name) && s.Lastname.Equals(student.Lastname))?.ToDto());

        }


        /// <summary>
        /// Updates an existing student.
        /// </summary>
        /// <param name="id">The ID of the student to update.</param>
        /// <param name="student">The updated student data.</param>
        /// <returns>A task representing the asynchronous operation with a nullable <see cref="StudentDto"/>.</returns>
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

        /// <summary>
        /// Deletes a group by group year and group number.
        /// </summary>
        /// <param name="gyear">The year of the group.</param>
        /// <param name="gnumber">The number of the group.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Retrieves a group by group year and group number.
        /// </summary>
        /// <param name="gyear">The year of the group.</param>
        /// <param name="gnumber">The number of the group.</param>
        /// <returns>A task representing the asynchronous operation, returning the group DTO if found, otherwise null.</returns>
        public async Task<GroupDto?> GetGroupByIds(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.Include(g => g.Students).FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber)
                ?.ToDto();
            Translator.GroupMapper.Reset();
            return await Task.FromResult(group);
        }


        /// <summary>
        /// Retrieves a page of groups.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of groups per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the groups.</returns>
        public async Task<PageReponse<GroupDto>> GetGroups(int index, int count)
        {
            var groups = _libraryContext.GroupSet.Include(g => g.Students).ToDtos();
            Translator.GroupMapper.Reset();
            return await Task.FromResult(new PageReponse<GroupDto>(groups.Count(), groups.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Adds a new group.
        /// </summary>
        /// <param name="group">The group DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added group DTO.</returns>
        public Task<GroupDto?> PostGroup(GroupDto group)
        {
            var groupTest = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber);
            if (groupTest != null) return Task.FromResult(groupTest.ToDto());
            _libraryContext.GroupSet.AddAsync(group.ToEntity());
            _libraryContext.SaveChanges();
            Translator.GroupMapper.Reset();
            return Task.FromResult(_libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber)?.ToDto());
        }


        /// <summary>
        /// Updates a group.
        /// </summary>
        /// <param name="gyear">The year of the group.</param>
        /// <param name="gnumber">The number of the group.</param>
        /// <param name="newGroup">The updated group DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated group DTO if found, otherwise null.</returns>
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


        /// <summary>
        /// Retrieves criteria by template ID.
        /// </summary>
        /// <param name="id">The ID of the template.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the criteria.</returns>
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


        /// <summary>
        /// Deletes a criterion by ID.
        /// </summary>
        /// <param name="id">The ID of the criterion to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Retrieves a page of text criteria.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of text criteria per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the text criteria.</returns>
        public Task<PageReponse<TextCriteriaDto>> GetTextCriterions(int index, int count)
        {
            var criterions = _libraryContext.TextCriteriaSet.ToDtos();
            Translator.TextCriteriaMapper.Reset();
            return Task.FromResult(new PageReponse<TextCriteriaDto>(criterions.Count(),
                criterions.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Retrieves a text criterion by ID.
        /// </summary>
        /// <param name="id">The ID of the text criterion to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the text criterion DTO if found, otherwise null.</returns>
        public Task<TextCriteriaDto?> GetTextCriterionByIds(long id)
        {
            var criterion = _libraryContext.TextCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            Translator.TextCriteriaMapper.Reset();
            return Task.FromResult(criterion);
        }


        /// <summary>
        /// Adds a new text criterion.
        /// </summary>
        /// <param name="templateId">The ID of the template to which the text criterion belongs.</param>
        /// <param name="text">The text criterion DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added text criterion DTO.</returns>
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


        /// <summary>
        /// Updates a text criterion.
        /// </summary>
        /// <param name="id">The ID of the text criterion to update.</param>
        /// <param name="text">The updated text criterion DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated text criterion DTO if found, otherwise null.</returns>
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


        /// <summary>
        /// Deletes a text criterion by ID.
        /// </summary>
        /// <param name="id">The ID of the text criterion to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Retrieves a page of slider criteria.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of slider criteria per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the slider criteria.</returns>
        public Task<PageReponse<SliderCriteriaDto>> GetSliderCriterions(int index, int count)
        {
            var criterions = _libraryContext.SliderCriteriaSet.ToDtos();
            Translator.SliderCriteriaMapper.Reset();
            return Task.FromResult(new PageReponse<SliderCriteriaDto>(criterions.Count(),
                criterions.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Retrieves a slider criterion by ID.
        /// </summary>
        /// <param name="id">The ID of the slider criterion to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the slider criterion DTO if found, otherwise null.</returns>
        public Task<SliderCriteriaDto?> GetSliderCriterionByIds(long id)
        {   
            var criterion = _libraryContext.SliderCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            Translator.SliderCriteriaMapper.Reset();
            return Task.FromResult(criterion);
        }


        /// <summary>
        /// Adds a new slider criterion.
        /// </summary>
        /// <param name="templateId">The ID of the template to which the slider criterion belongs.</param>
        /// <param name="slider">The slider criterion DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added slider criterion DTO.</returns>
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


        /// <summary>
        /// Updates a slider criterion.
        /// </summary>
        /// <param name="id">The ID of the slider criterion to update.</param>
        /// <param name="slider">The updated slider criterion DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated slider criterion DTO if found, otherwise null.</returns>
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


        /// <summary>
        /// Deletes a slider criterion by ID.
        /// </summary>
        /// <param name="id">The ID of the slider criterion to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Retrieves a page of radio criteria.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of radio criteria per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the radio criteria.</returns>
        public Task<PageReponse<RadioCriteriaDto>> GetRadioCriterions(int index, int count)
        {
            var criterions = _libraryContext.RadioCriteriaSet.ToDtos();
            
            return Task.FromResult(new PageReponse<RadioCriteriaDto>(criterions.Count(),
                criterions.Skip((int)index).Take(count)));
        }


        /// <summary>
        /// Retrieves a radio criterion by ID.
        /// </summary>
        /// <param name="id">The ID of the radio criterion to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the radio criterion DTO if found, otherwise null.</returns>
        public Task<RadioCriteriaDto?> GetRadioCriterionByIds(long id)
        {
            var criterion = _libraryContext.RadioCriteriaSet.FirstOrDefault(s => s.Id == id)?.ToDto();
            return Task.FromResult(criterion);
        }


        /// <summary>
        /// Adds a new radio criterion.
        /// </summary>
        /// <param name="templateId">The ID of the template to which the radio criterion belongs.</param>
        /// <param name="radio">The radio criterion DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added radio criterion DTO.</returns>
        public Task<RadioCriteriaDto?> PostRadioCriterion(long templateId, RadioCriteriaDto radio)
        {
            var template = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            if (template == null) return Task.FromResult<RadioCriteriaDto?>(null);
            radio.TemplateId = templateId;
            _libraryContext.RadioCriteriaSet.AddAsync(radio.ToEntity());
            _libraryContext.SaveChanges();
            return Task.FromResult(radio);
        }


        /// <summary>
        /// Updates a radio criterion.
        /// </summary>
        /// <param name="id">The ID of the radio criterion to update.</param>
        /// <param name="radio">The updated radio criterion DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated radio criterion DTO if found, otherwise null.</returns>
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


        /// <summary>
        /// Deletes a radio criterion by ID.
        /// </summary>
        /// <param name="id">The ID of the radio criterion to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Retrieves a page of users.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of users per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the users.</returns>
        public Task<PageReponse<UserDto>> GetUsers(int index, int count)
        {
            var users = _libraryContext.UserSet.ToDtos();
            Translator.UserMapper.Reset();
            return Task.FromResult(new PageReponse<UserDto>(users.Count(), users.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the user DTO if found, otherwise null.</returns>
        public Task<UserDto> GetUserById(long id)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id)?.ToDto();
            Translator.UserMapper.Reset();
            return Task.FromResult(user);
        }


        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added user DTO.</returns>
        public Task<UserDto?> PostUser(UserDto user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _libraryContext.UserSet.AddAsync(user.ToEntity());
            _libraryContext.SaveChanges();
            Translator.UserMapper.Reset();
            return Task.FromResult(user);
        }


        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequest">The login request DTO containing username and password.</param>
        /// <returns>A task representing the asynchronous operation, returning the login response DTO if login is successful, otherwise null.</returns>
        public Task<LoginResponseDto?> Login(LoginRequestDto loginRequest)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Username == loginRequest.Username);
            if (user == null) return Task.FromResult<LoginResponseDto?>(null);
            if (BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return Task.FromResult(new LoginResponseDto
                {
                    Username = user.Username,
                    Roles = user.Roles,
                    Id = user.Id
                });
            }

            return Task.FromResult<LoginResponseDto?>(null);
        }


        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated user DTO if found, otherwise null.</returns>
        public Task<UserDto?> PutUser(long id, UserDto user)
        {
            var oldUser = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id);
            if (oldUser == null) return Task.FromResult<UserDto?>(null);
            oldUser.Username = user.Username;
            oldUser.Password = user.Password == null ? oldUser.Password : BCrypt.Net.BCrypt.HashPassword(user.Password);
            oldUser.Roles = user.roles;
            _libraryContext.SaveChanges();
            return Task.FromResult(user);
        }


        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Retrieves a page of templates by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose templates are to be retrieved.</param>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of templates per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the templates.</returns>
        public Task<PageReponse<TemplateResponseDto>> GetTemplatesByUserId(long userId, int index, int count)
        {
            var templates = _libraryContext.TemplateSet.Include(c => c.Criteria).Where(t => t.TeacherId == userId).ToResponseDtos();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(new PageReponse<TemplateResponseDto>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }

        /// <summary>
        /// Retrieves a page of empty templates by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose empty templates are to be retrieved.</param>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of empty templates per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the empty templates.</returns>
        public Task<PageReponse<TemplateResponseDto>> GetEmptyTemplatesByUserId(long userId, int index, int count)
        {
            var templates = _libraryContext.TemplateSet
                .Include(c => c.Criteria)
                .Where(t => t.TeacherId == userId)
                .Where(t => !_libraryContext.EvaluationSet.Any(e => e.TemplateId == t.Id))
                .ToResponseDtos();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(new PageReponse<TemplateResponseDto>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Retrieves a template by ID.
        /// </summary>
        /// <param name="userId">The ID of the user who owns the template.</param>
        /// <param name="templateId">The ID of the template to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the template DTO if found, otherwise null.</returns>
        public Task<TemplateResponseDto?> GetTemplateById(long userId, long templateId)
        {
            var template = _libraryContext.TemplateSet.Include(t => t.Criteria).FirstOrDefault(t => t.Id == templateId);
            if (template == null) return Task.FromResult<TemplateResponseDto?>(null);
            Translator.TemplateMapper.Reset();
            return Task.FromResult(template.ToResponseDto());
        }


        /// <summary>
        /// Adds a new template.
        /// </summary>
        /// <param name="userId">The ID of the user who owns the template.</param>
        /// <param name="template">The template DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added template DTO.</returns>
        public Task<TemplateResponseDto?> PostTemplate(long userId, TemplateDto template)
        {
            var templateEntity = template.ToEntity();
            templateEntity.TeacherId = userId;
            _libraryContext.TemplateSet.AddAsync(templateEntity);
            _libraryContext.SaveChanges();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(templateEntity.ToResponseDto());
        }


        /// <summary>
        /// Updates a template.
        /// </summary>
        /// <param name="templateId">The ID of the template to update.</param>
        /// <param name="template">The updated template DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated template DTO if found, otherwise null.</returns>
        public Task<TemplateResponseDto?> PutTemplate(long templateId, TemplateDto template)
        {
            var oldTemplate = _libraryContext.TemplateSet.Include(t => t.Criteria).FirstOrDefault(t => t.Id == templateId);
            if (oldTemplate == null) return Task.FromResult<TemplateResponseDto?>(null);
            oldTemplate.Name = template.Name;
            _libraryContext.SaveChanges();
            Translator.TemplateMapper.Reset();
            return Task.FromResult(oldTemplate.ToResponseDto());
        }


        /// <summary>
        /// Deletes a template by ID.
        /// </summary>
        /// <param name="templateId">The ID of the template to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Retrieves a page of lessons.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of lessons per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the lessons.</returns>
        public Task<PageReponse<LessonReponseDto>> GetLessons(int index, int count)
        {
            var lessons = _libraryContext.LessonSet.Include(l => l.Teacher).Include(l => l.Group).ToReponseDtos();
            Translator.LessonMapper.Reset();
            return Task.FromResult(new PageReponse<LessonReponseDto>(lessons.Count(), lessons.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Retrieves a lesson by ID.
        /// </summary>
        /// <param name="id">The ID of the lesson to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the lesson DTO if found, otherwise null.</returns>
        public Task<LessonReponseDto?> GetLessonById(long id)
        {
            var lesson = _libraryContext.LessonSet.Include(l => l.Teacher).Include(l => l.Group).FirstOrDefault(l => l.Id == id)?.ToReponseDto();
            Translator.LessonMapper.Reset();
            return Task.FromResult(lesson);
        }


        /// <summary>
        /// Updates a lesson.
        /// </summary>
        /// <param name="id">The ID of the lesson to update.</param>
        /// <param name="newLesson">The updated lesson DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated lesson DTO if found, otherwise null.</returns>
        public Task<LessonReponseDto?> PutLesson(long id, LessonDto newLesson)
        {
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id);
            if (lesson == null) return Task.FromResult<LessonReponseDto?>(null);
            lesson.CourseName = newLesson.CourseName;
            lesson.Classroom = newLesson.Classroom;
            lesson.Start = newLesson.Start;
            lesson.End = newLesson.End;
            lesson.TeacherEntityId = newLesson.TeacherId;
            lesson.GroupNumber = newLesson.GroupNumber;
            lesson.GroupYear= newLesson.GroupYear;
            _libraryContext.SaveChanges();         
            Translator.LessonMapper.Reset();
            return Task.FromResult(_libraryContext.LessonSet.Include(l => l.Teacher).Include(l => l.Group).FirstOrDefault(l => l.Id == id)?.ToReponseDto());
        }


        /// <summary>
        /// Deletes a lesson by ID.
        /// </summary>
        /// <param name="id">The ID of the lesson to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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


        /// <summary>
        /// Adds a new lesson.
        /// </summary>
        /// <param name="lesson">The lesson DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added lesson DTO.</returns>
        public Task<LessonReponseDto?> PostLesson(LessonDto lesson)
        {
            var lessonEntity = lesson.ToEntity();
            _libraryContext.LessonSet.AddAsync(lessonEntity);
            _libraryContext.SaveChanges();
            Translator.LessonMapper.Reset();
            return Task.FromResult(_libraryContext.LessonSet.Include(l => l.Teacher).Include(l=>l.Group).FirstOrDefault(l => l.Id==lessonEntity.Id)?.ToReponseDto());
        }


        /// <summary>
        /// Retrieves a page of lessons by teacher ID.
        /// </summary>
        /// <param name="id">The ID of the teacher whose lessons are to be retrieved.</param>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of lessons per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the lessons.</returns>
        public Task<PageReponse<LessonReponseDto>> GetLessonsByTeacherId(long id, int index, int count)
        {
            var lessons = _libraryContext.LessonSet.Include(l => l.Teacher).Include(l => l.Group).Where(l => l.TeacherEntityId == id).ToReponseDtos();
            Translator.LessonMapper.Reset();
            return Task.FromResult(new PageReponse<LessonReponseDto>(lessons.Count(),lessons.Skip(count*index).Take(count)));
        }


        //Evaluations


        /// <summary>
        /// Retrieves a page of evaluations.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of evaluations per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the evaluations.</returns>
        public Task<PageReponse<EvaluationReponseDto>> GetEvaluations(int index, int count)
        {
            var evals = _libraryContext.EvaluationSet.Include(e => e.Template).Include(e => e.Student).Include(e => e.Teacher).ToReponseDtos();
            Translator.EvaluationReponseMapper.Reset();
            return Task.FromResult(new PageReponse<EvaluationReponseDto>(evals.Count(), evals.Skip(count * index).Take(count)));
        }


        /// <summary>
        /// Retrieves an evaluation by ID.
        /// </summary>
        /// <param name="id">The ID of the evaluation to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the evaluation DTO if found, otherwise null.</returns>
        public Task<EvaluationReponseDto?> GetEvaluationById(long id)
        {
            var eval = _libraryContext.EvaluationSet.Include(e => e.Template).Include(e => e.Student).Include(e => e.Teacher).FirstOrDefault(e => e.Id == id)?.ToReponseDto();
            Translator.EvaluationReponseMapper.Reset();
            return Task.FromResult(eval);
        }


        /// <summary>
        /// Retrieves a page of evaluations by teacher ID.
        /// </summary>
        /// <param name="id">The ID of the teacher whose evaluations are to be retrieved.</param>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of evaluations per page.</param>
        /// <returns>A task representing the asynchronous operation, returning a page response containing the evaluations.</returns>
        public Task<PageReponse<EvaluationReponseDto>> GetEvaluationsByTeacherId(long id, int index, int count)
        {
            var evals = _libraryContext.EvaluationSet.Include(e => e.Template).Include(e => e.Student).Include(e => e.Teacher).Where(e => e.TeacherId == id).ToReponseDtos();
            Translator.EvaluationMapper.Reset();
            return Task.FromResult(new PageReponse<EvaluationReponseDto>(evals.Count(), evals.Skip(count * index).Take(count)));
        }


        /// <summary>
        /// Adds a new evaluation.
        /// </summary>
        /// <param name="eval">The evaluation DTO to add.</param>
        /// <returns>A task representing the asynchronous operation, returning the added evaluation DTO.</returns>
        public Task<EvaluationReponseDto?> PostEvaluation(EvaluationDto eval)
        {
            var evalEntity = eval.ToEntity();
            _libraryContext.EvaluationSet.AddAsync(evalEntity);
            _libraryContext.SaveChanges();
            Translator.EvaluationMapper.Reset();
            return Task.FromResult(_libraryContext.EvaluationSet.Include(e => e.Teacher).Include(e => e.Template).Include(e => e.Student).FirstOrDefault(e => e.Id == evalEntity.Id)?.ToReponseDto());
        }


        /// <summary>
        /// Updates an evaluation.
        /// </summary>
        /// <param name="id">The ID of the evaluation to update.</param>
        /// <param name="newEval">The updated evaluation DTO.</param>
        /// <returns>A task representing the asynchronous operation, returning the updated evaluation DTO if found, otherwise null.</returns>
        public Task<EvaluationReponseDto?> PutEvaluation(long id, EvaluationDto newEval)
        {
            var eval = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id);
            if (eval == null) return Task.FromResult<EvaluationReponseDto?>(null);
            eval.CourseName = newEval.CourseName;
            eval.PairName = newEval.PairName;
            eval.Grade = newEval.Grade;
            eval.Date = newEval.Date;
            eval.StudentId= newEval.StudentId;
            eval.TemplateId=newEval.TemplateId;
            eval.TeacherId = newEval.TeacherId;

            _libraryContext.SaveChanges();
            Translator.EvaluationMapper.Reset();
             eval = _libraryContext.EvaluationSet.Include(e => e.Teacher).Include(e => e.Template).Include(e => e.Student).FirstOrDefault(e => e.Id == id);
            return Task.FromResult(eval?.ToReponseDto());
        }


        /// <summary>
        /// Deletes an evaluation by ID.
        /// </summary>
        /// <param name="id">The ID of the evaluation to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
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
