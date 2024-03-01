using EF_DbContextLib;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils; 

// Classe regroupant les méthodes utilitaires pour les tests des Group
public static class GroupTestUtils
{
    public static void AddGroup(LibraryContext context,  int gYear, int gNumber)
    {
        context.GroupSet.Add(new GroupEntity{GroupYear = gYear,  GroupNumber = gNumber});
        context.SaveChanges();
        Console.WriteLine($"Group '{gYear}-{gNumber}' added to the database");
    }
    
    public static void DisplayAllGroups(LibraryContext context)
    {
        Console.WriteLine("\nAll groups in the database:");
        foreach (var group in context.GroupSet)
        {
            Console.WriteLine($"{group.GroupYear}A G{group.GroupNumber}");
            if (group.Students != null)
            {
                Console.WriteLine("Students:");
                foreach (var student in group.Students)
                {
                    Console.WriteLine($"  - {student.Id}: {student.Name} {student.Lastname}");
                }
            }
        }
        Console.WriteLine();
    }
    
    public static void DeleteGroup(LibraryContext context, int gYear, int gNumber)
    {
        var group = context.GroupSet.FirstOrDefault(g => g.GroupYear == gYear && g.GroupNumber == gNumber);
        if (group != null)
        {
            context.GroupSet.Remove(group);
            context.SaveChanges();
            Console.WriteLine($"Group '{gYear}-{gNumber}' deleted");
        }
        else
        {
            Console.WriteLine($"Group '{gYear}-{gNumber}' not found");
        }
    }
}