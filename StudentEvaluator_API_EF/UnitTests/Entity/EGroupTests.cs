using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class EGroupTests
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
}