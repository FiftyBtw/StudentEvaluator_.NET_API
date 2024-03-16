using Client_Model;

namespace EF_UnitTests.Model;

public class EvaluationCreationTests
{
    [Fact]
    public void TestConstructor()
    {
        // Arrange
        var date = new DateTime(2024, 3, 16, 9, 0, 0);
        var courseName = "Mathematics";
        var teacherId = 1;
        var templateId = 1;
        var studentId = 1;
        var grade = 10;
        var pairName = "toto";

        // Act
        var evaluation = new EvaluationCreation(date, courseName, grade, pairName, teacherId, templateId, studentId);

        // Assert
        Assert.Equal(date, evaluation.Date);
        Assert.Equal(courseName, evaluation.CourseName);
        Assert.Equal(teacherId, evaluation.TeacherId);
        Assert.Equal(templateId, evaluation.TemplateId);
        Assert.Equal(studentId, evaluation.StudentId);
        Assert.Equal(grade, evaluation.Grade);
        Assert.Equal(pairName, evaluation.PairName);
        
        evaluation.CourseName = "Math";
        Assert.Equal("Math", evaluation.CourseName);
    }
}