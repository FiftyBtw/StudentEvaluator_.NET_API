using EF_DbContextLib;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils;

/// <summary>
/// Class containing utility methods for Student tests.
/// </summary>
public class StudentTestUtils
{
    /// <summary>
    /// Adds a new student to the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="firstname">The first name of the student.</param>
    /// <param name="lastname">The last name of the student.</param>
    /// <param name="urlPhoto">The URL of the student's photo.</param>
    /// <param name="groupYear">The group year of the student.</param>
    /// <param name="groupNumber">The group number of the student.</param>
    public static void AddStudent(LibraryContext context, string firstname, string lastname, string urlPhoto, int groupYear, int groupNumber)
    {
        context.StudentSet.Add(new StudentEntity { Name = firstname, Lastname = lastname, UrlPhoto = urlPhoto, GroupYear = groupYear, GroupNumber = groupNumber});
        context.SaveChanges();
        Console.WriteLine($"Student '{firstname} {lastname}' added to the database");
    }

    /// <summary>
    /// Displays all students in the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    public static void DisplayAllStudents(LibraryContext context)
    {
        Console.WriteLine("\nAll students in the database:");
        foreach (var student in context.StudentSet)
        {
            Console.WriteLine($"ID {student.Id}: {student.Name} {student.Lastname} {student.UrlPhoto} {student.GroupYear} {student.GroupNumber}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Updates a student in the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="id">The ID of the student to update.</param>
    /// <param name="newName">The new name of the student.</param>
    /// <param name="newLastname">The new last name of the student.</param>
    /// <param name="newUrlPhoto">The new URL of the student's photo.</param>
    public static void UpdateStudent(LibraryContext context, long id, string newName, string newLastname, string newUrlPhoto)
    {
        var student = context.StudentSet.FirstOrDefault(s => s.Id == id);
        if (student != null)
        {
            student.Name = newName;
            student.Lastname = newLastname;
            student.UrlPhoto = newUrlPhoto;
            context.SaveChanges();
            Console.WriteLine($"Student with id {id} updated");
        }
        else 
        {
            Console.WriteLine($"Student with id {id} not found");
        }
    }


    /// <summary>
    /// Deletes a student from the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="id">The ID of the student to delete.</param>
    public static void DeleteStudent(LibraryContext context, long id)
    {
        var student = context.StudentSet.FirstOrDefault(s => s.Id == id);
        if (student != null)
        {
            context.StudentSet.Remove(student);
            context.SaveChanges();
            Console.WriteLine($"Student with id {id} deleted");
        }
        else
        {
            Console.WriteLine($"Student with id {id} not found");
        }
    }
}