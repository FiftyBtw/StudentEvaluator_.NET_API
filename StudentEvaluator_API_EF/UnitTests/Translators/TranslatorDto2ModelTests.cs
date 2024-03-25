using API_Dto;
using Client_Model;
using Dto2Model;

namespace EF_UnitTests.Translators;

public class TranslatorDto2ModelTests
{
[Fact]
    public void StudentDtosToModels_ConvertsCorrectly()
    {
        // Arrange
        var studentDtos = new List<StudentDto>
        {
            new StudentDto { Id = 1, Name = "John", Lastname = "Doe", UrlPhoto = "http://example.com/photo.jpg", GroupNumber = 1, GroupYear = 1 },
            new StudentDto { Id = 2, Name = "Jane", Lastname = "Smith", UrlPhoto = "http://example.com/photo2.jpg", GroupNumber = 2, GroupYear = 1 }
        };

        // Act
        var studentModels = studentDtos.ToModels();

        // Assert
        Assert.Equal(studentDtos.Count, studentModels.Count());
        Assert.Equal(studentDtos[0].Id, studentModels.ElementAt(0).Id);
        Assert.Equal(studentDtos[1].Name, studentModels.ElementAt(1).Name);
    }

    [Fact]
    public void StudentModelsToDtos_ConvertsCorrectly()
    {
        // Arrange
        var studentModels = new List<Student>
        {
            new Student(1, "John", "Doe", "http://example.com/photo.jpg", 1, 1),
            new Student(2, "Jane", "Smith", "http://example.com/photo2.jpg", 1, 2)
        };

        // Act
        var studentDtos = studentModels.ToDtos();

        // Assert
        Assert.Equal(studentModels.Count, studentDtos.Count());
        Assert.Equal(studentModels[0].Id, studentDtos.ElementAt(0).Id);
        Assert.Equal(studentModels[1].Lastname, studentDtos.ElementAt(1).Lastname);
    }
    
    [Fact]
    public void GroupDtosToModels_ConvertsCorrectly()
    {
        // Arrange
        var groupDtos = new List<GroupDto>
        {
            new GroupDto { GroupYear = 1, GroupNumber = 1, Students = new List<StudentDto> { new StudentDto { Id = 1, Name = "John", Lastname = "Doe" } } },
            new GroupDto { GroupYear = 1, GroupNumber = 2, Students = new List<StudentDto> { new StudentDto { Id = 2, Name = "Jane", Lastname = "Smith" } } }
        };

        // Act
        var groupModels = groupDtos.ToModels();

        // Assert
        Assert.Equal(groupDtos.Count, groupModels.Count());
        Assert.Equal(groupDtos[0].GroupYear, groupModels.ElementAt(0).GroupYear);
        Assert.Single(groupModels.ElementAt(0).Students);
    }

    [Fact]
    public void GroupModelToDto_ConvertsCorrectly()
    {
        // Arrange
        var group = new Group(1, 1, new List<Student> { new Student(1, "John", "Doe", "http://example.com/photo.jpg", 1, 1) });
        
        // Act
        var groupDtos = group.ToDto();

        // Assert
        Assert.Equal(group.GroupYear, groupDtos.GroupYear);
        Assert.Single(groupDtos.Students);
    }
    
    [Fact]
    public void TeacherDtosToModels_ConvertsCorrectly()
    {
        // Arrange
        var teacherDtos = new List<TeacherDto>
        {
            new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasseTresSecure" },
            new TeacherDto { Id = "2", Username = "ProfDurand", Password = "MotDePasseTresSecure" }
        };

        // Act
        var teacherModels = teacherDtos.ToModels();

        // Assert
        var enumerable = teacherModels as Teacher[] ?? teacherModels.ToArray();
        Assert.Equal(teacherDtos.Count, enumerable.Length);
        Assert.Equal(teacherDtos[0].Id, enumerable.ElementAt(0).Id);
        Assert.Equal(teacherDtos[1].Username, enumerable.ElementAt(1).Username);
    }
    
    [Fact]
    public void TeacherModelsToDto_ConvertsCorrectly()
    {
        // Arrange
        var teacher = new Teacher("1", "ProfDupont", "MotDePasseTresSecure");
        
        // Act
        var teacherDtos = teacher.ToDto();
        
        // Assert
        Assert.Equal(teacher.Id, teacherDtos.Id);
        Assert.Equal(teacher.Username, teacherDtos.Username);
        Assert.Equal(teacher.Password, teacherDtos.Password);
    }
    
    // Lesson
    
