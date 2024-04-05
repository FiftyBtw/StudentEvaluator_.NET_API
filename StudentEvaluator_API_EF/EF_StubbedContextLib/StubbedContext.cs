using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_StubbedContextLib;

/// <summary>
/// Represents a stubbed version of the database context.
/// </summary>
public class StubbedContext : LibraryContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StubbedContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public StubbedContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StubbedContext"/> class.
    /// </summary>
    public StubbedContext() : base()
    {
    }
    
    // All the data are seeded in SeedData.cs
}