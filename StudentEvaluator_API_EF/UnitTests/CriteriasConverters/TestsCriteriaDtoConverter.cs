using API_Dto;
using API_EF.Controllers.V1;
using API_EF.Token;
using Client_Model;
using Dto2Model;
using EF_Entities;
using Entities2Dto;
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
    public class TestsCriteriaDtoConverter
    {
        [Fact]
        public void ConvertToDto_SliderCriteriaEntity_ConvertsCorrectly()
        {
            // Arrange
            var sliderCriteriaEntity = new SliderCriteriaEntity
            {
                Id = 1,
                Name = "Slider",
                ValueEvaluation = 5,
                TemplateId = 10,
                Value = 20
            };

            // Act
            var result = CriteriaDtoConverter.ConvertToDto(sliderCriteriaEntity);

            // Assert
            Assert.IsType<SliderCriteriaDto>(result);
            var sliderCriteriaDto = result as SliderCriteriaDto;
            Assert.Equal(sliderCriteriaEntity.Id, sliderCriteriaDto.Id);
            Assert.Equal(sliderCriteriaEntity.Name, sliderCriteriaDto.Name);
            Assert.Equal(sliderCriteriaEntity.ValueEvaluation, sliderCriteriaDto.ValueEvaluation);
            Assert.Equal(sliderCriteriaEntity.TemplateId, sliderCriteriaDto.TemplateId);
            Assert.Equal(sliderCriteriaEntity.Value, sliderCriteriaDto.Value);
        }

        [Fact]
        public void ConvertToEntity_SliderCriteriaDto_ConvertsCorrectly()
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
            var result = CriteriaDtoConverter.ConvertToEntity(sliderCriteriaDto);

            // Assert
            Assert.IsType<SliderCriteriaEntity>(result);
            var sliderCriteriaEntity = result as SliderCriteriaEntity;
            Assert.Equal(sliderCriteriaDto.Id, sliderCriteriaEntity.Id);
            Assert.Equal(sliderCriteriaDto.Name, sliderCriteriaEntity.Name);
            Assert.Equal(sliderCriteriaDto.ValueEvaluation, sliderCriteriaEntity.ValueEvaluation);
            Assert.Equal(sliderCriteriaDto.TemplateId, sliderCriteriaEntity.TemplateId);
            Assert.Equal(sliderCriteriaDto.Value, sliderCriteriaEntity.Value);
        }


        [Fact]
        public void ConvertToDto_RadioCriteriaEntity_ConvertsCorrectly()
        {
            // Arrange
            var radioCriteriaEntity = new RadioCriteriaEntity
            {
                Id = 1,
                Name = "Radio",
                ValueEvaluation = 5,
                TemplateId = 10,
                Options = [ "Option1", "Option2", "Option3"],
                SelectedOption = "Option2"
            };

            // Act
            var result = CriteriaDtoConverter.ConvertToDto(radioCriteriaEntity);

            // Assert
            Assert.IsType<RadioCriteriaDto>(result);
            var radioCriteriaDto = result as RadioCriteriaDto;
            Assert.Equal(radioCriteriaEntity.Id, radioCriteriaDto.Id);
            Assert.Equal(radioCriteriaEntity.Name, radioCriteriaDto.Name);
            Assert.Equal(radioCriteriaEntity.ValueEvaluation, radioCriteriaDto.ValueEvaluation);
            Assert.Equal(radioCriteriaEntity.TemplateId, radioCriteriaDto.TemplateId);
            Assert.Equal(radioCriteriaEntity.Options, radioCriteriaDto.Options);
            Assert.Equal(radioCriteriaEntity.SelectedOption, radioCriteriaDto.SelectedOption);
        }

        [Fact]
        public void ConvertToEntity_RadioCriteriaDto_ConvertsCorrectly()
        {
            // Arrange
            var radioCriteriaDto = new RadioCriteriaDto
            {
                Id = 1,
                Name = "Radio",
                ValueEvaluation = 5,
                TemplateId = 10,
                Options = ["Option1", "Option2", "Option3"],
                SelectedOption = "Option2"
            };

            // Act
            var result = CriteriaDtoConverter.ConvertToEntity(radioCriteriaDto);

            // Assert
            Assert.IsType<RadioCriteriaEntity>(result);
            var radioCriteriaEntity = result as RadioCriteriaEntity;
            Assert.Equal(radioCriteriaDto.Id, radioCriteriaEntity.Id);
            Assert.Equal(radioCriteriaDto.Name, radioCriteriaEntity.Name);
            Assert.Equal(radioCriteriaDto.ValueEvaluation, radioCriteriaEntity.ValueEvaluation);
            Assert.Equal(radioCriteriaDto.TemplateId, radioCriteriaEntity.TemplateId);
            Assert.Equal(radioCriteriaDto.Options, radioCriteriaEntity.Options);
            Assert.Equal(radioCriteriaDto.SelectedOption, radioCriteriaEntity.SelectedOption);
        }

        [Fact]
        public void ConvertToDto_TextCriteriaEntity_ConvertsCorrectly()
        {
            // Arrange
            var textCriteriaEntity = new TextCriteriaEntity
            {
                Id = 1,
                Name = "Text",
                ValueEvaluation = 5,
                TemplateId = 10,
                Text = "Sample text"
            };

            // Act
            var result = CriteriaDtoConverter.ConvertToDto(textCriteriaEntity);

            // Assert
            Assert.IsType<TextCriteriaDto>(result);
            var textCriteriaDto = result as TextCriteriaDto;
            Assert.Equal(textCriteriaEntity.Id, textCriteriaDto.Id);
            Assert.Equal(textCriteriaEntity.Name, textCriteriaDto.Name);
            Assert.Equal(textCriteriaEntity.ValueEvaluation, textCriteriaDto.ValueEvaluation);
            Assert.Equal(textCriteriaEntity.TemplateId, textCriteriaDto.TemplateId);
            Assert.Equal(textCriteriaEntity.Text, textCriteriaDto.Text);
        }

        [Fact]
        public void ConvertToEntity_TextCriteriaDto_ConvertsCorrectly()
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
            var result = CriteriaDtoConverter.ConvertToEntity(textCriteriaDto);

            // Assert
            Assert.IsType<TextCriteriaEntity>(result);
            var textCriteriaEntity = result as TextCriteriaEntity;
            Assert.Equal(textCriteriaDto.Id, textCriteriaEntity.Id);
            Assert.Equal(textCriteriaDto.Name, textCriteriaEntity.Name);
            Assert.Equal(textCriteriaDto.ValueEvaluation, textCriteriaEntity.ValueEvaluation);
            Assert.Equal(textCriteriaDto.TemplateId, textCriteriaEntity.TemplateId);
            Assert.Equal(textCriteriaDto.Text, textCriteriaEntity.Text);
        }
    }

}