    [Fact]
    public void LessonResponseDtoToModels_ConvertsCorrectly()
    {
        // Arrange
        var lessonDtos = new List<LessonReponseDto>
        {
            new LessonReponseDto() { Id = 1, Start = DateTime.Now, End = DateTime.Now, CourseName = "Math", Classroom = "A1", Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasseTresSecure" }, Group = new GroupDto { GroupYear = 1, GroupNumber = 1, Students = new List<StudentDto> { new StudentDto { Id = 1, Name = "John", Lastname = "Doe" } } } },
            new LessonReponseDto { Id = 2, Start = DateTime.Now, End = DateTime.Now, CourseName = "Math", Classroom = "A1", Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasseTresSecure" }, Group = new GroupDto { GroupYear = 1, GroupNumber = 1, Students = new List<StudentDto> { new StudentDto { Id = 1, Name = "John", Lastname = "Doe" } } } }
        };

        // Act
        var lessonModels = lessonDtos.ToModels();

        // Assert
        var enumerable = lessonModels as Lesson[] ?? lessonModels.ToArray();
        Assert.Equal(lessonDtos.Count, enumerable.Length);
        Assert.Equal(lessonDtos[0].Id, enumerable.ElementAt(0).Id);
        Assert.Equal(lessonDtos[1].CourseName, enumerable.ElementAt(1).CourseName);
    }
    
    [Fact]
    public void LessonCreationModelToDto_ConvertsCorrectly()
    {
        // Arrange
        var lesson = new LessonCreation(DateTime.Now, DateTime.Now, "Math", "A1",  "1", 1, 1);
        
        // Act
        var lessonDtos = lesson.ToDto();
        
        // Assert
        Assert.Equal(lesson.CourseName, lessonDtos.CourseName);
        Assert.Equal(lesson.Classroom, lessonDtos.Classroom);
        Assert.Equal(lesson.GroupYear, lessonDtos.GroupYear);
        Assert.Equal(lesson.GroupNumber, lessonDtos.GroupNumber);
        Assert.Equal(lesson.TeacherId, lessonDtos.TeacherId);
        Assert.Equal(lesson.Start, lessonDtos.Start);
        Assert.Equal(lesson.End, lessonDtos.End);
    }

    [Fact]
    public void LessonModelToLessonResponseDto_ConvertsCorrectly()
    {
        // Arrange
        var lesson = new Lesson(1, DateTime.Now, DateTime.Now, "Math", "A1",
            new Teacher("1", "ProfDupont", "MotDePasseTresSecure"),
            new Group(1, 1, new List<Student> { new Student(1, "John", "Doe", "http://example.com/photo.jpg", 1, 1) }));

        // Act
        var lessonDtos = lesson.ToReponseDto();

        // Assert
        Assert.Equal(lesson.Id, lessonDtos.Id);
        Assert.Equal(lesson.CourseName, lessonDtos.CourseName);
        Assert.Equal(lesson.Classroom, lessonDtos.Classroom);
        Assert.Equal(lesson.Group.GroupYear, lessonDtos.Group.GroupYear);
        Assert.Equal(lesson.Group.GroupNumber, lessonDtos.Group.GroupNumber);
        Assert.Equal(lesson.Teacher.Id, lessonDtos.Teacher.Id);
        Assert.Equal(lesson.Start, lessonDtos.Start);
        Assert.Equal(lesson.End, lessonDtos.End);
    }
    
    [Fact]
    public void TemplateDtoToModels_ConvertsCorrectly()
    {
        // Arrange
        var templateDtos = new List<TemplateDto>
        {
            new TemplateDto() { Id = 1, Name = "Math", Criterias = []},
            new TemplateDto { Id = 2, Name = "Math", Criterias = [] }
        };

        // Act
        var templateModels = templateDtos.ToModels();

        // Assert
        var enumerable = templateModels as Template[] ?? templateModels.ToArray();
        Assert.Equal(templateDtos.Count, enumerable.Length);
        Assert.Equal(templateDtos[0].Id, enumerable.ElementAt(0).Id);
        Assert.Equal(templateDtos[1].Name, enumerable.ElementAt(1).Name);
    }
    
    [Fact]
    public void TemplateModelToDto_ConvertsCorrectly()
    {
        // Arrange
        var template = new Template(1, "Math", new List<Criteria>());
        
        // Act
        var templateDtos = template.ToDto();
        
        // Assert
        Assert.Equal(template.Id, templateDtos.Id);
        Assert.Equal(template.Name, templateDtos.Name);
    }
    
    // Evaluation similaire aux tests de Lesson
    
    [Fact]
    public void EvaluationDtoToModels_ConvertsCorrectly()
    {
        // Arrange
        var evaluationDtos = new List<EvaluationReponseDto>
        {
            new() { Id = 1, Date = DateTime.Now, CourseName = "Math", Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasseTresSecure"}, PairName = "toto", Grade = 2, Student = new StudentDto() { Id = 1, Name = "John", Lastname = "Doe" }, Template = new TemplateDto() { Id = 1, Name = "Math", Criterias = [] }},
            new() { Id = 2, Date = DateTime.Now, CourseName = "Math", Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasseTresSecure" }, PairName = "toto", Grade = 2, Student = new StudentDto() { Id = 1, Name = "John", Lastname = "Doe" }, Template = new TemplateDto() { Id = 2, Name = "Math", Criterias = [] }}
        };

        // Act
        var evaluationModels = evaluationDtos.ToModels();

        // Assert
        var enumerable = evaluationModels as Evaluation[] ?? evaluationModels.ToArray();
        Assert.Equal(evaluationDtos.Count, enumerable.Length);
        Assert.Equal(evaluationDtos[0].Id, enumerable.ElementAt(0).Id);
        Assert.Equal(evaluationDtos[1].CourseName, enumerable.ElementAt(1).CourseName);
    }
    
    [Fact]
    public void EvaluationCreationModelToDto_ConvertsCorrectly()
    {
        // Arrange
        var evaluation = new EvaluationCreation(DateTime.Now, "Math", 1, "toto", "2", 1, 1);
        
        // Act
        var evaluationDtos = evaluation.ToDto();
        
        // Assert
        Assert.Equal(evaluation.CourseName, evaluationDtos.CourseName);
        Assert.Equal(evaluation.Date, evaluationDtos.Date);
        Assert.Equal(evaluation.Grade, evaluationDtos.Grade);
        Assert.Equal(evaluation.PairName, evaluationDtos.PairName);
        Assert.Equal(evaluation.StudentId, evaluationDtos.StudentId);
        Assert.Equal(evaluation.TemplateId, evaluationDtos.TemplateId);
        Assert.Equal(evaluation.TeacherId, evaluationDtos.TeacherId);
    }
    
    [Fact]
    public void EvaluationModelToEvaluationResponseDto_ConvertsCorrectly()
    {
        // Arrange
        var evaluation = new Evaluation(1, DateTime.Now, "Math", 1,"toto", new Teacher("1", "ProfDupont", "MotDePasseTresSecure"), new Template(1, "Math", new List<Criteria>()), new Student(1, "John", "Doe", "http://example.com/photo.jpg", 1, 1));
        
        // Act
        var evaluationDtos = evaluation.ToReponseDto();
        
        // Assert
        Assert.Equal(evaluation.Id, evaluationDtos.Id);
        Assert.Equal(evaluation.CourseName, evaluationDtos.CourseName);
        Assert.Equal(evaluation.Date, evaluationDtos.Date);
        Assert.Equal(evaluation.Grade, evaluationDtos.Grade);
        Assert.Equal(evaluation.PairName, evaluationDtos.PairName);
        Assert.Equal(evaluation.Student.Id, evaluationDtos.Student.Id);
        Assert.NotNull(evaluationDtos.Template);
        Assert.Equal(evaluation.Template.Id, evaluationDtos.Template.Id);
        Assert.Equal(evaluation.Teacher.Id, evaluationDtos.Teacher.Id);
    }
    
    // User 
    
    [Fact]
    public void TeacherDtoToModels_ConvertsCorrectly()
    {
        // Arrange
        var teacherDtos = new List<TeacherDto>
        {
            new() { Id = "1", Username = "ProfDupont", Password = "MotDePasseTresSecure" },
            new() { Id = "2", Username = "ProfDurand", Password = "MotDePasseTresSecure" }
        };

        // Act
        var userModels = teacherDtos.ToModels();

        // Assert
        var enumerable = userModels as User[] ?? userModels.ToArray();
        Assert.Equal(teacherDtos.Count, enumerable.Length);
        Assert.Equal(teacherDtos[0].Id, enumerable.ElementAt(0).Id);
        Assert.Equal(teacherDtos[1].Username, enumerable.ElementAt(1).Username);
    }
    
    [Fact]
    public void TeacherModelToDto_ConvertsCorrectly()
    {
        // Arrange
        var teacher = new Teacher("1", "ProfDupont", "MotDePasseTresSecure");
        
        // Act
        var userDtos = teacher.ToDto();
        
        // Assert
        Assert.Equal(teacher.Id, userDtos.Id);
        Assert.Equal(teacher.Username, userDtos.Username);
        Assert.Equal(teacher.Password, userDtos.Password);
    }
    
}