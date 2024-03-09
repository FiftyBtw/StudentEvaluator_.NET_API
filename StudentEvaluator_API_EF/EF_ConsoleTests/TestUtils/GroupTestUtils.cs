using EF_DbContextLib;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils;

/// <summary>
/// Class containing utility methods for Group tests.
/// </summary>
public static class GroupTestUtils
{
    /// <summary>
    /// Adds a new group to the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="gYear">The group year.</param>
    /// <param name="gNumber">The group number.</param>
    public static void AddGroup(LibraryContext context,  int gYear, int gNumber)
    {
        context.GroupSet.Add(new GroupEntity{GroupYear = gYear,  GroupNumber = gNumber});
        context.SaveChanges();
        Console.WriteLine($"Group '{gYear}-{gNumber}' added to the database");
    }

    /// <summary>
    /// Displays all groups in the database.
    /// </summary>
    /// <param name="context">The database context.</param>
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

    /// <summary>
    /// Deletes a group from the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="gYear">The group year of the group to delete.</param>
    /// <param name="gNumber">The group number of the group to delete.</param>
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