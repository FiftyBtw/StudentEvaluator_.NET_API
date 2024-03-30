using EF_DbContextLib;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils;

public static class EvaluationTestUtils
{
    public static void AddEvaluation(LibraryContext context, long templateId, string teacherId, long studentId, DateTime date, string courseName, string pairName, int grade)
    {
        var evaluationToAdd = new EvaluationEntity
        {
            TemplateId = templateId,
            TeacherId = teacherId,
            StudentId = studentId,
            Date = date,
            CourseName = courseName,
            PairName = pairName,
            Grade = grade
        };

        context.EvaluationSet.Add(evaluationToAdd);
        context.SaveChanges();
    }

    public static void DisplayAllEvaluations(LibraryContext context)
    {
        Console.WriteLine("\nAll evaluations in the database:");
        var evaluations = context.EvaluationSet.ToList();
        foreach (var evaluation in evaluations)
        {
            Console.WriteLine($"ID {evaluation.Id}: {evaluation.CourseName} (Teacher ID: {evaluation.TeacherId})");
            Console.WriteLine($"Teacher ID: {evaluation.TeacherId}");
            Console.WriteLine($"Student ID: {evaluation.StudentId}");
            Console.WriteLine();
        }
    }
    
    public static void UpdateEvaluation(LibraryContext context, long id,  string courseName, string pairName, int grade)
    {
        var evaluation = context.EvaluationSet.FirstOrDefault(e => e.Id == id);
        if (evaluation != null)
        {
            evaluation.CourseName = courseName;
            evaluation.PairName = pairName;
            evaluation.Grade = grade;
            context.SaveChanges();
        }
    }

    public static void DeleteEvaluation(LibraryContext context, long id)
    {
        var evaluation = context.EvaluationSet.FirstOrDefault(e => e.Id == id);
        if (evaluation != null)
        {
            context.EvaluationSet.Remove(evaluation);
            context.SaveChanges();
        }
    }
}