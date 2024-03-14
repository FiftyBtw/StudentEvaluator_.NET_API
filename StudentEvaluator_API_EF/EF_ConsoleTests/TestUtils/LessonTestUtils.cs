using EF_DbContextLib;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils;

public class LessonTestUtils
{
    public static void AddLesson(LibraryContext context, string courseName, DateTime start, DateTime end, long teacherId, string classroom, int groupYear, int groupNumber)
    {
        var lessonToAdd = new LessonEntity
        {
            CourseName = courseName,
            Start = start,
            End = end,
            TeacherEntityId = teacherId,
            Classroom = classroom,
            GroupYear = groupYear,
            GroupNumber = groupNumber,
        };

        context.LessonSet.Add(lessonToAdd);
        context.SaveChanges();
    }
    
    public  static void UpdateLesson(LibraryContext context, long id, string courseName, DateTime start, DateTime end, long teacherId, string classroom, int groupYear, int groupNumber)
    {
        var lesson = context.LessonSet.FirstOrDefault(l => l.Id == id);
        if (lesson != null)
        {
            lesson.CourseName = courseName;
            lesson.Start = start;
            lesson.End = end;
            lesson.TeacherEntityId = teacherId;
            lesson.Classroom = classroom;
            lesson.GroupYear = groupYear;
            lesson.GroupNumber = groupNumber;
            context.SaveChanges();
        }
    }

    public static void DisplayAllLessons(LibraryContext context)
    {
        Console.WriteLine("\nAll lessons in the database:");
        var lessons = context.LessonSet.ToList();
        foreach (var lesson in lessons)
        {
            Console.WriteLine($"ID {lesson.Id}: {lesson.CourseName} (Teacher ID: {lesson.TeacherEntityId})");
            Console.WriteLine($"Start: {lesson.Start}");
            Console.WriteLine($"End: {lesson.End}");
            Console.WriteLine();
        }
    }

    public static void DeleteLesson(LibraryContext context, long id)
    {
        var lesson = context.LessonSet.FirstOrDefault(l => l.Id == id);
        if (lesson != null)
        {
            context.LessonSet.Remove(lesson);
            context.SaveChanges();
        }
    }
}