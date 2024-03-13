using EF_DbContextLib;
using Microsoft.EntityFrameworkCore;
using EF_ConsoleTests.TestUtils;

namespace EF_ConsoleTests;

/// <summary>
/// Represents the main program class.
/// </summary>
internal static class Program
{

    /// <summary>
    /// The entry point of the program.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite("Data Source=StudentEvaluator_API_EF_Tests.db")
            .Options;
        var context = new LibraryContext(options);
        
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
            TeacherTestUtils.AddTeacher(context, "John", "Doe", ["Teacher"]);
            TeacherTestUtils.AddTeacher(context, "Bob", "Dylan", ["Teacher"]);
            TeacherTestUtils.AddTeacher(context, "Alice", "Cooper", ["Teacher"]);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
            // UpdateTeacher
            TeacherTestUtils.UpdateTeacher(context, 1, "John", "Doe");
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
            // DeleteTeacher
            TeacherTestUtils.DeleteTeacher(context, 2);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
            
            Console.WriteLine("\n--------  Template  --------\n");
            
            
            // AddTemplate
            TemplateTestUtils.AddTemplate(context, "Template1", 1);
            TemplateTestUtils.AddTemplate(context, "Template2", 1);
            TemplateTestUtils.AddTemplate(context, "Template3", 3);
            TemplateTestUtils.AddTemplate(context, "Template4", 3);
            
            // DisplayAllTemplates
            TemplateTestUtils.DisplayAllTemplates(context);
            
            // DeleteTemplate
            TemplateTestUtils.DeleteTemplate(context, 2);
            
            // DisplayAllTemplates
            TemplateTestUtils.DisplayAllTemplates(context);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
            
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
            TeacherTestUtils.DisplayAllTeachers(context);
            
            
            Console.WriteLine("\n--------  Lesson  --------\n");
            
            // AddLesson
            LessonTestUtils.AddLesson(context, "Course1", DateTime.Now, DateTime.Now, 1, "Classroom1", 1, 1);
            LessonTestUtils.AddLesson(context, "Course2", DateTime.Now, DateTime.Now, 1, "Classroom2", 1, 1);
            LessonTestUtils.AddLesson(context, "Course3", DateTime.Now, DateTime.Now, 3, "Classroom3", 2, 2);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
            
            // UpdateLesson
            LessonTestUtils.UpdateLesson(context, 1, "NewCourse1", DateTime.Now, DateTime.Now, 1, "NewClassroom1", 1, 1);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
            
            // DeleteLesson
            LessonTestUtils.DeleteLesson(context, 2);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
            
            // DisplayAllTeachers
            TeacherTestUtils.DisplayAllTeachers(context);
            
            Console.WriteLine("\n--------  Evaluation  --------\n");
            
            // AddEvaluation
            EvaluationTestUtils.AddEvaluation(context, 1, 1, 1, DateTime.Now, "Course1", "Pair1", 1);
            EvaluationTestUtils.AddEvaluation(context, 4, 1, 1, DateTime.Now, "Course2", "Pair2", 2);
            EvaluationTestUtils.AddEvaluation(context, 3, 3, 3, DateTime.Now, "Course3", "Pair3", 3);
            
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
            TeacherTestUtils.DisplayAllTeachers(context);
            
            // DisplayAllTemplates
            TemplateTestUtils.DisplayAllTemplates(context);
            
            // DisplayAllLessons
            LessonTestUtils.DisplayAllLessons(context);
        }
    }
}