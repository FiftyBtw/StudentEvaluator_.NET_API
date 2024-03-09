using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;
using EF_ConsoleTests.TestUtils;


/// <summary>
/// Represents the main program class.
/// </summary>
class Program
{

    /// <summary>
    /// The entry point of the program.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite("Data Source=StudentEvaluator_API_EF_Tests.db")
            .Options;
        var context = new LibraryContext(options);
        
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        
        using (context)
        {
            Console.WriteLine("\n--------  Group  --------\n");
            
            // AddGroup
            GroupTestUtils.AddGroup(context, 1, 1);
            GroupTestUtils.AddGroup(context, 1, 2);
            GroupTestUtils.AddGroup(context, 2, 1);
            GroupTestUtils.AddGroup(context, 2, 2);

            // DisplayAllGroups
            GroupTestUtils.DisplayAllGroups(context);
            
            // DeleteGroup
            GroupTestUtils.DeleteGroup(context, 1, 2);
            GroupTestUtils.DeleteGroup(context, 2, 1);
            
            // DisplayAllGroups
            GroupTestUtils.DisplayAllGroups(context);

            
            Console.WriteLine("--------  Student  --------\n");
            
            // AddStudent
            StudentTestUtils.AddStudent(context, "John", "Doe", "url", 1, 1);
            StudentTestUtils.AddStudent(context, "Bob", "Dylan", "url", 1, 1);
            StudentTestUtils.AddStudent(context, "Alice", "Cooper", "url", 2, 2);
            StudentTestUtils.AddStudent(context, "Elvis", "Presley", "url", 2, 2);
            
            // DisplayAllStudents
            StudentTestUtils.DisplayAllStudents(context);
            
            // UpdateStudent
            StudentTestUtils.UpdateStudent(context, 1 ,"John", "Doe", "newUrl");
            
            // DeleteStudent
            StudentTestUtils.DeleteStudent(context, 2);
            
            // DisplayAllStudents
            StudentTestUtils.DisplayAllStudents(context);
            
            // DisplayAllGroups 
            GroupTestUtils.DisplayAllGroups(context);
            
            
            Console.WriteLine("\n--------  Teacher  --------\n");
            
            // AddTeacher
            TeacherTestUtils.AddTeacher(context, "John", "Doe", ["Teacher"]);
            TeacherTestUtils.AddTeacher(context, "Bob", "Dylan", ["Teacher"]);
            TeacherTestUtils.AddTeacher(context, "Alice", "Cooper", ["Teacher"]);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
            // UpdateTeacher
            TeacherTestUtils.UpdateTeacher(context, 1, "John", "Doe");
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
            // DeleteTeacher
            TeacherTestUtils.DeleteTeacher(context, 2);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
        }
    }
}