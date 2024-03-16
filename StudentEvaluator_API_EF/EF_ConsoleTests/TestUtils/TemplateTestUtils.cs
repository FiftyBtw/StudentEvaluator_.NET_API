using EF_DbContextLib;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils;

public static class TemplateTestUtils
{
    public static void AddTemplate(LibraryContext context, string name, long teacherId)
    {
        var templateToAdd = new TemplateEntity
        {
            Name = name,
            TeacherId = teacherId
        };

        context.TemplateSet.Add(templateToAdd);
        context.SaveChanges();
    }

    public static void DisplayAllTemplates(LibraryContext context)
    {
        Console.WriteLine("\nAll templates in the database:");
        var templates = context.TemplateSet.ToList();
        foreach (var template in templates)
        {
            Console.WriteLine($"ID {template.Id}: {template.Name} (Teacher ID: {template.TeacherId})");
            
            if (template.Criteria != null)
            {
                Console.WriteLine("Criteria:");
                foreach (var criteria in template.Criteria)
                {
                    Console.WriteLine($"  - {criteria.Id}: {criteria.Name}");
                }
            }
        }
        Console.WriteLine();
    }

    public static void DeleteTemplate(LibraryContext context, long id)
    {
        var template = context.TemplateSet.FirstOrDefault(t => t.Id == id);
        if (template != null)
        {
            if (template.Criteria != null)
            {
                foreach (var criteria in template.Criteria)
                {
                    context.CriteriaSet.Remove(criteria);
                }
            }
            context.TemplateSet.Remove(template);
            context.SaveChanges();
        }
    }
}