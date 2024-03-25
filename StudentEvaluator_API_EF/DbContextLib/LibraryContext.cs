using EF_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EF_DbContextLib
{
    /// <summary>
    /// Represents the database context for the library.
    /// </summary>
    public class LibraryContext : IdentityDbContext<TeacherEntity>
    {
        // Propriété DbSet pour représenter l'ensemble de students dans la base de données
        public DbSet<StudentEntity> StudentSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de groupes dans la base de données
        public DbSet<GroupEntity> GroupSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de templates dans la base de données
        public DbSet<TemplateEntity> TemplateSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de criteria dans la base de données
        public DbSet<CriteriaEntity> CriteriaSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de sliderCriteria dans la base de données
        public DbSet<SliderCriteriaEntity> SliderCriteriaSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de textCriteria dans la base de données
        public DbSet<TextCriteriaEntity> TextCriteriaSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de radioCriteria dans la base de données
        public DbSet<RadioCriteriaEntity> RadioCriteriaSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble des evaluations dans la base de données
        public DbSet<EvaluationEntity> EvaluationSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble des cours dans la base de données
        public DbSet<LessonEntity> LessonSet { get; set; }
        
        public LibraryContext() { }
        
        public LibraryContext(DbContextOptions options) : base(options) { }
        
        /// <summary>
        /// Method called when configuring the database context.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the database context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Méthode appelée lors de la configuration du contexte de la base de données
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                // Utilise SQLite comme fournisseur de base de données avec le chemin spécifié
                optionsBuilder.UseSqlite($"Data Source=StudentEvaluator_API_EF.db")
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
            }

        }

        /// <summary>
        /// Method called when building the database model.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Clé primaire composite pour GroupEntity
            modelBuilder.Entity<GroupEntity>()
                .HasKey(g => new { g.GroupYear, g.GroupNumber });

            // Héritage pour CriteriaEntity et UserEntity
            modelBuilder.Entity<CriteriaEntity>()
                .HasDiscriminator<string>("criteria_type")
                .HasValue<SliderCriteriaEntity>("slider")
                .HasValue<TextCriteriaEntity>("text")
                .HasValue<RadioCriteriaEntity>("radio");

            /*
            modelBuilder.Entity<UserEntity>()
                .HasDiscriminator<string>("user_type")
                .HasValue<TeacherEntity>("teacher");
            */

            // Relation entre StudentEntity et GroupEntity
            modelBuilder.Entity<StudentEntity>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => new { s.GroupYear, s.GroupNumber });

            // Relation entre TeacherEntity et TemplateEntity
            modelBuilder.Entity<TeacherEntity>()
                .HasMany(t => t.Templates)
                .WithOne(te => te.Teacher)
                .HasForeignKey(te => te.TeacherId);

            // Relation entre TemplateEntity et CriteriaEntity
            modelBuilder.Entity<TemplateEntity>()
                .HasMany(t => t.Criteria)
                .WithOne(c => c.Template)
                .HasForeignKey(c => c.TemplateId);

            // Relation un-à-un entre EvaluationEntity et TemplateEntity
            modelBuilder.Entity<TemplateEntity>()
                .HasOne(t => t.Evaluation)
                .WithOne(e => e.Template)
                .HasForeignKey<TemplateEntity>(t => t.EvaluationId);


            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(e => e.Template)
                .WithOne(t => t.Evaluation)
                .HasForeignKey<EvaluationEntity>(e => e.TemplateId);


            // Relation entre EvaluationEntity et TeacherEntity
            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(e => e.Teacher)
                .WithMany(t => t.Evaluations)
                .HasForeignKey(e => e.TeacherId);

            // Relation entre EvaluationEntity et StudentEntity
            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Evaluations)
                .HasForeignKey(e => e.StudentId);

            // Relation entre LessonEntity et GroupEntity
            modelBuilder.Entity<LessonEntity>()
                .HasOne(l => l.Group)
                .WithMany(g => g.Lessons)
                .HasForeignKey(l => new { l.GroupYear, l.GroupNumber });

            // Relation entre LessonEntity et TeacherEntity
            modelBuilder.Entity<LessonEntity>()
                .HasOne(l => l.Teacher)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TeacherEntityId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new()
                {
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
            
        }
    }

}
