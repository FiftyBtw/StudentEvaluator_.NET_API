using API_Dto;
using Moq;
using Shared;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EF_UnitTests.WebApi;

public class StudentTests
{
    private readonly Mock<IStudentService<StudentDto>> _mockRepo;
    private readonly Mock<ILogger<StudentsController>> _mockLogger;

    public StudentTests()
    {
        _mockRepo = new Mock<IStudentService<StudentDto>>();
        _mockLogger = new Mock<ILogger<StudentsController>>();

    }

    [Fact]
    public async void TestAddStudent_OkResult()
    {
        // Arrange
        var studentDto = new StudentDto
        {
            Id = 1,
            Name = "John",
            Lastname = "Doe",
            GroupYear = 2024,
            GroupNumber = 1
        };

        _mockRepo.Setup(x => x.PostStudent(It.IsAny<StudentDto>()))
                          .ReturnsAsync(studentDto);

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);

        // Act
        var result = await controller.PostStudent(studentDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(studentDto, result.Value);
    }

    [Fact]
    public async void TestAddStudent_BadRequest()
    {
        // Arrange


        _mockRepo.Setup(x => x.PostStudent(It.IsAny<StudentDto>()))
                          .ReturnsAsync((StudentDto)null);

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);
        var studentDto = new StudentDto(); 

        // Act
        var result = await controller.PostStudent(studentDto) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }


    [Fact]
    public async void TestDeleteStudent_OkResult()
    {
        // Arrange


        _mockRepo.Setup(x => x.DeleteStudent(It.IsAny<long>()))
                          .ReturnsAsync(true);

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);
        long studentIdToDelete = 1; 

        // Act
        var result = await controller.DeleteStudent(studentIdToDelete) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.True((bool)result.Value);
    }


    [Fact]
    public async void TestDeleteStudent_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.DeleteStudent(It.IsAny<long>()))
            .ReturnsAsync(false); 

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);

        // Act
        var result = await controller.DeleteStudent(123); 

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestUpdateStudent_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.PutStudent(It.IsAny<long>(), It.IsAny<StudentDto>()))
            .ReturnsAsync((long id, StudentDto student) => student);

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);

        // Act
        var result = await controller.PutStudent(123, new StudentDto());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<StudentDto>(okResult.Value);
    }

    [Fact]
    public async void TestUpdateStudent_NotFoundResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.PutStudent(It.IsAny<long>(), It.IsAny<StudentDto>()))
            .ReturnsAsync((long id, StudentDto student) => null); 

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);

        // Act
        var result = await controller.PutStudent(123, new StudentDto());

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetStudent_OkResult()
    {
        // Arrange
        var studentId = 123; 
        var existingStudent = new StudentDto { Id = studentId, Name = "John", Lastname = "Doe", GroupYear = 2024, GroupNumber = 1 };

        _mockRepo.Setup(service => service.GetStudentById(It.IsAny<long>()))
            .ReturnsAsync((long id) => id == studentId ? existingStudent : null); 

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetStudentById(studentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<StudentDto>(okResult.Value);
        Assert.Equal(studentId, returnedStudent.Id);
    }

    [Fact]
    public async void TestGetStudent_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetStudentById(It.IsAny<long>()))
            .ReturnsAsync((long id) => null); 

        var controller = new StudentsController(_mockRepo.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetStudentById(123); 

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

}

