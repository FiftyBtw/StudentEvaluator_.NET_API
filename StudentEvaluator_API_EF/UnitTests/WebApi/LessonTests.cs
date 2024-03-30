using System.Security.Claims;
using API_Dto;
using Moq;
using Shared;
using API_EF;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_EF.Controllers;
using Microsoft.AspNetCore.Http;

namespace EF_UnitTests.WebApi;

public class LessonTests
{
    private readonly Mock<ILessonService<LessonDto, LessonReponseDto>> _mockRepo=new();
    private readonly Mock<ILogger<LessonsController>> _mockLogger=new();
    private readonly LessonsController _lessonsController;

    public LessonTests()
    {
        _lessonsController = new LessonsController(_mockRepo.Object, _mockLogger.Object);
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
            TeacherId = "1",
            GroupNumber = 1,
            GroupYear = 2024
        };

       
        _mockRepo.Setup(service => service.PostLesson(lessonDto))
            .ReturnsAsync(new LessonReponseDto
            {
                Id = 1,
                Start = lessonDto.Start,
                End = lessonDto.End,
                CourseName = lessonDto.CourseName,
                Classroom = lessonDto.Classroom,
                Teacher = new TeacherDto
                {
                    Id = lessonDto.TeacherId,
                },
                Group = new GroupDto(lessonDto.GroupYear, lessonDto.GroupNumber, new List<StudentDto>())
            });

     

        // Act
        var result = await _lessonsController.PostLesson(lessonDto);

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

        _mockRepo.Setup(service => service.PostLesson(lessonDto))
            .ReturnsAsync((LessonReponseDto)null); 
 
        // Act
        var result = await _lessonsController.PostLesson(lessonDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async void TestDeleteLesson_OkResult()
    {
        // Arrange
        var lessonId = 1;

    
        _mockRepo.Setup(service => service.DeleteLesson(lessonId))
            .ReturnsAsync(true); 


        // Act
        var result = await _lessonsController.DeleteLesson(lessonId);

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

        _mockRepo.Setup(service => service.DeleteLesson(lessonId))
            .ReturnsAsync(false); 

        // Act
        var result = await _lessonsController.DeleteLesson(lessonId);

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
            TeacherId = "1",
            GroupNumber=1,
            GroupYear=1,
        };

        var updatedLessonDto = new LessonReponseDto
        {
            Id = lessonId,
            Start = newLessonDto.Start,
            End = newLessonDto.End,
            CourseName = newLessonDto.CourseName,
            Classroom = newLessonDto.Classroom,
            Teacher = new TeacherDto
            {
                Id= newLessonDto.TeacherId
            },
            Group = new GroupDto(newLessonDto.GroupYear, newLessonDto.GroupNumber, new List<StudentDto>())
        };

        _mockRepo.Setup(service => service.PutLesson(lessonId, newLessonDto))
            .ReturnsAsync(updatedLessonDto); 

        // Act
        var result = await _lessonsController.PutLesson(lessonId, newLessonDto);

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

        _mockRepo.Setup(service => service.PutLesson(lessonId, newLessonDto))
            .ReturnsAsync((LessonReponseDto)null);

     
        // Act
        var result = await _lessonsController.PutLesson(lessonId, newLessonDto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetLesson_OkResult()
    {
        // Arrange
        var lessons = new List<LessonReponseDto>
        {
            new LessonReponseDto{
                Id = 1,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                CourseName = "Math",
                Classroom = "A11",
                Teacher = new TeacherDto
                {
                    Id= "1",
                },
                Group = new GroupDto
                {
                    GroupNumber = 1,
                    GroupYear =2
                }

                },
             new LessonReponseDto{
                Id=2,
               Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                CourseName = "Java Script",
                Classroom = "B12",
                Teacher = new TeacherDto
                {
                    Id= "2",
                },
                Group = new GroupDto
                {
                    GroupNumber = 2,
                    GroupYear =2
                }

                },
        };
        var pageResponse = new PageReponse<LessonReponseDto>(lessons.Count, lessons);

        _mockRepo.Setup(service => service.GetLessons(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _lessonsController.GetLessons();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<LessonReponseDto>>(okResult.Value);
        Assert.Equal(lessons.Count(), returnedPageResponse.Data.Count());
        Assert.Equal(pageResponse.nbElement, returnedPageResponse.nbElement);
    }

    [Fact]
    public async void TestGetLesson_NoContent()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetLessons(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<LessonReponseDto>)null);

        // Act
        var result = await _lessonsController.GetLessons();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void TestGetLessonById_OkResult()
    {
        // Arrange
        var id = 123;
        var existingLesson = new LessonReponseDto
        {
            Id = id,
            Start = DateTime.Now,
            End = DateTime.Now.AddHours(1),
            CourseName = "Math",
            Classroom = "A11",
            Teacher = new TeacherDto
            {
                Id= "1",
            },
            Group = new GroupDto
            {
                GroupNumber = 1,
                GroupYear =2
            }

        };

        _mockRepo.Setup(service => service.GetLessonById(It.IsAny<long>()))
            .ReturnsAsync((long id) => existingLesson);

        // Act
        var result = await _lessonsController.GetLessonById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedLessonReponseDto = Assert.IsType<LessonReponseDto>(okResult.Value);
        Assert.Equal(existingLesson.Id, returnedLessonReponseDto.Id);
        Assert.Equal(existingLesson.CourseName, returnedLessonReponseDto.CourseName);
        Assert.Equal(existingLesson.Classroom, returnedLessonReponseDto.Classroom);
        Assert.Equal(existingLesson.Start, returnedLessonReponseDto.Start);
        Assert.Equal(existingLesson.End, returnedLessonReponseDto.End);
        Assert.Equal(existingLesson.Teacher, returnedLessonReponseDto.Teacher);
        Assert.Equal(existingLesson.Group, returnedLessonReponseDto.Group);
        
    }

    [Fact]
    public async void TestGetLessonById_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetLessonById(It.IsAny<long>()))
            .ReturnsAsync((long id) => null);

        // Act
        var result = await _lessonsController.GetLessonById(123);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestGetLessonByTeacherId_OkResult()
    {

        // Arrange

        var teacherid = "1";
        var lessons = new List<LessonReponseDto>
        {
            new LessonReponseDto{
                Id = 1,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                CourseName = "Math",
                Classroom = "A11",
                Teacher = new TeacherDto
                {
                    Id= teacherid,
                },
                Group = new GroupDto
                {
                    GroupNumber = 1,
                    GroupYear =2
                }

                },
             new LessonReponseDto{
                Id=2,
               Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                CourseName = "Java Script",
                Classroom = "B12",
                Teacher = new TeacherDto
                {
                    Id= teacherid,
                },
                Group = new GroupDto
                {
                    GroupNumber = 2,
                    GroupYear =2
                }

                },
        };
        var pageResponse = new PageReponse<LessonReponseDto>(lessons.Count, lessons);

        _mockRepo.Setup(service => service.GetLessonsByTeacherId(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _lessonsController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _lessonsController.GetLessonsByTeacher();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageRepDto = Assert.IsType<PageReponse<LessonReponseDto>>(okResult.Value);
        Assert.Equal(pageResponse.nbElement, returnedPageRepDto.nbElement);
        Assert.Equal(pageResponse.Data.Count(), returnedPageRepDto.Data.Count());
    }

    [Fact]
    public async void TestGetLessonByTeacherId_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetLessonsByTeacherId(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<LessonReponseDto>)null);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _lessonsController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _lessonsController.GetLessonsByTeacher();

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
    }
}

