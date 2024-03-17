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
        Assert.Equal(2, students.nbElement);
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
        Assert.Equal(2, groups.nbElement);
    }
    
    [Fact]
    public async Task PostLesson_AddsLessonCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Math", "A1", 1, 1, 1);
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        manager.PostGroup(new Group(1, 1));

        // Act
        var addedLesson = await manager.PostLesson(newLesson);

        // Assert
        Assert.NotNull(addedLesson);
        Assert.Equal(new DateTime(2024, 3, 16), addedLesson.Start);
        Assert.Equal(new DateTime(2024, 3, 16), addedLesson.End);
        Assert.Equal("Math", addedLesson.CourseName);
        Assert.Equal("A1", addedLesson.Classroom);
        Assert.Equal(1, addedLesson.Teacher.Id);
        Assert.Equal(1, addedLesson.Group.GroupYear);
        Assert.Equal(1, addedLesson.Group.GroupNumber);
    }
    
    [Fact]
    public async Task GetLessonById_WhenLessonExists_ReturnsLesson()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Math", "A1", 1, 1, 1);
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        manager.PostGroup(new Group(1, 1));
        await manager.PostLesson(newLesson);

        // Act
        var lesson = await manager.GetLessonById(1);

        // Assert
        Assert.NotNull(lesson);
        Assert.Equal(new DateTime(2024, 3, 16), lesson.Start);
        Assert.Equal(new DateTime(2024, 3, 16), lesson.End);
        Assert.Equal("Math", lesson.CourseName);
        Assert.Equal("A1", lesson.Classroom);
        Assert.Equal(1, lesson.Teacher.Id);
        Assert.Equal(1, lesson.Group.GroupYear);
        Assert.Equal(1, lesson.Group.GroupNumber);
    }
    
    [Fact]
    public async Task DeleteLesson_WhenLessonExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Math", "A1", 1, 1, 1);
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        manager.PostGroup(new Group(1, 1));
        await manager.PostLesson(newLesson);

        // Act
        var result = await manager.DeleteLesson(1);

        // Assert
        Assert.True(result);
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
    public async Task GetLessons_WhenLessonsExist_ReturnsLessons()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Math", "A1", 1, 1, 1);
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        manager.PostGroup(new Group(1, 1));
        await manager.PostLesson(newLesson);
        await manager.PostLesson(newLesson);

        // Act
        var lessons = await manager.GetLessons(0, 10);

        // Assert
        Assert.NotNull(lessons);
        Assert.Equal(2, lessons.nbElement);
    }
    
    [Fact]
    public async Task GetLessonsByTeacherId_WhenLessonsExist_ReturnsLessons()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Math", "A1", 1, 1, 1);
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        manager.PostGroup(new Group(1, 1));
        await manager.PostLesson(newLesson);
        await manager.PostLesson(newLesson);

        // Act
        var lessons = await manager.GetLessonsByTeacherId(1, 0, 10);

        // Assert
        Assert.NotNull(lessons);
        Assert.Equal(2, lessons.nbElement);
    }
    
    [Fact]
    public async Task PutLesson_WhenLessonExists_UpdatesLesson()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Math", "A1", 1, 1, 1);
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        manager.PostGroup(new Group(1, 1));
        await manager.PostLesson(newLesson);
        var updatedLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16), "Math", "A1", 1, 1, 1);

        // Act
        var result = await manager.PutLesson(1, updatedLesson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new DateTime(2024, 3, 16), result.Start);
        Assert.Equal(new DateTime(2024, 3, 16), result.End);
        Assert.Equal("Math", result.CourseName);
        Assert.Equal("A1", result.Classroom);
        Assert.Equal(1, result.Teacher.Id);
        Assert.Equal(1, result.Group.GroupYear);
        Assert.Equal(1, result.Group.GroupNumber);
    }
    
    [Fact]
    public async Task PostEvaluation_AddsEvaluationCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 1, 1);
        manager.PostStudent(new Student(1, "John", "Doe", "https://example.com", 1, 1));
        manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));

        // Act
        var addedEvaluation = await manager.PostEvaluation(newEvaluation);

        // Assert
        Assert.NotNull(addedEvaluation);
        Assert.Equal(new DateTime(2024, 3, 16), addedEvaluation.Date);
        Assert.Equal("Entity Framework", addedEvaluation.CourseName);
        Assert.Equal(1, addedEvaluation.Grade);
        Assert.Equal("toto", addedEvaluation.PairName);
        Assert.Equal(1, addedEvaluation.Student.Id);
        Assert.Equal(1, addedEvaluation.Template.Id);
        Assert.Equal(1, addedEvaluation.Teacher.Id);
    }
    
    [Fact]
    public async Task GetEvaluationById_WhenEvaluationExists_ReturnsEvaluation()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 1, 1);
        manager.PostStudent(new Student(1, "John", "Doe", "https://example.com", 1, 1));
        manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        await manager.PostEvaluation(newEvaluation);

        // Act
        var evaluation = await manager.GetEvaluationById(1);

        // Assert
        Assert.NotNull(evaluation);
        Assert.Equal(new DateTime(2024, 3, 16), evaluation.Date);
        Assert.Equal("Entity Framework", evaluation.CourseName);
        Assert.Equal(1, evaluation.Grade);
        Assert.Equal("toto", evaluation.PairName);
        Assert.Equal(1, evaluation.Student.Id);
        Assert.Equal(1, evaluation.Template.Id);
        Assert.Equal(1, evaluation.Teacher.Id);
    }
    
    [Fact]
    public async Task DeleteEvaluation_WhenEvaluationExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 1, 1);
        manager.PostStudent(new Student(1, "John", "Doe", "https://example.com", 1, 1));
        manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        await manager.PostEvaluation(newEvaluation);

        // Act
        var result = await manager.DeleteEvaluation(1);

        // Assert
        Assert.True(result);
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
    public async Task PutEvaluation_WhenEvaluationExists_UpdatesEvaluation()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 1, 1);
        manager.PostStudent(new Student(1, "John", "Doe", "https://example.com", 1, 1));
        manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        await manager.PostEvaluation(newEvaluation);
        var updatedEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 2, "toto", 1, 1, 1);

        // Act
        var result = await manager.PutEvaluation(1, updatedEvaluation);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new DateTime(2024, 3, 16), result.Date);
        Assert.Equal("Entity Framework", result.CourseName);
        Assert.Equal(2, result.Grade);
        Assert.Equal("toto", result.PairName);
        Assert.Equal(1, result.Student.Id);
        Assert.Equal(1, result.Template.Id);
        Assert.Equal(1, result.Teacher.Id);
    }
    
    [Fact]
    public async Task PutEvaluation_WhenEvaluationDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var updatedEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 2, "toto", 1, 1, 1);

        // Act
        var result = await manager.PutEvaluation(1, updatedEvaluation);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetEvaluations_WhenEvaluationsExist_ReturnsEvaluations()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 1, 1);
        var newEvaluation2 = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 2, 1);
        manager.PostStudent(new Student(1, "John", "Doe", "https://example.com", 1, 1));
        manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
        manager.PostTemplate(1, new Template(2, "Entity Framework", new List<Criteria>()));
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        await manager.PostEvaluation(newEvaluation);
        await manager.PostEvaluation(newEvaluation2);

        // Act
        var evaluations = await manager.GetEvaluations(0, 10);

        // Assert
        Assert.NotNull(evaluations);
        Assert.Equal(2, evaluations.nbElement);
    }
    
    [Fact]
    public async Task GetEvaluationsByTeacherId_WhenEvaluationsExist_ReturnsEvaluations()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 1, 1);
        var newEvaluation2 = new EvaluationCreation(new DateTime(2024, 3, 16), "Entity Framework", 1, "toto", 1, 2, 1);
        manager.PostStudent(new Student(1, "John", "Doe", "https://example.com", 1, 1));
        manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
        manager.PostTemplate(1, new Template(2, "Entity Framework", new List<Criteria>()));
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        await manager.PostEvaluation(newEvaluation);
        await manager.PostEvaluation(newEvaluation2);

        // Act
        var evaluations = await manager.GetEvaluationsByTeacherId(1, 0, 10);

        // Assert
        Assert.NotNull(evaluations);
        Assert.Equal(2, evaluations.nbElement);
    }
    
    [Fact]
    public async Task PostUser_AddsUserCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newUser = new User(1, "ProfDupont","MotDePasseSécu" ,["User"]);

        // Act
        var addedUser = await manager.PostUser(newUser);

        // Assert
        Assert.NotNull(addedUser);
        Assert.Equal("ProfDupont", addedUser.Username);
        Assert.Equal("MotDePasseSécu", addedUser.Password);
        Assert.Equal(["User"], addedUser.Roles);
    }
    
    [Fact]
    public async Task GetUserById_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostUser(new User(1, "ProfDupont","MotDePasseSécu" ,["User"]));

        // Act
        var user = await manager.GetUserById(1);

        // Assert
        Assert.NotNull(user);
        Assert.Equal("ProfDupont", user.Username);
        Assert.Equal("MotDePasseSécu", user.Password);
        Assert.Equal(["User"], user.Roles);
    }
    
    [Fact]
    public async Task DeleteUser_WhenUserExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostUser(new User(1, "ProfDupont","MotDePasseSécu" ,["User"]));

        // Act
        var result = await manager.DeleteUser(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task DeleteUser_WhenUserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var result = await manager.DeleteUser(1);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task PutUser_WhenUserExists_UpdatesUser()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostUser(new User(1, "ProfDupont","MotDePasseSécu" ,["User"]));
        var updatedUser = new User(1, "ProfDupont","MotDePasseSécu" ,["User"]);

        // Act
        var result = await manager.PutUser(1, updatedUser);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ProfDupont", result.Username);
        Assert.Equal("MotDePasseSécu", result.Password);
        Assert.Equal(["User"], result.Roles);
    }
    
    [Fact]
    public void PutUser_WhenUserDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var updatedUser = new User(1, "ProfDupont","MotDePasseSécu" ,["User"]);

        // Act
        var result = manager.PutUser(1, updatedUser).Result;

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetUsers_WhenUsersExist_ReturnsUsers()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostUser(new User(1, "ProfDupont","MotDePasseSécu" ,["User"]));
        await manager.PostUser(new User(2, "ProfDupont2","MotDePasseSécu2" ,["User"]));

        // Act
        var users = await manager.GetUsers(0, 10);

        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.nbElement);
    }
    
    [Fact]
    public async Task Login_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostUser(new User(1, "ProfDupont","MotDePasseSécu" ,["User"]));

        // Act
        var user = await manager.Login(new LoginRequest("ProfDupont", "MotDePasseSécu"));

        // Assert
        Assert.NotNull(user);
        Assert.Equal("ProfDupont", user.Username);
        Assert.Equal(["User"], user.Roles);
    }
    
    [Fact]
    public async Task Login_WhenUserDoesNotExist_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();

        // Act
        var user = await manager.Login(new LoginRequest("ProfDupont", "MotDePasseSécu"));

        // Assert
        Assert.Null(user);
    }
    
    [Fact]
    public async Task Login_WhenPasswordIsIncorrect_ReturnsNull()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostUser(new User(1, "ProfDupont","MotDePasseSécu" ,["User"]));

        // Act
        var user = await manager.Login(new LoginRequest("ProfDupont", "MotDePasseSécu2"));

        // Assert
        Assert.Null(user);
    }
    
    [Fact]
    public async Task PostTemplate_AddsTemplateCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        var newTemplate = new Template(1, "Entity Framework", new List<Criteria>());

        // Act
        var addedTemplate = await manager.PostTemplate(1, newTemplate);

        // Assert
        Assert.NotNull(addedTemplate);
        Assert.Equal("Entity Framework", addedTemplate.Name);
    }
    
    [Fact]
    public async Task GetTemplateById_WhenTemplateExists_ReturnsTemplate()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        await manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));

        // Act
        var template = await manager.GetTemplateById(1, 1);

        // Assert
        Assert.NotNull(template);
        Assert.Equal("Entity Framework", template.Name);
    }
    
    [Fact]
    public async Task DeleteTemplate_WhenTemplateExists_ReturnsTrue()
    {
        // Arrange
        var manager = CreateManagerWithInMemoryDb();
        await manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));

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
        await manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
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
        manager.PostTeacher(new Teacher(1, "John", "Doe", ["Teacher"]));
        await manager.PostTemplate(1, new Template(1, "Entity Framework", new List<Criteria>()));
        await manager.PostTemplate(1, new Template(2, "Entity Framework", new List<Criteria>()));

        // Act
        var templates = await manager.GetTemplatesByUserId(1, 0, 10);

        // Assert
        Assert.NotNull(templates);
        Assert.Equal(2, templates.nbElement);
    }
}