using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_StubbedContextLib
{
    public class StubbedContext : LibraryContext
    {
        
        public StubbedContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        
        public StubbedContext() : base()
        {
        }

        // Méthode appelée lors de la création du modèle de la base de données
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appelle la méthode de la classe de base pour effectuer la configuration initiale du modèle
            base.OnModelCreating(modelBuilder);
            
            
            // Configure les données initiales (stub) pour l'entité GroupEntity
            modelBuilder.Entity<GroupEntity>().HasData(
                new GroupEntity { GroupYear = 1, GroupNumber = 1 },
                new GroupEntity { GroupYear = 1, GroupNumber = 2 },
                new GroupEntity { GroupYear = 1, GroupNumber = 3 },
                new GroupEntity { GroupYear = 1, GroupNumber = 4 },
                new GroupEntity { GroupYear = 1, GroupNumber = 5 },
                new GroupEntity { GroupYear = 1, GroupNumber = 6 },
                new GroupEntity { GroupYear = 1, GroupNumber = 7 },
                new GroupEntity { GroupYear = 1, GroupNumber = 8 },
                new GroupEntity { GroupYear = 1, GroupNumber = 9 },
                new GroupEntity { GroupYear = 1, GroupNumber = 10 },
                new GroupEntity { GroupYear = 2, GroupNumber = 1 },
                new GroupEntity { GroupYear = 2, GroupNumber = 2 },
                new GroupEntity { GroupYear = 2, GroupNumber = 3 },
                new GroupEntity { GroupYear = 2, GroupNumber = 4 },
                new GroupEntity { GroupYear = 2, GroupNumber = 5 },
                new GroupEntity { GroupYear = 2, GroupNumber = 6 },
                new GroupEntity { GroupYear = 2, GroupNumber = 7 }
            );
            
            // Configure les données initiales (stub) pour l'entité StudentEntity
            modelBuilder.Entity<StudentEntity>().HasData(
                new StudentEntity { Id = 1, Name = "Jean", Lastname = "Dupont", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-07b5ede889e54291946bfb76f2fc9780.jpg", GroupYear = 1, GroupNumber = 1},
                new StudentEntity { Id = 2, Name = "Marie", Lastname = "Durand", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-c60caadc22a7454fb452bf44157fa576.jpg", GroupYear = 1, GroupNumber = 2},
                new StudentEntity { Id = 3, Name = "Sophie", Lastname = "Leroy", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-939b96340eca4542a306cc87ac6d6b6e.jpg", GroupYear = 1, GroupNumber = 3},
                new StudentEntity { Id = 4, Name = "Hugo", Lastname = "Bernard", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-3e978dc8f56d4e9da78299875ef7f6f5.jpg", GroupYear = 1, GroupNumber = 2},
                new StudentEntity { Id = 5, Name = "Sarah", Lastname = "Dubois", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-474858ebc45441eca887dc208ce33721.jpg", GroupYear = 1, GroupNumber = 3},
                new StudentEntity { Id = 6, Name = "Guillaume", Lastname = "Moreau", UrlPhoto = "gui_moreau.jpg", GroupYear = 1, GroupNumber = 3},
                new StudentEntity { Id = 7, Name = "Clara", Lastname = "Fontaine", UrlPhoto = "clara_fontaine.jpg", GroupYear = 1, GroupNumber = 4},
                new StudentEntity { Id = 8, Name = "David", Lastname = "Lefebvre", UrlPhoto = "david_lefebvre.jpg", GroupYear = 1, GroupNumber = 4},
                new StudentEntity { Id = 9, Name = "Julie", Lastname = "Blanc", UrlPhoto = "julie_blanc.jpg", GroupYear = 1, GroupNumber = 5},
                new StudentEntity { Id = 10, Name = "Richard", Lastname = "Leroux", UrlPhoto = "richard_leroux.jpg", GroupYear = 1, GroupNumber = 5},
                new StudentEntity { Id = 11, Name = "Marie", Lastname = "Garnier", UrlPhoto = "marie_gernier.jpg", GroupYear = 1, GroupNumber = 6},
                new StudentEntity { Id = 12, Name = "Christophe", Lastname = "Martin", UrlPhoto = "christophe_martin.jpg", GroupYear = 1, GroupNumber = 6},
                new StudentEntity { Id = 13, Name = "Patricia", Lastname = "Clerc", UrlPhoto = "patricia_clerc.jpg", GroupYear = 1, GroupNumber = 7},
                new StudentEntity { Id = 14, Name = "Mathieu", Lastname = "Allain", UrlPhoto = "mathieu_allain.jpg", GroupYear = 1, GroupNumber = 7},
                new StudentEntity { Id = 15, Name = "Lucas", Lastname = "Martin", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-ab5de70d249c440aa6031f9e33224c61.jpg", GroupYear = 2, GroupNumber = 1},
                new StudentEntity { Id = 16, Name = "Émilie", Lastname = "Petit", UrlPhoto = "https://u-static.fotor.com/images/text-to-image/result/PRO-e5bbffbcdf5240acad364c4a6e63067f.jpg", GroupYear = 2, GroupNumber = 1},
                new StudentEntity { Id = 17, Name = "Élisabeth", Lastname = "Hardy", UrlPhoto = "elizabeth_hardy.jpg", GroupYear = 2, GroupNumber = 2},
                new StudentEntity { Id = 18, Name = "Thomas", Lastname = "Girard", UrlPhoto = "thomas_girard.jpg", GroupYear = 2, GroupNumber = 2},
                new StudentEntity { Id = 19, Name = "Jessica", Lastname = "Salle", UrlPhoto = "jessica_salle.jpg", GroupYear = 2, GroupNumber = 3},
                new StudentEntity { Id = 20, Name = "Daniel", Lastname = "Jeune", UrlPhoto = "daniel_jeune.jpg", GroupYear = 2, GroupNumber = 3},
                new StudentEntity { Id = 21, Name = "Suzanne", Lastname = "Roy", UrlPhoto = "suzanne_roy.jpg", GroupYear = 2, GroupNumber = 4},
                new StudentEntity { Id = 22, Name = "Paul", Lastname = "Marchand", UrlPhoto = "paul_marchand.jpg", GroupYear = 2, GroupNumber = 4},
                new StudentEntity { Id = 23, Name = "Marie", Lastname = "Perez", UrlPhoto = "marie_perez.jpg", GroupYear = 2, GroupNumber = 5},
                new StudentEntity { Id = 24, Name = "Kevin", Lastname = "Henry", UrlPhoto = "kevin_henry.jpg", GroupYear = 2, GroupNumber = 5},
                new StudentEntity { Id = 25, Name = "Nancy", Lastname = "Rodriguez", UrlPhoto = "nancy_rodriguez.jpg", GroupYear = 2, GroupNumber = 6},
                new StudentEntity { Id = 26, Name = "Marc", Lastname = "Scott", UrlPhoto = "marc_scott.jpg", GroupYear = 2, GroupNumber = 6},
                new StudentEntity { Id = 27, Name = "Karen", Lastname = "Martinez", UrlPhoto = "karen_martinez.jpg", GroupYear = 2, GroupNumber = 7},
                new StudentEntity { Id = 28, Name = "Édouard", Lastname = "David", UrlPhoto = "edouard_david.jpg", GroupYear = 2, GroupNumber = 7}
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
                new TemplateEntity { Id = 1, Name = "Évaluation Générale", TeacherId = 3, Criteria = [] },
                new TemplateEntity { Id = 2, Name = "Contrôle Continu", TeacherId = 2, Criteria = [] , EvaluationId = 1},
                new TemplateEntity { Id = 3, Name = "Examen Mi-Semestre", TeacherId = 1, Criteria = [] },
                new TemplateEntity { Id = 4, Name = "Projet Final", TeacherId = 4, Criteria = [] },
                new TemplateEntity { Id = 5, Name = "Travaux Pratiques", TeacherId = 5, Criteria = [] },
                new TemplateEntity { Id = 6, Name = "Participation en Classe", TeacherId = 2, Criteria = [] },
                new TemplateEntity { Id = 7, Name = "Recherche et Rédaction", TeacherId = 3, Criteria = [] },
                new TemplateEntity { Id = 8, Name = "Examen de Fin de Semestre", TeacherId = 1, Criteria = [] },
                new TemplateEntity { Id = 9, Name = "Examen Oral", TeacherId = 1, Criteria = [] },
                new TemplateEntity { Id = 10, Name = "Examen Écrit", TeacherId = 1, Criteria = [] }
            );
           

            // Configure les données initiales (stub) pour l'entité SliderCriteriaEntity
            modelBuilder.Entity<SliderCriteriaEntity>().HasData(
                new SliderCriteriaEntity { Id = 1, Name = "Quiz 1", Value = 0, TemplateId = 1, ValueEvaluation = 3},
                new SliderCriteriaEntity { Id = 2, Name = "Quiz 2", Value = 0, TemplateId = 3, ValueEvaluation = 3 },
                new SliderCriteriaEntity { Id = 3, Name = "Présentation en Classe", Value = 0, TemplateId = 5 , ValueEvaluation = 3},
                new SliderCriteriaEntity { Id = 4, Name = "Travaux Pratiques", Value = 0, TemplateId = 6, ValueEvaluation = 3 },
                new SliderCriteriaEntity { Id = 5, Name = "Devoir en Groupe", Value = 0, TemplateId = 7 , ValueEvaluation = 3},
                new SliderCriteriaEntity { Id = 6, Name = "Examen Oral", Value = 0, TemplateId = 8, ValueEvaluation = 3 }
            );
            

            // Configure les données initiales (stub) pour l'entité RadioCriteriaEntity
            modelBuilder.Entity<RadioCriteriaEntity>().HasData(
                new RadioCriteriaEntity { Id = 7, Name = "Type d’Examen", Options = ["mi-semestre", "examen final"], SelectedOption = "", TemplateId = 1 },
                new RadioCriteriaEntity { Id = 8, Name = "Type de Quiz", Options = ["Quiz 1", "Quiz 2"], SelectedOption = "", TemplateId = 3 },
                new RadioCriteriaEntity { Id = 9, Name = "Type de Présentation", Options = ["Individuelle", "Groupe"], SelectedOption = "Individuelle", TemplateId = 5 },
                new RadioCriteriaEntity { Id = 10, Name = "Type de TP", Options = ["Graphe", "Méthode d'optimisation", "Probabilités"], SelectedOption = "Méthode d'optimisation", TemplateId = 6 },
                new RadioCriteriaEntity { Id = 11, Name = "Type de Devoir", Options = ["Individuel", "Groupe"], SelectedOption = "Individuel", TemplateId = 7 },
                new RadioCriteriaEntity { Id = 12, Name = "Type de Papier", Options = ["Rapport", "Dissertation"], SelectedOption = "Mémoire de Recherche", TemplateId = 8 }
            );
            
            // Configure les données initiales (stub) pour l'entité TextCriteriaEntity
            modelBuilder.Entity<TextCriteriaEntity>().HasData(
                new TextCriteriaEntity { Id = 13, Name = "Devoir 1", Text = "", TemplateId = 1 },
                new TextCriteriaEntity { Id = 14, Name = "Devoir 2", Text = "", TemplateId = 1 },
                new TextCriteriaEntity { Id = 15, Name = "Examen Final", Text = "", TemplateId = 1 },
                new TextCriteriaEntity { Id = 16, Name = "Quiz 1", Text = "", TemplateId = 3 },
                new TextCriteriaEntity { Id = 17, Name = "Projet", Text = "Soumettez votre proposition de projet pour approbation. Assurez-vous qu'elle soit innovante et bien structurée.", TemplateId = 4 },
                new TextCriteriaEntity { Id = 18, Name = "Participation", Text = "Participez activement aux discussions en classe et aux activités. Votre engagement est essentiel pour votre évaluation.", TemplateId = 4 },  
                new TextCriteriaEntity { Id = 19, Name = "Devoirs", Text = "Complétez tous les problèmes de devoirs assignés. La régularité et la précision sont évaluées.", TemplateId = 5 },
                new TextCriteriaEntity { Id = 20, Name = "Rapport", Text = "Soumettez votre rapport avant la date limite. L'originalité et la rigueur académique sont cruciales.", TemplateId = 8 }
            );


            // Configure les données initiales (stub) pour l'entité LessonEntity
            modelBuilder.Entity<LessonEntity>().HasData(
                new LessonEntity { Id=1 , Classroom="A23", CourseName="Introduction au SQL", Date=new DateOnly(2023,10,30), Start=new TimeOnly(9,0), End=new TimeOnly(11,0), GroupYear=1, GroupNumber=2, TeacherEntityId=1 },
                new LessonEntity { Id = 2, Classroom = "B23", CourseName = "Bases du développement Web", Date = new DateOnly(2023, 10, 31), Start = new TimeOnly(14, 0), End = new TimeOnly(16, 0), GroupYear = 1, GroupNumber = 3, TeacherEntityId = 2 },
                new LessonEntity { Id = 3, Classroom = "Amphi A", CourseName = "Fondamentaux de JavaScript", Date = new DateOnly(2023, 11, 02), Start = new TimeOnly(10, 0), End = new TimeOnly(12, 0), GroupYear = 1, GroupNumber = 1, TeacherEntityId = 3 },
                new LessonEntity { Id = 4, Classroom = "A22", CourseName = "Conception de Bases de Données", Date = new DateOnly(2023, 11, 03), Start = new TimeOnly(13, 30), End = new TimeOnly(15, 30), GroupYear = 1, GroupNumber = 2, TeacherEntityId = 1 },
                new LessonEntity { Id = 5, Classroom = "A19", CourseName = "Programmation en Python", Date = new DateOnly(2023, 11, 05), Start = new TimeOnly(9, 30), End = new TimeOnly(11, 30), GroupYear = 2, GroupNumber = 3, TeacherEntityId = 2 },
                new LessonEntity { Id = 6, Classroom = "B23", CourseName = "Bases du HTML", Date = new DateOnly(2023, 11, 07), Start = new TimeOnly(10, 30), End = new TimeOnly(12, 30), GroupYear = 1, GroupNumber = 3, TeacherEntityId = 1 },
                new LessonEntity { Id = 7, Classroom = "A12", CourseName = "Stylisation CSS", Date = new DateOnly(2023, 11, 08), Start = new TimeOnly(14, 30), End = new TimeOnly(16, 30), GroupYear = 2, GroupNumber = 4, TeacherEntityId = 2 },
                new LessonEntity { Id = 8, Classroom = "B20", CourseName = "Programmation en Java", Date = new DateOnly(2023, 11, 10), Start = new TimeOnly(11, 0), End = new TimeOnly(13, 0), GroupYear = 1, GroupNumber = 4, TeacherEntityId = 1 },
                new LessonEntity { Id = 9, Classroom = "A21", CourseName = "Sécurité Réseau", Date = new DateOnly(2023, 11, 12), Start = new TimeOnly(10, 0), End = new TimeOnly(12, 0), GroupYear = 2, GroupNumber = 5, TeacherEntityId = 2 },
                new LessonEntity { Id = 10, Classroom = "Amphi A", CourseName = "Apprentissage Automatique", Date = new DateOnly(2023, 11, 14), Start = new TimeOnly(15, 0), End = new TimeOnly(17, 0), GroupYear = 1, GroupNumber = 5, TeacherEntityId = 1 },
                new LessonEntity { Id = 11, Classroom = "Amphi B", CourseName = "Intelligence Artificielle", Date = new DateOnly(2023, 11, 16), Start = new TimeOnly(13, 30), End = new TimeOnly(15, 30), GroupYear = 2, GroupNumber = 6, TeacherEntityId = 2 },
                new LessonEntity { Id = 12, Classroom = "B12", CourseName = "Analyse de Données", Date = new DateOnly(2023, 11, 18), Start = new TimeOnly(9, 30), End = new TimeOnly(11, 30), GroupYear = 1, GroupNumber = 6, TeacherEntityId = 1 },
                new LessonEntity { Id = 13, Classroom = "A10", CourseName = "Développement d’Applications Mobiles", Date = new DateOnly(2023, 11, 20), Start = new TimeOnly(14, 0), End = new TimeOnly(16, 0), GroupYear = 2, GroupNumber = 7, TeacherEntityId = 2 },
                new LessonEntity { Id = 14, Classroom = "A19", CourseName = "Sécurité Web", Date = new DateOnly(2023, 11, 22), Start = new TimeOnly(11, 30), End = new TimeOnly(13, 30), GroupYear = 1, GroupNumber = 7, TeacherEntityId = 1 },
                new LessonEntity { Id = 15, Classroom = "A22", CourseName = "Gestion de Bases de Données", Date = new DateOnly(2023, 11, 24), Start = new TimeOnly(10, 0), End = new TimeOnly(12, 0), GroupYear = 2, GroupNumber = 7, TeacherEntityId = 2 }
            );
            
           
            // Configure les données initiales (stub) pour l'entité EvaluationEntity
            modelBuilder.Entity<EvaluationEntity>().HasData(
                new EvaluationEntity { Id = 1, Date = new DateOnly(2023, 11, 05), CourseName = "Introduction au SQL", Grade = 15, TeacherId = 1, TemplateId = 1, StudentId = 1 },
                new EvaluationEntity { Id = 2, Date = new DateOnly(2023, 11, 07), CourseName = "Introduction au SQL", Grade = 14, TeacherId = 2, TemplateId = 4, StudentId = 2 },
                new EvaluationEntity { Id = 3, Date = new DateOnly(2023, 11, 09), CourseName = "Bases du Développement Web", Grade = 13, TeacherId = 3, TemplateId = 2, StudentId = 3 },
                new EvaluationEntity { Id = 4, Date = new DateOnly(2023, 11, 11), CourseName = "Bases du Développement Web", Grade = 12, TeacherId = 4, TemplateId = 5, StudentId = 1 },
                new EvaluationEntity { Id = 5, Date = new DateOnly(2023, 11, 13), CourseName = "Fondamentaux de JavaScript", Grade = 11, TeacherId = 1, TemplateId = 3, StudentId = 2 },
                new EvaluationEntity { Id = 6, Date = new DateOnly(2023, 11, 15), CourseName = "Fondamentaux de JavaScript", Grade = 10, TeacherId = 2, TemplateId = 6, StudentId = 3 },
                new EvaluationEntity { Id = 7, Date = new DateOnly(2023, 11, 17), CourseName = "Conception de Bases de Données", Grade = 9, TeacherId = 3, TemplateId = 7, StudentId = 1 },
                new EvaluationEntity { Id = 8, Date = new DateOnly(2023, 11, 19), CourseName = "Conception de Bases de Données", Grade = 8, TeacherId = 4, TemplateId = 8, StudentId = 2 }
               /* new EvaluationEntity { Id = 9, Date = new DateOnly(2023, 11, 21), CourseName = "Programmation en Python", Grade = 7, TeacherId = 5, StudentId = 3 },
                new EvaluationEntity { Id = 10, Date = new DateOnly(2023, 11, 23), CourseName = "Programmation en Python", Grade = 6, TeacherId = 1,  StudentId = 1 },
                new EvaluationEntity { Id = 11, Date = new DateOnly(2023, 11, 25), CourseName = "Bases du HTML", Grade = 5, TeacherId = 2, StudentId = 2 },
                new EvaluationEntity { Id = 12, Date = new DateOnly(2023, 11, 27), CourseName = "Bases du HTML", Grade = 4, TeacherId = 3, StudentId = 3 },
                new EvaluationEntity { Id = 13, Date = new DateOnly(2023, 11, 29), CourseName = "Stylisation CSS", Grade = 3, TeacherId = 4, StudentId = 1 },
                new EvaluationEntity { Id = 14, Date = new DateOnly(2023, 12, 01), CourseName = "Stylisation CSS", Grade = 2, TeacherId = 5,  StudentId = 2 },
                new EvaluationEntity { Id = 15, Date = new DateOnly(2023, 12, 03), CourseName = "Programmation en Java", Grade = 1, TeacherId = 1, StudentId = 3 }
                */
            );
        }
    }

}