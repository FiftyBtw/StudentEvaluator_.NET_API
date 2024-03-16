using Client_Model;

namespace EF_UnitTests.Model;

public class RadioCriteriaTests
{
    [Fact]
    public void TestConstructor()
    {
        // Arrange
        var id = 20L;
        var name = "RadioCriteriaName";
        var valueEvaluation = 10L;
        var templateId = 3L;
        var selectedOption = "Option2";
        var options = new[] { "Option1", "Option2", "Option3" };

        // Act
        var radioCriteria = new RadioCriteria(id, name, valueEvaluation, templateId, selectedOption, options);

        // Assert
        Assert.Equal(id, radioCriteria.Id);
        Assert.Equal(name, radioCriteria.Name);
        Assert.Equal(valueEvaluation, radioCriteria.ValueEvaluation);
        Assert.Equal(templateId, radioCriteria.TemplateId);
        Assert.Equal(selectedOption, radioCriteria.SelectedOption);
        Assert.Equal(options, radioCriteria.Options);
    }

    [Fact]
    public void TestDefaultConstructor()
    {
        // Act
        var radioCriteria = new RadioCriteria();

        // Assert
        Assert.Equal(0, radioCriteria.Id);
        Assert.Equal("", radioCriteria.Name);
        Assert.Equal(0, radioCriteria.ValueEvaluation);
        Assert.Equal(0, radioCriteria.TemplateId);
        Assert.Equal("", radioCriteria.SelectedOption);
        Assert.Empty(radioCriteria.Options);
    }

    [Fact]
    public void TestToString()
    {
        // Arrange
        var radioCriteria = new RadioCriteria(20, "RadioCriteriaName", 10, 3, "Option2",
            new[] { "Option1", "Option2", "Option3" });
        var expectedFormat =
            "RadioCriteria : 20, Nom :RadioCriteriaName, Value :10, (3)\nOptions :Option1, Option2, Option3, Selected options : Option2\n\n";

        // Act
        var toStringOutput = radioCriteria.ToString();

        // Assert
        Assert.Equal(expectedFormat.TrimEnd([',', ' ']),
            toStringOutput.TrimEnd([',', ' '])); 
    }
}