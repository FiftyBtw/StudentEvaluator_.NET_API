using API_Dto;
using API_EF.Controllers.V1;
using API_EF.Token;
using Client_Model;
using Dto2Model;
using EF_Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EF_UnitTests.CriteriasConverters
{
    public class TestsCriteriaModelConverter
    {
        [Fact]
        public void ConvertToDto_SliderCriteria_ConvertsCorrectly()
        {
            // Arrange
            var sliderCriteria = new SliderCriteria(1,"Slider",5,10,20);

            // Act
            var result = CriteriaModelConverter.ConvertToDto(sliderCriteria);

            // Assert
            Assert.IsType<SliderCriteriaDto>(result);
            var sliderCriteriaDto = result as SliderCriteriaDto;
            Assert.Equal(sliderCriteria.Id, sliderCriteriaDto.Id);
            Assert.Equal(sliderCriteria.Name, sliderCriteriaDto.Name);
            Assert.Equal(sliderCriteria.ValueEvaluation, sliderCriteriaDto.ValueEvaluation);
            Assert.Equal(sliderCriteria.TemplateId, sliderCriteriaDto.TemplateId);
            Assert.Equal(sliderCriteria.Value, sliderCriteriaDto.Value);
        }

        [Fact]
        public void ConvertToModel_SliderCriteriaDto_ConvertsCorrectly()
        {
            // Arrange
            var sliderCriteriaDto = new SliderCriteriaDto
            {
                Id = 1,
                Name = "Slider",
                ValueEvaluation = 5,
                TemplateId = 10,
                Value = 20
            };

            // Act
            var result = CriteriaModelConverter.ConvertToModel(sliderCriteriaDto);

            // Assert
            Assert.IsType<SliderCriteria>(result);
            var sliderCriteria = result as SliderCriteria;
            Assert.Equal(sliderCriteriaDto.Id, sliderCriteria.Id);
            Assert.Equal(sliderCriteriaDto.Name, sliderCriteria.Name);
            Assert.Equal(sliderCriteriaDto.ValueEvaluation, sliderCriteria.ValueEvaluation);
            Assert.Equal(sliderCriteriaDto.TemplateId, sliderCriteria.TemplateId);
            Assert.Equal(sliderCriteriaDto.Value, sliderCriteria.Value);
        }

        [Fact]
        public void ConvertToDto_RadioCriteria_ConvertsCorrectly()
        {
            // Arrange
            var radioCriteria = new RadioCriteria(1, "Radio", 5, 10,"Options2", ["Options1","Options 2"]);

            // Act
            var result = CriteriaModelConverter.ConvertToDto(radioCriteria);

            // Assert
            Assert.IsType<RadioCriteriaDto>(result);
            var radioCriteriaDto = result as RadioCriteriaDto;
            Assert.Equal(radioCriteria.Id, radioCriteriaDto.Id);
            Assert.Equal(radioCriteria.Name, radioCriteriaDto.Name);
            Assert.Equal(radioCriteria.ValueEvaluation, radioCriteriaDto.ValueEvaluation);
            Assert.Equal(radioCriteria.TemplateId, radioCriteriaDto.TemplateId);
            Assert.Equal(radioCriteria.Options, radioCriteriaDto.Options);
            Assert.Equal(radioCriteria.SelectedOption, radioCriteriaDto.SelectedOption);
        }

        [Fact]
        public void ConvertToModel_RadioCriteriaDto_ConvertsCorrectly()
        {
            // Arrange
            var radioCriteriaDto = new RadioCriteriaDto
            {
                Id = 1,
                Name = "Radio",
                ValueEvaluation = 5,
                TemplateId = 10,
                Options =["Option1", "Option2", "Option3" ],
                SelectedOption = "Option2"
            };

            // Act
            var result = CriteriaModelConverter.ConvertToModel(radioCriteriaDto);

            // Assert
            Assert.IsType<RadioCriteria>(result);
            var radioCriteria = result as RadioCriteria;
            Assert.Equal(radioCriteriaDto.Id, radioCriteria.Id);
            Assert.Equal(radioCriteriaDto.Name, radioCriteria.Name);
            Assert.Equal(radioCriteriaDto.ValueEvaluation, radioCriteria.ValueEvaluation);
            Assert.Equal(radioCriteriaDto.TemplateId, radioCriteria.TemplateId);
            Assert.Equal(radioCriteriaDto.Options, radioCriteria.Options);
            Assert.Equal(radioCriteriaDto.SelectedOption, radioCriteria.SelectedOption);
        }

        [Fact]
        public void ConvertToDto_TextCriteria_ConvertsCorrectly()
        {
            // Arrange
            var textCriteria = new TextCriteria(1,"Text",5,10,"Sample text");

            // Act
            var result = CriteriaModelConverter.ConvertToDto(textCriteria);

            // Assert
            Assert.IsType<TextCriteriaDto>(result);
            var textCriteriaDto = result as TextCriteriaDto;
            Assert.Equal(textCriteria.Id, textCriteriaDto.Id);
            Assert.Equal(textCriteria.Name, textCriteriaDto.Name);
            Assert.Equal(textCriteria.ValueEvaluation, textCriteriaDto.ValueEvaluation);
            Assert.Equal(textCriteria.TemplateId, textCriteriaDto.TemplateId);
            Assert.Equal(textCriteria.Text, textCriteriaDto.Text);
        }

        [Fact]
        public void ConvertToModel_TextCriteriaDto_ConvertsCorrectly()
        {
            // Arrange
            var textCriteriaDto = new TextCriteriaDto
            {
                Id = 1,
                Name = "Text",
                ValueEvaluation = 5,
                TemplateId = 10,
                Text = "Sample text"
            };

            // Act
            var result = CriteriaModelConverter.ConvertToModel(textCriteriaDto);

            // Assert
            Assert.IsType<TextCriteria>(result);
            var textCriteria = result as TextCriteria;
            Assert.Equal(textCriteriaDto.Id, textCriteria.Id);
            Assert.Equal(textCriteriaDto.Name, textCriteria.Name);
            Assert.Equal(textCriteriaDto.ValueEvaluation, textCriteria.ValueEvaluation);
            Assert.Equal(textCriteriaDto.TemplateId, textCriteria.TemplateId);
            Assert.Equal(textCriteriaDto.Text, textCriteria.Text);
        }
    }

}
