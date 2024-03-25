using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EF_Entities; // Remplacez par le bon espace de noms de vos entités
using EF_DbContextLib; // Remplacez par le bon espace de noms de votre DbContext

namespace EF_StubbedContextLib;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TeacherEntity>>();
            var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

            // Création des utilisateurs (enseignants)
            var teachers = new List<(string UserName, string Password)>
            {
                ("ProfDupont", "InstructorPassword1$"),
                ("ProfDurand", "InstructorPassword2$"),
                ("ProfMartin", "InstructorPassword3$"),
                ("ProfPetit", "InstructorPassword4$"),
                ("ProfLeroy", "InstructorPassword5$")
            };

            var profDupont = await EnsureUserAsync(userManager, teachers[0].Password, teachers[0].UserName);
            var profDurand = await EnsureUserAsync(userManager, teachers[1].Password, teachers[1].UserName);
            var profMartin = await EnsureUserAsync(userManager, teachers[2].Password, teachers[2].UserName);
            var profPetit = await EnsureUserAsync(userManager, teachers[3].Password, teachers[3].UserName);
            var profLeroy = await EnsureUserAsync(userManager, teachers[4].Password, teachers[4].UserName);

            if (!await context.GroupSet.AnyAsync())
            {
                context.GroupSet.AddRange(new List<GroupEntity>
                {
                    new() { GroupYear = 1, GroupNumber = 1 },
                    new() { GroupYear = 1, GroupNumber = 2 },
                    new() { GroupYear = 1, GroupNumber = 3 },
                    new() { GroupYear = 1, GroupNumber = 4 },
                    new() { GroupYear = 1, GroupNumber = 5 },
                    new() { GroupYear = 1, GroupNumber = 6 },
                    new() { GroupYear = 1, GroupNumber = 7 },
                    new() { GroupYear = 1, GroupNumber = 8 },
                    new() { GroupYear = 1, GroupNumber = 9 },
                    new() { GroupYear = 1, GroupNumber = 10 },
                    new() { GroupYear = 2, GroupNumber = 1 },
                    new() { GroupYear = 2, GroupNumber = 2 },
                    new() { GroupYear = 2, GroupNumber = 3 },
                    new() { GroupYear = 2, GroupNumber = 4 },
                    new() { GroupYear = 2, GroupNumber = 5 },
                    new() { GroupYear = 2, GroupNumber = 6 },
                    new() { GroupYear = 2, GroupNumber = 7 }
                });
                
            }

            if (!await context.StudentSet.AnyAsync())
            {
                context.StudentSet.AddRange(new List<StudentEntity>
                { 
                    new() { Id = 1, Name = "Jean", Lastname = "Dupont", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-07b5ede889e54291946bfb76f2fc9780.jpg", GroupYear = 1, GroupNumber = 1},
                    new() { Id = 2, Name = "Marie", Lastname = "Durand", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-c60caadc22a7454fb452bf44157fa576.jpg", GroupYear = 1, GroupNumber = 2},
                    new() { Id = 3, Name = "Sophie", Lastname = "Leroy", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-939b96340eca4542a306cc87ac6d6b6e.jpg", GroupYear = 1, GroupNumber = 3},
                    new() { Id = 4, Name = "Hugo", Lastname = "Bernard", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-3e978dc8f56d4e9da78299875ef7f6f5.jpg", GroupYear = 1, GroupNumber = 2},
                    new() { Id = 5, Name = "Sarah", Lastname = "Dubois", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-474858ebc45441eca887dc208ce33721.jpg", GroupYear = 1, GroupNumber = 3},
                    new() { Id = 6, Name = "Guillaume", Lastname = "Moreau", UrlPhoto = "gui_moreau.jpg", GroupYear = 1, GroupNumber = 3},
                    new() { Id = 7, Name = "Clara", Lastname = "Fontaine", UrlPhoto = "clara_fontaine.jpg", GroupYear = 1, GroupNumber = 4},
                    new() { Id = 8, Name = "David", Lastname = "Lefebvre", UrlPhoto = "david_lefebvre.jpg", GroupYear = 1, GroupNumber = 4},
                    new() { Id = 9, Name = "Julie", Lastname = "Blanc", UrlPhoto = "julie_blanc.jpg", GroupYear = 1, GroupNumber = 5},
                    new() { Id = 10, Name = "Richard", Lastname = "Leroux", UrlPhoto = "richard_leroux.jpg", GroupYear = 1, GroupNumber = 5},
                    new() { Id = 11, Name = "Marie", Lastname = "Garnier", UrlPhoto = "marie_gernier.jpg", GroupYear = 1, GroupNumber = 6},
                    new() { Id = 12, Name = "Christophe", Lastname = "Martin", UrlPhoto = "christophe_martin.jpg", GroupYear = 1, GroupNumber = 6},
                    new() { Id = 13, Name = "Patricia", Lastname = "Clerc", UrlPhoto = "patricia_clerc.jpg", GroupYear = 1, GroupNumber = 7},
                    new() { Id = 14, Name = "Mathieu", Lastname = "Allain", UrlPhoto = "mathieu_allain.jpg", GroupYear = 1, GroupNumber = 7},
                    new() { Id = 15, Name = "Lucas", Lastname = "Martin", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-ab5de70d249c440aa6031f9e33224c61.jpg", GroupYear = 2, GroupNumber = 1},
                    new() { Id = 16, Name = "Émilie", Lastname = "Petit", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-e5bbffbcdf5240acad364c4a6e63067f.jpg", GroupYear = 2, GroupNumber = 1},
                    new() { Id = 17, Name = "Élisabeth", Lastname = "Hardy", UrlPhoto = "elizabeth_hardy.jpg", GroupYear = 2, GroupNumber = 2},
                    new() { Id = 18, Name = "Thomas", Lastname = "Girard", UrlPhoto = "thomas_girard.jpg", GroupYear = 2, GroupNumber = 2},
                    new() { Id = 19, Name = "Jessica", Lastname = "Salle", UrlPhoto = "jessica_salle.jpg", GroupYear = 2, GroupNumber = 3},
                    new() { Id = 20, Name = "Daniel", Lastname = "Jeune", UrlPhoto = "daniel_jeune.jpg", GroupYear = 2, GroupNumber = 3},
                    new() { Id = 21, Name = "Suzanne", Lastname = "Roy", UrlPhoto = "suzanne_roy.jpg", GroupYear = 2, GroupNumber = 4},
                    new() { Id = 22, Name = "Paul", Lastname = "Marchand", UrlPhoto = "paul_marchand.jpg", GroupYear = 2, GroupNumber = 4},
                    new() { Id = 23, Name = "Marie", Lastname = "Perez", UrlPhoto = "marie_perez.jpg", GroupYear = 2, GroupNumber = 5},
                    new() { Id = 24, Name = "Kevin", Lastname = "Henry", UrlPhoto = "kevin_henry.jpg", GroupYear = 2, GroupNumber = 5},
                    new() { Id = 25, Name = "Nancy", Lastname = "Rodriguez", UrlPhoto = "nancy_rodriguez.jpg", GroupYear = 2, GroupNumber = 6},
                    new() { Id = 26, Name = "Marc", Lastname = "Scott", UrlPhoto = "marc_scott.jpg", GroupYear = 2, GroupNumber = 6},
                    new() { Id = 27, Name = "Karen", Lastname = "Martinez", UrlPhoto = "karen_martinez.jpg", GroupYear = 2, GroupNumber = 7},
                    new() { Id = 28, Name = "Édouard", Lastname = "David", UrlPhoto = "edouard_david.jpg", GroupYear = 2, GroupNumber = 7}
                });
            }

            if (!await context.TemplateSet.AnyAsync())
            {
                context.TemplateSet.AddRange(
                    new TemplateEntity { Id = 1, Name = "Évaluation Générale", TeacherId = profMartin.Id, Criteria = [] },
                    new TemplateEntity { Id = 2, Name = "Contrôle Continu", TeacherId = profDurand.Id, Criteria = [], EvaluationId = 1 },
                    new TemplateEntity { Id = 3, Name = "Examen Mi-Semestre", TeacherId = profDupont.Id, Criteria = [] },
                    new TemplateEntity { Id = 4, Name = "Projet Final", TeacherId = profPetit.Id, Criteria = [] },
                    new TemplateEntity { Id = 5, Name = "Travaux Pratiques", TeacherId = profLeroy.Id, Criteria = [] },
                    new TemplateEntity { Id = 6, Name = "Participation en Classe", TeacherId = profDurand.Id, Criteria = [] },
                    new TemplateEntity { Id = 7, Name = "Recherche et Rédaction", TeacherId = profMartin.Id, Criteria = [] },
                    new TemplateEntity { Id = 8, Name = "Examen de Fin de Semestre", TeacherId = profMartin.Id, Criteria = [] },
                    new TemplateEntity { Id = 9, Name = "Examen Oral", TeacherId = profDupont.Id, Criteria = [] },
                    new TemplateEntity { Id = 10, Name = "Examen Écrit", TeacherId = profDupont.Id, Criteria = [] }
                );
            }

            if (!await context.CriteriaSet.AnyAsync())
            {
                context.CriteriaSet.AddRange(
                    new SliderCriteriaEntity { Id = 1, Name = "Quiz 1", Value = 0, TemplateId = 1, ValueEvaluation = 3 },
                    new SliderCriteriaEntity { Id = 2, Name = "Quiz 2", Value = 0, TemplateId = 3, ValueEvaluation = 3 },
                    new SliderCriteriaEntity { Id = 3, Name = "Présentation en Classe", Value = 0, TemplateId = 5, ValueEvaluation = 3 },
                    new SliderCriteriaEntity { Id = 4, Name = "Travaux Pratiques", Value = 0, TemplateId = 6, ValueEvaluation = 3 },
                    new SliderCriteriaEntity { Id = 5, Name = "Devoir en Groupe", Value = 0, TemplateId = 7, ValueEvaluation = 3 },
                    new SliderCriteriaEntity { Id = 6, Name = "Examen Oral", Value = 0, TemplateId = 8, ValueEvaluation = 3 }
                );
            }

            if (!await context.CriteriaSet.AnyAsync())
            {
                context.CriteriaSet.AddRange(
                    new RadioCriteriaEntity { Id = 7, Name = "Type d’Examen", Options = ["mi-semestre", "examen final"], SelectedOption = "", TemplateId = 1 },
                    new RadioCriteriaEntity { Id = 8, Name = "Type de Quiz", Options = ["Quiz 1", "Quiz 2"], SelectedOption = "", TemplateId = 3 },
                    new RadioCriteriaEntity { Id = 9, Name = "Type de Présentation", Options = ["Individuelle", "Groupe"], SelectedOption = "Individuelle", TemplateId = 5 },
                    new RadioCriteriaEntity { Id = 10, Name = "Type de TP", Options = ["Graphe", "Méthode d'optimisation", "Probabilités"], SelectedOption = "Méthode d'optimisation", TemplateId = 6 },
                    new RadioCriteriaEntity { Id = 11, Name = "Type de Devoir", Options = ["Individuel", "Groupe"], SelectedOption = "Individuel", TemplateId = 7 },
                    new RadioCriteriaEntity { Id = 12, Name = "Type de Papier", Options = ["Rapport", "Dissertation"], SelectedOption = "Mémoire de Recherche", TemplateId = 8 }
                );
            }

            if (!await context.CriteriaSet.AnyAsync())
            {
                context.CriteriaSet.AddRange(
                    new TextCriteriaEntity { Id = 13, Name = "Devoir 1", Text = "", TemplateId = 1 },
                    new TextCriteriaEntity { Id = 14, Name = "Devoir 2", Text = "", TemplateId = 1 },
                    new TextCriteriaEntity { Id = 15, Name = "Examen Final", Text = "", TemplateId = 1 },
                    new TextCriteriaEntity { Id = 16, Name = "Quiz 1", Text = "", TemplateId = 3 },
                    new TextCriteriaEntity { Id = 17, Name = "Projet", Text = "Soumettez votre proposition de projet pour approbation. Assurez-vous qu'elle soit innovante et bien structurée.", TemplateId = 4 },
                    new TextCriteriaEntity { Id = 18, Name = "Participation", Text = "Participez activement aux discussions en classe et aux activités. Votre engagement est essentiel pour votre évaluation.", TemplateId = 4 },
                    new TextCriteriaEntity { Id = 19, Name = "Devoirs", Text = "Complétez tous les problèmes de devoirs assignés. La régularité et la précision sont évaluées.", TemplateId = 5 },
                    new TextCriteriaEntity { Id = 20, Name = "Rapport", Text = "Soumettez votre rapport avant la date limite. L'originalité et la rigueur académique sont cruciales.", TemplateId = 8 }
                );
            }

            if (!await context.LessonSet.AnyAsync())
            {
                context.LessonSet.AddRange(
                    new LessonEntity { Id=1 , Classroom="A23", CourseName="Introduction au SQL",Start= new DateOnly(2023, 10, 30).ToDateTime(new TimeOnly(9,0)), End= new DateOnly(2023, 10, 30).ToDateTime(new TimeOnly(11, 0)), GroupYear=1, GroupNumber=2, TeacherEntityId=profDupont.Id },
                    new LessonEntity { Id = 2, Classroom = "B23", CourseName = "Bases du développement Web", Start = new DateOnly(2023, 10, 30).ToDateTime(new TimeOnly(14, 0)) , End = new DateOnly(2023, 10, 30).ToDateTime(new TimeOnly(16, 0)), GroupYear = 1, GroupNumber = 3, TeacherEntityId = profDurand.Id },
                    new LessonEntity { Id = 3, Classroom = "Amphi A", CourseName = "Fondamentaux de JavaScript", Start = new DateOnly(2023, 11, 02).ToDateTime(new TimeOnly(10, 0)), End = new DateOnly(2023, 11, 02).ToDateTime( new TimeOnly(12, 0)), GroupYear = 1, GroupNumber = 1, TeacherEntityId = profMartin.Id },
                    new LessonEntity { Id = 4, Classroom = "A22", CourseName = "Conception de Bases de Données", Start = new DateOnly(2023, 11, 03).ToDateTime(new TimeOnly(13, 30)), End = new DateOnly(2023, 11, 03).ToDateTime(new TimeOnly(15, 30)), GroupYear = 1, GroupNumber = 2, TeacherEntityId = profDupont.Id },
                    new LessonEntity { Id = 5, Classroom = "A19", CourseName = "Programmation en Python", Start = new DateOnly(2023, 11, 05).ToDateTime( new TimeOnly(9, 30)), End = new DateOnly(2023, 11, 05).ToDateTime( new TimeOnly(11, 30)), GroupYear = 2, GroupNumber = 3, TeacherEntityId = profDurand.Id },
                    new LessonEntity { Id = 6, Classroom = "B23", CourseName = "Bases du HTML", Start = new DateOnly(2023, 11, 07).ToDateTime( new TimeOnly(10, 30)), End = new DateOnly(2023, 11, 07).ToDateTime( new TimeOnly(12, 30)), GroupYear = 1, GroupNumber = 3, TeacherEntityId = profDupont.Id },
                    new LessonEntity { Id = 7, Classroom = "A12", CourseName = "Stylisation CSS", Start = new DateOnly(2023, 11, 08).ToDateTime( new TimeOnly(14, 30)), End = new DateOnly(2023, 11, 08).ToDateTime( new TimeOnly(16, 30)), GroupYear = 2, GroupNumber = 4, TeacherEntityId = profDurand.Id },
                    new LessonEntity { Id = 8, Classroom = "B20", CourseName = "Programmation en Java", Start = new DateOnly(2023, 11, 10).ToDateTime( new TimeOnly(11, 0)), End = new DateOnly(2023, 11, 10).ToDateTime( new TimeOnly(13, 0)), GroupYear = 1, GroupNumber = 4, TeacherEntityId = profDupont.Id },
                    new LessonEntity { Id = 9, Classroom = "A21", CourseName = "Sécurité Réseau", Start = new DateOnly(2023, 11, 12).ToDateTime(new TimeOnly(10, 0)), End = new DateOnly(2023, 11, 12).ToDateTime(new TimeOnly(12, 0)), GroupYear = 2, GroupNumber = 5, TeacherEntityId = profDurand.Id },
                    new LessonEntity { Id = 10, Classroom = "Amphi A", CourseName = "Apprentissage Automatique", Start = new DateOnly(2023, 11, 14).ToDateTime(new TimeOnly(15, 0)), End = new DateOnly(2023, 11, 14).ToDateTime(new TimeOnly(17, 0)), GroupYear = 1, GroupNumber = 5, TeacherEntityId = profDupont.Id },
                    new LessonEntity { Id = 11, Classroom = "Amphi B", CourseName = "Intelligence Artificielle", Start = new DateOnly(2023, 11, 16).ToDateTime(new TimeOnly(13, 30)), End = new DateOnly(2023, 11, 16).ToDateTime(new TimeOnly(15, 30)), GroupYear = 2, GroupNumber = 6, TeacherEntityId = profDurand.Id }
                );
            }

            if (!await context.EvaluationSet.AnyAsync())
            {
                context.EvaluationSet.AddRange(
                new EvaluationEntity { Id = 1, Date = new DateOnly(2023, 11, 05).ToDateTime(new TimeOnly(0,0)), CourseName = "Introduction au SQL", Grade = 15, TeacherId = profDupont.Id, TemplateId = 1, StudentId = 1 },
                new EvaluationEntity { Id = 2, Date = new DateOnly(2023, 11, 07).ToDateTime(new TimeOnly(0, 0)), CourseName = "Introduction au SQL", Grade = 14, TeacherId = profDurand.Id, TemplateId = 4, StudentId = 2 },
                new EvaluationEntity { Id = 3, Date = new DateOnly(2023, 11, 09).ToDateTime(new TimeOnly(0, 0)), CourseName = "Bases du Développement Web", Grade = 13, TeacherId = profMartin.Id, TemplateId = 2, StudentId = 3 },
                new EvaluationEntity { Id = 4, Date = new DateOnly(2023, 11, 11).ToDateTime(new TimeOnly(0, 0)), CourseName = "Bases du Développement Web", Grade = 12, TeacherId = profPetit.Id, TemplateId = 5, StudentId = 1 },
                new EvaluationEntity { Id = 5, Date = new DateOnly(2023, 11, 13).ToDateTime(new TimeOnly(0, 0)), CourseName = "Fondamentaux de JavaScript", Grade = 11, TeacherId = profDupont.Id, TemplateId = 3, StudentId = 2 },
                new EvaluationEntity { Id = 6, Date = new DateOnly(2023, 11, 15).ToDateTime(new TimeOnly(0, 0)), CourseName = "Fondamentaux de JavaScript", Grade = 10, TeacherId = profDurand.Id, TemplateId = 6, StudentId = 3 },
                new EvaluationEntity { Id = 7, Date = new DateOnly(2023, 11, 17).ToDateTime(new TimeOnly(0, 0)), CourseName = "Conception de Bases de Données", Grade = 9, TeacherId = profMartin.Id, TemplateId = 7, StudentId = 1 },
                new EvaluationEntity { Id = 8, Date = new DateOnly(2023, 11, 19).ToDateTime(new TimeOnly(0, 0)), CourseName = "Conception de Bases de Données", Grade = 8, TeacherId = profPetit.Id, TemplateId = 8, StudentId = 2 }
                
                );
            }

            await context.SaveChangesAsync();
        }
    }

    private static async Task<TeacherEntity> EnsureUserAsync(UserManager<TeacherEntity> userManager, string password, string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            user = new TeacherEntity { UserName = userName, Email = $"{userName.ToLower()}@example.com" };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user {userName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        return user;
    }
}
