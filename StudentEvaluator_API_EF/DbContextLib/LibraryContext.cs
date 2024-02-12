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
        
        // Méthode appelée lors de la création du modèle de la base de données
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appelle la méthode de la classe de base pour effectuer la configuration initiale du modèle
            base.OnModelCreating(modelBuilder);
            
            // Configure la clé primaire composite pour GroupStudent
            modelBuilder.Entity<GroupEntity>()
                .HasKey(g => new { g.GroupYear, g.GroupNumber });

            // Configure la relation entre les entités StudentEntity et GroupEntity
            modelBuilder.Entity<StudentEntity>()
                .HasOne<GroupEntity>(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => new { s.GroupYear, s.GroupNumber });
        }
    }

}
