using API_Dto;
using Moq;
using Shared;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EF_UnitTests.WebApi;

public class StudentTests
{
    private readonly Mock<IStudentService<StudentDto>> _mockRepo = new();
    private readonly Mock<ILogger<StudentsController>> _mockLogger= new();
    private readonly StudentsController _studentsController;

    public StudentTests()
    {
        _studentsController = new StudentsController(_mockRepo.Object,_mockLogger.Object);
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

        // Act
        var result = await _studentsController.PostStudent(studentDto) as OkObjectResult;

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

        var studentDto = new StudentDto(); 

        // Act
        var result = await _studentsController.PostStudent(studentDto) as BadRequestResult;

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

        long studentIdToDelete = 1; 

        // Act
        var result = await _studentsController.DeleteStudent(studentIdToDelete) as OkObjectResult;

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

        // Act
        var result = await _studentsController.DeleteStudent(123); 

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestUpdateStudent_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.PutStudent(It.IsAny<long>(), It.IsAny<StudentDto>()))
            .ReturnsAsync((long id, StudentDto student) => student);


        // Act
        var result = await _studentsController.PutStudent(123, new StudentDto());

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

        // Act
        var result = await _studentsController.PutStudent(123, new StudentDto());

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


        // Act
        var result = await _studentsController.GetStudentById(studentId);

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


        // Act
        var result = await _studentsController.GetStudentById(123); 

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

}

