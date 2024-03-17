using API_Dto;
using Moq;
using Shared;
using API_EF;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EF_UnitTests.WebApi;


public class GroupTests
{
    private readonly Mock<IGroupService<GroupDto>> _mockRepo = new();
    private readonly Mock<ILogger<GroupsController>> _mockLogger = new();
    private readonly GroupsController _groupsController;

    public GroupTests()
    {
       _groupsController= new GroupsController(_mockRepo.Object,_mockLogger.Object);
    }


    [Fact]
    public async void TestAddGroup_OkResult()
    {
        // Arrange
        var newGroupDto = new GroupDto(2024, 1, new List<StudentDto>());

        _mockRepo.Setup(service => service.PostGroup(It.IsAny<GroupDto>()))
            .ReturnsAsync(newGroupDto);

      

        // Act
        var result = await _groupsController.PostGroup(newGroupDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGroup = Assert.IsType<GroupDto>(okResult.Value);
        Assert.Equal(newGroupDto.GroupYear, returnedGroup.GroupYear);
        Assert.Equal(newGroupDto.GroupNumber, returnedGroup.GroupNumber);
    }

    [Fact]
    public async void TestAddGroup_BadRequest()
    {
        // Arrange
        _mockRepo.Setup(service => service.PostGroup(It.IsAny<GroupDto>()))
            .ReturnsAsync((GroupDto group) => null);

     
        // Act
        var result = await _groupsController.PostGroup(new GroupDto());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async void TestDeleteGroup_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.DeleteGroup(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(true); 

  

        // Act
        var result = await _groupsController.DeleteGroup(2024, 1); 

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<bool>(okResult.Value);
        Assert.True(returnValue);
    }


    [Fact]
    public async void TestDeleteGroup_NotFoundResult()
    {
        // Arrange
     
        _mockRepo.Setup(service => service.DeleteGroup(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(false); 


        // Act
        var result = await _groupsController.DeleteGroup(2024, 1); 

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetGroup_OkResult()
    {
        // Arrange
        var groups = new List<GroupDto>
        {
            new GroupDto(2024, 1, new List<StudentDto>()),
            new GroupDto(2024, 2, new List<StudentDto>())
        };
        var pageResponse = new PageReponse<GroupDto>(groups.Count, groups);

        _mockRepo.Setup(service => service.GetGroups(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _groupsController.GetGroups();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<GroupDto>>(okResult.Value);
        Assert.Equal(groups.Count, returnedPageResponse.Data.Count());
    }

    [Fact]
    public async void TestGetGroup_NotContent()
    {
        // Arrange


        _mockRepo.Setup(service => service.GetGroups(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<GroupDto>)null);

        // Act
        var result = await _groupsController.GetGroups();

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void TestGetGroupById_OkResult()
    {
        // Arrange
        var gyear = 2024;
        var gnumber = 1;
        var existingGroupDto = new GroupDto(gyear, gnumber, new List<StudentDto>());

      
        _mockRepo.Setup(service => service.GetGroupByIds(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(existingGroupDto); 

        // Act
        var result = await _groupsController.GetGroupById(gyear, gnumber);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGroup = Assert.IsType<GroupDto>(okResult.Value);
        Assert.Equal(gyear, returnedGroup.GroupYear);
        Assert.Equal(gnumber, returnedGroup.GroupNumber);
    }

    [Fact]
    public async void TestGetGroupById_NotFound()
    {
        // Arrange
        var gyear = 2024;
        var gnumber = 1;   
        _mockRepo.Setup(service => service.GetGroupByIds(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((GroupDto)null); 


        // Act
        var result = await _groupsController.GetGroupById(gyear, gnumber);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

}

