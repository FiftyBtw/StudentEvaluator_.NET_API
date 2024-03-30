using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity.CriteriaEntity;

public class RadioCriteriaEntityTests
{
    /*
    [Fact]
    public void TestAddSliderCriteria()
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
                UserName = "ProfToto",
                PasswordHash = "TotoPassword",
                
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

            var radioCriteriaToAdd = new RadioCriteriaEntity
            {
                Name = "RadioCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Options = ["Option1", "Option2", "Option3"],
                SelectedOption = "Option2"
            };
            
            context.RadioCriteriaSet.Add(radioCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var radioCriteriaFromDb = context.RadioCriteriaSet.FirstOrDefault();
            Assert.NotNull(radioCriteriaFromDb);
            
            Assert.Equal(radioCriteriaToAdd.Name, radioCriteriaFromDb.Name);
            Assert.Equal(radioCriteriaToAdd.ValueEvaluation, radioCriteriaFromDb.ValueEvaluation);
            Assert.Equal(radioCriteriaToAdd.TemplateId, radioCriteriaFromDb.TemplateId);
            Assert.Equal(radioCriteriaToAdd.Options, radioCriteriaFromDb.Options);
            Assert.Equal(radioCriteriaToAdd.SelectedOption, radioCriteriaFromDb.SelectedOption);
            
            Assert.Contains(context.TemplateSet.FirstOrDefault().Criteria, t => t.Id == radioCriteriaFromDb.Id);
        }
    }

    [Fact]
    public void TestUpdateRadioCriteria()
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

            var radioCriteriaToAdd = new RadioCriteriaEntity
            {
                Name = "RadioCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Options = ["Option1", "Option2", "Option3"],
                SelectedOption = "Option2"
            };
            
            context.RadioCriteriaSet.Add(radioCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var radioCriteriaFromDb = context.RadioCriteriaSet.FirstOrDefault();
            Assert.NotNull(radioCriteriaFromDb);
            
            Assert.Equal(radioCriteriaToAdd.Name, radioCriteriaFromDb.Name);
            Assert.Equal(radioCriteriaToAdd.ValueEvaluation, radioCriteriaFromDb.ValueEvaluation);
            Assert.Equal(radioCriteriaToAdd.TemplateId, radioCriteriaFromDb.TemplateId);
            Assert.Equal(radioCriteriaToAdd.Options, radioCriteriaFromDb.Options);
            Assert.Equal(radioCriteriaToAdd.SelectedOption, radioCriteriaFromDb.SelectedOption);
            
            Assert.Contains(context.TemplateSet.FirstOrDefault().Criteria, t => t.Id == radioCriteriaFromDb.Id);
        }
    }
    
    [Fact]
    public void TestDeleteRadioCriteria()
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

            var radioCriteriaToAdd = new RadioCriteriaEntity
            {
                Name = "RadioCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Options = ["Option1", "Option2", "Option3"],
                SelectedOption = "Option2"
            };
            
            context.RadioCriteriaSet.Add(radioCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var radioCriteriaFromDb = context.RadioCriteriaSet.Include(r => r.Template).FirstOrDefault();
            Assert.NotNull(radioCriteriaFromDb);
            
            Assert.Equal(radioCriteriaToAdd.Name, radioCriteriaFromDb.Name);
            Assert.Equal(radioCriteriaToAdd.ValueEvaluation, radioCriteriaFromDb.ValueEvaluation);
            Assert.Equal(radioCriteriaToAdd.TemplateId, radioCriteriaFromDb.TemplateId);
            Assert.Equal(radioCriteriaToAdd.Options, radioCriteriaFromDb.Options);
            Assert.Equal(radioCriteriaToAdd.SelectedOption, radioCriteriaFromDb.SelectedOption);
            Assert.Equal(radioCriteriaToAdd.Template, templateToAdd);
            
            Assert.Contains(context.TemplateSet.FirstOrDefault().Criteria, t => t.Id == radioCriteriaFromDb.Id);
            
            context.RadioCriteriaSet.Remove(radioCriteriaFromDb);
            context.SaveChanges();
            
            Assert.Empty(context.RadioCriteriaSet);
        }
    }
    */
}