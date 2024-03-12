using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class ECriteriasTests
{
    // RadioCriteria

    [Fact]
    public void TestAddRadioCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var radioCriteriaEntityToAdd = new RadioCriteriaEntity
        {
            SelectedOption = "Option A",
            Options = new string[] { "Option A", "Option B", "Option C" }
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.RadioCriteriaSet.Add(radioCriteriaEntityToAdd);
            context.SaveChanges();

            // Assert
            var radioCriteriaEntityFromDb = context.RadioCriteriaSet.FirstOrDefault();
            Assert.NotNull(radioCriteriaEntityFromDb);

            Assert.Equal(radioCriteriaEntityToAdd.SelectedOption, radioCriteriaEntityFromDb.SelectedOption);
            Assert.Equal(radioCriteriaEntityToAdd.Options.Length, radioCriteriaEntityFromDb.Options.Length);
            Assert.All(radioCriteriaEntityToAdd.Options, option => Assert.Contains(option, radioCriteriaEntityFromDb.Options));
        }
    }

    [Fact]
    public void TestUpdateRadioCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var radioCriteriaEntityToUpdate = new RadioCriteriaEntity
        {
            SelectedOption = "Option A",
            Options = new string[] { "Option A", "Option B", "Option C" }
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.RadioCriteriaSet.Add(radioCriteriaEntityToUpdate);
            context.SaveChanges();

            radioCriteriaEntityToUpdate.SelectedOption = "Option B";
            context.RadioCriteriaSet.Update(radioCriteriaEntityToUpdate);
            context.SaveChanges();

            // Assert
            var updatedRadioCriteriaEntityFromDb = context.RadioCriteriaSet.FirstOrDefault();
            Assert.NotNull(updatedRadioCriteriaEntityFromDb);
            Assert.Equal("Option B", updatedRadioCriteriaEntityFromDb.SelectedOption);
        }
    }

    [Fact]
    public void TestDeleteRadioCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var radioCriteriaEntityToDelete = new RadioCriteriaEntity
        {
            SelectedOption = "Option A",
            Options = new string[] { "Option A", "Option B", "Option C" }
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.RadioCriteriaSet.Add(radioCriteriaEntityToDelete);
            context.SaveChanges();

            context.RadioCriteriaSet.Remove(radioCriteriaEntityToDelete);
            context.SaveChanges();

            // Assert
            var deletedRadioCriteriaEntityFromDb = context.RadioCriteriaSet.FirstOrDefault();
            Assert.Null(deletedRadioCriteriaEntityFromDb);
        }
    }






    // SliderCriteria

    [Fact]
    public void TestAddSliderCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var sliderCriteriaEntityToAdd = new SliderCriteriaEntity
        {
            Value = 10
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.SliderCriteriaSet.Add(sliderCriteriaEntityToAdd);
            context.SaveChanges();

            // Assert
            var sliderCriteriaEntityFromDb = context.SliderCriteriaSet.FirstOrDefault();
            Assert.NotNull(sliderCriteriaEntityFromDb);

            Assert.Equal(sliderCriteriaEntityToAdd.Value, sliderCriteriaEntityFromDb.Value);
        }
    }

    [Fact]
    public void TestUpdateSliderCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var sliderCriteriaEntityToUpdate = new SliderCriteriaEntity
        {
            Value = 10
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.SliderCriteriaSet.Add(sliderCriteriaEntityToUpdate);
            context.SaveChanges();

            sliderCriteriaEntityToUpdate.Value = 20;
            context.SliderCriteriaSet.Update(sliderCriteriaEntityToUpdate);
            context.SaveChanges();

            // Assert
            var updatedSliderCriteriaEntityFromDb = context.SliderCriteriaSet.FirstOrDefault();
            Assert.NotNull(updatedSliderCriteriaEntityFromDb);
            Assert.Equal(20, updatedSliderCriteriaEntityFromDb.Value);
        }
    }

    [Fact]
    public void TestDeleteSliderCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var sliderCriteriaEntityToDelete = new SliderCriteriaEntity
        {
            Value = 10
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.SliderCriteriaSet.Add(sliderCriteriaEntityToDelete);
            context.SaveChanges();

            context.SliderCriteriaSet.Remove(sliderCriteriaEntityToDelete);
            context.SaveChanges();

            // Assert
            var deletedSliderCriteriaEntityFromDb = context.SliderCriteriaSet.FirstOrDefault();
            Assert.Null(deletedSliderCriteriaEntityFromDb);
        }
    }






    // TextCriteria

    [Fact]
    public void TestAddTextCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var textCriteriaEntityToAdd = new TextCriteriaEntity
        {
            Text = "Sample text"
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.TextCriteriaSet.Add(textCriteriaEntityToAdd);
            context.SaveChanges();

            // Assert
            var textCriteriaEntityFromDb = context.TextCriteriaSet.FirstOrDefault();
            Assert.NotNull(textCriteriaEntityFromDb);

            Assert.Equal(textCriteriaEntityToAdd.Text, textCriteriaEntityFromDb.Text);
        }
    }

    [Fact]
    public void TestUpdateTextCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var textCriteriaEntityToUpdate = new TextCriteriaEntity
        {
            Text = "Initial text"
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.TextCriteriaSet.Add(textCriteriaEntityToUpdate);
            context.SaveChanges();

            textCriteriaEntityToUpdate.Text = "Updated text";
            context.TextCriteriaSet.Update(textCriteriaEntityToUpdate);
            context.SaveChanges();

            // Assert
            var updatedTextCriteriaEntityFromDb = context.TextCriteriaSet.FirstOrDefault();
            Assert.NotNull(updatedTextCriteriaEntityFromDb);
            Assert.Equal("Updated text", updatedTextCriteriaEntityFromDb.Text);
        }
    }

    [Fact]
    public void TestDeleteTextCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var textCriteriaEntityToDelete = new TextCriteriaEntity
        {
            Text = "Text to delete"
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.TextCriteriaSet.Add(textCriteriaEntityToDelete);
            context.SaveChanges();

            context.TextCriteriaSet.Remove(textCriteriaEntityToDelete);
            context.SaveChanges();

            // Assert
            var deletedTextCriteriaEntityFromDb = context.TextCriteriaSet.FirstOrDefault();
            Assert.Null(deletedTextCriteriaEntityFromDb);
        }
    }





    // Criteria

    [Fact]
    public void TestAddCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var template = new TemplateEntity
        {
            Name = "Sample Template",
            TeacherId = 1 
        };

        var criteriaEntityToAdd = new TextCriteriaEntity
        {
            Name = "Sample Criteria",
            ValueEvaluation = 10,
            Template = template
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.CriteriaSet.Add(criteriaEntityToAdd);
            context.SaveChanges();

            // Assert
            var criteriaEntityFromDb = context.CriteriaSet.FirstOrDefault();
            Assert.NotNull(criteriaEntityFromDb);

            Assert.Equal(criteriaEntityToAdd.Name, criteriaEntityFromDb.Name);
            Assert.Equal(criteriaEntityToAdd.ValueEvaluation, criteriaEntityFromDb.ValueEvaluation);
            Assert.Equal(criteriaEntityToAdd.TemplateId, criteriaEntityFromDb.TemplateId);
        }
    }

    [Fact]
    public void TestUpdateCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var template = new TemplateEntity
        {
            Name = "Sample Template",
            TeacherId = 1 
        };

        var initialCriteriaEntity = new TextCriteriaEntity
        {
            Name = "Initial Criteria",
            ValueEvaluation = 10,
            Template = template
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.CriteriaSet.Add(initialCriteriaEntity);
            context.SaveChanges();

            initialCriteriaEntity.Name = "Updated Criteria";
            initialCriteriaEntity.ValueEvaluation = 20;

            context.CriteriaSet.Update(initialCriteriaEntity);
            context.SaveChanges();

            // Assert
            var criteriaEntityFromDb = context.CriteriaSet.FirstOrDefault();
            Assert.NotNull(criteriaEntityFromDb);
            Assert.Equal("Updated Criteria", criteriaEntityFromDb.Name);
            Assert.Equal(20, criteriaEntityFromDb.ValueEvaluation);
        }
    }

    [Fact]
    public void TestDeleteCriteriaEntity()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var template = new TemplateEntity
        {
            Name = "Sample Template",
            TeacherId = 1 
        };

        var criteriaEntityToDelete = new TextCriteriaEntity
        {
            Name = "Criteria to delete",
            ValueEvaluation = 10,
            Template = template
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.CriteriaSet.Add(criteriaEntityToDelete);
            context.SaveChanges();

            context.CriteriaSet.Remove(criteriaEntityToDelete);
            context.SaveChanges();

            // Assert
            var criteriaEntityFromDb = context.CriteriaSet.FirstOrDefault();
            Assert.Null(criteriaEntityFromDb);
        }
    }


}

