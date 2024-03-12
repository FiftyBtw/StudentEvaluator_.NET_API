using EF_DbContextLib;
using EF_Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF_UnitTests.Entity;

public class EvaluationEntityTests
{
    [Fact]
    public void TestAddEvaluation()
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
            
            var evaluationToAdd = new EvaluationEntity
            {
                CourseName = "Entity Framework",
                Date = new DateTime(2024, 3, 24),
                StudentId = 1,
                TemplateId = 1,
                TeacherId = 1,
                Grade = 10,
                PairName = "toto"
            };

            context.EvaluationSet.Add(evaluationToAdd);
            context.SaveChanges();

            // Assert
            var evaluationFromDb = context.EvaluationSet.FirstOrDefault();
            Assert.NotNull(evaluationFromDb);
            
            Assert.Equal(evaluationToAdd.StudentId, evaluationFromDb.StudentId);
            Assert.Equal(evaluationToAdd.TemplateId, evaluationFromDb.TemplateId);
            Assert.Equal(evaluationToAdd.Grade, evaluationFromDb.Grade);
            Assert.Contains(context.StudentSet.FirstOrDefault().Evaluations, e => e.Id == evaluationFromDb.Id);
            Assert.Equal(context.TemplateSet.FirstOrDefault().Evaluation, evaluationFromDb);
        }
    }

    [Fact]
    public void TestUpdateEvaluation()
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

            var evaluationToAdd = new EvaluationEntity
            {
                CourseName = "Entity Framework",
                Date = new DateTime(2024, 3, 24),
                StudentId = 1,
                TemplateId = 1,
                TeacherId = 1,
                Grade = 10,
                PairName = "toto"
            };

            context.EvaluationSet.Add(evaluationToAdd);
            context.SaveChanges();

            evaluationToAdd.CourseName = "Entity Framework 2";
            evaluationToAdd.Date = new DateTime(2024, 3, 25);
            evaluationToAdd.Grade = 12;
            evaluationToAdd.PairName = "tata";

            context.EvaluationSet.Update(evaluationToAdd);
            context.SaveChanges();

            // Assert

            var evaluationFromDb = context.EvaluationSet.FirstOrDefault();
            Assert.NotNull(evaluationFromDb);

            Assert.Equal(evaluationToAdd.CourseName, evaluationFromDb.CourseName);
            Assert.Equal(evaluationToAdd.Date, evaluationFromDb.Date);
            Assert.Equal(evaluationToAdd.Grade, evaluationFromDb.Grade);
            Assert.Equal(evaluationToAdd.PairName, evaluationFromDb.PairName);
            
            Assert.Contains(context.StudentSet.FirstOrDefault().Evaluations, e => e.Id == evaluationFromDb.Id);
        }
    }

    [Fact]
    public void TestDeleteEvaluation()
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

            var evaluationToAdd = new EvaluationEntity
            {
                CourseName = "Entity Framework",
                Date = new DateTime(2024, 3, 24),
                StudentId = 1,
                TemplateId = 1,
                TeacherId = 1,
                Grade = 10,
                PairName = "toto"
            };

            context.EvaluationSet.Add(evaluationToAdd);
            context.SaveChanges();

            // Assert

            var evaluationFromDb = context.EvaluationSet.FirstOrDefault();
            Assert.NotNull(evaluationFromDb);

            Assert.Contains(context.StudentSet.FirstOrDefault().Evaluations, e => e.Id == evaluationFromDb.Id);

            context.EvaluationSet.Remove(evaluationFromDb);
            context.SaveChanges();

            Assert.Null(context.EvaluationSet.FirstOrDefault());
        }
    }

}