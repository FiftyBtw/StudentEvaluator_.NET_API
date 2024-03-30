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


public class TemplatesTests
{
    private readonly Mock<ITemplateService<TemplateDto>> _mockRepo = new();
    private readonly Mock<ILogger<TemplatesController>> _mockLogger = new();
    private readonly TemplatesController _templateController;

    public TemplatesTests()
    {
        _templateController = new TemplatesController(_mockRepo.Object, _mockLogger.Object);
    }


    [Fact]
    public async Task TestAddTemplate_OkResult()
    {
        // Arrange
        var newTemplateDto = new TemplateDto
        {
            Id = 1,
            Name = "Exam SQL",
            Criterias = new List<CriteriaDto>
            {
                new TextCriteriaDto(),
                new RadioCriteriaDto()
            }
        };

        _mockRepo.Setup(service => service.PostTemplate(It.IsAny<string>(), It.IsAny<TemplateDto>()))
            .ReturnsAsync(newTemplateDto);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "testUserId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _templateController.PostTemplate(newTemplateDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTemplateDto = Assert.IsType<TemplateDto>(okResult.Value);
        Assert.Equal(newTemplateDto.Name, returnedTemplateDto.Name);
        Assert.Equal(newTemplateDto.Id, returnedTemplateDto.Id);
        Assert.Equal(newTemplateDto.Criterias.Count, returnedTemplateDto.Criterias.Count);
    }


    [Fact]
    public async Task TestAddTemplate_BadRequest()
    {
        _mockRepo.Setup(service => service.PostTemplate(It.IsAny<string>(), It.IsAny<TemplateDto>()))
            .ReturnsAsync((string userId, TemplateDto template) => null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "someUserId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var result = await _templateController.PostTemplate(new TemplateDto());

        Assert.IsType<BadRequestResult>(result);
    }



    [Fact]
    public async void TestDeleteTemplate_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.DeleteTemplate(It.IsAny<long>()))
            .ReturnsAsync(true);

        // Act
        var result = await _templateController.DeleteTemplate(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<bool>(okResult.Value);
        Assert.True(returnValue);
    }


    [Fact]
    public async void TestDeleteTemplate_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.DeleteTemplate(It.IsAny<long>()))
            .ReturnsAsync(false);

        // Act
        var result = await _templateController.DeleteTemplate(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task TestGetTemplatesByUserId_OkResult()
    {
        // Arrange
        var templates = new List<TemplateDto>
        {
            new TemplateDto {
                Id = 1,
                Name = "Exam SQL",
                Criterias = new List<CriteriaDto>{
                    new TextCriteriaDto(),
                    new RadioCriteriaDto()
                }
            },
            new TemplateDto {
                Id = 2,
                Name = "Exam Réseaux",
                Criterias = new List<CriteriaDto>{
                    new TextCriteriaDto(),
                    new RadioCriteriaDto()
                }
            },
        };
        var pageResponse = new PageReponse<TemplateDto>(templates.Count, templates);

        _mockRepo.Setup(service => service.GetTemplatesByUserId(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Simulate an authenticated user, if necessary
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _templateController.GetTemplatesByUserId(1); // Adjust this line if the method does not take a parameter

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<TemplateDto>>(okResult.Value);
        Assert.Equal(templates.Count, returnedPageResponse.Data.Count());
    }


    [Fact]
    public async void TestGetTemplatesByUserId_NotContent()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetTemplatesByUserId(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<TemplateDto>)null);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _templateController.GetTemplatesByUserId(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void TestGetTemplateById_OkResult()
    {
        // Arrange
        var userid = 1;
        var templateid = 1;
        var existingTemplateDto = new TemplateDto
        {
            Id = templateid,
            Name = "Exam SQL",
            Criterias = new List<CriteriaDto>{
                new TextCriteriaDto(),
                new RadioCriteriaDto()
                }

        };
        

        _mockRepo.Setup(service => service.GetTemplateById( It.IsAny<long>()))
            .ReturnsAsync(existingTemplateDto);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _templateController.GetTemplateById(templateid);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTemplateDto = Assert.IsType<TemplateDto>(okResult.Value);
        Assert.Equal(existingTemplateDto.Name, returnedTemplateDto.Name);
        Assert.Equal(existingTemplateDto.Id, returnedTemplateDto.Id);
        Assert.Equal(existingTemplateDto.Criterias.Count, returnedTemplateDto.Criterias.Count);
    }

    [Fact]
    public async void TestGetTemplateById_NotFound()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetTemplateById(It.IsAny<long>()))
            .ReturnsAsync((TemplateDto)null);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _templateController.GetTemplateById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task TestGetEmptyTemplateByUserId_OkResult()
    {
        // Arrange
        var templates = new List<TemplateDto>
        {
            new TemplateDto {
                Id = 1,
                Name = "Exam SQL",
                Criterias = new List<CriteriaDto>()
            },
            new TemplateDto {
                Id = 2,
                Name = "Exam Réseaux",
                Criterias = new List<CriteriaDto>()
            },
        };
        var pageResponse = new PageReponse<TemplateDto>(templates.Count, templates);

        _mockRepo.Setup(service => service.GetEmptyTemplatesByUserId(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        
        var result = await _templateController.GetEmptyTemplatesByUserId();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<TemplateDto>>(okResult.Value);
        Assert.Equal(pageResponse.nbElement, returnedPageResponse.nbElement);
    }


    [Fact]
    public async Task TestGetEmptyTemplateByUserId_NoContent()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetEmptyTemplatesByUserId(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<TemplateDto>)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "userId"),
        }, "TestAuthentication"));
        _templateController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _templateController.GetEmptyTemplatesByUserId(1, 1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void TestUpdateTemplate_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.PutTemplate(It.IsAny<long>(), It.IsAny<TemplateDto>()))
            .ReturnsAsync((long id, TemplateDto template) => template);

        // Act
        var result = await _templateController.PutTemplate(123, new TemplateDto());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<TemplateDto>(okResult.Value);
    }

    [Fact]
    public async void TestUpdateTemplate_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutTemplate(It.IsAny<long>(), It.IsAny<TemplateDto>()))
            .ReturnsAsync((long id, TemplateDto template) => null);

        // Act
        var result = await _templateController.PutTemplate(123, new TemplateDto());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

