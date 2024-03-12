using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class TemplateTestsEntity
{
    
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
            
            var templateToAdd = new TemplateEntity
            {
                Name = "Template1",
                TeacherId = teacherToAdd.Id
            };

            context.TemplateSet.Add(templateToAdd);
            context.SaveChanges();

            // Assert
            var templateFromDb = context.TemplateSet.FirstOrDefault();
            Assert.NotNull(templateFromDb);
            
            Assert.Equal(templateToAdd.Name, templateFromDb.Name);
            Assert.Equal(templateToAdd.TeacherId, templateFromDb.TeacherId);
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
    
}