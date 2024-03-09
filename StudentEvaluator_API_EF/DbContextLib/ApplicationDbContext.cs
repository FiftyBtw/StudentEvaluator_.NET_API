using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TP_ConsoDev.Data;


/// <summary>
/// Represents the application's database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for the database context.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options) { }
}