using Microsoft.AspNetCore.Identity;
using EF_Entities;

namespace EF_ConsoleTests.TestUtils;

public static class TeacherTestUtils
{
    public static async Task<string> AddTeacherAsync(UserManager<TeacherEntity> userManager, string userName,
        string password)
    {
        var user = new TeacherEntity { UserName = userName };
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            Console.WriteLine($"Teacher '{userName}' added with ID {user.Id}");
            return user.Id;
        }
        else
        {
            throw new InvalidOperationException(
                $"Failed to create teacher {userName}: {string.Join(", ", result.Errors)}");
        }
    }

    public static async Task UpdateTeacherAsync(UserManager<TeacherEntity> userManager, string userId,
        string newUserName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            user.UserName = newUserName;
            // La mise à jour du mot de passe peut nécessiter des étapes supplémentaires, comme la réinitialisation
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                Console.WriteLine($"Teacher with ID {userId} updated");
            }
            else
            {
                throw new InvalidOperationException(
                    $"Failed to update teacher {userId}: {string.Join(", ", result.Errors)}");
            }
        }
        else
        {
            Console.WriteLine($"Teacher with ID {userId} not found");
        }
    }

    public static async Task DeleteTeacherAsync(UserManager<TeacherEntity> userManager, string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                Console.WriteLine($"Teacher with ID {userId} deleted");
            }
            else
            {
                throw new InvalidOperationException(
                    $"Failed to delete teacher {userId}: {string.Join(", ", result.Errors)}");
            }
        }
        else
        {
            Console.WriteLine($"Teacher with ID {userId} not found");
        }
    }

    public static void DisplayAllTeachers(UserManager<TeacherEntity> userManager)
    {
        var users = userManager.Users;
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.UserName}, Email: {user.Email}");
        }
    }
}
