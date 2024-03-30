using EF_DbContextLib;
using Microsoft.EntityFrameworkCore;
using EF_ConsoleTests.TestUtils;
using EF_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EF_ConsoleTests;

/// <summary>
/// Represents the main program class.
/// </summary>
internal static class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<LibraryContext>(options =>
            options.UseSqlite("Data Source=StudentEvaluator_API_EF_Tests.db"));

        services.AddScoped<IUserStore<TeacherEntity>>(provider => 
            new UserStore<TeacherEntity, IdentityRole, LibraryContext>(
                provider.GetRequiredService<LibraryContext>()));
        services.AddScoped<IRoleStore<IdentityRole>>(provider => 
            new RoleStore<IdentityRole, LibraryContext>(
                provider.GetRequiredService<LibraryContext>()));

        services.AddIdentityCore<TeacherEntity>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<LibraryContext>();

        services.AddScoped<IPasswordHasher<TeacherEntity>, PasswordHasher<TeacherEntity>>();

        var serviceProvider = services.BuildServiceProvider();

        var userManager = serviceProvider.GetRequiredService<UserManager<TeacherEntity>>();


        var context = serviceProvider.GetRequiredService<LibraryContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        
        using (context)
        {
            Console.WriteLine("\n--------  Group  --------\n");
            
            // AddGroup
            GroupTestUtils.AddGroup(context, 1, 1);
            GroupTestUtils.AddGroup(context, 1, 2);
            GroupTestUtils.AddGroup(context, 2, 1);
            GroupTestUtils.AddGroup(context, 2, 2);

            // DisplayAllGroups
            GroupTestUtils.DisplayAllGroups(context);
            
            // DeleteGroup
            GroupTestUtils.DeleteGroup(context, 1, 2);
            GroupTestUtils.DeleteGroup(context, 2, 1);
            
            // DisplayAllGroups
            GroupTestUtils.DisplayAllGroups(context);

            
            Console.WriteLine("--------  Student  --------\n");
            
            // AddStudent
            StudentTestUtils.AddStudent(context, "John", "Doe", "url", 1, 1);
            StudentTestUtils.AddStudent(context, "Bob", "Dylan", "url", 1, 1);
            StudentTestUtils.AddStudent(context, "Alice", "Cooper", "url", 2, 2);
            StudentTestUtils.AddStudent(context, "Elvis", "Presley", "url", 2, 2);
            
            // DisplayAllStudents
            StudentTestUtils.DisplayAllStudents(context);
            
            // UpdateStudent
            StudentTestUtils.UpdateStudent(context, 1 ,"John", "Doe", "newUrl");
            
            // DeleteStudent
            StudentTestUtils.DeleteStudent(context, 2);
            
            // DisplayAllStudents
            StudentTestUtils.DisplayAllStudents(context);
            
            // DisplayAllGroups 
            GroupTestUtils.DisplayAllGroups(context);
            
            
            Console.WriteLine("\n--------  Teacher  --------\n");
            
            // AddTeacher
            var teacherId1 = await TeacherTestUtils.AddTeacherAsync(userManager, "John", "JohnDoe1234$");
            var teacherId2 = await TeacherTestUtils.AddTeacherAsync(userManager, "Bob", "BobDylan123$");
            var teacherId3 = await TeacherTestUtils.AddTeacherAsync(userManager, "Alice", "AliceCooper1$");
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(userManager);
            
            // UpdateTeacher
            TeacherTestUtils.UpdateTeacherAsync(userManager, teacherId1, "NewJohn");
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(userManager);
            
            // DeleteTeacher
            TeacherTestUtils.DeleteTeacherAsync(userManager, teacherId2);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(userManager);
            
            
            Console.WriteLine("\n--------  Template  --------\n");
            
            
            // AddTemplate
            TemplateTestUtils.AddTemplate(context, "Template1", teacherId1);
            TemplateTestUtils.AddTemplate(context, "Template2", teacherId1);
            TemplateTestUtils.AddTemplate(context, "Template3", teacherId3);
            TemplateTestUtils.AddTemplate(context, "Template4", teacherId3);
            
            // DisplayAllTemplates
            TemplateTestUtils.DisplayAllTemplates(context);
            
            // DeleteTemplate
            TemplateTestUtils.DeleteTemplate(context, 2);
            
            // DisplayAllTemplates
            TemplateTestUtils.DisplayAllTemplates(context);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(userManager);
            
            
            Console.WriteLine("\n--------  Criteria  --------\n");
            
            // AddTextCriteria
            CriteriaTestUtils.AddTextCriteria(context, "Text1", 1, 1, "Text1");
            CriteriaTestUtils.AddTextCriteria(context, "Text2", 1, 1, "Text2");
            CriteriaTestUtils.AddTextCriteria(context, "Text3", 3, 3, "Text3");
            
            // DisplayAllTextCriteria
            CriteriaTestUtils.DisplayAllTextCriteria(context);
            
            // AddSliderCriteria
            CriteriaTestUtils.AddSliderCriteria(context, "Slider1", 1, 1, 1);
            CriteriaTestUtils.AddSliderCriteria(context, "Slider2", 1, 1, 2);
            CriteriaTestUtils.AddSliderCriteria(context, "Slider3", 3, 3, 3);
            
            // DisplayAllSliderCriteria
            CriteriaTestUtils.DisplayAllSliderCriteria(context);
            
            // AddRadioCriteria
            CriteriaTestUtils.AddRadioCriteria(context, "Radio1", 1, 1, ["Radio1", "Radio2"], "Radio1");
            CriteriaTestUtils.AddRadioCriteria(context, "Radio2", 1, 1, ["Radio1", "Radio2"], "Radio2");
            CriteriaTestUtils.AddRadioCriteria(context, "Radio3", 3, 3, ["Radio1", "Radio2"], "Radio3");
            
            // DisplayAllRadioCriteria
            CriteriaTestUtils.DisplayAllRadioCriteria(context);
            
            // UpdateTextCriteria
            CriteriaTestUtils.UpdateTextCriteria(context, 1, "NewText1");
            
            // DisplayAllTextCriteria
            CriteriaTestUtils.DisplayAllTextCriteria(context);
            
            // UpdateSliderCriteria
            CriteriaTestUtils.UpdateSliderCriteria(context, 1, 2);
            
            // DisplayAllSliderCriteria
            CriteriaTestUtils.DisplayAllSliderCriteria(context);
            
            // UpdateRadioCriteria
            CriteriaTestUtils.UpdateRadioCriteria(context, 1, "Radio2");
            
            // DisplayAllRadioCriteria
            CriteriaTestUtils.DisplayAllRadioCriteria(context);
            
            // DeleteTextCriteria
            CriteriaTestUtils.DeleteCriteria(context, 2);
            
            // DisplayAllTextCriteria
            CriteriaTestUtils.DisplayAllTextCriteria(context);
            
            // DeleteSliderCriteria
            CriteriaTestUtils.DeleteCriteria(context, 1);
            
            // DisplayAllSliderCriteria
            CriteriaTestUtils.DisplayAllSliderCriteria(context);
            
            // DeleteRadioCriteria
            CriteriaTestUtils.DeleteCriteria(context, 3);
            
            // DisplayAllRadioCriteria
            CriteriaTestUtils.DisplayAllRadioCriteria(context);
            
            // DisplayAllTemplates
            TemplateTestUtils.DisplayAllTemplates(context);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(userManager);
            
            
            Console.WriteLine("\n--------  Lesson  --------\n");
            
            // AddLesson
            LessonTestUtils.AddLesson(context, "Course1", DateTime.Now, DateTime.Now, teacherId1, "Classroom1", 1, 1);
            LessonTestUtils.AddLesson(context, "Course2", DateTime.Now, DateTime.Now, teacherId1, "Classroom2", 1, 1);
            LessonTestUtils.AddLesson(context, "Course3", DateTime.Now, DateTime.Now, teacherId3, "Classroom3", 2, 2);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
            
            // UpdateLesson
            LessonTestUtils.UpdateLesson(context, 1, "NewCourse1", DateTime.Now, DateTime.Now, teacherId1, "NewClassroom1", 1, 1);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
            
            // DeleteLesson
            LessonTestUtils.DeleteLesson(context, 2);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(userManager);
            
            Console.WriteLine("\n--------  Evaluation  --------\n");
            
            // AddEvaluation
            EvaluationTestUtils.AddEvaluation(context, 1, teacherId1, 1, DateTime.Now, "Course1", "Pair1", 1);
            EvaluationTestUtils.AddEvaluation(context, 4, teacherId1, 1, DateTime.Now, "Course2", "Pair2", 2);
            EvaluationTestUtils.AddEvaluation(context, 3, teacherId3, 3, DateTime.Now, "Course3", "Pair3", 3);
            
            // DisplayAllEvaluations
            EvaluationTestUtils.DisplayAllEvaluations(context);
            
            // UpdateEvaluation
            EvaluationTestUtils.UpdateEvaluation(context, 1, "NewCourse1", "NewPair1", 1);
            
            // DisplayAllEvaluations
            EvaluationTestUtils.DisplayAllEvaluations(context);
            
            // DeleteEvaluation
            EvaluationTestUtils.DeleteEvaluation(context, 2);
            
            // DisplayAllEvaluations
            EvaluationTestUtils.DisplayAllEvaluations(context);
            
            // DisplayAllStudents
            StudentTestUtils.DisplayAllStudents(context);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(userManager);
            
            // DisplayAllTemplates
            TemplateTestUtils.DisplayAllTemplates(context);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
        }
    }
}