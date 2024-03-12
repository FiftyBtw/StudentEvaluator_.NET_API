using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity.CriteriaEntity;

public class TextCriteriaEntityTests
{
    [Fact]
    public void TestAddTextCriteria()
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
            
            var teacherToAdd = new TeacherEntity
            {
                Username = "ProfToto",
                Password = "TotoPassword",
                Roles = ["Teacher"]
            };
        
            var templateToAdd = new TemplateEntity
            {
                Name = "Template1",
                TeacherId = 1
            };
            
            context.Database.EnsureCreated();
            context.TeacherSet.Add(teacherToAdd);
            context.SaveChanges();
            context.TemplateSet.Add(templateToAdd);
            context.SaveChanges();

            var textCriteriaToAdd = new TextCriteriaEntity
            {
                Name = "TextCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Text = "This is a text"
            };
            
            context.TextCriteriaSet.Add(textCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var textCriteriaFromDb = context.TextCriteriaSet.Include(t => t.Template).FirstOrDefault();
            Assert.NotNull(textCriteriaFromDb);
            
            Assert.Equal(textCriteriaToAdd.Name, textCriteriaFromDb.Name);
            Assert.Equal(textCriteriaToAdd.ValueEvaluation, textCriteriaFromDb.ValueEvaluation);
            Assert.Equal(textCriteriaToAdd.TemplateId, textCriteriaFromDb.TemplateId);
            Assert.Equal(textCriteriaToAdd.Text, textCriteriaFromDb.Text);
            Assert.Equal(textCriteriaToAdd.Template, templateToAdd);
            
            Assert.Contains(context.TemplateSet.FirstOrDefault().Criteria, t => t.Id == textCriteriaFromDb.Id);
        }
    }
    
    [Fact]
    public void TestUpdateTextCriteria()
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
            var teacherToAdd = new TeacherEntity
            {
                Username = "ProfToto",
                Password = "TotoPassword",
                Roles = ["Teacher"]
            };
        
            var templateToAdd = new TemplateEntity
            {
                Name = "Template1",
                TeacherId = 1
            };
            
            context.Database.EnsureCreated();
            context.TeacherSet.Add(teacherToAdd);
            context.SaveChanges();
            context.TemplateSet.Add(templateToAdd);
            context.SaveChanges();

            var textCriteriaToAdd = new TextCriteriaEntity
            {
                Name = "TextCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Text = "This is a text"
            };
            
            context.TextCriteriaSet.Add(textCriteriaToAdd);
            context.SaveChanges();
            
            textCriteriaToAdd.Name = "TextCriteria2";
            textCriteriaToAdd.ValueEvaluation = 3;
            textCriteriaToAdd.Text = "This is another text";
            
            context.TextCriteriaSet.Update(textCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var textCriteriaFromDb = context.TextCriteriaSet.FirstOrDefault();
            Assert.NotNull(textCriteriaFromDb);
            
            Assert.Equal(textCriteriaToAdd.Name, textCriteriaFromDb.Name);
            Assert.Equal(textCriteriaToAdd.ValueEvaluation, textCriteriaFromDb.ValueEvaluation);
            Assert.Equal(textCriteriaToAdd.TemplateId, textCriteriaFromDb.TemplateId);
            Assert.Equal(textCriteriaToAdd.Text, textCriteriaFromDb.Text);
            Assert.Contains(context.TemplateSet.FirstOrDefault().Criteria, t => t.Id == textCriteriaFromDb.Id);
        }
    }
    
    [Fact]
    public void TestDeleteTextCriteria()
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
            var teacherToAdd = new TeacherEntity
            {
                Username = "ProfToto",
                Password = "TotoPassword",
                Roles = ["Teacher"]
            };
        
            var templateToAdd = new TemplateEntity
            {
                Name = "Template1",
                TeacherId = 1
            };
            
            context.Database.EnsureCreated();
            context.TeacherSet.Add(teacherToAdd);
            context.SaveChanges();
            context.TemplateSet.Add(templateToAdd);
            context.SaveChanges();

            var textCriteriaToAdd = new TextCriteriaEntity
            {
                Name = "TextCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Text = "This is a text"
            };
            
            context.TextCriteriaSet.Add(textCriteriaToAdd);
            context.SaveChanges();
            
            context.TextCriteriaSet.Remove(textCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var textCriteriaFromDb = context.TextCriteriaSet.FirstOrDefault();
            Assert.Null(textCriteriaFromDb);
        }
    }

}

