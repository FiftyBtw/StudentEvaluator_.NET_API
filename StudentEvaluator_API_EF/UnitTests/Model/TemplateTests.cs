using Client_Model;

namespace EF_UnitTests.Model;

public class TemplateTests
{
    [Fact]
    public void TestConstructor()
    {
        // Arrange
        var id = 1L;
        var name = "Test Template";
        var criterias = new List<Criteria>
        {
            new TextCriteria(1, "TextCriteria1", 10, 1, "Sample Text 1"),
            new SliderCriteria(2, "SliderCriteria1", 5, 1, 50)
        };

        // Act
        var template = new Template(id, name, criterias);

        // Assert
        Assert.Equal(id, template.Id);
        Assert.Equal(name, template.Name);
        Assert.Equal(criterias.Count, template.Criterias.Count);
        foreach (var criteria in criterias)
        {
            Assert.Contains(criteria, template.Criterias);
        }
    }

    [Fact]
    public void TestToString()
    {
        // Arrange
        var id = 2L;
        var name = "Another Test Template";
        var criterias = new List<Criteria>
        {
            new RadioCriteria(3, "RadioCriteria1", 20, 2, "Option1", new[] {"Option1", "Option2"})
        };
        var template = new Template(id, name, criterias);
        
        var expectedStringBuilder = new System.Text.StringBuilder();
        expectedStringBuilder.AppendLine($"Template : {id}, {name}");
        expectedStringBuilder.AppendLine();
        expectedStringBuilder.AppendLine("Criterias :");
        foreach (var criteria in criterias)
        {
            expectedStringBuilder.Append(criteria.ToString());
        }
        var expectedFormat = expectedStringBuilder.ToString();

        // Act
        var toStringOutput = template.ToString();

        // Assert
        Assert.Equal(expectedFormat, toStringOutput);
    }
}