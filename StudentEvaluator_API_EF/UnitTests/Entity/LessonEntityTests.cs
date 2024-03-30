using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class LessonEntityTests
{
    /*
    [Fact]
    public void TestAddLesson()
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
            
            var lessonToAdd = new LessonEntity
            {
                GroupYear = groupToAdd.GroupYear,
                GroupNumber = groupToAdd.GroupNumber,
                TeacherEntityId = 1,
                Classroom = "A19",
                CourseName = "Entity Framework",
                Start = new DateTime(2024, 3, 12),
                End = new DateTime(2024, 3, 12).AddHours(2)
            };

            context.LessonSet.Add(lessonToAdd);
            context.SaveChanges();
            
            // Assert
            var lessonFromDb = context.LessonSet.Include(l => l.Group).Include(l => l.Teacher).FirstOrDefault();
            Assert.NotNull(lessonFromDb);
            
            Assert.Equal(lessonToAdd.GroupYear, lessonFromDb.GroupYear);
            Assert.Equal(lessonToAdd.GroupNumber, lessonFromDb.GroupNumber);
            Assert.Equal(lessonToAdd.Group, groupToAdd);
            Assert.Equal(lessonToAdd.TeacherEntityId, lessonFromDb.TeacherEntityId);
            Assert.Equal(lessonToAdd.Teacher, teacherToAdd);
            Assert.Equal(teacherToAdd.Id, lessonFromDb.TeacherEntityId);
            Assert.Equal(lessonToAdd.Classroom, lessonFromDb.Classroom);
            Assert.Equal(lessonToAdd.CourseName, lessonFromDb.CourseName);
            Assert.Equal(lessonToAdd.Start, lessonFromDb.Start);
            Assert.Equal(lessonToAdd.End, lessonFromDb.End);
            
            Assert.Contains(context.GroupSet.FirstOrDefault().Lessons, l => l.Id == lessonFromDb.Id);
            Assert.Contains(context.TeacherSet.FirstOrDefault().Lessons, l => l.Id == lessonFromDb.Id);
        }
    }

    [Fact]
    public void TestUpdateLesson()
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

            var lessonToAdd = new LessonEntity
            {
                GroupYear = groupToAdd.GroupYear,
                GroupNumber = groupToAdd.GroupNumber,
                TeacherEntityId = 1,
                Classroom = "A19",
                CourseName = "Entity Framework",
                Start = new DateTime(2024, 3, 12),
                End = new DateTime(2024, 3, 12).AddHours(2)
            };

            context.LessonSet.Add(lessonToAdd);
            context.SaveChanges();

            lessonToAdd.Classroom = "A20";
            lessonToAdd.CourseName = "Entity Framework 2";
            lessonToAdd.Start = new DateTime(2024, 3, 25);
            lessonToAdd.End = new DateTime(2024, 3, 25).AddHours(2);

            context.LessonSet.Update(lessonToAdd);
            context.SaveChanges();

            // Assert
            var lessonFromDb = context.LessonSet.FirstOrDefault();
            Assert.NotNull(lessonFromDb);

            Assert.Equal(lessonToAdd.GroupYear, lessonFromDb.GroupYear);
            Assert.Equal(lessonToAdd.GroupNumber, lessonFromDb.GroupNumber);
            Assert.Equal(lessonToAdd.TeacherEntityId, lessonFromDb.TeacherEntityId);
            Assert.Equal(lessonToAdd.Classroom, lessonFromDb.Classroom);
            Assert.Equal(lessonToAdd.CourseName, lessonFromDb.CourseName);
            Assert.Equal(lessonToAdd.Start, lessonFromDb.Start);
            Assert.Equal(lessonToAdd.End, lessonFromDb.End);

            Assert.Contains(context.GroupSet.FirstOrDefault().Lessons, l => l.Id == lessonFromDb.Id);
            Assert.Contains(context.TeacherSet.FirstOrDefault().Lessons, l => l.Id == lessonFromDb.Id);
        }
    }

    [Fact]
    public void TestDeleteLesson()
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

            var lessonToAdd = new LessonEntity
            {
                GroupYear = groupToAdd.GroupYear,
                GroupNumber = groupToAdd.GroupNumber,
                TeacherEntityId = 1,
                Classroom = "A19",
                CourseName = "Entity Framework",
                Start = new DateTime(2024, 3, 12),
                End = new DateTime(2024, 3, 12).AddHours(2)
            };

            context.LessonSet.Add(lessonToAdd);
            context.SaveChanges();

            context.LessonSet.Remove(lessonToAdd);
            context.SaveChanges();

            // Assert
            var lessonFromDb = context.LessonSet.FirstOrDefault();
            Assert.Null(lessonFromDb);
            Assert.Empty(context.GroupSet.FirstOrDefault().Lessons);
            Assert.Empty(context.TeacherSet.FirstOrDefault().Lessons);
        }
    }
    */
}