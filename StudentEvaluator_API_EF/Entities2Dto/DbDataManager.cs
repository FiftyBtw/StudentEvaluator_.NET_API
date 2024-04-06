using System.Linq.Expressions;
using API_Dto;
using EF_DbContextLib;
using EF_Entities;
using EF_StubbedContextLib;
using Microsoft.EntityFrameworkCore;
using Shared;
namespace Entities2Dto;

/// <summary>
/// Provides data management functionalities for interacting with the database.
/// Implements various service interfaces for managing students, groups, criteria, users, templates, lessons, and evaluations.
/// </summary>
public class DbDataManager : IStudentService<StudentDto>, IGroupService<GroupDto>, ICriteriaService<CriteriaDto,TextCriteriaDto,SliderCriteriaDto,RadioCriteriaDto>,
    /*IUserService<UserDto,LoginDto, LoginResponseDto>,*/ ITemplateService<TemplateDto>, ILessonService<LessonDto,LessonReponseDto>, IEvaluationService<EvaluationDto,EvaluationReponseDto>
{
    private readonly UnitOfWork.UnitOfWork _unitOfWork;


    /// <summary>
    /// Initializes a new instance of the <see cref="DbDataManager"/> class with the provided database context.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="unitOfWork">The UnitOfWork pattern</param>
    public DbDataManager(LibraryContext context, UnitOfWork.UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //Student


    /// <summary>
    /// Deletes a student with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the student to delete.</param>
    /// <returns>A task representing the asynchronous operation with a boolean indicating whether the deletion was successful.</returns>
    public async Task<bool> DeleteStudent(long id)
    {
        var student = await _unitOfWork.StudentsRepository.GetByIdAsync(id);
        if (student == null) return false;

        await _unitOfWork.StudentsRepository.Delete(student);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

        
    /// <summary>
    /// Retrieves a student by ID.
    /// </summary>
    /// <param name="id">The ID of the student to retrieve.</param>
    /// <returns>A task representing the asynchronous operation with a nullable <see cref="StudentDto"/>.</returns>
    public async Task<StudentDto?> GetStudentById(long id)
    {
        var student = await _unitOfWork.StudentsRepository.GetByIdAsync(id);
        Translator.StudentMapper.Reset();
        return student?.ToDto();
    }


    /// <summary>
    /// Retrieves a page of students.
    /// </summary>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of students per page.</param>
    /// <returns>A task representing the asynchronous operation with a <see cref="PageReponse{T}"/> of <see cref="StudentDto"/>.</returns>
    public async Task<PageReponse<StudentDto>> GetStudents(int index = 0, int count = 10)
    {
        var students = await _unitOfWork.StudentsRepository.Get(index: index, count: count);
        var studentsDto = students.ToList().ToDtos();
        
        Translator.StudentMapper.Reset();
        var studentDtos = studentsDto.ToList();
        return await Task.FromResult(new PageReponse<StudentDto>(studentDtos.Count(), studentDtos.Skip(index * count).Take(count)));
    }


    /// <summary>
    /// Adds a new student.
    /// </summary>
    /// <param name="student">The student to add.</param>
    /// <returns>A task representing the asynchronous operation with a nullable <see cref="StudentDto"/>.</returns>
    public async Task<StudentDto?> PostStudent(StudentDto student)
    {
        var studentEntity = student.ToEntity();
        await _unitOfWork.StudentsRepository.Insert(studentEntity);
        var group = await _unitOfWork.GroupsRepository.GetById([],student.GroupYear, student.GroupNumber);
        if(group == null) throw new KeyNotFoundException("Group not found, insert failed.");
        await _unitOfWork.SaveChangesAsync();
        Translator.StudentMapper.Reset();
        return studentEntity.ToDto();
    }


    /// <summary>
    /// Updates an existing student.
    /// </summary>
    /// <param name="id">The ID of the student to update.</param>
    /// <param name="student">The updated student data.</param>
    /// <returns>A task representing the asynchronous operation with a nullable <see cref="StudentDto"/>.</returns>
    public async Task<StudentDto?> PutStudent(long id, StudentDto student)
    {
        var oldStudent = await _unitOfWork.StudentsRepository.GetByIdAsync(id);
        if (oldStudent == null) return null;
        var group = await _unitOfWork.GroupsRepository.GetById([],student.GroupYear, student.GroupNumber);
        if(group == null) throw new KeyNotFoundException("Group not found, update failed.");
        oldStudent.Name = student.Name;
        oldStudent.Lastname = student.Lastname;
        oldStudent.UrlPhoto = student.UrlPhoto;
        oldStudent.GroupYear = student.GroupYear;
        oldStudent.GroupNumber = student.GroupNumber;
        await _unitOfWork.SaveChangesAsync();
        return oldStudent.ToDto();
    }

    //Group

    /// <summary>
    /// Deletes a group by group year and group number.
    /// </summary>
    /// <param name="gyear">The year of the group.</param>
    /// <param name="gnumber">The number of the group.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
    public async Task<bool> DeleteGroup(int gyear, int gnumber)
    {
        var includes = new List<Expression<Func<GroupEntity, object>>>(0);
        var group = await _unitOfWork.GroupsRepository.GetById(includes,gyear, gnumber);
        if (group == null) return false;
        await _unitOfWork.GroupsRepository.Delete(gyear, gnumber);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a group by group year and group number.
    /// </summary>
    /// <param name="gyear">The year of the group.</param>
    /// <param name="gnumber">The number of the group.</param>
    /// <returns>A task representing the asynchronous operation, returning the group DTO if found, otherwise null.</returns>
    public async Task<GroupDto?> GetGroupByIds(int gyear, int gnumber)
    {
        var includes = new List<Expression<Func<GroupEntity, object>>>(1);
        includes.Add(g => g.Students);
        var group = await _unitOfWork.GroupsRepository.GetById(includes, gyear, gnumber);
        Translator.GroupMapper.Reset();
        return group?.ToDto();
    }


    /// <summary>
    /// Retrieves a page of groups.
    /// </summary>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of groups per page.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the groups.</returns>
    public async Task<PageReponse<GroupDto>> GetGroups(int index = 0, int count = 10)
    {
        var groups = await _unitOfWork.GroupsRepository.Get(includeProperties: "Students", index: index, count: count);
        var groupsDto = groups.ToList().ToDtos();
        Translator.GroupMapper.Reset();
        return new PageReponse<GroupDto>(groupsDto.Count(), groupsDto.Skip(index * count).Take(count));
    }


    /// <summary>
    /// Adds a new group.
    /// </summary>
    /// <param name="group">The group DTO to add.</param>
    /// <returns>A task representing the asynchronous operation, returning the added group DTO.</returns>
    public async Task<GroupDto?> PostGroup(GroupDto group)
    {
        await _unitOfWork.GroupsRepository.Insert(group.ToEntity());
        await _unitOfWork.SaveChangesAsync();
        Translator.GroupMapper.Reset();
        return group;
    }

    // Criteria

    /// <summary>
    /// Retrieves criteria by template ID.
    /// </summary>
    /// <param name="id">The ID of the template.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the criteria.</returns>
    public Task<PageReponse<CriteriaDto>> GetCriterionsByTemplateId(long id)
    {
        /*var criterions = _libraryContext.TemplateSet
            .Include(t => t.Criteria)
            .FirstOrDefault(t => t.Id == id)
            ?.Criteria
            .Select(CriteriaDtoConverter.ConvertToDto)
            .ToList();
    */
        var criterions = _unitOfWork.TemplatesRepository.GetByIdAsync(id, t => t.Criteria).Result.Criteria.Select(CriteriaDtoConverter.ConvertToDto).ToList();
        Translator.CriteriaMapper.Reset();
        return Task.FromResult(new PageReponse<CriteriaDto>(criterions.Count(), criterions));
    }


    /// <summary>
    /// Deletes a criterion by ID.
    /// </summary>
    /// <param name="id">The ID of the criterion to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
    public async Task<bool> DeleteCriteria(long id)
    {
        var criterion = await _unitOfWork.CriteriasRepository.GetByIdAsync(id);
        if (criterion == null) return false;
        await _unitOfWork.CriteriasRepository.Delete(criterion);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }


    // TextCriteria
        
    /// <summary>
    /// Retrieves a text criterion by ID.
    /// </summary>
    /// <param name="id">The ID of the text criterion to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, returning the text criterion DTO if found, otherwise null.</returns>
    public Task<TextCriteriaDto?> GetTextCriterionByIds(long id)
    {
        var text = _unitOfWork.CriteriasRepository.GetByIdAsync(id).Result;
        Translator.TextCriteriaMapper.Reset();
        return Task.FromResult(CriteriaDtoConverter.ConvertToDto(text) as TextCriteriaDto);
    }


    /// <summary>
    /// Adds a new text criterion.
    /// </summary>
    /// <param name="templateId">The ID of the template to which the text criterion belongs.</param>
    /// <param name="text">The text criterion DTO to add.</param>
    /// <returns>A task representing the asynchronous operation, returning the added text criterion DTO.</returns>
    public async Task<TextCriteriaDto?> PostTextCriterion(long templateId, TextCriteriaDto text)
    {
        var template = _unitOfWork.TemplatesRepository.GetByIdAsync(templateId, entity => entity.Criteria!).Result;
        if (template == null) return null;
        text.TemplateId = templateId;
        var textEntity = text.ToEntity();
        template.Criteria?.Add(textEntity);
        await _unitOfWork.CriteriasRepository.Insert(textEntity);
        await _unitOfWork.SaveChangesAsync();
        Translator.TextCriteriaMapper.Reset();
        return textEntity.ToDto();
    }


    /// <summary>
    /// Updates a text criterion.
    /// </summary>
    /// <param name="id">The ID of the text criterion to update.</param>
    /// <param name="text">The updated text criterion DTO.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated text criterion DTO if found, otherwise null.</returns>
    public async Task<TextCriteriaDto?> PutTextCriterion(long id, TextCriteriaDto text)
    {
        if (_unitOfWork.CriteriasRepository.GetByIdAsync(id).Result is not TextCriteriaEntity oldText) return null;
        oldText.Name = text.Name;
        oldText.TemplateId = text.TemplateId == 0 ? oldText.TemplateId : text.TemplateId;
        oldText.ValueEvaluation = text.ValueEvaluation;
        oldText.Text = text.Text;
        await _unitOfWork.SaveChangesAsync();
        return oldText.ToDto();
    }

    // SliderCriteria
        
    /// <summary>
    /// Retrieves a slider criterion by ID.
    /// </summary>
    /// <param name="id">The ID of the slider criterion to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, returning the slider criterion DTO if found, otherwise null.</returns>
    public async Task<SliderCriteriaDto?> GetSliderCriterionByIds(long id)
    {   
        var slider = await _unitOfWork.CriteriasRepository.GetByIdAsync(id);
        if(slider == null) return null;
        Translator.SliderCriteriaMapper.Reset();
        return CriteriaDtoConverter.ConvertToDto(slider) as SliderCriteriaDto;
    }


    /// <summary>
    /// Adds a new slider criterion.
    /// </summary>
    /// <param name="templateId">The ID of the template to which the slider criterion belongs.</param>
    /// <param name="slider">The slider criterion DTO to add.</param>
    /// <returns>A task representing the asynchronous operation, returning the added slider criterion DTO.</returns>
    public async Task<SliderCriteriaDto?> PostSliderCriterion(long templateId, SliderCriteriaDto slider)
    {
        var template = _unitOfWork.TemplatesRepository.GetByIdAsync(templateId).Result;
        if (template == null) return null;
        slider.TemplateId = templateId;
        var sliderEntity = slider.ToEntity();
        template.Criteria?.Add(sliderEntity);
        await _unitOfWork.CriteriasRepository.Insert(sliderEntity);
        await _unitOfWork.SaveChangesAsync();
        Translator.SliderCriteriaMapper.Reset();
        return sliderEntity.ToDto();
    }


    /// <summary>
    /// Updates a slider criterion.
    /// </summary>
    /// <param name="id">The ID of the slider criterion to update.</param>
    /// <param name="slider">The updated slider criterion DTO.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated slider criterion DTO if found, otherwise null.</returns>
    public async Task<SliderCriteriaDto?> PutSliderCriterion(long id, SliderCriteriaDto slider)
    {
        var oldSlider = _unitOfWork.CriteriasRepository.GetByIdAsync(id).Result as SliderCriteriaEntity;
        if (oldSlider == null) return null;
        oldSlider.Name = slider.Name;
        oldSlider.TemplateId = slider.TemplateId == 0 ? oldSlider.TemplateId : slider.TemplateId;
        oldSlider.ValueEvaluation = slider.ValueEvaluation;
        oldSlider.Value = slider.Value;
       await  _unitOfWork.SaveChangesAsync();
       return oldSlider.ToDto();
    }

    // RadioCriteria
        
    /// <summary>
    /// Retrieves a radio criterion by ID.
    /// </summary>
    /// <param name="id">The ID of the radio criterion to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, returning the radio criterion DTO if found, otherwise null.</returns>
    public Task<RadioCriteriaDto?> GetRadioCriterionByIds(long id)
    {
        var radio = _unitOfWork.CriteriasRepository.GetByIdAsync(id).Result;
        if(radio == null) return Task.FromResult<RadioCriteriaDto?>(null);
        Translator.RadioCriteriaMapper.Reset();
        return Task.FromResult(CriteriaDtoConverter.ConvertToDto(radio) as RadioCriteriaDto);
    }


    /// <summary>
    /// Adds a new radio criterion.
    /// </summary>
    /// <param name="templateId">The ID of the template to which the radio criterion belongs.</param>
    /// <param name="radio">The radio criterion DTO to add.</param>
    /// <returns>A task representing the asynchronous operation, returning the added radio criterion DTO.</returns>
    public async Task<RadioCriteriaDto?> PostRadioCriterion(long templateId, RadioCriteriaDto radio)
    {
        var template = _unitOfWork.TemplatesRepository.GetByIdAsync(templateId).Result;
        if (template == null) return null;
        radio.TemplateId = templateId;
        var radioEntity = radio.ToEntity();
        template.Criteria?.Add(radioEntity);
        await _unitOfWork.CriteriasRepository.Insert(radioEntity);
        await _unitOfWork.SaveChangesAsync();
        Translator.RadioCriteriaMapper.Reset();
        return radioEntity.ToDto();
    }


    /// <summary>
    /// Updates a radio criterion.
    /// </summary>
    /// <param name="id">The ID of the radio criterion to update.</param>
    /// <param name="radio">The updated radio criterion DTO.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated radio criterion DTO if found, otherwise null.</returns>
    public async Task<RadioCriteriaDto?> PutRadioCriterion(long id, RadioCriteriaDto radio)
    {
        var oldRadio = _unitOfWork.CriteriasRepository.GetByIdAsync(id).Result as RadioCriteriaEntity;
        if (oldRadio == null) return null;
        oldRadio.Name = radio.Name;
        oldRadio.TemplateId = radio.TemplateId == 0 ? oldRadio.TemplateId : radio.TemplateId;
        oldRadio.ValueEvaluation = radio.ValueEvaluation;
        oldRadio.Options = radio.Options;
        oldRadio.SelectedOption = radio.SelectedOption;
        await _unitOfWork.SaveChangesAsync();
        return oldRadio.ToDto();
    }
    

    // Template

    /// <summary>
    /// Retrieves a page of templates by user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose templates are to be retrieved.</param>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of templates per page.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the templates.</returns>
    public async Task<PageReponse<TemplateDto>> GetTemplatesByUserId(string userId, int index = 0, int count = 10)
    {
        var templates = await _unitOfWork.TemplatesRepository.Get(t => t.TeacherId == userId, includeProperties: "Criteria",index: index, count: count);
        var templatesDto = templates.ToList().ToDtos();
        if(templates == null) return new PageReponse<TemplateDto>(0, new List<TemplateDto>());
        Translator.TemplateMapper.Reset();
        return new PageReponse<TemplateDto>(templatesDto.Count(), templatesDto.Skip(index * count).Take(count));
    }

    /// <summary>
    /// Retrieves a page of empty templates by user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose empty templates are to be retrieved.</param>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of empty templates per page.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the empty templates.</returns>
    public async Task<PageReponse<TemplateDto>> GetEmptyTemplatesByUserId(string userId, int index = 0, int count = 10)
    {
        var templates = _unitOfWork.TemplatesRepository.Get(t => t.TeacherId == userId && t.Criteria.Count == 0, index: index, count: count).Result;
        if(templates == null) return new PageReponse<TemplateDto>(0, new List<TemplateDto>());
        Translator.TemplateMapper.Reset();
        return new PageReponse<TemplateDto>(templates.Count(), templates.Skip(index * count).Take(count).Select(t => t.ToDto()));
    }


    /// <summary>
    /// Retrieves a template by ID.
    /// </summary>
    /// <param name="userId">The ID of the user who owns the template.</param>
    /// <param name="templateId">The ID of the template to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, returning the template DTO if found, otherwise null.</returns>
    public Task<TemplateDto?> GetTemplateById(long templateId)
    {
        var template = _unitOfWork.TemplatesRepository.GetByIdAsync(templateId, t => t.Criteria).Result;
        if (template == null) return Task.FromResult<TemplateDto?>(null);
        Translator.TemplateMapper.Reset();
        return Task.FromResult(template.ToDto());
    }


    /// <summary>
    /// Adds a new template.
    /// </summary>
    /// <param name="userId">The ID of the user who owns the template.</param>
    /// <param name="template">The template DTO to add.</param>
    /// <returns>A task representing the asynchronous operation, returning the added template DTO.</returns>
    public async Task<TemplateDto?> PostTemplate(string userId, TemplateDto template)
    {
        var templateEntity = template.ToEntity();
        templateEntity.TeacherId = userId;
        await _unitOfWork.TemplatesRepository.Insert(templateEntity);
        await _unitOfWork.SaveChangesAsync();
        Translator.TemplateMapper.Reset();
        return templateEntity.ToDto();
    }


    /// <summary>
    /// Updates a template.
    /// </summary>
    /// <param name="templateId">The ID of the template to update.</param>
    /// <param name="template">The updated template DTO.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated template DTO if found, otherwise null.</returns>
    public async Task<TemplateDto?> PutTemplate(long templateId, TemplateDto template)
    {
        var oldTemplate = _unitOfWork.TemplatesRepository.GetByIdAsync(templateId, t => t.Criteria!).Result;
        if (oldTemplate == null) return null;
        oldTemplate.Name = template.Name;
        await _unitOfWork.SaveChangesAsync();
        Translator.TemplateMapper.Reset();
        return oldTemplate.ToDto();
    }


    /// <summary>
    /// Deletes a template by ID.
    /// </summary>
    /// <param name="templateId">The ID of the template to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
    public async Task<bool> DeleteTemplate(long templateId)
    {
        var template = await _unitOfWork.TemplatesRepository.GetByIdAsync(templateId, t => t.Criteria);
    
        if (template == null) return false;

        await _unitOfWork.TemplatesRepository.Delete(template);
        await _unitOfWork.SaveChangesAsync();

        return await _unitOfWork.TemplatesRepository.GetByIdAsync(templateId) == null;
    }


    //Lesson


    /// <summary>
    /// Retrieves a page of lessons.
    /// </summary>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of lessons per page.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the lessons.</returns>
    public Task<PageReponse<LessonReponseDto>> GetLessons(int index = 0, int count = 10)
    {
        var lessons = _unitOfWork.LessonsRepository.Get(includeProperties: "Teacher,Group", index: index, count: count).Result.ToList().ToReponseDtos();
        if (lessons == null) return Task.FromResult(new PageReponse<LessonReponseDto>(0, new List<LessonReponseDto>()));
        Translator.LessonMapper.Reset();
        return Task.FromResult(new PageReponse<LessonReponseDto>(lessons.Count(), lessons.Skip(index * count).Take(count)));
    }


    /// <summary>
    /// Retrieves a lesson by ID.
    /// </summary>
    /// <param name="id">The ID of the lesson to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, returning the lesson DTO if found, otherwise null.</returns>
    public  Task<LessonReponseDto?> GetLessonById(long id)
    {
        var lesson = _unitOfWork.LessonsRepository.GetByIdAsync(id, l => l.Teacher, l => l.Group).Result?.ToReponseDto();
        if(lesson == null) return Task.FromResult<LessonReponseDto?>(null);
        Translator.LessonMapper.Reset();
        return Task.FromResult(lesson);
    }


    /// <summary>
    /// Updates a lesson.
    /// </summary>
    /// <param name="id">The ID of the lesson to update.</param>
    /// <param name="newLesson">The updated lesson DTO.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated lesson DTO if found, otherwise null.</returns>
    public async Task<LessonReponseDto?> PutLesson(long id, LessonDto newLesson)
    {
        var lesson = _unitOfWork.LessonsRepository.GetByIdAsync(id).Result;
        if (lesson == null) return null;
        lesson.CourseName = newLesson.CourseName;
        lesson.Classroom = newLesson.Classroom;
        lesson.Start = newLesson.Start;
        lesson.End = newLesson.End;
        lesson.TeacherEntityId = newLesson.TeacherId;
        lesson.GroupNumber = newLesson.GroupNumber;
        lesson.GroupYear= newLesson.GroupYear;
        var teacher = await _unitOfWork.TeachersRepository.GetByIdAsync(newLesson.TeacherId, entity => entity.Lessons);
        if(teacher == null) throw new KeyNotFoundException("Teacher not found, update failed.");
        teacher?.Lessons.Add(lesson);
        var includesGroup = new List<Expression<Func<GroupEntity, object>>>(1);
        includesGroup.Add(g => g.Lessons);
        var group = await _unitOfWork.GroupsRepository.GetById(includesGroup, newLesson.GroupYear, newLesson.GroupNumber);
        if(group == null) throw new KeyNotFoundException("Group not found, update failed.");
        group?.Lessons.Add(lesson);
        await _unitOfWork.SaveChangesAsync();
        Translator.LessonMapper.Reset();
        return lesson.ToReponseDto();
    }


    /// <summary>
    /// Deletes a lesson by ID.
    /// </summary>
    /// <param name="id">The ID of the lesson to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
    public async Task<bool> DeleteLesson(long id)
    {
        var lesson = await _unitOfWork.LessonsRepository.GetByIdAsync(id);
        if (lesson == null)
        {
            return false;
        }
        await _unitOfWork.LessonsRepository.Delete(lesson);
        await _unitOfWork.SaveChangesAsync();
        return await _unitOfWork.LessonsRepository.GetByIdAsync(id) == null;
    }



    /// <summary>
    /// Adds a new lesson.
    /// </summary>
    /// <param name="lesson">The lesson DTO to add.</param>
    /// <returns>A task representing the asynchronous operation, returning the added lesson DTO.</returns>
    public async Task<LessonReponseDto?> PostLesson(LessonDto lesson)
    {
        var lessonEntity = lesson.ToEntity();
        await _unitOfWork.LessonsRepository.Insert(lessonEntity);
        var teacher = await _unitOfWork.TeachersRepository.GetByIdAsync(lesson.TeacherId, entity => entity.Lessons);
        if(teacher == null) throw new KeyNotFoundException("Teacher not found, insert failed.");
        teacher?.Lessons.Add(lessonEntity);
        var includesGroup = new List<Expression<Func<GroupEntity, object>>>(1);
        includesGroup.Add(g => g.Lessons);
        var group = await _unitOfWork.GroupsRepository.GetById(includesGroup,lesson.GroupYear, lesson.GroupNumber);
        if (group == null) throw new KeyNotFoundException("Group not found, insert failed.");
        group?.Lessons.Add(lessonEntity);
        await _unitOfWork.SaveChangesAsync();
        Translator.LessonMapper.Reset();
        return lessonEntity.ToReponseDto();
    }


    /// <summary>
    /// Retrieves a page of lessons by teacher ID.
    /// </summary>
    /// <param name="id">The ID of the teacher whose lessons are to be retrieved.</param>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of lessons per page.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the lessons.</returns>
    public Task<PageReponse<LessonReponseDto>> GetLessonsByTeacherId(string userId, int index = 0, int count = 10)
    {
        var lessons = _unitOfWork.LessonsRepository.Get(l => l.TeacherEntityId == userId, includeProperties: "Teacher,Group", index: index, count: count).Result.ToList().ToReponseDtos();
        if (lessons == null) return Task.FromResult(new PageReponse<LessonReponseDto>(0, new List<LessonReponseDto>()));
        Translator.LessonMapper.Reset();
        return Task.FromResult(new PageReponse<LessonReponseDto>(lessons.Count(), lessons.Skip(index * count).Take(count)));
    }


    //Evaluations


    /// <summary>
    /// Retrieves a page of evaluations.
    /// </summary>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of evaluations per page.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the evaluations.</returns>
    public Task<PageReponse<EvaluationReponseDto>> GetEvaluations(int index = 0, int count = 10)
    {
        var evals = _unitOfWork.EvaluationsRepository.Get(includeProperties: "Teacher,Template,Student", index: index, count: count).Result.ToList().ToReponseDtos();
        if(evals == null) return Task.FromResult(new PageReponse<EvaluationReponseDto>(0, new List<EvaluationReponseDto>()) );
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
        var eval = _unitOfWork.EvaluationsRepository.GetByIdAsync(id, e => e.Template, e => e.Student, e => e.Teacher).Result?.ToReponseDto();
        if (eval == null) return Task.FromResult<EvaluationReponseDto?>(null);
        Translator.EvaluationReponseMapper.Reset();
        return Task.FromResult(eval)!;
    }


    /// <summary>
    /// Retrieves a page of evaluations by teacher ID.
    /// </summary>
    /// <param name="id">The ID of the teacher whose evaluations are to be retrieved.</param>
    /// <param name="index">The index of the page.</param>
    /// <param name="count">The number of evaluations per page.</param>
    /// <returns>A task representing the asynchronous operation, returning a page response containing the evaluations.</returns>
    public Task<PageReponse<EvaluationReponseDto>> GetEvaluationsByTeacherId(string userId, int index = 0, int count = 10)
    {
        var evals = _unitOfWork.EvaluationsRepository.Get(e => e.TeacherId == userId, includeProperties: "Teacher,Template,Student", index: index, count: count).Result.ToList().ToReponseDtos();
        if(evals == null) return Task.FromResult(new PageReponse<EvaluationReponseDto>(0, new List<EvaluationReponseDto>()) );
        Translator.EvaluationMapper.Reset();
        return Task.FromResult(new PageReponse<EvaluationReponseDto>(evals.Count(), evals.Skip(count * index).Take(count)));
    }


    /// <summary>
    /// Adds a new evaluation.
    /// </summary>
    /// <param name="eval">The evaluation DTO to add.</param>
    /// <returns>A task representing the asynchronous operation, returning the added evaluation DTO.</returns>
    public async Task<EvaluationReponseDto?> PostEvaluation(EvaluationDto eval)
    {
        var evalEntity = eval.ToEntity();
        await _unitOfWork.EvaluationsRepository.Insert(evalEntity);
        var student = await _unitOfWork.StudentsRepository.GetByIdAsync(eval.StudentId, entity => entity.Evaluations);
        if(student == null) throw new KeyNotFoundException("Student not found, insert failed.");
        student?.Evaluations.Add(evalEntity);
        
        var template = await _unitOfWork.TemplatesRepository.GetByIdAsync(eval.TemplateId, entity => entity.Evaluation);
        if(template == null) throw new KeyNotFoundException("Template not found, insert failed.");
        template.Evaluation = evalEntity;
        
        eval.TeacherId = evalEntity.TeacherId = template.TeacherId;
        
        var teacher = await _unitOfWork.TeachersRepository.GetByIdAsync(evalEntity.TeacherId, entity => entity.Evaluations);
        if(teacher == null) throw new KeyNotFoundException("Teacher not found, insert failed.");
        teacher?.Evaluations.Add(evalEntity);
        
        await _unitOfWork.SaveChangesAsync();
        Translator.EvaluationMapper.Reset();
        return evalEntity.ToReponseDto();
    }


    /// <summary>
    /// Updates an evaluation.
    /// </summary>
    /// <param name="id">The ID of the evaluation to update.</param>
    /// <param name="newEval">The updated evaluation DTO.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated evaluation DTO if found, otherwise null.</returns>
    public async Task<EvaluationReponseDto?> PutEvaluation(long id, EvaluationDto newEval)
    {
        var eval = _unitOfWork.EvaluationsRepository.GetByIdAsync(id).Result;
        if (eval == null) return null;
        eval.CourseName = newEval.CourseName;
        eval.PairName = newEval.PairName;
        eval.Grade = newEval.Grade;
        eval.Date = newEval.Date;
        eval.StudentId= newEval.StudentId;
        eval.TemplateId=newEval.TemplateId;
        
        var student = await _unitOfWork.StudentsRepository.GetByIdAsync(newEval.StudentId, entity => entity.Evaluations);
        if(student == null) throw new KeyNotFoundException("Student not found, update failed.");
        student?.Evaluations.Add(eval);
        
        var template = await _unitOfWork.TemplatesRepository.GetByIdAsync(newEval.TemplateId, entity => entity.Evaluation);
        if(template == null) throw new KeyNotFoundException("Template not found, update failed.");
        if(template.Evaluation != null && template.Evaluation.Id != id) throw new InvalidOperationException("Template already has an evaluation, update failed.");
        template.Evaluation = eval;
        eval.TeacherId = template.TeacherId;
        
        var teacher = await _unitOfWork.TeachersRepository.GetByIdAsync(eval.TeacherId, entity => entity.Evaluations);
        if(teacher == null) throw new KeyNotFoundException("Teacher not found, update failed.");
        teacher?.Evaluations.Add(eval);
        

        await _unitOfWork.SaveChangesAsync();
        Translator.EvaluationMapper.Reset();
        return eval!.ToReponseDto();
    }


    /// <summary>
    /// Deletes an evaluation by ID.
    /// </summary>
    /// <param name="id">The ID of the evaluation to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the deletion was successful, otherwise false.</returns>
    public async Task<bool> DeleteEvaluation(long id)
    {
        var evaluation = await _unitOfWork.EvaluationsRepository.GetByIdAsync(id);
        if (evaluation == null)
        {
            return false;
        }

        await _unitOfWork.EvaluationsRepository.Delete(evaluation);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
    
    public async Task<TeacherDto> PostTeacher(TeacherDto teacher)
    {
        var teacherEntity = teacher.ToEntity();
        await _unitOfWork.TeachersRepository.Insert(teacherEntity);
        await _unitOfWork.SaveChangesAsync();
        Translator.TeacherMapper.Reset();
        return teacherEntity.ToDto();
    }
}