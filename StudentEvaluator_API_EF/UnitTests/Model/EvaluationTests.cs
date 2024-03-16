using Client_Model;

namespace EF_UnitTests.Model;

public class EvaluationTests
{
    [Fact]
    public void TestConstructor()
    {
        // Arrange
        var id = 1L;
        var date = new DateTime(2024, 3, 16, 9, 0, 0);
        var course = "Mathematics";
        var grade = 10;
        var pairName = "toto";
        var teacher = new Teacher {Username = "ProfSmith", Password = "password", Roles = ["Teacher"]};
        var student = new Student (1,"John", "Doeeeee", "https:::/www.google.com",  1, 1);
        var template = new Template(1, "Template1", new List<Criteria>());        
        
        // Act
        var evaluation = new Evaluation(1,date, course , grade, pairName, teacher, template, student);
        
        // Assert
        Assert.Equal(date, evaluation.Date);
        Assert.Equal(grade, evaluation.Grade);
        Assert.Equal(student.Id, evaluation.Student.Id);
        Assert.Equal(template.Id, evaluation.Template.Id);
        Assert.Equal(teacher.Id, evaluation.Teacher.Id);
        Assert.Equal(pairName, evaluation.PairName);
    }

    [Fact]
    public void TestToString()
    {
        // Arrange
        var date = new DateTime(2024, 3, 16, 9, 0, 0);
        var course = "Mathematics";
        var grade = 10;
        var pairName = "toto";
        var teacher = new Teacher {Username = "ProfSmith", Password = "password", Roles = ["Teacher"]};
        var student = new Student (1,"John", "Doeeeee", "https:::/www.google.com",  1, 1);
        var template = new Template(1, "Template1", new List<Criteria>());
        var evaluation = new Evaluation(1,date, course , grade, pairName, teacher, template, student);
        var expectedFormat = $"Evaluation : 1, Mathematics, {new DateTime(2024, 3, 16, 9, 0, 0) }, 10, toto\n" +
                             "\tTeacher : ProfSmith\n" +
                             "\tStudent : John Doeeeee\n" +
                             "\tTemplate : Template1\n";
        
        // Act
        var toStringOutput = evaluation.ToString();
        
        // Assert
        Assert.Equal(expectedFormat, toStringOutput);
    }

}