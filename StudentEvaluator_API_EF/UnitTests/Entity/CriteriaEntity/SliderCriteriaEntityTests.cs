using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity.CriteriaEntity;

public class SliderCriteriaEntityTests
{
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
            
            var sliderCriteriaToAdd = new SliderCriteriaEntity
            {
                Name = "SliderCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Value = 12
            };
            
            context.SliderCriteriaSet.Add(sliderCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var sliderCriteriaFromDb = context.SliderCriteriaSet.Include(s => s.Template).FirstOrDefault();
            Assert.NotNull(sliderCriteriaFromDb);
            
            Assert.Equal(sliderCriteriaToAdd.Name, sliderCriteriaFromDb.Name);
            Assert.Equal(sliderCriteriaToAdd.ValueEvaluation, sliderCriteriaFromDb.ValueEvaluation);
            Assert.Equal(sliderCriteriaToAdd.TemplateId, sliderCriteriaFromDb.TemplateId);
            Assert.Equal(sliderCriteriaToAdd.Value, sliderCriteriaFromDb.Value);
            Assert.Equal(sliderCriteriaToAdd.Template, templateToAdd);

            
            Assert.Contains(context.TemplateSet.FirstOrDefault().Criteria, t => t.Id == sliderCriteriaFromDb.Id);
        }
    }

    [Fact]
    public void TestUpdateSliderCriteria()
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
            
            var sliderCriteriaToAdd = new SliderCriteriaEntity
            {
                Name = "SliderCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Value = 12
            };
            
            context.SliderCriteriaSet.Add(sliderCriteriaToAdd);
            context.SaveChanges();
            
            sliderCriteriaToAdd.Name = "SliderCriteria2";
            sliderCriteriaToAdd.ValueEvaluation = 3;
            sliderCriteriaToAdd.TemplateId = 1;
            sliderCriteriaToAdd.Value = 15;
            
            context.SliderCriteriaSet.Update(sliderCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var sliderCriteriaFromDb = context.SliderCriteriaSet.FirstOrDefault();
            Assert.NotNull(sliderCriteriaFromDb);
            
            Assert.Equal(sliderCriteriaToAdd.Name, sliderCriteriaFromDb.Name);
            Assert.Equal(sliderCriteriaToAdd.ValueEvaluation, sliderCriteriaFromDb.ValueEvaluation);
            Assert.Equal(sliderCriteriaToAdd.TemplateId, sliderCriteriaFromDb.TemplateId);
            Assert.Equal(sliderCriteriaToAdd.Value, sliderCriteriaFromDb.Value);
            
            Assert.Contains(context.TemplateSet.FirstOrDefault().Criteria, t => t.Id == sliderCriteriaFromDb.Id);
        }
    }
    
    [Fact]
    public void TestDeleteSliderCriteria()
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
            
            var sliderCriteriaToAdd = new SliderCriteriaEntity
            {
                Name = "SliderCriteria1",
                ValueEvaluation = 2,
                TemplateId = 1,
                Value = 12
            };
            
            context.SliderCriteriaSet.Add(sliderCriteriaToAdd);
            context.SaveChanges();
            
            context.SliderCriteriaSet.Remove(sliderCriteriaToAdd);
            context.SaveChanges();
            
            // Assert
            
            var sliderCriteriaFromDb = context.SliderCriteriaSet.FirstOrDefault();
            Assert.Null(sliderCriteriaFromDb);
            Assert.Empty(context.TemplateSet.FirstOrDefault().Criteria);
        }
    }
    
}