using API_Dto;
using Moq;
using Shared;
using API_EF;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_EF.Controllers;

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
    public async void TestAddTemplate_OkResult()
    {
        // Arrange
        var newTemplateDto = new TemplateDto
        {
            Id = 1,
            Name = "Exam SQL",
            Criterias = new List<CriteriaDto>{
                new TextCriteriaDto(),
                new RadioCriteriaDto()
                }

        };

        _mockRepo.Setup(service => service.PostTemplate(It.IsAny<long>(), It.IsAny<TemplateDto>()))
             .ReturnsAsync(newTemplateDto);

        // Act
        var result = await _templateController.PostTemplate(1, newTemplateDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTemplateDto = Assert.IsType<TemplateDto>(okResult.Value);
        Assert.Equal(newTemplateDto.Name, returnedTemplateDto.Name);
        Assert.Equal(newTemplateDto.Id, returnedTemplateDto.Id);
        Assert.Equal(newTemplateDto.Criterias.Count, returnedTemplateDto.Criterias.Count);
    }

    [Fact]
    public async void TestAddTemplate_BadRequest()
    {
        _mockRepo.Setup(service => service.PostTemplate(It.IsAny<long>(), It.IsAny<TemplateDto>()))
         .ReturnsAsync((long id, TemplateDto template) => null);

        // Act
        var result = await _templateController.PostTemplate(1, new TemplateDto());

        // Assert
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
    public async void TestGetTemplatesByUserId_OkResult()
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

        _mockRepo.Setup(service => service.GetTemplatesByUserId(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _templateController.GetTemplatesByUserId(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<TemplateDto>>(okResult.Value);
        Assert.Equal(templates.Count, returnedPageResponse.Data.Count());
    }

    [Fact]
    public async void TestGetTemplatesByUserId_NotContent()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetTemplatesByUserId(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<TemplateDto>)null);

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

        _mockRepo.Setup(service => service.GetTemplateById(It.IsAny<long>(), It.IsAny<long>()))
            .ReturnsAsync(existingTemplateDto);

        // Act
        var result = await _templateController.GetTemplateById(userid, templateid);

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
        _mockRepo.Setup(service => service.GetTemplateById(It.IsAny<long>(), It.IsAny<long>()))
            .ReturnsAsync((TemplateDto)null);

        // Act
        var result = await _templateController.GetTemplateById(1, 1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetEmptyTemplateByUserId_OkResult()
    {
        // Arrange

        var id = 1;
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

        _mockRepo.Setup(service => service.GetEmptyTemplatesByUserId(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _templateController.GetEmptyTemplatesByUserId(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageRepDto = Assert.IsType<PageReponse<TemplateDto>>(okResult.Value);
        Assert.Equal(pageResponse.nbElement, returnedPageRepDto.nbElement);

    }

    [Fact]
    public async void TestGetEmptyTemplateByUserId_NotFound()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetEmptyTemplatesByUserId(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<TemplateDto>)null);

        // Act
        var result = await _templateController.GetEmptyTemplatesByUserId(1, 1);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
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

