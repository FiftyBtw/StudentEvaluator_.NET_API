using Microsoft.EntityFrameworkCore;
using EF_StubbedContextLib;
using Client_Model;
using EF_DbContextLib;
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
}