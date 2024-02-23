using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite("Data Source=StudentEvaluator_API_EF.db")
            .Options;
        var context = new LibraryContext(options);
        
        // Create the database if it doesn't exist
        await context.Database.EnsureCreatedAsync();

        using (context)
        {
            // Add a new student
            var newStudent = new StudentEntity { Name = "John", Lastname = "Doe", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-07b5ede889e54291946bfb76f2fc9780.jpg", GroupYear = 1, GroupNumber = 1 };
            context.StudentSet.Add(newStudent);
            await context.SaveChangesAsync();
            
            
            // Display all students
            var students = await context.StudentSet.ToListAsync();
            foreach (var s in students)
            {
                Console.WriteLine($"Student: {s.Name} {s.Lastname}");
            }
            
            // Display all groups
            var groups = await context.GroupSet.ToListAsync();
            foreach (var g in groups)
            {
                Console.WriteLine($"Group: {g.GroupYear} {g.GroupNumber}");
            }
            
            // Add a new group
            var newGroup = new GroupEntity { GroupYear = 2, GroupNumber = 1 };
            context.GroupSet.Add(newGroup);
            await context.SaveChangesAsync();
            
        }
    }
}