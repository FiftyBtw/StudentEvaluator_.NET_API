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
        // Propriété DbSet pour représenter l'ensemble de teachers dans la base de données
        public DbSet<UserEntity> UserSet { get; set; }
        // Propriété DbSet pour représenter l'ensemble de templates dans la base de données
        public DbSet<TemplateEntity> TemplateSet { get; set; }

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
            
            // Configure la clé primaire pour EvaluationEntity
            modelBuilder.Entity<CriteriaEntity>()
                .HasDiscriminator<string>("criteria_type")
                .HasValue<SliderCriteriaEntity>("slider")
                .HasValue<TextCriteriaEntity>("text")
                .HasValue<RadioCriteriaEntity>("radio");
            
            modelBuilder.Entity<UserEntity>()
                .HasDiscriminator<string>("user_type")
                .HasValue<TeacherEntity>("teacher");    

            // Configure la relation entre les entités StudentEntity et GroupEntity
            modelBuilder.Entity<StudentEntity>()
                .HasOne<GroupEntity>(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => new { s.GroupYear, s.GroupNumber });
            
            modelBuilder.Entity<GroupEntity>()
                .HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(s => new { s.GroupYear, s.GroupNumber });
            
            // Configure la relation entre les entités TeacherEntity et TemplateEntity
            modelBuilder.Entity<TeacherEntity>()
                .HasMany<TemplateEntity>(t => t.Templates)
                .WithOne(te => te.Teacher)
                .HasForeignKey(te => te.TeacherId);

            modelBuilder.Entity<TemplateEntity>()
                .HasOne<TeacherEntity>(te => te.Teacher)
                .WithMany(t => t.Templates)
                .HasForeignKey(te => te.TeacherId);
            
            // Configure la relation entre les entités TemplateEntity et CriteriaEntity
            modelBuilder.Entity<TemplateEntity>()
                .HasMany(t => t.Criteria)
                .WithOne(c => c.Template)
                .HasForeignKey(c => c.TemplateId)
                .IsRequired(false);
            
            modelBuilder.Entity<CriteriaEntity>()
                .HasOne(c => c.Template)
                .WithMany(t => t.Criteria)
                .HasForeignKey(c => c.TemplateId)
                .IsRequired(false);
            
            // Configure la relation entre les entités EvaluationEntity et TemplateEntity
            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(e => e.Template)
                .WithMany(t => t.Evaluations)
                .HasForeignKey(e => e.TemplateId)
                .IsRequired(false);
            
            modelBuilder.Entity<TemplateEntity>()
                .HasMany(t => t.Evaluations)
                .WithOne(e => e.Template)
                .HasForeignKey(e => e.TemplateId)
                .IsRequired(false);
            
            // Configure la relation entre les entités EvaluationEntity et TeacherEntity
            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(e => e.Teacher)
                .WithMany(t => t.Evaluations)
                .HasForeignKey(e => e.TeacherId)
                .IsRequired(false);
            
            modelBuilder.Entity<TeacherEntity>()
                .HasMany(t => t.Evaluations)
                .WithOne(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .IsRequired(false);
            
            // Configure la relation entre les entités EvaluationEntity et StudentEntity
            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Evaluations)
                .HasForeignKey(e => e.StudentId)
                .IsRequired(false);

            modelBuilder.Entity<StudentEntity>()
                .HasMany(e => e.Evaluations)
                .WithOne(s => s.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired(false);


            //Configure la relation entre les entités LessonEntity et GroupEntity
            modelBuilder.Entity<LessonEntity>()
               .HasOne<GroupEntity>(s => s.Group)
               .WithMany(g => g.Lessons)
               .HasForeignKey(s => new { s.GroupYear, s.GroupNumber });

            modelBuilder.Entity<GroupEntity>()
                .HasMany(g => g.Lessons)
                .WithOne(s => s.Group)
                .HasForeignKey(s => new { s.GroupYear, s.GroupNumber });
        }
    }

}
