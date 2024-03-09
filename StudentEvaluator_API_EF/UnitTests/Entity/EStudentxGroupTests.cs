using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class EStudentxGroupTests
{
    [Fact]
    public void TestAddGroup()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var groupToAdd = new GroupEntity
        {
            GroupYear = 1,
            GroupNumber = 1
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.GroupSet.Add(groupToAdd);
            context.SaveChanges();

            // Assert
            var groupFromDb = context.GroupSet.FirstOrDefault();
            Assert.NotNull(groupFromDb);

            Assert.Equal(groupToAdd.GroupYear, groupFromDb.GroupYear);
            Assert.Equal(groupToAdd.GroupNumber, groupFromDb.GroupNumber);
        }
    }

    
    [Fact]
    public void TestDeleteGroup()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(connection)
            .Options;

        var groupToAdd = new GroupEntity
        {
            GroupYear = 1,
            GroupNumber = 1
        };

        // Act
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureCreated();

            context.GroupSet.Add(groupToAdd);
            context.SaveChanges();

            context.GroupSet.Remove(groupToAdd);
            context.SaveChanges();

            // Assert
            var groupFromDb = context.GroupSet.FirstOrDefault();
            Assert.Null(groupFromDb);
        }
    }
    
    
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

            var existingGroup = context.GroupSet
                .FirstOrDefault(g => g.GroupYear == groupToAdd.GroupYear && g.GroupNumber == groupToAdd.GroupNumber);

            if (existingGroup == null)
            {
                context.GroupSet.Add(groupToAdd);
                context.SaveChanges();
                existingGroup = groupToAdd;
            }

            var studentToAdd = new StudentEntity
            {
                Name = "John",
                Lastname = "Doe",
                UrlPhoto = "https://www.google.com",
                GroupYear = existingGroup.GroupYear,
                GroupNumber = existingGroup.GroupNumber
            };

            context.StudentSet.Add(studentToAdd);
            context.SaveChanges();

            // Assert
            var studentFromDb = context.StudentSet.FirstOrDefault();
            Assert.NotNull(studentFromDb);

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

            var existingGroup = context.GroupSet
                .FirstOrDefault(g => g.GroupYear == groupToAdd.GroupYear && g.GroupNumber == groupToAdd.GroupNumber);

            if (existingGroup == null)
            {
                context.GroupSet.Add(groupToAdd);
                context.SaveChanges();
                existingGroup = groupToAdd;
            }

            var studentToAdd = new StudentEntity
            {
                Name = "John",
                Lastname = "Doe",
                UrlPhoto = "https://www.google.com",
                GroupYear = existingGroup.GroupYear,
                GroupNumber = existingGroup.GroupNumber
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

            var existingGroup = context.GroupSet
                .FirstOrDefault(g => g.GroupYear == groupToAdd.GroupYear && g.GroupNumber == groupToAdd.GroupNumber);

            if (existingGroup == null)
            {
                context.GroupSet.Add(groupToAdd);
                context.SaveChanges();
                existingGroup = groupToAdd;
            }

            var studentToAdd = new StudentEntity
            {
                Name = "John",
                Lastname = "Doe",
                UrlPhoto = "https://www.google.com",
                GroupYear = existingGroup.GroupYear,
                GroupNumber = existingGroup.GroupNumber
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
            }
        }
    }
}