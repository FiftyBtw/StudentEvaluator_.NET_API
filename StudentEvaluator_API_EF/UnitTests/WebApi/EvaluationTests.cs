using API_Dto;
using Moq;
using Shared;
using API_EF;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_EF.Controllers;

namespace EF_UnitTests.WebApi;


public class EvaluationTests
{
    private readonly Mock<IEvaluationService<EvaluationDto,EvaluationReponseDto>> _mockRepo = new();
    private readonly Mock<ILogger<EvaluationsController>> _mockLogger = new();
    private readonly EvaluationsController _evalController;

    public EvaluationTests()
    {
        _evalController = new EvaluationsController(_mockRepo.Object, _mockLogger.Object);
    }


    [Fact]
    public async void TestAddEvaluation_OkResult()
    {
        // Arrange
        var newEvalDto = new EvaluationDto{
            Date = new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)),
            CourseName = "JavaScript",
            Grade = 13,
            PairName=null,
            TeacherId=1,
            TemplateId=10,
            StudentId=1
        };

        _mockRepo.Setup(service => service.PostEvaluation(It.IsAny<EvaluationDto>()))
             .ReturnsAsync(new EvaluationReponseDto
             {
                 Id = 1,
                 Date = newEvalDto.Date,
                 Grade = newEvalDto.Grade,
                 CourseName = newEvalDto.CourseName,
                 PairName = newEvalDto.PairName,
                 Teacher = new TeacherDto
                 {
                     Id=1,
                 },
                 Student = new StudentDto
                 {
                     Id=1,
                 },
                 Template = new TemplateDto
                 {
                     Id=10,
                 },

             });

        // Act
        var result = await _evalController.PostEvaluation(newEvalDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedEvalDto = Assert.IsType<EvaluationReponseDto>(okResult.Value);
        Assert.Equal(newEvalDto.Date, returnedEvalDto.Date);
        Assert.Equal(newEvalDto.PairName, returnedEvalDto.PairName);
        Assert.Equal(newEvalDto.Grade, returnedEvalDto.Grade);
        Assert.Equal(newEvalDto.CourseName, returnedEvalDto.CourseName);
        Assert.Equal(newEvalDto.TeacherId, returnedEvalDto.Teacher.Id);
        Assert.Equal(newEvalDto.StudentId, returnedEvalDto.Student.Id);
        Assert.Equal(newEvalDto.TemplateId, returnedEvalDto.Template?.Id);
    }

    [Fact]
    public async void TestAddEvaluation_BadRequest()
    {
        // Arrange
        _mockRepo.Setup(service => service.PostEvaluation(It.IsAny<EvaluationDto>()))
            .ReturnsAsync((EvaluationDto evaluation) => null);

        // Act
        var result = await _evalController.PostEvaluation(new EvaluationDto());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async void TestDeleteEvaluation_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.DeleteEvaluation(It.IsAny<long>()))
            .ReturnsAsync(true); 

        // Act
        var result = await _evalController.DeleteEvaluation(1); 

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<bool>(okResult.Value);
        Assert.True(returnValue);
    }


    [Fact]
    public async void TestDeleteEvaluation_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.DeleteEvaluation(It.IsAny<long>()))
            .ReturnsAsync(false); 

        // Act
        var result = await _evalController.DeleteEvaluation(1); 

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetEvaluations_OkResult()
    {
        // Arrange
        var evaluations = new List<EvaluationReponseDto>
        {
            new EvaluationReponseDto {
                 Id = 1,
                 Date = new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)),
                 Grade = 13,
                 CourseName = "Java Script",
                 PairName = null,
                 Teacher = new TeacherDto(),
                 Student = new StudentDto(),
                 Template = new TemplateDto(),

             },

            new EvaluationReponseDto {
                 Id = 2,
                 Date = new DateOnly(2023, 11, 30).ToDateTime(new TimeOnly(11, 0)),
                 Grade = 15,
                 CourseName = "SQL",
                 PairName = null,
                 Teacher = new TeacherDto(),
                 Student = new StudentDto(),
                 Template = new TemplateDto(),

             },
         };
        var pageResponse = new PageReponse<EvaluationReponseDto>(evaluations.Count, evaluations);

        _mockRepo.Setup(service => service.GetEvaluations(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _evalController.GetEvaluations();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<EvaluationReponseDto>>(okResult.Value);
        Assert.Equal(evaluations.Count, returnedPageResponse.Data.Count());
    }

    [Fact]
    public async void TestGetEvaluation_NotContent()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetEvaluations(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<EvaluationReponseDto>)null);

        // Act
        var result = await _evalController.GetEvaluations();

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void TestGetEvaluationId_OkResult()
    {
        // Arrange
     
        var id = 1;
        var existingEvalRepDto = new EvaluationReponseDto
        {
            Id = id,
            Date = new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)),
            Grade = 13,
            CourseName = "Java Script",
            PairName = null,
            Teacher = new TeacherDto(),
            Student = new StudentDto(),
            Template = new TemplateDto(),

        };

        _mockRepo.Setup(service => service.GetEvaluationById(It.IsAny<long>()))
            .ReturnsAsync(existingEvalRepDto); 

        // Act
        var result = await _evalController.GetEvaluationById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedEvalRepDto = Assert.IsType<EvaluationReponseDto>(okResult.Value);
        Assert.Equal(existingEvalRepDto.Date, returnedEvalRepDto.Date);
        Assert.Equal(existingEvalRepDto.PairName, returnedEvalRepDto.PairName);
        Assert.Equal(existingEvalRepDto.Grade, returnedEvalRepDto.Grade);
        Assert.Equal(existingEvalRepDto.CourseName, returnedEvalRepDto.CourseName);
        Assert.Equal(existingEvalRepDto.Teacher, returnedEvalRepDto.Teacher);
        Assert.Equal(existingEvalRepDto.Student, returnedEvalRepDto.Student);
        Assert.Equal(existingEvalRepDto.Template, returnedEvalRepDto.Template);
    }

    [Fact]
    public async void TestGetEvaluationId_NotFound()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetEvaluationById(It.IsAny<long>()))
            .ReturnsAsync((EvaluationReponseDto)null); 

        // Act
        var result = await _evalController.GetEvaluationById(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetEvaluationByTeacherId_OkResult()
    {
        // Arrange

        var id = 1;
        var evaluations = new List<EvaluationReponseDto>
        {
            new EvaluationReponseDto {
                 Id = 1,
                 Date = new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)),
                 Grade = 13,
                 CourseName = "Java Script",
                 PairName = null,
                 Teacher = new TeacherDto
                 {
                     Id=id
                 },
                 Student = new StudentDto(),
                 Template = new TemplateDto(),

             },

            new EvaluationReponseDto {
                 Id = 2,
                 Date = new DateOnly(2023, 11, 30).ToDateTime(new TimeOnly(11, 0)),
                 Grade = 15,
                 CourseName = "SQL",
                 PairName = null,
                 Teacher = new TeacherDto
                 {
                     Id = id
                 },
                 Student = new StudentDto(),
                 Template = new TemplateDto(),

             },
         };
        var pageResponse = new PageReponse<EvaluationReponseDto>(evaluations.Count, evaluations);

        _mockRepo.Setup(service => service.GetEvaluationsByTeacherId(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _evalController.GetEvaluationsByTeacherId(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageRepDto = Assert.IsType<PageReponse<EvaluationReponseDto>>(okResult.Value);
        Assert.Equal(pageResponse.nbElement, returnedPageRepDto.nbElement);

    }

    [Fact]
    public async void TestGetEvaluationByTeacherId_NotFound()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetEvaluationsByTeacherId(It.IsAny<long>(),It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<EvaluationReponseDto>)null);

        // Act
        var result = await _evalController.GetEvaluationsByTeacherId(1);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void TestUpdateEval_OkResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutEvaluation(It.IsAny<long>(), It.IsAny<EvaluationDto>()))
            .ReturnsAsync(new EvaluationReponseDto
            {
                CourseName = "Java",
                Date = DateTime.Now,
                PairName = null
            });

        // Act
        var result = await _evalController.PutEvaluation(123, new EvaluationDto());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<EvaluationReponseDto>(okResult.Value);
    }

    [Fact]
    public async void TestUpdateEval_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutEvaluation(It.IsAny<long>(), It.IsAny<EvaluationDto>()))
            .ReturnsAsync((EvaluationReponseDto)null);

        // Act
        var result = await _evalController.PutEvaluation(123, new EvaluationDto());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

