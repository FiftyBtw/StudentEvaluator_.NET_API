using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class TeacherTestsEntity
{
    [Fact]
    public void TestAddTeacher()
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

            // Assert
            var teacherFromDb = context.TeacherSet.FirstOrDefault();
            Assert.NotNull(teacherFromDb);

            Assert.Equal(teacherToAdd.Username, teacherFromDb.Username);
            Assert.Equal(teacherToAdd.Password, teacherFromDb.Password);
            Assert.Equal(teacherToAdd.Roles, teacherFromDb.Roles);
        }
    }
    
    [Fact]
    public void TestUpdateTeacher()
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

            teacherToAdd.Username = "ProfTata";
            teacherToAdd.Password = "TataPassword";
            teacherToAdd.Roles = ["Teacher", "Admin"];

            context.TeacherSet.Update(teacherToAdd);
            context.SaveChanges();

            // Assert
            var teacherFromDb = context.TeacherSet.FirstOrDefault();
            Assert.NotNull(teacherFromDb);

            Assert.Equal(1, context.TeacherSet.Count());
            Assert.Equal(teacherToAdd.Username, teacherFromDb.Username);
            Assert.Equal(teacherToAdd.Password, teacherFromDb.Password);
            Assert.Equal(teacherToAdd.Roles, teacherFromDb.Roles);
        }
    }


    [Fact]
    public void TestDeleteTeacher()
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

            context.TeacherSet.Remove(teacherToAdd);
            context.SaveChanges();

            // Assert
            var teacherFromDb = context.TeacherSet.FirstOrDefault();
            Assert.Null(teacherFromDb);
        }
    }
        
        
}