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
using Model2Entities;
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
    public class TestsCriteriaEntityConverter
    {
        [Fact]
        public void ConvertToEntity_SliderCriteria_ConvertsCorrectly()
        {
            // Arrange
            var sliderCriteria = new SliderCriteria(1, "Slider", 5, 10, 20);

            // Act
            var result = CriteriaEntityConverter.ConvertToEntity(sliderCriteria);

            // Assert
            Assert.IsType<SliderCriteriaEntity>(result);
            var sliderCriteriaEntity = result as SliderCriteriaEntity;
            Assert.Equal(sliderCriteria.Id, sliderCriteriaEntity.Id);
            Assert.Equal(sliderCriteria.Name, sliderCriteriaEntity.Name);
            Assert.Equal(sliderCriteria.ValueEvaluation, sliderCriteriaEntity.ValueEvaluation);
            Assert.Equal(sliderCriteria.TemplateId, sliderCriteriaEntity.TemplateId);
            Assert.Equal(sliderCriteria.Value, sliderCriteriaEntity.Value);
        }



        [Fact]
        public void ConvertToModel_SliderCriteriaEntity_ConvertsCorrectly()
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
            var result = CriteriaEntityConverter.ConvertToModel(sliderCriteriaEntity);

            // Assert
            Assert.IsType<SliderCriteria>(result);
            var sliderCriteria = result as SliderCriteria;
            Assert.Equal(sliderCriteriaEntity.Id, sliderCriteria.Id);
            Assert.Equal(sliderCriteriaEntity.Name, sliderCriteria.Name);
            Assert.Equal(sliderCriteriaEntity.ValueEvaluation, sliderCriteria.ValueEvaluation);
            Assert.Equal(sliderCriteriaEntity.TemplateId, sliderCriteria.TemplateId);
            Assert.Equal(sliderCriteriaEntity.Value, sliderCriteria.Value);
        }

        [Fact]
        public void ConvertToEntity_RadioCriteria_ConvertsCorrectly()
        {
            // Arrange
            var radioCriteria = new RadioCriteria(1, "Radio", 5, 10, "Option2", ["Option1", "Option2", "Option3"]);

            // Act
            var result = CriteriaEntityConverter.ConvertToEntity(radioCriteria);

            // Assert
            Assert.IsType<RadioCriteriaEntity>(result);
            var radioCriteriaEntity = result as RadioCriteriaEntity;
            Assert.Equal(radioCriteria.Id, radioCriteriaEntity.Id);
            Assert.Equal(radioCriteria.Name, radioCriteriaEntity.Name);
            Assert.Equal(radioCriteria.ValueEvaluation, radioCriteriaEntity.ValueEvaluation);
            Assert.Equal(radioCriteria.TemplateId, radioCriteriaEntity.TemplateId);
            Assert.Equal(radioCriteria.Options, radioCriteriaEntity.Options);
            Assert.Equal(radioCriteria.SelectedOption, radioCriteriaEntity.SelectedOption);
        }

        [Fact]
        public void ConvertToModel_RadioCriteriaEntity_ConvertsCorrectly()
        {
            // Arrange
            var radioCriteriaEntity = new RadioCriteriaEntity
            {
                Id = 1,
                Name = "Radio",
                ValueEvaluation = 5,
                TemplateId = 10,
                Options = ["Option1", "Option2", "Option3"],
                SelectedOption = "Option2"
            };

            // Act
            var result = CriteriaEntityConverter.ConvertToModel(radioCriteriaEntity);

            // Assert
            Assert.IsType<RadioCriteria>(result);
            var radioCriteria = result as RadioCriteria;
            Assert.Equal(radioCriteriaEntity.Id, radioCriteria.Id);
            Assert.Equal(radioCriteriaEntity.Name, radioCriteria.Name);
            Assert.Equal(radioCriteriaEntity.ValueEvaluation, radioCriteria.ValueEvaluation);
            Assert.Equal(radioCriteriaEntity.TemplateId, radioCriteria.TemplateId);
            Assert.Equal(radioCriteriaEntity.Options, radioCriteria.Options);
            Assert.Equal(radioCriteriaEntity.SelectedOption, radioCriteria.SelectedOption);
        }

        [Fact]
        public void ConvertToEntity_TextCriteria_ConvertsCorrectly()
        {
            // Arrange
            var textCriteria = new TextCriteria(1, "Text", 5, 10, "SampleText");

            // Act
            var result = CriteriaEntityConverter.ConvertToEntity(textCriteria);

            // Assert
            Assert.IsType<TextCriteriaEntity>(result);
            var textCriteriaEntity = result as TextCriteriaEntity;
            Assert.Equal(textCriteria.Id, textCriteriaEntity.Id);
            Assert.Equal(textCriteria.Name, textCriteriaEntity.Name);
            Assert.Equal(textCriteria.ValueEvaluation, textCriteriaEntity.ValueEvaluation);
            Assert.Equal(textCriteria.TemplateId, textCriteriaEntity.TemplateId);
            Assert.Equal(textCriteria.Text, textCriteriaEntity.Text);
        }

        [Fact]
        public void ConvertToModel_TextCriteriaEntity_ConvertsCorrectly()
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
            var result = CriteriaEntityConverter.ConvertToModel(textCriteriaEntity);

            // Assert
            Assert.IsType<TextCriteria>(result);
            var textCriteria = result as TextCriteria;
            Assert.Equal(textCriteriaEntity.Id, textCriteria.Id);
            Assert.Equal(textCriteriaEntity.Name, textCriteria.Name);
            Assert.Equal(textCriteriaEntity.ValueEvaluation, textCriteria.ValueEvaluation);
            Assert.Equal(textCriteriaEntity.TemplateId, textCriteria.TemplateId);
            Assert.Equal(textCriteriaEntity.Text, textCriteria.Text);
        }

    }
}