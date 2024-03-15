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
    private readonly Mock<IGroupService<GroupDto>> _mockRepo;
    private readonly Mock<ILogger<GroupsController>> _mockLogger;
    private readonly GroupsController _groupsController;

    public GroupTests()
    {
        _mockRepo = new Mock<IGroupService<GroupDto>>();
        _mockLogger = new Mock<ILogger<GroupsController>>();
        _groupsController = new GroupsController(_mockRepo.Object, _mockLogger.Object);
    }


    [Fact]
    public async void TestAddGroup_OkResult()
    {
        // Arrange
        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();

        var newGroupDto = new GroupDto(2024, 1, new List<StudentDto>());

        mockGroupService.Setup(service => service.PostGroup(It.IsAny<GroupDto>()))
            .ReturnsAsync(newGroupDto);

        var controller = new GroupsController(mockGroupService.Object, mockGroupLogger.Object);

        // Act
        var result = await controller.PostGroup(newGroupDto);

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
        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();

        mockGroupService.Setup(service => service.PostGroup(It.IsAny<GroupDto>()))
            .ReturnsAsync((GroupDto group) => null);

        var controller = new GroupsController(mockGroupService.Object, mockGroupLogger.Object);

        // Act
        var result = await controller.PostGroup(new GroupDto());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async void TestDeleteGroup_OkResult()
    {
        // Arrange
        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();
        mockGroupService.Setup(service => service.DeleteGroup(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(true); 

        var controller = new GroupsController(mockGroupService.Object,mockGroupLogger.Object );

        // Act
        var result = await controller.DeleteGroup(2024, 1); 

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<bool>(okResult.Value);
        Assert.True(returnValue);
    }


    [Fact]
    public async void TestDeleteGroup_NotFoundResult()
    {
        // Arrange
        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();
        mockGroupService.Setup(service => service.DeleteGroup(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(false); 

        var controller = new GroupsController(mockGroupService.Object, mockGroupLogger.Object);

        // Act
        var result = await controller.DeleteGroup(2024, 1); 

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetGroup_OkResult()
    {
        // Arrange
        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();
        var groups = new List<GroupDto>
        {
            new GroupDto(2024, 1, new List<StudentDto>()),
            new GroupDto(2024, 2, new List<StudentDto>())
            // Ajoutez plus de données si nécessaire pour simuler différents groupes
        };
        var pageResponse = new PageReponse<GroupDto>(groups.Count, groups);

        // Configure the mock to return the page response when GetGroups is called
        mockGroupService.Setup(service => service.GetGroups(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        var controller = new GroupsController(mockGroupService.Object, mockGroupLogger.Object);

        // Act
        var result = await controller.GetGroups();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<GroupDto>>(okResult.Value);
        // Assurez-vous que la liste renvoyée contient le même nombre d'éléments que les groupes simulés
        Assert.Equal(groups.Count, returnedPageResponse.Data.Count());
    }

    [Fact]
    public async void TestGetGroup_NotContent()
    {
        // Arrange
        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();

        mockGroupService.Setup(service => service.GetGroups(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<GroupDto>)null);

        var controller = new GroupsController(mockGroupService.Object, mockGroupLogger.Object);

        // Act
        var result = await controller.GetGroups();

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

        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();
        mockGroupService.Setup(service => service.GetGroupByIds(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(existingGroupDto); 

        var controller = new GroupsController(mockGroupService.Object, mockGroupLogger.Object);

        // Act
        var result = await controller.GetGroupById(gyear, gnumber);

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

        var mockGroupService = new Mock<IGroupService<GroupDto>>();
        var mockGroupLogger = new Mock<ILogger<GroupsController>>();
        mockGroupService.Setup(service => service.GetGroupByIds(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((GroupDto)null); 

        var controller = new GroupsController(mockGroupService.Object, mockGroupLogger.Object);

        // Act
        var result = await controller.GetGroupById(gyear, gnumber);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

}

