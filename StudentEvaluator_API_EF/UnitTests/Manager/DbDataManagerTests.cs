using API_Dto;
using EF_DbContextLib;
using EF_StubbedContextLib;
using Entities2Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EF_UnitTests.Manager;

public class DbDataManagerTests
{
    private DbDataManager CreateManagerWithInMemoryDb()
    {
        // Création d'une base de données en mémoire avec un nom unique
        var dbName = Guid.NewGuid().ToString(); 
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
    
        var context = new StubbedContext(options);
        return new DbDataManager(context, new UnitOfWork.UnitOfWork(context));
    }

    [Fact]
    public async Task DeleteStudent_WhenStudentExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new StudentDto { Id = 1, Name = "John", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 });
        
        // Act
        var result = await manager.DeleteStudent(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteStudent_WhenStudentDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        
        // Act
        var result = await manager.DeleteStudent(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetStudentById_WhenStudentExists_ReturnsStudentDto()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new StudentDto { Id = 2, Name = "Jane", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 });

        // Act
        var student = await manager.GetStudentById(2);

        // Assert
        Assert.NotNull(student);
        Assert.Equal(2, student.Id);
        Assert.Equal("Jane", student.Name);
    }

    [Fact]
    public async Task GetStudentById_WhenStudentDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var student = await manager.GetStudentById(999);

        // Assert
        Assert.Null(student);
    }
    
    [Fact]
    public async Task GetStudents_WhenStudentsExist_ReturnsListOfStudentDtos()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new StudentDto { Id = 1, Name = "John", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 });
        await manager.PostStudent(new StudentDto { Id = 2, Name = "Jane", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 });

        // Act
        var students = await manager.GetStudents(0, 10);

        // Assert
        Assert.NotNull(students);
        Assert.Equal(2, students.NbElement);
    }
    
    [Fact]
    public async Task PutStudent_WhenStudentExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new StudentDto { Id = 1, Name = "John", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 });
        var studentDto = new StudentDto { Id = 1, Name = "Jane", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 };

        // Act
        var result = await manager.PutStudent(1, studentDto);

        // Assert 
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Jane", result.Name);
        Assert.Equal("Doe", result.Lastname);
        Assert.Equal("http://example.com/photo.jpg", result.UrlPhoto);
        Assert.Equal(1, result.GroupNumber);
        Assert.Equal(1, result.GroupYear);
    }
    
    [Fact]
    public async Task PutStudent_WhenStudentDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var studentDto = new StudentDto { Id = 1, Name = "Jane", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 };

        // Act
        var result = await manager.PutStudent(1, studentDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddGroup_AddsGroupCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var groupDto = new GroupDto { GroupYear = 1, GroupNumber = 1 };

        // Act
        var addedGroup = await manager.PostGroup(groupDto);

        // Assert
        Assert.NotNull(addedGroup);
        Assert.Equal(1, addedGroup.GroupYear);
        Assert.Equal(1, addedGroup.GroupNumber);
    }

    [Fact]
    public async Task GetGroupByIds_ReturnsCorrectGroup()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var groupDto = new GroupDto { GroupYear = 1, GroupNumber = 1 };
        await manager.PostGroup(groupDto);

        // Act
        var retrievedGroup = await manager.GetGroupByIds(1, 1);

        // Assert
        Assert.NotNull(retrievedGroup);
        Assert.Equal(1, retrievedGroup.GroupYear);
        Assert.Equal(1, retrievedGroup.GroupNumber);
    }

    [Fact]
    public async Task DeleteGroup_DeletesGroupCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var groupDto = new GroupDto { GroupYear = 1, GroupNumber = 1 };
        await manager.PostGroup(groupDto);

        // Act
        var result = await manager.DeleteGroup(1, 1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task DeleteGroup_WhenGroupDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var result = await manager.DeleteGroup(1, 1);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task GetGroups_WhenGroupsExist_ReturnsListOfGroupDtos()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new GroupDto { GroupYear = 1, GroupNumber = 1 });
        await manager.PostGroup(new GroupDto { GroupYear = 1, GroupNumber = 2 });

        // Act
        var groups = await manager.GetGroups(0, 10);

        // Assert
        Assert.NotNull(groups);
        Assert.Equal(2, groups.NbElement);
    }
    
    [Fact]
    public async Task AddTemplate_AddsTemplateCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var templateDto = new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] };

        // Act
        var addedTemplate = await manager.PostTemplate("1", templateDto);

        // Assert
        Assert.NotNull(addedTemplate);
        Assert.Equal(1, addedTemplate.Id);
        Assert.Equal("Template 1", addedTemplate.Name);
        Assert.Empty(addedTemplate.Criterias);
    }
    
    [Fact]
    public async Task GetTemplateById_ReturnsCorrectTemplate()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var templateDto = new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] };
        await manager.PostTemplate("1", templateDto);

        // Act
        var retrievedTemplate = await manager.GetTemplateById(1);

        // Assert
        Assert.NotNull(retrievedTemplate);
        Assert.Equal(1, retrievedTemplate.Id);
        Assert.Equal("Template 1", retrievedTemplate.Name);
        Assert.Empty(retrievedTemplate.Criterias);
    }
    
    [Fact]
    public async Task GetTemplateById_WhenTemplateDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var retrievedTemplate = await manager.GetTemplateById(1);

        // Assert
        Assert.Null(retrievedTemplate);
    }
    
    [Fact]
    public async Task DeleteTemplate_DeletesTemplateCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var templateDto = new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] };
        await manager.PostTemplate("1", templateDto);

        // Act
        var result = await manager.DeleteTemplate(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task DeleteTemplate_WhenTemplateDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var result = await manager.DeleteTemplate(1);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task PutTemplate_WhenTemplateExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });
        var templateDto = new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] };

        // Act
        var result = await manager.PutTemplate(1, templateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Template 1", result.Name);
        Assert.Empty(result.Criterias);
    }
    
    [Fact]
    public async Task PutTemplate_WhenTemplateDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var templateDto = new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] };

        // Act
        var result = await manager.PutTemplate(1, templateDto);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetTemplatesByUserId_WhenTemplatesExist_ReturnsListOfTemplateDtos()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var criteria = new TextCriteriaDto() { Id = 1, Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [criteria] });
        await manager.PostTemplate("1", new TemplateDto { Id = 2, Name = "Template 2", Criterias = [] });

        // Act
        var templates = await manager.GetTemplatesByUserId("1", 0, 10);

        // Assert
        Assert.NotNull(templates);
        Assert.Equal(2, templates.NbElement);
    }

    
    [Fact]
    public async Task GetEmptyTemplatesByUserId_WhenTemplatesExist_ReturnsListOfTemplateDtos()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var criteria = new TextCriteriaDto() { Id = 1, Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [criteria] });
        await manager.PostTemplate("1", new TemplateDto { Id = 2, Name = "Template 2", Criterias = [] });
        
        await manager.PostEvaluation(new EvaluationDto { Id = 1, StudentId = 1, TemplateId = 1, TeacherId = "1", CourseName = "Entity Framework", Date = DateTime.Now, PairName = "toto", Grade = 2});

        // Act
        var templates = await manager.GetEmptyTemplatesByUserId("1", 0, 10);

        // Assert
        Assert.NotNull(templates);
        Assert.Equal(1,  templates.NbElement);
    }
    
    // Lesson
    /*
    [Fact]
    public async Task AddLesson_AddsLessonCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var lessonDto = new LessonDto { CourseName = "Entity Framework", Start = DateTime.Now, End = DateTime.Now, GroupYear = 1, GroupNumber = 1, Classroom = "Classroom 1", TeacherId = 1 };
        await manager.PostTeacher(new TeacherDto() { Id = 1, Username = "John", Password = "Doe" });
        await manager.PostGroup(new GroupDto { GroupYear = 1, GroupNumber = 1 });

        // Act
        var addedLesson = await manager.PostLesson(lessonDto);

        // Assert
        Assert.NotNull(addedLesson);
        Assert.Equal(1, addedLesson.Id);
        Assert.Equal("Entity Framework", addedLesson.CourseName);
        Assert.Equal(1, addedLesson.Group.GroupYear);
        Assert.Equal(1, addedLesson.Group.GroupNumber);
        Assert.Equal("Classroom 1", addedLesson.Classroom);
        Assert.Equal(1, addedLesson.Teacher.Id);
    }
    
    [Fact]
    public async Task GetLessonById_ReturnsCorrectLesson()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var lessonDto = new LessonDto { CourseName = "Entity Framework", Start = DateTime.Now, End = DateTime.Now, GroupYear = 1, GroupNumber = 1, Classroom = "Classroom 1", TeacherId = 1 };
        await manager.PostTeacher(new TeacherDto() { Id = 1, Username = "John", Password = "Doe", roles = ["Teacher"] });
        await manager.PostGroup(new GroupDto { GroupYear = 1, GroupNumber = 1 });
        await manager.PostLesson(lessonDto);

        // Act
        var retrievedLesson = await manager.GetLessonById(1);

        // Assert
        Assert.NotNull(retrievedLesson);
        Assert.Equal(1, retrievedLesson.Id);
        Assert.Equal("Entity Framework", retrievedLesson.CourseName);
        Assert.Equal(1, retrievedLesson.Group.GroupYear);
        Assert.Equal(1, retrievedLesson.Group.GroupNumber);
        Assert.Equal("Classroom 1", retrievedLesson.Classroom);
        Assert.Equal(1, retrievedLesson.Teacher.Id);
    }
    */
    [Fact]
    public async Task GetLessonById_WhenLessonDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var retrievedLesson = await manager.GetLessonById(1);

        // Assert
        Assert.Null(retrievedLesson);
    }
    
    /*
    [Fact]
    public async Task DeleteLesson_DeletesLessonCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var lessonDto = new LessonDto { CourseName = "Entity Framework", Start = DateTime.Now, End = DateTime.Now, GroupYear = 1, GroupNumber = 1, Classroom = "Classroom 1", TeacherId = 1 };
        await manager.PostTeacher(new TeacherDto() { Id = 1, Username = "John", Password = "Doe", roles = ["Teacher"] });
        await manager.PostGroup(new GroupDto { GroupYear = 1, GroupNumber = 1 });
        await manager.PostLesson(lessonDto);

        // Act
        var result = await manager.DeleteLesson(1);

        // Assert
        Assert.True(result);
    }
    
    */
    
    [Fact]
    public async Task DeleteLesson_WhenLessonDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var result = await manager.DeleteLesson(1);

        // Assert
        Assert.False(result);
    }
    
    /*
    [Fact]
    public async Task PutLesson_WhenLessonExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTeacher(new TeacherDto() { Id = 1, Username = "John", Password = "Doe", roles = ["Teacher"] });
        await manager.PostGroup(new GroupDto { GroupYear = 1, GroupNumber = 1 });
        await manager.PostLesson(new LessonDto { CourseName = "Entity Framework", Start = DateTime.Now, End = DateTime.Now, GroupYear = 1, GroupNumber = 1, Classroom = "Classroom 1", TeacherId = 1 });
        var lessonDto = new LessonDto { CourseName = "Entity Framework", Start = DateTime.Now, End = DateTime.Now, GroupYear = 1, GroupNumber = 1, Classroom = "Classroom 1", TeacherId = 1 };

        // Act
        var result = await manager.PutLesson(1, lessonDto);

        // Assert 
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Entity Framework", result.CourseName);
        Assert.Equal(1, result.Group.GroupYear);
        Assert.Equal(1, result.Group.GroupNumber);
        Assert.Equal("Classroom 1", result.Classroom);
        Assert.Equal(1, result.Teacher.Id);
    }
    */
    
    [Fact]
    public async Task PutLesson_WhenLessonDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var lessonDto = new LessonDto {CourseName = "Entity Framework", Start = DateTime.Now, End = DateTime.Now, GroupYear = 1, GroupNumber = 1, Classroom = "Classroom 1", TeacherId = "1" };

        // Act
        var result = await manager.PutLesson(1, lessonDto);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task AddCriterion_AddsCriterionCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var textCriteriaDto = new TextCriteriaDto { Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteriaDto = new SliderCriteriaDto { Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteriaDto = new RadioCriteriaDto { Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"],  SelectedOption = "Option 1", TemplateId = 1};
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });

        // Act
        var addedCriterion = await manager.PostTextCriterion(1, textCriteriaDto);
        var addedCriterion2 = await manager.PostSliderCriterion(1, sliderCriteriaDto);
        var addedCriterion3 = await manager.PostRadioCriterion(1, radioCriteriaDto);
        

        // Assert
        Assert.NotNull(addedCriterion);
        Assert.NotNull(addedCriterion2);
        Assert.NotNull(addedCriterion3);
        Assert.Equal("Criteria 1", addedCriterion.Name);
        Assert.Equal(1, addedCriterion.ValueEvaluation);
        Assert.Equal("Text 1", addedCriterion.Text);
        Assert.Equal(1, addedCriterion.TemplateId);
        Assert.Equal("Criteria 2", addedCriterion2.Name);
        Assert.Equal(1, addedCriterion2.ValueEvaluation);
        Assert.Equal(2, addedCriterion2.Value);
        Assert.Equal(1, addedCriterion2.TemplateId);
        Assert.Equal("Criteria 3", addedCriterion3.Name);
        Assert.Equal(1, addedCriterion3.ValueEvaluation);
        Assert.Equal(["Option 1", "Option 2"], addedCriterion3.Options);
        Assert.Equal("Option 1", addedCriterion3.SelectedOption);
        Assert.Equal(1, addedCriterion3.TemplateId);
    }
    
    [Fact]
    public async Task AddCriterion_WhenTemplateDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var textCriteriaDto = new TextCriteriaDto { Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteriaDto = new SliderCriteriaDto { Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteriaDto = new RadioCriteriaDto { Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"],  SelectedOption = "Option 1", TemplateId = 1};

        // Act
        var addedCriterion = await manager.PostTextCriterion(1, textCriteriaDto);
        var addedCriterion2 = await manager.PostSliderCriterion(1, sliderCriteriaDto);
        var addedCriterion3 = await manager.PostRadioCriterion(1, radioCriteriaDto);

        // Assert
        Assert.Null(addedCriterion);
        Assert.Null(addedCriterion2);
        Assert.Null(addedCriterion3);
    }
    
    [Fact]
    public async Task GetCriterionById_ReturnsCorrectCriterion()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var textCriteriaDto = new TextCriteriaDto { Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteriaDto = new SliderCriteriaDto { Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteriaDto = new RadioCriteriaDto { Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"],  SelectedOption = "Option 1", TemplateId = 1};
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });
        await manager.PostTextCriterion(1, textCriteriaDto);
        await manager.PostSliderCriterion(1, sliderCriteriaDto);
        await manager.PostRadioCriterion(1, radioCriteriaDto);

        // Act
        var textCriteria = await manager.GetTextCriterionByIds(1);
        var sliderCriteria = await manager.GetSliderCriterionByIds(2);
        var radioCriteria = await manager.GetRadioCriterionByIds(3);

        // Assert
        Assert.NotNull(textCriteria);
        Assert.Equal("Criteria 1", textCriteria.Name);
        Assert.Equal(1, textCriteria.ValueEvaluation);
        Assert.Equal("Text 1", textCriteria.Text);
        Assert.Equal(1, textCriteria.TemplateId);
        Assert.NotNull(sliderCriteria);
        Assert.Equal("Criteria 2", sliderCriteria.Name);
        Assert.Equal(1, sliderCriteria.ValueEvaluation);
        Assert.Equal(2, sliderCriteria.Value);
        Assert.Equal(1, sliderCriteria.TemplateId);
        Assert.NotNull(radioCriteria);
        Assert.Equal("Criteria 3", radioCriteria.Name);
        Assert.Equal(1, radioCriteria.ValueEvaluation);
        Assert.Equal(["Option 1", "Option 2"], radioCriteria.Options);
        Assert.Equal("Option 1", radioCriteria.SelectedOption);
        Assert.Equal(1, radioCriteria.TemplateId);
    }
    
    [Fact]
    public async Task GetCriterionById_WhenCriterionDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var textCriteria = await manager.GetTextCriterionByIds(1);
        var sliderCriteria = await manager.GetSliderCriterionByIds(2);
        var radioCriteria = await manager.GetRadioCriterionByIds(3);

        // Assert
        Assert.Null(textCriteria);
        Assert.Null(sliderCriteria);
        Assert.Null(radioCriteria);
    }
    
    [Fact]
    public async Task DeleteCriterion_DeletesCriterionCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var textCriteriaDto = new TextCriteriaDto { Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteriaDto = new SliderCriteriaDto { Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteriaDto = new RadioCriteriaDto { Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"],  SelectedOption = "Option 1", TemplateId = 1};
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });
        await manager.PostTextCriterion(1, textCriteriaDto);
        await manager.PostSliderCriterion(1, sliderCriteriaDto);
        await manager.PostRadioCriterion(1, radioCriteriaDto);

        // Act
        var result = await manager.DeleteCriteria(1);
        var result2 = await manager.DeleteCriteria(2);
        var result3 = await manager.DeleteCriteria(3);

        // Assert
        Assert.True(result);
        Assert.True(result2);
        Assert.True(result3);
    }
    
    [Fact]
    public async Task DeleteCriterion_WhenCriterionDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var result = await manager.DeleteCriteria(1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task PutCriterion_WhenCriterionExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var textCriteriaDto = new TextCriteriaDto
            { Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteriaDto = new SliderCriteriaDto
            { Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteriaDto = new RadioCriteriaDto
        {
            Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"], SelectedOption = "Option 1",
            TemplateId = 1
        };
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });
        await manager.PostTextCriterion(1, textCriteriaDto);
        await manager.PostSliderCriterion(1, sliderCriteriaDto);
        await manager.PostRadioCriterion(1, radioCriteriaDto);
        var textCriteria = new TextCriteriaDto
            { Id = 1, Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteria = new SliderCriteriaDto
            { Id = 2, Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteria = new RadioCriteriaDto
        {
            Id = 3, Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"],
            SelectedOption = "Option 1", TemplateId = 1
        };

        // Act
        var result = await manager.PutTextCriterion(1, textCriteria);
        var result2 = await manager.PutSliderCriterion(2, sliderCriteria);
        var result3 = await manager.PutRadioCriterion(3, radioCriteria);

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
        Assert.Equal(1, result.Id);
        Assert.Equal("Criteria 1", result.Name);
        Assert.Equal(1, result.ValueEvaluation);
        Assert.Equal("Text 1", result.Text);
        Assert.Equal(1, result.TemplateId);
        Assert.Equal(2, result2.Id);
        Assert.Equal("Criteria 2", result2.Name);
        Assert.Equal(1, result2.ValueEvaluation);
        Assert.Equal(2, result2.Value);
        Assert.Equal(1, result2.TemplateId);
        Assert.Equal(3, result3.Id);
        Assert.Equal("Criteria 3", result3.Name);
        Assert.Equal(1, result3.ValueEvaluation);
        Assert.Equal(["Option 1", "Option 2"], result3.Options);
        Assert.Equal("Option 1", result3.SelectedOption);
        Assert.Equal(1, result3.TemplateId);
    }
    
    [Fact]
    public async Task PutCriterion_WhenCriterionDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var textCriteria = new TextCriteriaDto
            { Id = 1, Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteria = new SliderCriteriaDto
            { Id = 2, Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteria = new RadioCriteriaDto
        {
            Id = 3, Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"],
            SelectedOption = "Option 1", TemplateId = 1
        };

        // Act
        var result = await manager.PutTextCriterion(1, textCriteria);
        var result2 = await manager.PutSliderCriterion(2, sliderCriteria);
        var result3 = await manager.PutRadioCriterion(3, radioCriteria);

        // Assert
        Assert.Null(result);
        Assert.Null(result2);
        Assert.Null(result3);
    }
    
    [Fact]
    public async Task GetCriterionsByTemplateId_WhenCriterionsExist_ReturnsListOfCriterionDtos()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var textCriteriaDto = new TextCriteriaDto { Name = "Criteria 1", ValueEvaluation = 1, Text = "Text 1", TemplateId = 1 };
        var sliderCriteriaDto = new SliderCriteriaDto { Name = "Criteria 2", ValueEvaluation = 1, Value = 2, TemplateId = 1 };
        var radioCriteriaDto = new RadioCriteriaDto { Name = "Criteria 3", ValueEvaluation = 1, Options = ["Option 1", "Option 2"],  SelectedOption = "Option 1", TemplateId = 1};
        await manager.PostTemplate("1", new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });
        await manager.PostTextCriterion(1, textCriteriaDto);
        await manager.PostSliderCriterion(1, sliderCriteriaDto);
        await manager.PostRadioCriterion(1, radioCriteriaDto);

        // Act
        var criterions = await manager.GetCriterionsByTemplateId(1);

        // Assert
        Assert.NotNull(criterions);
        Assert.Equal(3, criterions.NbElement);
    }
    
    // Evaluation
    /*
    [Fact]
    public async Task AddEvaluation_AddsEvaluationCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var evaluationDto = new EvaluationDto { StudentId = 1, TemplateId = 1, TeacherId = 1, CourseName = "Entity Framework", Date = DateTime.Now, PairName = "toto", Grade = 2 };
        await manager.PostStudent(new StudentDto { Id = 1, Name = "Jane", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 });
        await manager.PostTemplate(1, new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });
        await manager.PostTeacher(new TeacherDto { Id = 1, Username = "John", Password = "Doe", roles = ["Teacher"] });

        // Act
        var addedEvaluation = await manager.PostEvaluation(evaluationDto);

        // Assert
        Assert.NotNull(addedEvaluation);
        Assert.Equal(1, addedEvaluation.Student.Id);
        Assert.Equal(1, addedEvaluation.Template.Id);
        Assert.Equal(1, addedEvaluation.Teacher.Id);
        Assert.Equal("Entity Framework", addedEvaluation.CourseName);
        Assert.Equal(2, addedEvaluation.Grade);
    }
    
    [Fact]
    public async Task GetEvaluationById_ReturnsCorrectEvaluation()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var evaluationDto = new EvaluationDto { StudentId = 1, TemplateId = 1, TeacherId = 1, CourseName = "Entity Framework", Date = DateTime.Now, PairName = "toto", Grade = 2 };
        await manager.PostStudent(new StudentDto { Id = 1, Name = "Jane", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 });
        await manager.PostTemplate(1, new TemplateDto { Id = 1, Name = "Template 1", Criterias = [] });
        await manager.PostTeacher(new TeacherDto { Id = 1, Username = "John", Password = "Doe", roles = ["Teacher"] });
        await manager.PostEvaluation(evaluationDto);

        // Act
        var retrievedEvaluation = await manager.GetEvaluationById(1);

        // Assert
        Assert.NotNull(retrievedEvaluation);
        Assert.Equal(1, retrievedEvaluation.Student.Id);
        Assert.NotNull(retrievedEvaluation.Template);
        Assert.Equal(1, retrievedEvaluation.Template.Id);
        Assert.Equal(1, retrievedEvaluation.Teacher.Id);
        Assert.Equal("Entity Framework", retrievedEvaluation.CourseName);
        Assert.Equal(2, retrievedEvaluation.Grade);
    }
    */
    
    [Fact]
    public async Task GetEvaluationById_WhenEvaluationDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var retrievedEvaluation = await manager.GetEvaluationById(1);

        // Assert
        Assert.Null(retrievedEvaluation);
    }
    
    [Fact]
    public async Task DeleteEvaluation_WhenEvaluationDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var result = await manager.DeleteEvaluation(1);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task PutEvaluation_WhenEvaluationDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var evaluation = new EvaluationDto { Id = 1, StudentId = 1, TemplateId = 1, TeacherId = "1", CourseName = "Entity Framework", Date = DateTime.Now, PairName = "toto", Grade = 2 };

        // Act
        var result = await manager.PutEvaluation(1, evaluation);

        // Assert
        Assert.Null(result);
    }
    
}