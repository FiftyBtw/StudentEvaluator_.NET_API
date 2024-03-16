using Client_Model;

namespace EF_UnitTests.Model;

public class SliderCriteriaTests
{
    [Fact]
    public void TestConstructor()
    {
        // Arrange
        var id = 10L;
        var name = "SliderCriteriaName";
        var valueEvaluation = 5L;
        var templateId = 2L;
        var sliderValue = 50L;

        // Act
        var sliderCriteria = new SliderCriteria(id, name, valueEvaluation, templateId, sliderValue);

        // Assert
        Assert.Equal(id, sliderCriteria.Id);
        Assert.Equal(name, sliderCriteria.Name);
        Assert.Equal(valueEvaluation, sliderCriteria.ValueEvaluation);
        Assert.Equal(templateId, sliderCriteria.TemplateId);
        Assert.Equal(sliderValue, sliderCriteria.Value);
    }

    [Fact]
    public void TestDefaultConstructor()
    {
        // Act
        var sliderCriteria = new SliderCriteria();

        // Assert
        Assert.Equal(0, sliderCriteria.Id);
        Assert.Equal("", sliderCriteria.Name);
        Assert.Equal(0, sliderCriteria.ValueEvaluation);
        Assert.Equal(0, sliderCriteria.TemplateId);
        Assert.Equal(0, sliderCriteria.Value); 
    }

    [Fact]
    public void TestToString()
    {
        // Arrange
        var sliderCriteria = new SliderCriteria(10, "SliderCriteriaName", 5, 2, 50);
        var expectedFormat = "SliderCriteria : 10,, Nom : SliderCriteriaName, Value : 5, SliderValue : 50, (2)\n\n";

        // Act
        var toStringOutput = sliderCriteria.ToString();

        // Assert
        Assert.Equal(expectedFormat, toStringOutput);
    }
}