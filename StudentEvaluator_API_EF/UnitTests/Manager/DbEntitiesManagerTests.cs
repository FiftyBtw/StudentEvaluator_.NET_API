using Microsoft.EntityFrameworkCore;
using EF_StubbedContextLib;
using Client_Model;
using EF_DbContextLib;
using EF_Entities;
using Model2Entities;

namespace EF_UnitTests.Manager;

public class DbEntitiesManagerTests
{
    private DbEntitiesManager CreateManagerWithInMemoryDb()
    {
        var dbName = Guid.NewGuid().ToString(); 
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
    
        var context = new StubbedContext(options);
        return new DbEntitiesManager(context);
    }

    [Fact]
    public async Task PostStudent_AddsStudentCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newStudent = new Student (1, "John Doe", "Doe", "https://example.com", 1,1);

        // Act
        var addedStudent = await manager.PostStudent(newStudent);

        // Assert
        Assert.NotNull(addedStudent);
        Assert.Equal("John Doe", addedStudent.Name);
    }

    [Fact]
    public async Task GetStudentById_WhenStudentExists_ReturnsStudent()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new Student (1, "John Doe", "Doe", "https://example.com", 1,1));

        // Act
        var student = await manager.GetStudentById(1);

        // Assert
        Assert.NotNull(student);
        Assert.Equal("John Doe", student.Name);
    }

    [Fact]
    public async Task DeleteStudent_WhenStudentExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new Student (1, "John Doe", "Doe", "https://example.com", 1,1));


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
        var result = await manager.DeleteStudent(1);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task PutStudent_WhenStudentExists_UpdatesStudent()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new Student (1, "John", "Doe", "https://example.com", 1,1));
        var updatedStudent = new Student (1, "Jane", "Doe", "https://example.com", 1,1);

        // Act
        var result = await manager.PutStudent(1, updatedStudent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jane", result.Name);
    }
    
    [Fact]
    public void PutStudent_WhenStudentDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var updatedStudent = new Student (1, "Jane", "Doe", "https://example.com", 1,1);

        // Act
        var result = manager.PutStudent(1, updatedStudent).Result;

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetStudents_WhenStudentsExist_ReturnsStudents()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostStudent(new Student (1, "John", "Doe", "https://example.com", 1,1));
        await manager.PostStudent(new Student (2, "Jane", "Doe", "https://example.com", 1,1));

        // Act
        var students = await manager.GetStudents(0, 10);

        // Assert
        Assert.NotNull(students);
        Assert.Equal(2, students.NbElement);
    }
    
    [Fact]
    public async Task PostGroup_AddsGroupCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newGroup = new Group (1, 1);

        // Act
        var addedGroup = await manager.PostGroup(newGroup);

        // Assert
        Assert.NotNull(addedGroup);
        Assert.Equal(1, addedGroup.GroupYear);
        Assert.Equal(1, addedGroup.GroupNumber);
    }
    
    [Fact]
    public async Task GetGroupById_WhenGroupExists_ReturnsGroup()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));

        // Act
        var group = await manager.GetGroupByIds(1, 1);

        // Assert
        Assert.NotNull(group);
        Assert.Equal(1, group.GroupYear);
        Assert.Equal(1, group.GroupNumber);
    }
    
    [Fact]
    public async Task DeleteGroup_WhenGroupExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));

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
    public async Task GetGroups_WhenGroupsExist_ReturnsGroups()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));
        await manager.PostGroup(new Group (1, 2));

        // Act
        var groups = await manager.GetGroups(0, 10);

        // Assert
        Assert.NotNull(groups);
        Assert.Equal(2, groups.NbElement);
    }
    
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
        var updatedEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 2, "toto", "1", 1, 1);

        // Act
        var result = await manager.PutEvaluation(1, updatedEvaluation);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task PostTemplate_AddsTemplateCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newTemplate = new Template(1, "Entity Framework", new List<Criteria>());

        // Act
        var addedTemplate = await manager.PostTemplate("1", newTemplate);

        // Assert
        Assert.NotNull(addedTemplate);
        Assert.Equal("Entity Framework", addedTemplate.Name);
    }
    
    [Fact]
    public async Task DeleteTemplate_WhenTemplateExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTemplate("1", new Template(1, "Entity Framework", new List<Criteria>()));

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
    public async Task PutTemplate_WhenTemplateExists_UpdatesTemplate()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTemplate("1", new Template(1, "Entity Framework", new List<Criteria>()));
        var updatedTemplate = new Template(1, "Entity Framework", new List<Criteria>());

        // Act
        var result = await manager.PutTemplate(1, updatedTemplate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Entity Framework", result.Name);
    }
    
    [Fact]
    public void PutTemplate_WhenTemplateDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var updatedTemplate = new Template(1, "Entity Framework", new List<Criteria>());

        // Act
        var result = manager.PutTemplate(1, updatedTemplate).Result;

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetTemplatesByUserId_WhenTemplatesExist_ReturnsTemplates()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTemplate("1", new Template(1, "Entity Framework", new List<Criteria>()));
        await manager.PostTemplate("1", new Template(2, "Entity Framework", new List<Criteria>()));

        // Act
        var templates = await manager.GetTemplatesByUserId("1", 0, 10);

        // Assert
        Assert.NotNull(templates);
        Assert.Equal(2, templates.NbElement);
    }
    
    [Fact]
    public async Task GetEmptyTemplatesByUserId_WhenTemplatesExist_ReturnsTemplates()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTemplate("1", new Template(1, "Entity Framework", new List<Criteria>()));
        await manager.PostTemplate("1", new Template(2, "Entity Framework", new List<Criteria>()));

        // Act
        var templates = await manager.GetEmptyTemplatesByUserId("1", 0, 10);

        // Assert
        Assert.NotNull(templates);
        Assert.Equal(2, templates.NbElement);
    }
    
    [Fact]
    public async Task GetTemplateById_WhenTemplateExists_ReturnsTemplate()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTemplate("1", new Template(1, "Entity Framework", new List<Criteria>()));

        // Act
        var template = await manager.GetTemplateById(1);

        // Assert
        Assert.NotNull(template);
        Assert.Equal("Entity Framework", template.Name);
    }
    
    /*
     // Lesson
       public async Task<PageReponse<Lesson>> GetLessons(int index = 0, int count = 10)
       {
           var lessons = _libraryContext.LessonSet.ToModels();
           return await Task.FromResult(new PageReponse<Lesson>(lessons.Count(),
               lessons.Skip(index * count).Take(count)));
       }
       public async Task<Lesson?> GetLessonById(long id)
       {
           var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id)?.ToModel();
           return await Task.FromResult(lesson);
       }
       public async Task<PageReponse<Lesson>> GetLessonsByTeacherId(string id, int index = 0, int count = 10)
       {
           var lessons = _libraryContext.LessonSet.Where(l => l.TeacherEntityId == id).ToModels();
           return await Task.FromResult(new PageReponse<Lesson>(lessons.Count(),
               lessons.Skip(index * count).Take(count)));
       }
       public async Task<Lesson?> PostLesson(LessonCreation lesson)
       {
           var lessonEntity = lesson.ToEntity();
           await _libraryContext.LessonSet.AddAsync(lessonEntity);
           await _libraryContext.SaveChangesAsync();
           return await Task.FromResult(_libraryContext.LessonSet.FirstOrDefault(l => l.Id == lessonEntity.Id)
               ?.ToModel());
       }
       public async Task<Lesson?> PutLesson(long id, LessonCreation lesson)
       {
           var existingLesson = await _libraryContext.LessonSet.FindAsync(id);
           if (existingLesson == null) return null;
           existingLesson.CourseName = lesson.CourseName;
           await _libraryContext.SaveChangesAsync();
           return existingLesson.ToModel();
       }
       public async Task<bool> DeleteLesson(long id)
       {
           var lesson = await _libraryContext.LessonSet.FindAsync(id);
           if (lesson == null) return false;
           _libraryContext.LessonSet.Remove(lesson);
           await _libraryContext.SaveChangesAsync();
           return true;
       }
       // Evaluation
       public async Task<PageReponse<Evaluation>> GetEvaluations(int index = 0, int count = 10)
       {
           var evaluations = _libraryContext.EvaluationSet.ToModels();
           return await Task.FromResult(new PageReponse<Evaluation>(evaluations.Count(),
               evaluations.Skip(index * count).Take(count)));
       }
       public async Task<Evaluation?> GetEvaluationById(long id)
       {
           var evaluation = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id)?.ToModel();
           return await Task.FromResult(evaluation);
       }
       public async Task<PageReponse<Evaluation>> GetEvaluationsByTeacherId(string id, int index = 0, int count = 10)
       {
           var evaluations = _libraryContext.EvaluationSet.Where(e => e.TeacherId == id).ToModels();
           return await Task.FromResult(new PageReponse<Evaluation>(evaluations.Count(),
               evaluations.Skip(index * count).Take(count)));
       }
       public async Task<Evaluation?> PostEvaluation(EvaluationCreation evaluation)
       {
           var evaluationEntity = evaluation.ToEntity();
           _libraryContext.EvaluationSet.AddAsync(evaluationEntity);
           _libraryContext.SaveChanges();
           return await Task.FromResult(_libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == evaluationEntity.Id)
               ?.ToModel());
       }
       public async Task<Evaluation?> PutEvaluation(long id, EvaluationCreation evaluation)
       {
           var existingEvaluation = await _libraryContext.EvaluationSet.FindAsync(id);
           if (existingEvaluation == null)
           {
               return null;
           }
           existingEvaluation.CourseName = evaluation.CourseName;
           existingEvaluation.Date = evaluation.Date;
           existingEvaluation.Grade = evaluation.Grade;
           existingEvaluation.PairName = evaluation.PairName;
           existingEvaluation.TeacherId = evaluation.TeacherId;
           existingEvaluation.TemplateId = evaluation.TemplateId;
           existingEvaluation.StudentId = evaluation.StudentId;
           await _libraryContext.SaveChangesAsync();
           return existingEvaluation.ToModel();
       }
     */
    
    [Fact]
    public async Task PostLesson_AddsLessonCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));
        await manager.PostTeacher(new Teacher("1", "John", "Doe", []));
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1);

        // Act
        var addedLesson = await manager.PostLesson(newLesson);

        // Assert
        Assert.NotNull(addedLesson);
        Assert.Equal("Entity Framework", addedLesson.CourseName);
        Assert.Equal("A1", addedLesson.Classroom);
        Assert.Equal("1", addedLesson.Teacher.Id);
        Assert.Equal(1, addedLesson.Group.GroupYear);
        Assert.Equal(1, addedLesson.Group.GroupNumber);
    }
    
    [Fact]
    public async Task GetLessonById_WhenLessonExists_ReturnsLesson()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));
        await manager.PostTeacher(new Teacher("1", "John", "Doe", []));
        await manager.PostLesson(new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1));

        // Act
        var lesson = await manager.GetLessonById(1);

        // Assert
        Assert.NotNull(lesson);
        Assert.Equal("Entity Framework", lesson.CourseName);
        Assert.Equal("A1", lesson.Classroom);
        Assert.Equal(1, lesson.Group.GroupYear);
        Assert.Equal(1, lesson.Group.GroupNumber);
    }
    
    [Fact]
    public async Task DeleteLesson_WhenLessonExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));
        await manager.PostTeacher(new Teacher("1", "John", "Doe", []));
        await manager.PostLesson(new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1));

        // Act
        var result = await manager.DeleteLesson(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task PutLesson_WhenLessonExists_UpdatesLesson()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));
        await manager.PostTeacher(new Teacher("1", "John", "Doe", []));
        await manager.PostLesson(new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1));
        var updatedLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1);

        // Act
        var result = await manager.PutLesson(1, updatedLesson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Entity Framework", result.CourseName);
        Assert.Equal("A1", result.Classroom);
        Assert.Equal(1, result.Group.GroupYear);
        Assert.Equal(1, result.Group.GroupNumber);
    }
    
    [Fact]
    public void PutLesson_WhenLessonDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var updatedLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1);

        // Act
        var result = manager.PutLesson(1, updatedLesson).Result;

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetLessons_WhenLessonsExist_ReturnsLessons()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostGroup(new Group (1, 1));
        await manager.PostTeacher(new Teacher("1", "John", "Doe", []));
        await manager.PostLesson(new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1));
        await manager.PostLesson(new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Entity Framework", "A1", "1", 1, 1));

        // Act
        var lessons = await manager.GetLessons(0, 10);

        // Assert
        Assert.NotNull(lessons);
        Assert.Equal(2, lessons.NbElement);
    }
    
}