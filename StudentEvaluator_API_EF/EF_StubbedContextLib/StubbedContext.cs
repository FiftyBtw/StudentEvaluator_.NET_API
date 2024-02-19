using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_StubbedContextLib
{
    public class StubbedContext : LibraryContext
    {

        // Méthode appelée lors de la création du modèle de la base de données
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appelle la méthode de la classe de base pour effectuer la configuration initiale du modèle
            base.OnModelCreating(modelBuilder);


            // Configure les données initiales (stub) pour l'entité GroupEntity
            modelBuilder.Entity<GroupEntity>().HasData(
                new GroupEntity { GroupYear = 1, GroupNumber = 1 },
                new GroupEntity { GroupYear = 1, GroupNumber = 2 },
                new GroupEntity { GroupYear = 1, GroupNumber = 3 }
            );
            
            
            // Configure les données initiales (stub) pour l'entité StudentEntity
            modelBuilder.Entity<StudentEntity>().HasData(
                new StudentEntity { Id = 1, Name = "Jean", Lastname = "Dupont", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-07b5ede889e54291946bfb76f2fc9780.jpg", GroupYear = 1, GroupNumber = 1},
                new StudentEntity { Id = 2, Name = "Marie", Lastname = "Durand", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-c60caadc22a7454fb452bf44157fa576.jpg", GroupYear = 1, GroupNumber = 2},
                new StudentEntity { Id = 3, Name = "Sophie", Lastname = "Leroy", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-939b96340eca4542a306cc87ac6d6b6e.jpg", GroupYear = 1, GroupNumber = 3}
            );

            // Configure les données initiales (stub) pour l'entité TeacherEntity
            modelBuilder.Entity<TeacherEntity>().HasData(
                new TeacherEntity { Id = 1, Username = "ProfDupont", Password = "$2y$10$kx6xmLJEiXGkVroyBT0CQetdvYRwK5EnuyUntvL3BxE5PHMBB8txe" ,roles = ["Teacher"], Templates = [] },
                new TeacherEntity { Id = 2, Username = "ProfDurand", Password = "$2y$10$v.CNQT.HevXlQcnJRh8p.u6MPlRUEWzKD9tDW.pT820nYkiUi8gEy", roles = ["Teacher"], Templates = [] },
                new TeacherEntity { Id = 3, Username = "ProfMartin", Password = "$2y$10$D9FztiIDadYSmCgy1gy.N.V.rwCTCGAvYckGacxpbWocS7DETTn8.", roles = ["Teacher"], Templates = [] },
                new TeacherEntity { Id = 4, Username = "ProfPetit", Password = "$2y$10$xL5hLjLYUphj2b2zFszg8.qiZCxpChGMmyOzVguqXYK7PoJrEPIOi", roles = ["Teacher"], Templates = [] },
                new TeacherEntity { Id = 5, Username = "ProfLeroy", Password = "$2y$10$uohwx42x7pZH/weZFgBSdu5A8ZNSgvxYrzH7v8QJgUnpfM8FWL8fG", roles = ["Teacher"], Templates = [] }
            );
            
            // Configure les données initiales (stub) pour l'entité TemplateEntity
            modelBuilder.Entity<TemplateEntity>().HasData(
                new TemplateEntity { Id = 1, Name = "Évaluation Générale", Criteria = [] },
                new TemplateEntity { Id = 2, Name = "Contrôle Continu", TeacherId = 2, Criteria = [] },
                new TemplateEntity { Id = 3, Name = "Examen Mi-Semestre", TeacherId = 1, Criteria = [] }
            );
           
            // Configure les données initiales (stub) pour l'entité SliderCriteriaEntity
            modelBuilder.Entity<SliderCriteriaEntity>().HasData(
                new SliderCriteriaEntity { Id = 1, Name = "Quiz 1", ValueEvaluation = 3, Value = 2 },
                new SliderCriteriaEntity { Id = 2, Name = "Quiz 2", ValueEvaluation = 2, Value = 3, TemplateId = 3},
                new SliderCriteriaEntity { Id = 3, Name = "Présentation en Classe", ValueEvaluation = 1, Value = 7, TemplateId = 2 }
            );
            
            modelBuilder.Entity<RadioCriteriaEntity>().HasData(
                new RadioCriteriaEntity { Id = 4, Name = "Type d’Examen", ValueEvaluation = 1, SelectedOption = "mi-semestre", Options = ["mi-semestre", "examen final"], TemplateId = 1 },
                new RadioCriteriaEntity { Id = 5, Name = "Type de Quiz", ValueEvaluation = 1, SelectedOption = "", Options = ["Quiz 1", "Quiz 2"], TemplateId = 3 },
                new RadioCriteriaEntity { Id = 6, Name = "Type de Présentation", ValueEvaluation = 1, SelectedOption = "Individuelle", Options = ["Individuelle", "Groupe"], TemplateId = 3 }
                
            );

        }
    }

}
