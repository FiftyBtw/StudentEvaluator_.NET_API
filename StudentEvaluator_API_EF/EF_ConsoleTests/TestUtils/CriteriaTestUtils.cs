using EF_DbContextLib;
using EF_Entities;
using Microsoft.Extensions.Options;

namespace EF_ConsoleTests.TestUtils;

public class CriteriaTestUtils
{
    // TextCriteria
    
    public static void AddTextCriteria(LibraryContext context, string name, long templateId, long valueEvaluation, string text)
    {
        var textCriteriaToAdd = new TextCriteriaEntity
        {
            Name = name,
            TemplateId = templateId,
            ValueEvaluation = valueEvaluation,
            Text = text
        };

        context.TextCriteriaSet.Add(textCriteriaToAdd);
        context.SaveChanges();
    }
    
    public static void DisplayAllTextCriteria(LibraryContext context)
    {
        Console.WriteLine("\nAll TextCriteria in the database:");
        var textCriteria = context.TextCriteriaSet.ToList();
        foreach (var criteria in textCriteria)
        {
            Console.WriteLine($"ID {criteria.Id}: {criteria.Name} (Template ID: {criteria.TemplateId})");
            Console.WriteLine($"Value Evaluation: {criteria.ValueEvaluation}");
            Console.WriteLine($"Text: {criteria.Text}");
            Console.WriteLine();
        }
    }
    
    public static void UpdateTextCriteria(LibraryContext context, long id, string text)
    {
        var criteria = context.TextCriteriaSet.FirstOrDefault(c => c.Id == id);
        if (criteria != null)
        {
            criteria.Text = text;
            context.SaveChanges();
        }
    }
    
    // SliderCriteria
    
    public static void AddSliderCriteria(LibraryContext context, string name, long templateId, long valueEvaluation, long value)
    {
        var sliderCriteriaToAdd = new SliderCriteriaEntity
        {
            Name = name,
            TemplateId = templateId,
            ValueEvaluation = valueEvaluation,
            Value = value
        };

        context.SliderCriteriaSet.Add(sliderCriteriaToAdd);
        context.SaveChanges();
    }
    
    public static void DisplayAllSliderCriteria(LibraryContext context)
    {
        Console.WriteLine("\nAll SliderCriteria in the database:");
        var sliderCriteria = context.SliderCriteriaSet.ToList();
        foreach (var criteria in sliderCriteria)
        {
            Console.WriteLine($"ID {criteria.Id}: {criteria.Name} (Template ID: {criteria.TemplateId})");
            Console.WriteLine($"Value Evaluation: {criteria.ValueEvaluation}");
            Console.WriteLine($"Value: {criteria.Value}");
            Console.WriteLine();
        }
    }
    
    public static void UpdateSliderCriteria(LibraryContext context, long id, long value)
    {
        var criteria = context.SliderCriteriaSet.FirstOrDefault(c => c.Id == id);
        if (criteria != null)
        {
            criteria.Value = value;
            context.SaveChanges();
        }
    }
    
    // RadioCriteria
    
    public static void AddRadioCriteria(LibraryContext context, string name, long templateId, long valueEvaluation, string[] options, string selectedOption)
    {
        var radioCriteriaToAdd = new RadioCriteriaEntity
        {
            Name = name,
            TemplateId = templateId,
            ValueEvaluation = valueEvaluation,
            Options = options,
            SelectedOption = selectedOption
        };

        context.RadioCriteriaSet.Add(radioCriteriaToAdd);
        context.SaveChanges();
    }
    
    public static void DisplayAllRadioCriteria(LibraryContext context)
    {
        Console.WriteLine("\nAll RadioCriteria in the database:");
        var radioCriteria = context.RadioCriteriaSet.ToList();
        foreach (var criteria in radioCriteria)
        {
            Console.WriteLine($"ID {criteria.Id}: {criteria.Name} (Template ID: {criteria.TemplateId})");
            Console.WriteLine($"Value Evaluation: {criteria.ValueEvaluation}");
            Console.WriteLine($"Options: {string.Join(", ", criteria.Options)}");
            Console.WriteLine($"Selected Option: {criteria.SelectedOption}");
            Console.WriteLine();
        }
    }
    
    public static void UpdateRadioCriteria(LibraryContext context, long id, string selectedOption)
    {
        var criteria = context.RadioCriteriaSet.FirstOrDefault(c => c.Id == id);
        if (criteria != null)
        {
            criteria.SelectedOption = selectedOption;
            context.SaveChanges();
        }
    }
    
    public static void DeleteCriteria(LibraryContext context, long id)
    {
        var criteria = context.CriteriaSet.FirstOrDefault(c => c.Id == id);
        if (criteria != null)
        {
            context.CriteriaSet.Remove(criteria);
            context.SaveChanges();
        }
    }
}