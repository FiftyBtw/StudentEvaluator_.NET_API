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


            // Configure les données initiales (stub) pour l'entité StudentEntity
            modelBuilder.Entity<StudentEntity>().HasData(
                new StudentEntity
                {
                    Id = 1,
                    Name = "Jean",
                    Lastname = "Dupont",
                    UrlPhoto= "https://u-static.fotor.com/images/text-to-image/result/PRO-07b5ede889e54291946bfb76f2fc9780.jpg",

                },
                new StudentEntity
                {
                    Id = 2,
                    Name = "Marie",
                    Lastname = "Durand",
                    UrlPhoto= "https://u-static.fotor.com/images/text-to-image/result/PRO-c60caadc22a7454fb452bf44157fa576.jpg",
                },
                 new StudentEntity
                 {
                     Id = 3,
                     Name = "Lucas",
                     Lastname = "Martin",
                     UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-ab5de70d249c440aa6031f9e33224c61.jpg",
                 }
            );

            // Configure les données initiales (stub) pour l'entité GroupEntity
            modelBuilder.Entity<GroupEntity>().HasData(
                new GroupEntity
                {
                   GroupNumber = 1,
                   GroupYear = 1,
                },
                new GroupEntity
                {
                    GroupNumber = 2,
                    GroupYear = 1,
                },
                new GroupEntity
                {
                    GroupNumber = 3,
                    GroupYear = 1,
                }
            );



        }
    }

}
