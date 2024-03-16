using Client_Model;

namespace EF_UnitTests.Model;

public class TextCriteriaTests
{
    [Fact]
    public void TestConstructor()
    {
        // Arrange
        var id = 10L;
        var name = "CriteriaName";
        var valueEvaluation = 5L;
        var templateId = 2L;
        var text = "Sample text";

        // Act
        var textCriteria = new TextCriteria(id, name, valueEvaluation, templateId, text);

        // Assert
        Assert.Equal(id, textCriteria.Id);
        Assert.Equal(name, textCriteria.Name);
        Assert.Equal(valueEvaluation, textCriteria.ValueEvaluation);
        Assert.Equal(templateId, textCriteria.TemplateId);
        Assert.Equal(text, textCriteria.Text);
    }
    
    [Fact]
    public void TestDefaultConstructor()
    {
        // Act
        var textCriteria = new TextCriteria();

        // Assert
        Assert.Equal(0, textCriteria.Id);
        Assert.Equal("", textCriteria.Name);
        Assert.Equal(0, textCriteria.ValueEvaluation);
        Assert.Equal(0, textCriteria.TemplateId);
        Assert.Equal("", textCriteria.Text);
    }

    [Fact]
    public void TestToString()
    {
        // Arrange
        var textCriteria = new TextCriteria(10, "CriteriaName", 5, 2, "Sample text");
        var expectedFormat = "TextCriteria : 10, Nom :CriteriaName, Value :5, (2)\nText : Sample text\n\n";

        // Act
        var toStringOutput = textCriteria.ToString();

        // Assert
        Assert.Equal(expectedFormat, toStringOutput);
    }
}