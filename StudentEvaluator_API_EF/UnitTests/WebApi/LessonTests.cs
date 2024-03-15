using API_Dto;
using Moq;
using Shared;
using API_EF;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
namespace EF_UnitTests.WebApi;

public class LessonTests
{
    private readonly Mock<ILessonService<LessonDto, LessonReponseDto>> _mockRepo;
    private readonly LessonsController _lessonsController;

    public LessonTests()
    {
        _mockRepo = new Mock<ILessonService<LessonDto, LessonReponseDto>>();
        _lessonsController = new LessonsController(_mockRepo.Object);
    }

    [Fact]
    public async void TestAddLesson_OkResult()
    {
        // Arrange
        var lessonDto = new LessonDto
        {
            Start = DateTime.Now,
            End = DateTime.Now.AddHours(1),
            CourseName = "Math",
            Classroom = "A101",
            TeacherId = 1,
            GroupNumber = 1,
            GroupYear = 2024
        };

        var mockLessonService = new Mock<ILessonService<LessonDto, LessonReponseDto>>();
        mockLessonService.Setup(service => service.PostLesson(lessonDto))
            .ReturnsAsync(new LessonReponseDto
            {
                Id = 1,
                Start = lessonDto.Start,
                End = lessonDto.End,
                CourseName = lessonDto.CourseName,
                Classroom = lessonDto.Classroom,
                Teacher = new TeacherDto(),
                Group = new GroupDto(lessonDto.GroupYear, lessonDto.GroupNumber, new List<StudentDto>())
            });

        var controller = new LessonsController(mockLessonService.Object);

        // Act
        var result = await controller.PostLesson(lessonDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedLesson = Assert.IsType<LessonReponseDto>(okResult.Value);
        Assert.Equal(lessonDto.Start, returnedLesson.Start);
        Assert.Equal(lessonDto.End, returnedLesson.End);
        Assert.Equal(lessonDto.CourseName, returnedLesson.CourseName);
        Assert.Equal(lessonDto.Classroom, returnedLesson.Classroom);
    }

    [Fact]
    public async void TestAddLesson_BadRequest()
    {
        // Arrange
        var lessonDto = new LessonDto
        {
        };

        var mockLessonService = new Mock<ILessonService<LessonDto, LessonReponseDto>>();
        mockLessonService.Setup(service => service.PostLesson(lessonDto))
            .ReturnsAsync((LessonReponseDto)null); 
        var controller = new LessonsController(mockLessonService.Object);

        // Act
        var result = await controller.PostLesson(lessonDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async void TestDeleteLesson_OkResult()
    {
        // Arrange
        var lessonId = 1;

        var mockLessonService = new Mock<ILessonService<LessonDto, LessonReponseDto>>();
        mockLessonService.Setup(service => service.DeleteLesson(lessonId))
            .ReturnsAsync(true); 

        var controller = new LessonsController(mockLessonService.Object);

        // Act
        var result = await controller.DeleteLesson(lessonId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedValue = Assert.IsType<bool>(okResult.Value);
        Assert.True(returnedValue);
    }


    [Fact]
    public async void TestDeleteLesson_NotFoundResult()
    {
        // Arrange
        var lessonId = 1;

        var mockLessonService = new Mock<ILessonService<LessonDto, LessonReponseDto>>();
        mockLessonService.Setup(service => service.DeleteLesson(lessonId))
            .ReturnsAsync(false); 

        var controller = new LessonsController(mockLessonService.Object);

        // Act
        var result = await controller.DeleteLesson(lessonId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestUpdateLesson_OkResult()
    {
        // Arrange
        var lessonId = 1;
        var newLessonDto = new LessonDto
        {
        };

        var updatedLessonDto = new LessonReponseDto
        {
            Id = lessonId,
            Start = newLessonDto.Start,
            End = newLessonDto.End,
            CourseName = newLessonDto.CourseName,
            Classroom = newLessonDto.Classroom,
            Teacher = new TeacherDto(),
            Group = new GroupDto(newLessonDto.GroupYear, newLessonDto.GroupNumber, new List<StudentDto>())
        };

        var mockLessonService = new Mock<ILessonService<LessonDto, LessonReponseDto>>();
        mockLessonService.Setup(service => service.PutLesson(lessonId, newLessonDto))
            .ReturnsAsync(updatedLessonDto); 

        var controller = new LessonsController(mockLessonService.Object);

        // Act
        var result = await controller.PutLesson(lessonId, newLessonDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedLesson = Assert.IsType<LessonReponseDto>(okResult.Value);
        Assert.Equal(updatedLessonDto.Id, returnedLesson.Id);
        Assert.Equal(updatedLessonDto.Start, returnedLesson.Start);
        Assert.Equal(updatedLessonDto.End, returnedLesson.End);
        Assert.Equal(updatedLessonDto.CourseName, returnedLesson.CourseName);
        Assert.Equal(updatedLessonDto.Classroom, returnedLesson.Classroom);
    }

    [Fact]
    public async void TestUpdateLesson_NotFoundResult()
    {
        // Arrange
        var lessonId = 1;
        var newLessonDto = new LessonDto
        {
        };

        var mockLessonService = new Mock<ILessonService<LessonDto, LessonReponseDto>>();
        mockLessonService.Setup(service => service.PutLesson(lessonId, newLessonDto))
            .ReturnsAsync((LessonReponseDto)null);

        var controller = new LessonsController(mockLessonService.Object);

        // Act
        var result = await controller.PutLesson(lessonId, newLessonDto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetLesson_OkResult()
    {
        
    }

    [Fact]
    public async void TestGetLesson_NoContent()
    {
        
    }

    [Fact]
    public async void TestGetLessonById_OkResult()
    {

    }

    [Fact]
    public async void TestGetLessonById_NotFoundResult()
    {

    }

    [Fact]
    public async void TestGetLessonByTeacherId_OkResult()
    {
        
    }

    [Fact]
    public async void TestGetLessonByTeacherId_NotFoundResult()
    {
        
    }
}

