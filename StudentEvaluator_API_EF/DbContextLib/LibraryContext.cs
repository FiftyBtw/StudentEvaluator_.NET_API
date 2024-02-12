using EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_DbContextLib
{
    public class LibraryContext : DbContext
    {
        // Propriété DbSet pour représenter l'ensemble de students dans la base de données
        public DbSet<StudentEntity> StudentSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de groupes dans la base de données
        public DbSet<GroupEntity> GroupSet { get; set; }

        public LibraryContext() { }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }


        // Méthode appelée lors de la configuration du contexte de la base de données
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Méthode appelée lors de la configuration du contexte de la base de données
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                // Utilise SQLite comme fournisseur de base de données avec le chemin spécifié
                optionsBuilder.UseSqlite($"Data Source=StudentEvaluator_API_EF.db");
            }

        }
    }

}
