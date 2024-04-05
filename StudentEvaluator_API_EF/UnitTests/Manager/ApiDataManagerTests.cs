using System.Net;
using System.Text;
using API_Dto;
using Client_Model;
using Dto2Model;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Shared;

namespace EF_UnitTests.Manager;

public class ApiDataManagerTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly ApiDataManager _apiDataManager;

    public ApiDataManagerTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var clientHandlerStub = _mockHttpMessageHandler.Object;
        var mockHttpClient = new HttpClient(clientHandlerStub)
        {
            BaseAddress = new Uri("http://fake.api/")
        };
        _apiDataManager = new ApiDataManager(mockHttpClient, 1);
    }

    [Fact]
    public async Task GetStudentById_ReturnsStudent()
    {
        // Arrange
        var expectedStudent = new StudentDto { Id = 1, Name = "Test", Lastname = "Student" };
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedStudent), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetStudentById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedStudent.Name, result.Name);
    }
    
    [Fact]
    public async Task DeleteStudent_WhenSuccessful_ReturnsTrue()
    {
        // Arrange
         _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var result = await _apiDataManager.DeleteStudent(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetStudents_ReturnsStudents()
    {
        // Arrange
        var expectedStudents = new PageReponse<StudentDto>(1, new List<StudentDto> { new StudentDto { Id = 1, Name = "John", Lastname = "Doe" } });

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedStudents), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetStudents();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedStudents.NbElement, result.NbElement);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task PostStudent_ReturnsStudent()
    {
        // Arrange
        var newStudent = new Student ( 1,  "John",  "Doe" , "http://fake.api/photo.jpg" ,  1, 1);
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(newStudent), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PostStudent(newStudent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newStudent.Name, result.Name);
    }
    
    [Fact]
    public async Task PutStudent_UpdatesStudent_ReturnsUpdatedStudent()
    {
        // Arrange
        var updatedStudent = new Student ( 1,  "John",  "Doe" , "http://fake.api/photo.jpg" ,  1, 1);
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(updatedStudent), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PutStudent(1, updatedStudent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedStudent.Name, result.Name);
    }

    [Fact]
    public async Task GetGroups_ReturnsGroups()
    {
        // Arrange
        var expectedGroups = new PageReponse<GroupDto>(1,new List<GroupDto> { new GroupDto { GroupYear = 2021, GroupNumber = 1 } }
        );
        
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedGroups), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetGroups();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGroups.NbElement, result.NbElement);
        Assert.Single(result.Data);
    }
    
    [Fact]
    public async Task GetGroupByIds_ReturnsGroup()
    {
        // Arrange
        var expectedGroup = new GroupDto { GroupYear = 1, GroupNumber = 1 };
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedGroup), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetGroupByIds(1, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGroup.GroupYear, result.GroupYear);
        Assert.Equal(expectedGroup.GroupNumber, result.GroupNumber);
    }

    [Fact]
    public async Task PostGroup_ReturnsGroup()
    {
        // Arrange
        var newGroup = new Group ( 1, 1 );
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(newGroup), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PostGroup(newGroup);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newGroup.GroupYear, result.GroupYear);
        Assert.Equal(newGroup.GroupNumber, result.GroupNumber);
    }

    [Fact]
    public async Task DeleteGroup_WhenSuccessful_ReturnsTrue()
    {
        // Arrange
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var result = await _apiDataManager.DeleteGroup(1, 1);

        // Assert
        Assert.True(result);
    }
    

    [Fact]
    public async Task GetLessonById_ReturnsLesson()
    {
        // Arrange
        var expectedLesson = new LessonReponseDto { Id = 1, CourseName = "Math", Classroom = "A19", Start = new DateTime(2024, 3, 16), End = new DateTime(2024, 3, 16), Group = new GroupDto { GroupYear = 1, GroupNumber = 1 } , Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasse" }};
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedLesson), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetLessonById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLesson.CourseName, result.CourseName);
    }

    [Fact]
    public async Task PostLesson_CreatesLesson_ReturnsLesson()
    {
        // Arrange
        var newLesson = new LessonCreation(new DateTime(2024, 3, 16), new DateTime(2024, 3, 16),  "Math", "A19", "1", 1, 1);
        var expectedLesson = new LessonReponseDto { Id = 1, CourseName = "Math", Classroom = "A19", Start = new DateTime(2024, 3, 16), End = new DateTime(2024, 3, 16), Group = new GroupDto { GroupYear = 1, GroupNumber = 1 } , Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasse" }};
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedLesson), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PostLesson(newLesson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLesson.CourseName, result.CourseName);
}

    [Fact]
    public async Task DeleteLesson_WhenSuccessful_ReturnsTrue()
    {
        // Arrange
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var result = await _apiDataManager.DeleteLesson(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task GetLessons_ReturnsLessons()
    {
        // Arrange
        var expectedLessons = new PageReponse<LessonReponseDto>(1, new List<LessonReponseDto> { new LessonReponseDto { Id = 1, CourseName = "Math", Classroom = "A19", Start = new DateTime(2024, 3, 16), End = new DateTime(2024, 3, 16), Group = new GroupDto { GroupYear = 1, GroupNumber = 1 } , Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasse" }} });
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedLessons), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetLessons();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLessons.NbElement, result.NbElement);
        Assert.Single(result.Data);
    }
    
    [Fact]
    public async Task PutLesson_UpdatesLesson_ReturnsUpdatedLesson()
    {
        // Arrange
        var updatedLesson = new LessonCreation( new DateTime(2024, 3, 16), new DateTime(2024, 3, 16),  "Math", "A19", "1", 1, 1);
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(updatedLesson), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PutLesson(1, updatedLesson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedLesson.CourseName, result.CourseName);
    }
    
    [Fact]
    public async Task GetLessonsByTeacherId_ReturnsLessons()
    {
        // Arrange
        var expectedLessons = new PageReponse<LessonReponseDto>(1, new List<LessonReponseDto> { new LessonReponseDto { Id = 1, CourseName = "Math", Classroom = "A19", Start = new DateTime(2024, 3, 16), End = new DateTime(2024, 3, 16), Group = new GroupDto { GroupYear = 1, GroupNumber = 1 } , Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasse"} } });
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedLessons), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetLessonsByTeacherId("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLessons.NbElement, result.NbElement);
        Assert.Single(result.Data);
    }
    
    [Fact]
    public async Task GetEvaluations_ReturnsEvaluations()
    {
        // Arrange
        var expectedEvaluations = new PageReponse<EvaluationReponseDto>
        (1,new List<EvaluationReponseDto>
            {
                new EvaluationReponseDto { Id = 1, CourseName = "Test Course" }
        });
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedEvaluations), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetEvaluations();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedEvaluations.NbElement, result.NbElement);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task GetEvaluationById_ReturnsEvaluation()
    {
        // Arrange
        var expectedEvaluation = new EvaluationReponseDto { Id = 1, Date = new DateTime(2024, 3, 16), CourseName = "Test Course", Grade = 10, PairName = "Test Pair", Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasse"}, Template = new TemplateDto { Id = 1, Name = "Test Template" }, Student = new StudentDto { Id = 1, Name = "John", Lastname = "Doe" } };
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedEvaluation), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetEvaluationById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedEvaluation.CourseName, result.CourseName);
    }

    [Fact]
    public async Task PostEvaluation_CreatesEvaluation_ReturnsEvaluation()
    {
        // Arrange
        var newEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Test Course", 10, "Test Pair", "1", 1, 1);
        var expectedEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Test Course", 10, "Test Pair", "1", 1, 1);
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedEvaluation), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PostEvaluation(newEvaluation);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedEvaluation.CourseName, result.CourseName);
        Assert.Equal(expectedEvaluation.Grade, result.Grade);
        Assert.Equal(expectedEvaluation.PairName, result.PairName);
    }

    [Fact]
    public async Task PutEvaluation_UpdatesEvaluation_ReturnsUpdatedEvaluation()
    {
        // Arrange
        var updatedEvaluation = new EvaluationCreation(new DateTime(2024, 3, 16), "Test Course", 10, "Test Pair", "1", 1, 1);
        var expectedUpdatedEvaluation = new EvaluationReponseDto { Id = 1, Date = new DateTime(2024, 3, 16), CourseName = "Test Course", Grade = 10, PairName = "Test Pair", Teacher = new TeacherDto { Id = "1", Username = "ProfDupont", Password = "MotDePasse"}, Template = new TemplateDto { Id = 1, Name = "Test Template" }, Student = new StudentDto { Id = 1, Name = "John", Lastname = "Doe" } };
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedUpdatedEvaluation), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PutEvaluation(1, updatedEvaluation);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUpdatedEvaluation.CourseName, result.CourseName);
        Assert.Equal(expectedUpdatedEvaluation.Grade, result.Grade);
        Assert.Equal(expectedUpdatedEvaluation.PairName, result.PairName);
        Assert.Equal(expectedUpdatedEvaluation.Date, result.Date);
        Assert.Equal(expectedUpdatedEvaluation.Teacher.Id, result.Teacher.Id);
    }

    [Fact]
    public async Task DeleteEvaluation_WhenSuccessful_ReturnsTrue()
    {
        // Arrange
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var result = await _apiDataManager.DeleteEvaluation(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task GetEvaluationsByTeacherId_ReturnsEvaluations()
    {
        // Arrange
        var teacherId = "1";
        var expectedEvaluations = new PageReponse<EvaluationReponseDto>
        (1, new List<EvaluationReponseDto>
            {
                new EvaluationReponseDto { Id = 1, CourseName = "Advanced Mathematics" }
            }
        );
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedEvaluations), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetEvaluationsByTeacherId(teacherId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedEvaluations.NbElement, result.NbElement);
        Assert.Single(result.Data);
    }
    
    [Fact]
    public async Task GetTemplatesByUserId_ReturnsTemplates()
    {
        // Arrange
        var expectedTemplates = new PageReponse<TemplateDto>
        (1, new List<TemplateDto>
            {
                new TemplateDto { Id = 1, Name = "Template 1" }
            }
        );
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedTemplates), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetTemplatesByUserId("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedTemplates.NbElement, result.NbElement);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task GetTemplateById_ReturnsTemplate()
    {
        // Arrange
        var expectedTemplate = new TemplateDto { Id = 1, Name = "Template 1", Criterias = []};
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedTemplate), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.GetTemplateById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedTemplate.Name, result.Name);
        Assert.Equal(expectedTemplate.Id, result.Id);
        Assert.Empty(result.Criterias);
    }

    [Fact]
    public async Task PostTemplate_CreatesTemplate_ReturnsTemplate()
    {
        // Arrange
        var newTemplate = new Template(1, "Template 1", []);
        var expectedTemplateDto = new TemplateDto { Id = 1, Name = "Template 1", Criterias = []};
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedTemplateDto), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PostTemplate("1", newTemplate);

        // Assert
        Assert.NotNull(result);
        // Assertions for the template properties
    }

    [Fact]
    public async Task PutTemplate_UpdatesTemplate_ReturnsUpdatedTemplate()
    {
        // Arrange
        var updatedTemplate = new Template ( 1, "Template 1", []);
        var expectedUpdatedTemplateDto = new TemplateDto { Id = 1, Name = "Template 1", Criterias = []};
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedUpdatedTemplateDto), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _apiDataManager.PutTemplate(1, updatedTemplate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUpdatedTemplateDto.Name, result.Name);
        Assert.Equal(expectedUpdatedTemplateDto.Id, result.Id);
        Assert.Empty(result.Criterias);
    }

    [Fact]
    public async Task DeleteTemplate_WhenSuccessful_ReturnsTrue()
    {
        // Arrange
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var result = await _apiDataManager.DeleteTemplate(1);

        // Assert
        Assert.True(result);
    }
}
