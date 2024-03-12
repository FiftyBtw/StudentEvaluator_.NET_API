using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class StudentTestsEntity
{
     [Fact]
    public void TestAddStudent()
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

            // Assert
            var studentFromDb = context.StudentSet.FirstOrDefault();
            Assert.NotNull(studentFromDb);
            
            Assert.Contains(context.GroupSet.FirstOrDefault().Students, s => s.Id == studentFromDb.Id);

            Assert.Equal(studentToAdd.Name, studentFromDb.Name);
            Assert.Equal(studentToAdd.Lastname, studentFromDb.Lastname);
            Assert.Equal(studentToAdd.UrlPhoto, studentFromDb.UrlPhoto);
            Assert.Equal(studentToAdd.GroupYear, studentFromDb.GroupYear);
            Assert.Equal(studentToAdd.GroupNumber, studentFromDb.GroupNumber);
        }
    }
        
    [Fact]
    public void TestUpdateStudent()
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

            var studentToUpdate = context.StudentSet
                .FirstOrDefault(s => s.Name == studentToAdd.Name && s.Lastname == studentToAdd.Lastname);

            if (studentToUpdate != null)
            {
                studentToUpdate.Name = "Bob";
                context.StudentSet.Update(studentToUpdate);
                context.SaveChanges();

                // Assert
                var studentFromDb = context.StudentSet.FirstOrDefault();
                Assert.NotNull(studentFromDb);

                Assert.Equal(studentToUpdate.Name, studentFromDb.Name);
            }
        }
    }

    [Fact]
    public void TestDeleteStudent()
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

            var studentToDelete = context.StudentSet
                .FirstOrDefault(s => s.Name == studentToAdd.Name && s.Lastname == studentToAdd.Lastname);

            if (studentToDelete != null)
            {
                context.StudentSet.Remove(studentToDelete);
                context.SaveChanges();

                // Assert
                var studentFromDb = context.StudentSet.FirstOrDefault(s => s.Name == studentToDelete.Name && s.Lastname == studentToDelete.Lastname);
                Assert.Null(studentFromDb);
                Assert.Empty(context.GroupSet.FirstOrDefault().Students);
            }
        }
    }
}