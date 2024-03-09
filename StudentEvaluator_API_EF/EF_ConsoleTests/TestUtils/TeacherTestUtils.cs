using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_ConsoleTests.TestUtils;

/// <summary>
/// Class containing utility methods for Teacher tests.
/// </summary>
public class TeacherTestUtils
{

    /// <summary>
    /// Adds a new teacher to the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="username">The username of the teacher.</param>
    /// <param name="password">The password of the teacher.</param>
    /// <param name="roles">The roles of the teacher.</param>
    public static void AddTeacher(LibraryContext context, string username, string password, string[] roles)
    {
        context.TeacherSet.Add(new TeacherEntity { Username = username, Password = password, roles = roles });
        context.SaveChanges();
        Console.WriteLine($"Teacher '{username}' added to the database");
    }

    /// <summary>
    /// Displays all teachers in the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    public static void DisplayAllTeachers(LibraryContext context)
    {
        Console.WriteLine("\nAll teachers in the database:");
        foreach (var teacher in context.TeacherSet.Include(t => t.Lessons).Include(t => t.Templates)
                     .Include(t => t.Evaluations))
        {
            Console.WriteLine($"ID {teacher.Id}: {teacher.Username} {teacher.Password}");
            if (teacher.roles != null)
            {
                Console.WriteLine("Roles:");
                foreach (var role in teacher.roles)
                {
                    Console.WriteLine($"  - {role}");
                }
            }
            if (teacher.Lessons != null)
            {
                Console.WriteLine("Lessons:");
                foreach (var lesson in teacher.Lessons)
                {
                    Console.WriteLine($"  - {lesson.Id}: {lesson.CourseName} {lesson.Start}");
                }
            }

            if (teacher.Templates != null)
            {
                Console.WriteLine("Templates:");
                foreach (var template in teacher.Templates)
                {
                    Console.WriteLine($"  - {template.Id}: {template.Name}");
                }
            }

            if (teacher.Evaluations != null)
            {
                Console.WriteLine("Evaluations:");
                foreach (var evaluation in teacher.Evaluations)
                {
                    Console.WriteLine($"  - {evaluation.Id}: {evaluation.Date} {evaluation.CourseName}");
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Updates a teacher in the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="id">The ID of the teacher to update.</param>
    /// <param name="newUsername">The new username.</param>
    /// <param name="newPassword">The new password.</param>
    public static void UpdateTeacher(LibraryContext context, long id, string newUsername, string newPassword)
    {
        var teacher = context.TeacherSet.FirstOrDefault(t => t.Id == id);
        if (teacher != null)
        {
            teacher.Username = newUsername;
            teacher.Password = newPassword;
            context.SaveChanges();
            Console.WriteLine($"Teacher with id {id} updated");
        }
        else
        {
            Console.WriteLine($"Teacher with id {id} not found");
        }
    }

    /// <summary>
    /// Deletes a teacher from the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="id">The ID of the teacher to delete.</param>
    public static void DeleteTeacher(LibraryContext context, long id)
    {
        var teacher = context.TeacherSet.FirstOrDefault(t => t.Id == id);
        if (teacher != null)
        {
            context.TeacherSet.Remove(teacher);
            context.SaveChanges();
            Console.WriteLine($"Teacher with id {id} deleted");
        }
        else
        {
            Console.WriteLine($"Teacher with id {id} not found");
        }
    }
}