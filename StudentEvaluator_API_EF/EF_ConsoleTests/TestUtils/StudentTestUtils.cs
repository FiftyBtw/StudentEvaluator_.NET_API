using EF_DbContextLib;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils;

// Classe regroupant les méthodes utilitaires pour les tests des Student
public class StudentTestUtils
{
    public static void AddStudent(LibraryContext context, string firstname, string lastname, string urlPhoto, int groupYear, int groupNumber)
    {
        context.StudentSet.Add(new StudentEntity { Name = firstname, Lastname = lastname, UrlPhoto = urlPhoto, GroupYear = groupYear, GroupNumber = groupNumber});
        context.SaveChanges();
        Console.WriteLine($"Student '{firstname} {lastname}' added to the database");
    }
    
    public static void DisplayAllStudents(LibraryContext context)
    {
        Console.WriteLine("\nAll students in the database:");
        foreach (var student in context.StudentSet)
        {
            Console.WriteLine($"ID {student.Id}: {student.Name} {student.Lastname} {student.UrlPhoto} {student.GroupYear} {student.GroupNumber}");
        }
        Console.WriteLine();
    }

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