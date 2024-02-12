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
            

        }
    }

}
