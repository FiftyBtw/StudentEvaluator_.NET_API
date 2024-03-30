using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class TemplateTestsEntity
{
    /*
    [Fact]
    public void TestAddTemplate()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            var teacherToAdd = new TeacherEntity
            {
                Username = "ProfToto",
                Password = "TotoPassword",
                Roles = ["Teacher"]
            };
            
            context.TeacherSet.Add(teacherToAdd);
            context.SaveChanges();
            
            var groupToAdd = new GroupEntity
            {
                GroupYear = 1,
                GroupNumber = 1
            };
            
            context.GroupSet.Add(groupToAdd);
            context.SaveChanges();
            
            var studentToAdd = new StudentEntity
            {
                Name = "John",
                Lastname = "Doe",
                UrlPhoto = "https://www.google.com",
                GroupYear = groupToAdd.GroupYear,
                GroupNumber = groupToAdd.GroupNumber
            };
            
            context.StudentSet.Add(studentToAdd);
            context.SaveChanges();
            
            var templateToAdd = new TemplateEntity
            {
                Name = "Template1",
                TeacherId = teacherToAdd.Id,
            };

            context.TemplateSet.Add(templateToAdd);
            context.SaveChanges();

            var evaluationToAdd = new EvaluationEntity
            {
                CourseName = "Entity Framework",
                Date = new DateTime(2024, 3, 24),
                StudentId = studentToAdd.Id, 
                TemplateId = templateToAdd.Id, 
                TeacherId = teacherToAdd.Id,
                Grade = 10,
                PairName = "toto",
            };
    
            context.EvaluationSet.Add(evaluationToAdd);
            context.SaveChanges();

            templateToAdd.EvaluationId = evaluationToAdd.Id;
            context.SaveChanges();

            // Assert
            var templateFromDb = context.TemplateSet.Include(t => t.Teacher).Include(t => t.Evaluation).FirstOrDefault();
            Assert.NotNull(templateFromDb);
            
            Assert.Equal(templateToAdd.Name, templateFromDb.Name);
            Assert.Equal(templateToAdd.TeacherId, templateFromDb.TeacherId);
            Assert.Equal(templateFromDb.Teacher, teacherToAdd);
            Assert.Equal(templateFromDb.EvaluationId, evaluationToAdd.Id);
            Assert.Equal(templateFromDb.Evaluation, evaluationToAdd);
            Assert.Contains(context.TeacherSet.FirstOrDefault().Templates, t => t.Id == templateFromDb.Id);
        }
    }
    
    [Fact]
    public void TestUpdateTemplate()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var teacherToAdd = new TeacherEntity
        {
            Username = "ProfToto",
            Password = "TotoPassword",
            Roles = ["Teacher"]
        };
        
        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();
            
            context.TeacherSet.Add(teacherToAdd);
            context.SaveChanges();
            
            var templateToAdd = new TemplateEntity
            {
                Name = "Template1",
                TeacherId = teacherToAdd.Id
            };

            context.TemplateSet.Add(templateToAdd);
            context.SaveChanges();
            
            templateToAdd.Name = "Template2";
            context.TemplateSet.Update(templateToAdd);
            context.SaveChanges();

            // Assert
            var templateFromDb = context.TemplateSet.FirstOrDefault();
            Assert.NotNull(templateFromDb);
            
            Assert.Equal(templateToAdd.Name, templateFromDb.Name);
        }
    }
    
    [Fact]
    public void TestDeleteTemplate()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var teacherToAdd = new TeacherEntity
        {
            Username = "ProfToto",
            Password = "TotoPassword",
            Roles = ["Teacher"]
        };
        
        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();
            
            context.TeacherSet.Add(teacherToAdd);
            context.SaveChanges();
            
            var templateToAdd = new TemplateEntity
            {
                Name = "Template1",
                TeacherId = teacherToAdd.Id
            };

            context.TemplateSet.Add(templateToAdd);
            context.SaveChanges();
            
            context.TemplateSet.Remove(templateToAdd);
            context.SaveChanges();

            // Assert
            var templateFromDb = context.TemplateSet.FirstOrDefault();
            Assert.Null(templateFromDb);
            Assert.Empty(context.TeacherSet.FirstOrDefault().Templates);
        }
    }
    */
}