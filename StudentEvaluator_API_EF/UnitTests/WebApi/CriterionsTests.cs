using API_Dto;
using Moq;
using Shared;
using API_EF;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_EF.Controllers;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace EF_UnitTests.WebApi;


public class CriterionsTests
{
    private readonly Mock<ICriteriaService<CriteriaDto,TextCriteriaDto,SliderCriteriaDto,RadioCriteriaDto>> _mockRepo = new();
    private readonly Mock<ILogger<CriterionsController>> _mockLogger = new();
    private readonly CriterionsController _criterionsController;

    public CriterionsTests()
    {
        _criterionsController = new CriterionsController(_mockRepo.Object, _mockLogger.Object);
    }


    [Fact]
    public async void TestPostTextCriterion_OkResult()
    {
        // Arrange
        var newTextCriteriaDto = new TextCriteriaDto
        {
            Id = 1,
            Name = "Remarque",
            ValueEvaluation=1,
            Text= "L'éléve à réussi à finir le tp",
            TemplateId = 1,
        };

        _mockRepo.Setup(service => service.PostTextCriterion(It.IsAny<long>(), It.IsAny<TextCriteriaDto>()))
             .ReturnsAsync(newTextCriteriaDto);

        // Act
        var result = await _criterionsController.PostTextCriterion(1, newTextCriteriaDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTextCriteriaDto = Assert.IsType<TextCriteriaDto>(okResult.Value);
        Assert.Equal(newTextCriteriaDto.Name, returnedTextCriteriaDto.Name);
        Assert.Equal(newTextCriteriaDto.Id, returnedTextCriteriaDto.Id);
        Assert.Equal(newTextCriteriaDto.ValueEvaluation, returnedTextCriteriaDto.ValueEvaluation);
        Assert.Equal(newTextCriteriaDto.Text, returnedTextCriteriaDto.Text);
        Assert.Equal(newTextCriteriaDto.TemplateId, returnedTextCriteriaDto.TemplateId);
    }

    [Fact]
    public async void TestPostTextCriterion_BadRequest()
    {
        _mockRepo.Setup(service => service.PostTextCriterion(It.IsAny<long>(), It.IsAny<TextCriteriaDto>()))
         .ReturnsAsync((long id, TextCriteriaDto textcriteria) => null);

        // Act
        var result = await _criterionsController.PostTextCriterion(1, new TextCriteriaDto());

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async void TestPostRadioCriterion_OkResult()
    {
        // Arrange
        var newRadioCriteriaDto = new RadioCriteriaDto
        {
            Id = 1,
            Name = "Qualité du code :",
            ValueEvaluation = 1,
            Options = ["satisfaisante","moyenne","insufisante"],
            SelectedOption= "satisfaisante",
            TemplateId = 1,
        };

        _mockRepo.Setup(service => service.PostRadioCriterion(It.IsAny<long>(), It.IsAny<RadioCriteriaDto>()))
             .ReturnsAsync(newRadioCriteriaDto);

        // Act
        var result = await _criterionsController.PostRadioCriterion(1, newRadioCriteriaDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedRadioCriteriaDto = Assert.IsType<RadioCriteriaDto>(okResult.Value);
        Assert.Equal(newRadioCriteriaDto.Name, returnedRadioCriteriaDto.Name);
        Assert.Equal(newRadioCriteriaDto.Id, returnedRadioCriteriaDto.Id);
        Assert.Equal(newRadioCriteriaDto.ValueEvaluation, returnedRadioCriteriaDto.ValueEvaluation);
        Assert.Equal(newRadioCriteriaDto.Options.Count(), returnedRadioCriteriaDto.Options.Count());
        Assert.Equal(newRadioCriteriaDto.SelectedOption, returnedRadioCriteriaDto.SelectedOption);
        Assert.Equal(newRadioCriteriaDto.TemplateId, returnedRadioCriteriaDto.TemplateId);
    }

    [Fact]
    public async void TestPostRadioCriterion_BadRequest()
    {
        _mockRepo.Setup(service => service.PostRadioCriterion(It.IsAny<long>(), It.IsAny<RadioCriteriaDto>()))
         .ReturnsAsync((long id, RadioCriteriaDto radiocriteria) => null);

        // Act
        var result = await _criterionsController.PostRadioCriterion(1, new RadioCriteriaDto());

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async void TestPostSliderCriterion_OkResult()
    {
        // Arrange
        var newSliderCriteriaDto = new SliderCriteriaDto
        {
            Id = 1,
            Name = "Qualité du code :",
            ValueEvaluation = 1,
            Value = 1,
            TemplateId = 1,
        };

        _mockRepo.Setup(service => service.PostSliderCriterion(It.IsAny<long>(), It.IsAny<SliderCriteriaDto>()))
             .ReturnsAsync(newSliderCriteriaDto);

        // Act
        var result = await _criterionsController.PostSliderCriterion(1, newSliderCriteriaDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedSliderCriteriaDto = Assert.IsType<SliderCriteriaDto>(okResult.Value);
        Assert.Equal(newSliderCriteriaDto.Name, returnedSliderCriteriaDto.Name);
        Assert.Equal(newSliderCriteriaDto.Id, returnedSliderCriteriaDto.Id);
        Assert.Equal(newSliderCriteriaDto.ValueEvaluation, returnedSliderCriteriaDto.ValueEvaluation);
        Assert.Equal(newSliderCriteriaDto.Value, returnedSliderCriteriaDto.Value);
        Assert.Equal(newSliderCriteriaDto.TemplateId, returnedSliderCriteriaDto.TemplateId);
    }

    [Fact]
    public async void TestPostSliderCriterion_BadRequest()
    {
        _mockRepo.Setup(service => service.PostSliderCriterion(It.IsAny<long>(), It.IsAny<SliderCriteriaDto>()))
         .ReturnsAsync((long id, SliderCriteriaDto sliderCriteriaDto) => null);

        // Act
        var result = await _criterionsController.PostSliderCriterion(1, new SliderCriteriaDto());

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async void TestDeleteCriterion_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.DeleteCriteria(It.IsAny<long>()))
            .ReturnsAsync(true);

        // Act
        var result = await _criterionsController.DeleteCriterion(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<bool>(okResult.Value);
        Assert.True(returnValue);
    }


    [Fact]
    public async void TestDeleteCriterion_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.DeleteCriteria(It.IsAny<long>()))
            .ReturnsAsync(false);

        // Act
        var result = await _criterionsController.DeleteCriterion(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetCriterionsByTemplateId_OkResult()
    {
        // Arrange
        var criterions = new List<CriteriaDto>
        {    
            new TextCriteriaDto(),
            new RadioCriteriaDto()
              
         };
        var pageResponse = new PageReponse<CriteriaDto>(criterions.Count, criterions);

        _mockRepo.Setup(service => service.GetCriterionsByTemplateId(It.IsAny<long>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _criterionsController.GetCriterionsByTemplateId(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<CriteriaDto>>(okResult.Value);
        Assert.Equal(criterions.Count, returnedPageResponse.Data.Count());
    }

    [Fact]
    public async void TestUpdateTextCriterion_OkResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutTextCriterion(It.IsAny<long>(), It.IsAny<TextCriteriaDto>()))
            .ReturnsAsync((long id, TextCriteriaDto textCriteriaDto) => textCriteriaDto);

        // Act
        var result = await _criterionsController.PutTextCriterion(123, new TextCriteriaDto());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<TextCriteriaDto>(okResult.Value);
    }

    [Fact]
    public async void TestUpdateTextCriterion_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutTextCriterion(It.IsAny<long>(), It.IsAny<TextCriteriaDto>()))
            .ReturnsAsync((long id, TextCriteriaDto textCriteriaDto) => null);

        // Act
        var result = await _criterionsController.PutTextCriterion(123, new TextCriteriaDto());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestUpdateRadioCriterion_OkResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutRadioCriterion(It.IsAny<long>(), It.IsAny<RadioCriteriaDto>()))
            .ReturnsAsync((long id, RadioCriteriaDto radioCriteriaDto) => radioCriteriaDto);

        // Act
        var result = await _criterionsController.PutRadioCriterion(123, new RadioCriteriaDto());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<RadioCriteriaDto>(okResult.Value);
    }

    [Fact]
    public async void TestUpdateRadioCriterion_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutRadioCriterion(It.IsAny<long>(), It.IsAny<RadioCriteriaDto>()))
            .ReturnsAsync((long id, RadioCriteriaDto radioCriteriaDto) => null);

        // Act
        var result = await _criterionsController.PutRadioCriterion(123, new RadioCriteriaDto());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestUpdateSliderCriterion_OkResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutSliderCriterion(It.IsAny<long>(), It.IsAny<SliderCriteriaDto>()))
            .ReturnsAsync((long id, SliderCriteriaDto sliderCriteriaDto) => sliderCriteriaDto);

        // Act
        var result = await _criterionsController.PutSliderCriterion(123, new SliderCriteriaDto());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<SliderCriteriaDto>(okResult.Value);
    }

    [Fact]
    public async void TestUpdateSliderCriterion_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutSliderCriterion(It.IsAny<long>(), It.IsAny<SliderCriteriaDto>()))
            .ReturnsAsync((long id, SliderCriteriaDto sliderCriteriaDto) => null);

        // Act
        var result = await _criterionsController.PutSliderCriterion(123, new SliderCriteriaDto());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

