using API_Dto;
using Moq;
using Shared;
using API_EF;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
namespace EF_UnitTests.WebApi;

public class StudentTests
{
    private readonly Mock<IStudentService<StudentDto>> _mockRepo;
    private readonly StudentsController _studentsController;

    public StudentTests()
    {
        _mockRepo = new Mock<IStudentService<StudentDto>>();
        _studentsController = new StudentsController(_mockRepo.Object);
    }
    [Fact]
    public async void TestAddStudent()
    {
        // Arrange
        var student = new StudentDto
        {
            Name = "Pierre",
            Lastname = "Dupuit",
            GroupNumber = 1,
            GroupYear = 3,
        };
        // Act
        var reponse = await _studentsController.PostStudent(student);
        
        // Assert
        Assert.NotNull(reponse);
    }
}

