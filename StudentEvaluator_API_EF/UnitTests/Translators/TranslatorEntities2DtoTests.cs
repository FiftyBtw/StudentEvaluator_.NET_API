using System.Diagnostics;
using EF_Entities; // Adjust the namespace based on your project structure
using API_Dto; // Adjust the namespace based on your project structure

namespace  EF_UnitTests.Translators;

public class TranslatorEntities2DtoTests
{
    [Fact]
    public void StudentEntity_To_StudentDto()
    {
        // Arrange
        var studentEntity = new StudentEntity
        {
            Id = 1,
            Name = "John",
            Lastname = "Doe",
            UrlPhoto = "https://example.com/photo.jpg",
            GroupNumber = 1,
            GroupYear = 1
        };

        // Act
        var studentDto = Entities2Dto.Translator.ToDto(studentEntity);

        // Assert
        Assert.NotNull(studentDto);
        Assert.Equal(studentEntity.Id, studentDto.Id);
        Assert.Equal(studentEntity.Name, studentDto.Name);
        Assert.Equal(studentEntity.Lastname, studentDto.Lastname);
        Assert.Equal(studentEntity.UrlPhoto, studentDto.UrlPhoto);
        Assert.Equal(studentEntity.GroupNumber, studentDto.GroupNumber);
        Assert.Equal(studentEntity.GroupYear, studentDto.GroupYear);
    }

    [Fact]
    public void StudentDto_To_StudentEntity()
    {
        // Arrange
        var studentDto = new StudentDto
        {
            Id = 1,
            Name = "John",
            Lastname = "Doe",
            UrlPhoto = "https://example.com/photo.jpg",
            GroupNumber = 1,
            GroupYear = 1
        };

        // Act
        var studentEntity = Entities2Dto.Translator.ToEntity(studentDto);

        // Assert
        Assert.NotNull(studentEntity);
        Assert.Equal(studentDto.Id, studentEntity.Id);
        Assert.Equal(studentDto.Name, studentEntity.Name);
        Assert.Equal(studentDto.Lastname, studentEntity.Lastname);
        Assert.Equal(studentDto.UrlPhoto, studentEntity.UrlPhoto);
        Assert.Equal(studentDto.GroupNumber, studentEntity.GroupNumber);
        Assert.Equal(studentDto.GroupYear, studentEntity.GroupYear);
    }

    [Fact]
    public void StudentEntities_To_StudentDtos()
    {
        // Arrange 
        var studentEntities = new List<StudentEntity>
        {
            new StudentEntity
            {
                Id = 1,
                Name = "John",
                Lastname = "Doe",
                UrlPhoto = "https://example.com/photo.jpg",
                GroupNumber = 1,
                GroupYear = 1
            },
            new StudentEntity
            {
                Id = 2,
                Name = "Jane",
                Lastname = "Doe",
                UrlPhoto = "https://example.com/photo.jpg",
                GroupNumber = 1,
                GroupYear = 1
            }
        };
        
        // Act
        var studentDtos = Entities2Dto.Translator.ToDtos(studentEntities);
        
        // Assert
        Assert.NotNull(studentDtos);
        var enumerable = studentDtos as StudentDto[] ?? studentDtos.ToArray();
        Assert.Equal(studentEntities.Count, enumerable.Length);
        foreach (var studentDto in enumerable)
        {
            var studentEntity = studentEntities.FirstOrDefault(s => s.Id == studentDto.Id);
            Assert.NotNull(studentEntity);
            Assert.Equal(studentEntity.Id, studentDto.Id);
            Assert.Equal(studentEntity.Name, studentDto.Name);
            Assert.Equal(studentEntity.Lastname, studentDto.Lastname);
            Assert.Equal(studentEntity.UrlPhoto, studentDto.UrlPhoto);
            Assert.Equal(studentEntity.GroupNumber, studentDto.GroupNumber);
            Assert.Equal(studentEntity.GroupYear, studentDto.GroupYear);
        }
    }
    
    [Fact]
    public void StudentDtos_To_StudentEntities()
    {
        // Arrange 
        var studentDtos = new List<StudentDto>
        {
            new StudentDto
            {
                Id = 1,
                Name = "John",
                Lastname = "Doe",
                UrlPhoto = "https://example.com/photo.jpg",
                GroupNumber = 1,
                GroupYear = 1
            },
            new StudentDto
            {
                Id = 2,
                Name = "Jane",
                Lastname = "Doe",
                UrlPhoto = "https://example.com/photo.jpg",
                GroupNumber = 1,
                GroupYear = 1
            }
        };
        
        // Act
        var studentEntities = Entities2Dto.Translator.ToEntities(studentDtos);
        
        // Assert
        Assert.NotNull(studentEntities);
        var enumerable = studentEntities as StudentEntity[] ?? studentEntities.ToArray();
        Assert.Equal(studentDtos.Count, enumerable.Length);
        foreach (var studentEntity in enumerable)
        {
            var studentDto = studentDtos.FirstOrDefault(s => s.Id == studentEntity.Id);
            Assert.NotNull(studentDto);
            Assert.Equal(studentDto.Id, studentEntity.Id);
            Assert.Equal(studentDto.Name, studentEntity.Name);
            Assert.Equal(studentDto.Lastname, studentEntity.Lastname);
            Assert.Equal(studentDto.UrlPhoto, studentEntity.UrlPhoto);
            Assert.Equal(studentDto.GroupNumber, studentEntity.GroupNumber);
            Assert.Equal(studentDto.GroupYear, studentEntity.GroupYear);
        }
    }
    
    [Fact]
    public void GroupEntity_To_GroupDto()
    {
        // Arrange
        var groupEntity = new GroupEntity
        {
            GroupYear = 1,
            GroupNumber = 1,
            Students = new List<StudentEntity>
            {
                new StudentEntity
                {
                    Id = 1,
                    Name = "John",
                    Lastname = "Doe",
                    UrlPhoto = "https://example.com/photo.jpg",
                    GroupNumber = 1,
                    GroupYear = 1
                },
                new StudentEntity
                {
                    Id = 2,
                    Name = "Jane",
                    Lastname = "Doe",
                    UrlPhoto = "https//example.com/photo.jpg",
                    GroupNumber = 1,
                    GroupYear = 1
                }
            }
        };

        // Act
        var groupDto = Entities2Dto.Translator.ToDto(groupEntity);

        // Assert
        Assert.NotNull(groupDto);
        Assert.Equal(groupEntity.GroupYear, groupDto.GroupYear);
        Assert.Equal(groupEntity.GroupNumber, groupDto.GroupNumber);
        Assert.NotNull(groupDto.Students);
        Assert.Equal(groupEntity.Students.Count(), groupDto.Students.Count());
        
        foreach (var studentDto in groupDto.Students)
        {
            var studentEntity = groupEntity.Students.FirstOrDefault(s => s.Id == studentDto.Id);
            Assert.NotNull(studentEntity);
            Assert.Equal(studentEntity.Id, studentDto.Id);
            Assert.Equal(studentEntity.Name, studentDto.Name);
            Assert.Equal(studentEntity.Lastname, studentDto.Lastname);
            Assert.Equal(studentEntity.UrlPhoto, studentDto.UrlPhoto);
            Assert.Equal(studentEntity.GroupNumber, studentDto.GroupNumber);
            Assert.Equal(studentEntity.GroupYear, studentDto.GroupYear);
        }
    }
    
    [Fact]
    public void GroupDto_To_GroupEntity()
    {
        // Arrange
        var groupDto = new GroupDto
        {
            GroupYear = 1,
            GroupNumber = 1,
            Students = new List<StudentDto>
            {
                new StudentDto
                {
                    Id = 1,
                    Name = "John",
                    Lastname = "Doe",
                    UrlPhoto = "https://example.com/photo.jpg",
                    GroupNumber = 1,
                    GroupYear = 1
                },
                new StudentDto
                {
                    Id = 2,
                    Name = "Jane",
                    Lastname = "Doe",
                    UrlPhoto = "https://example.com/photo.jpg",
                    GroupNumber = 1,
                    GroupYear = 1
                }
            }
        };

        // Act
        var groupEntity = Entities2Dto.Translator.ToEntity(groupDto);

        // Assert
        Assert.NotNull(groupEntity);
        Assert.Equal(groupDto.GroupYear, groupEntity.GroupYear);
        Assert.Equal(groupDto.GroupNumber, groupEntity.GroupNumber);
        Assert.NotNull(groupEntity.Students);
        Assert.Equal(groupDto.Students.Count(), groupEntity.Students.Count());
        
        foreach (var studentEntity in groupEntity.Students)
        {
            var studentDto = groupDto.Students.FirstOrDefault(s => s.Id == studentEntity.Id);
            Assert.NotNull(studentDto);
            Assert.Equal(studentDto.Id, studentEntity.Id);
            Assert.Equal(studentDto.Name, studentEntity.Name);
            Assert.Equal(studentDto.Lastname, studentEntity.Lastname);
            Assert.Equal(studentDto.UrlPhoto, studentEntity.UrlPhoto);
            Assert.Equal(studentDto.GroupNumber, studentEntity.GroupNumber);
            Assert.Equal(studentDto.GroupYear, studentEntity.GroupYear);
        }
    }

    [Fact]
    public void GroupEntities_To_GroupDtos()
    {
        // Arrange 
        var groupEntities = new List<GroupEntity>
        {
            new GroupEntity
            {
                GroupYear = 1,
                GroupNumber = 1,
                Students = new List<StudentEntity>
                {
                    new StudentEntity
                    {
                        Id = 1,
                        Name = "John",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 1,
                        GroupYear = 1
                    },
                    new StudentEntity
                    {
                        Id = 2,
                        Name = "Jane",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 1,
                        GroupYear = 1
                    }
                }
            },
            new GroupEntity
            {
                GroupYear = 1,
                GroupNumber = 2,
                Students = new List<StudentEntity>
                {
                    new StudentEntity
                    {
                        Id = 3,
                        Name = "John",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 2,
                        GroupYear = 1
                    },
                    new StudentEntity
                    {
                        Id = 4,
                        Name = "Jane",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 2,
                        GroupYear = 1
                    }
                }
            }
        };
        
        // Act
        var groupDtos = Entities2Dto.Translator.ToDtos(groupEntities);
        
        // Assert
        Assert.NotNull(groupDtos);
        var enumerable = groupDtos as GroupDto[] ?? groupDtos.ToArray();
        Assert.Equal(groupEntities.Count, enumerable.Length);
        foreach (var groupDto in enumerable)
        {
            var groupEntity = groupEntities.FirstOrDefault(s => s.GroupYear == groupDto.GroupYear && s.GroupNumber == groupDto.GroupNumber);
            Assert.NotNull(groupEntity);
            Assert.Equal(groupEntity.GroupYear, groupDto.GroupYear);
            Assert.Equal(groupEntity.GroupNumber, groupDto.GroupNumber);
            Assert.NotNull(groupDto.Students);
            Assert.Equal(groupEntity.Students.Count(), groupDto.Students.Count());
            foreach (var studentDto in groupDto.Students)
            {
                var studentEntity = groupEntity.Students.FirstOrDefault(s => s.Id == studentDto.Id);
                Assert.NotNull(studentEntity);
                Assert.Equal(studentEntity.Id, studentDto.Id);
                Assert.Equal(studentEntity.Name, studentDto.Name);
                Assert.Equal(studentEntity.Lastname, studentDto.Lastname);
                Assert.Equal(studentEntity.UrlPhoto, studentDto.UrlPhoto);
                Assert.Equal(studentEntity.GroupNumber, studentDto.GroupNumber);
                Assert.Equal(studentEntity.GroupYear, studentDto.GroupYear);
            }
        }
    }

    [Fact]
    public void GroupDtos_To_GroupEntities()
    {
        // Arrange 
        var groupDtos = new List<GroupDto>
        {
            new GroupDto
            {
                GroupYear = 1,
                GroupNumber = 1,
                Students = new List<StudentDto>
                {
                    new StudentDto
                    {
                        Id = 1,
                        Name = "John",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 1,
                        GroupYear = 1
                    },
                    new StudentDto
                    {
                        Id = 2,
                        Name = "Jane",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 1,
                        GroupYear = 1
                    }
                }
            },
            new GroupDto
            {
                GroupYear = 1,
                GroupNumber = 2,
                Students = new List<StudentDto>
                {
                    new StudentDto
                    {
                        Id = 3,
                        Name = "John",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 2,
                        GroupYear = 1
                    },
                    new StudentDto
                    {
                        Id = 4,
                        Name = "Jane",
                        Lastname = "Doe",
                        UrlPhoto = "https://example.com/photo.jpg",
                        GroupNumber = 2,
                        GroupYear = 1
                    }
                }
            }
        };

        // Act
        var groupEntities = Entities2Dto.Translator.ToEntities(groupDtos);

        // Assert
        Assert.NotNull(groupEntities);
        var enumerable = groupEntities as GroupEntity[] ?? groupEntities.ToArray();
        Assert.Equal(groupDtos.Count, enumerable.Length);
        foreach (var groupEntity in enumerable)
        {
            var groupDto = groupDtos.FirstOrDefault(s =>
                s.GroupYear == groupEntity.GroupYear && s.GroupNumber == groupEntity.GroupNumber);
            Assert.NotNull(groupDto);
            Assert.Equal(groupEntity.GroupYear, groupDto.GroupYear);
            Assert.Equal(groupEntity.GroupNumber, groupDto.GroupNumber);
            Assert.NotNull(groupEntity.Students);
            Assert.Equal(groupDto.Students.Count(), groupEntity.Students.Count());
            foreach (var studentEntity in groupEntity.Students)
            {
                var studentDto = groupDto.Students.FirstOrDefault(s => s.Id == studentEntity.Id);
                Assert.NotNull(studentDto);
                Assert.Equal(studentDto.Id, studentEntity.Id);
                Assert.Equal(studentDto.Name, studentEntity.Name);
                Assert.Equal(studentDto.Lastname, studentEntity.Lastname);
                Assert.Equal(studentDto.UrlPhoto, studentEntity.UrlPhoto);
                Assert.Equal(studentDto.GroupNumber, studentEntity.GroupNumber);
                Assert.Equal(studentDto.GroupYear, studentEntity.GroupYear);
            }
        }
    }

    [Fact]
    public void TextCriteriaEntity_To_TextCriteriaDto()
    {
        // Arrange
        var textCriteriaEntity = new TextCriteriaEntity
        {
            Id = 1,
            Name = "Text1",
            ValueEvaluation = 4,
            Text = "This is a text",
            TemplateId = 1
        };

        // Act
        var textCriteriaDto = Entities2Dto.Translator.ToDto(textCriteriaEntity);

        // Assert
        Assert.NotNull(textCriteriaDto);
        Assert.Equal(textCriteriaEntity.Id, textCriteriaDto.Id);
        Assert.Equal(textCriteriaEntity.Name, textCriteriaDto.Name);
        Assert.Equal(textCriteriaEntity.ValueEvaluation, textCriteriaDto.ValueEvaluation);
        Assert.Equal(textCriteriaEntity.Text, textCriteriaDto.Text);
        Assert.Equal(textCriteriaEntity.TemplateId, textCriteriaDto.TemplateId);
    }

    [Fact]
    public void TextCriteriaDto_To_TextCriteriaEntity()
    {
        // Arrange
        var textCriteriaDto = new TextCriteriaDto
        {
            Id = 1,
            Name = "Text1",
            ValueEvaluation = 4,
            Text = "This is a text",
            TemplateId = 1
        };

        // Act
        var textCriteriaEntity = Entities2Dto.Translator.ToEntity(textCriteriaDto);

        // Assert
        Assert.NotNull(textCriteriaEntity);
        Assert.Equal(textCriteriaDto.Id, textCriteriaEntity.Id);
        Assert.Equal(textCriteriaDto.Name, textCriteriaEntity.Name);
        Assert.Equal(textCriteriaDto.ValueEvaluation, textCriteriaEntity.ValueEvaluation);
        Assert.Equal(textCriteriaDto.Text, textCriteriaEntity.Text);
        Assert.Equal(textCriteriaDto.TemplateId, textCriteriaEntity.TemplateId);
    }
    
    [Fact]
    public void TextCriteriaEntities_To_TextCriteriaDtos()
    {
        // Arrange 
        var textCriteriaEntities = new List<TextCriteriaEntity>
        {
            new TextCriteriaEntity
            {
                Id = 1,
                Name = "Text1",
                ValueEvaluation = 4,
                Text = "This is a text",
                TemplateId = 1
            },
            new TextCriteriaEntity
            {
                Id = 2,
                Name = "Text2",
                ValueEvaluation = 3,
                Text = "This is another text",
                TemplateId = 1
            }
        };
        
        // Act
        var textCriteriaDtos = Entities2Dto.Translator.ToDtos(textCriteriaEntities);
        
        // Assert
        Assert.NotNull(textCriteriaDtos);
        var criteriaDtos = textCriteriaDtos as TextCriteriaDto[] ?? textCriteriaDtos.ToArray();
        Assert.Equal(textCriteriaEntities.Count, criteriaDtos.Length);
        foreach (var textCriteriaDto in criteriaDtos)
        {
            var textCriteriaEntity = textCriteriaEntities.FirstOrDefault(s => s.Id == textCriteriaDto.Id);
            Assert.NotNull(textCriteriaEntity);
            Assert.Equal(textCriteriaEntity.Id, textCriteriaDto.Id);
            Assert.Equal(textCriteriaEntity.Name, textCriteriaDto.Name);
            Assert.Equal(textCriteriaEntity.ValueEvaluation, textCriteriaDto.ValueEvaluation);
            Assert.Equal(textCriteriaEntity.Text, textCriteriaDto.Text);
            Assert.Equal(textCriteriaEntity.TemplateId, textCriteriaDto.TemplateId);
        }
    }
    
    [Fact]
    public void TextCriteriaDtos_To_TextCriteriaEntities()
    {
        // Arrange 
        var textCriteriaDtos = new List<TextCriteriaDto>
        {
            new TextCriteriaDto
            {
                Id = 1,
                Name = "Text1",
                ValueEvaluation = 4,
                Text = "This is a text",
                TemplateId = 1
            },
            new TextCriteriaDto
            {
                Id = 2,
                Name = "Text2",
                ValueEvaluation = 3,
                Text = "This is another text",
                TemplateId = 1
            }
        };
        
        // Act
        var textCriteriaEntities = Entities2Dto.Translator.ToEntities(textCriteriaDtos);
        
        // Assert
        Assert.NotNull(textCriteriaEntities);
        var criteriaEntities = textCriteriaEntities as TextCriteriaEntity[] ?? textCriteriaEntities.ToArray();
        Assert.Equal(textCriteriaDtos.Count, criteriaEntities.Length);
        foreach (var textCriteriaEntity in criteriaEntities)
        {
            var textCriteriaDto = textCriteriaDtos.FirstOrDefault(s => s.Id == textCriteriaEntity.Id);
            Assert.NotNull(textCriteriaDto);
            Assert.Equal(textCriteriaDto.Id, textCriteriaEntity.Id);
            Assert.Equal(textCriteriaDto.Name, textCriteriaEntity.Name);
            Assert.Equal(textCriteriaDto.ValueEvaluation, textCriteriaEntity.ValueEvaluation);
            Assert.Equal(textCriteriaDto.Text, textCriteriaEntity.Text);
            Assert.Equal(textCriteriaDto.TemplateId, textCriteriaEntity.TemplateId);
        }
    }

    [Fact]
    public void SliderCriteriaEntity_To_SliderCriteriaDto()
    {
        // Arrange
        var sliderCriteriaEntity = new SliderCriteriaEntity
        {
            Id = 1,
            Name = "Slider1",
            ValueEvaluation = 4,
            Value = 5,
            TemplateId = 1
        };

        // Act
        var sliderCriteriaDto = Entities2Dto.Translator.ToDto(sliderCriteriaEntity);

        // Assert
        Assert.NotNull(sliderCriteriaDto);
        Assert.Equal(sliderCriteriaEntity.Id, sliderCriteriaDto.Id);
        Assert.Equal(sliderCriteriaEntity.Name, sliderCriteriaDto.Name);
        Assert.Equal(sliderCriteriaEntity.ValueEvaluation, sliderCriteriaDto.ValueEvaluation);
        Assert.Equal(sliderCriteriaEntity.Value, sliderCriteriaDto.Value);
        Assert.Equal(sliderCriteriaEntity.TemplateId, sliderCriteriaDto.TemplateId);
    }
    
    [Fact]
    public void SliderCriteriaDto_To_SliderCriteriaEntity()
    {
        // Arrange
        var sliderCriteriaDto = new SliderCriteriaDto
        {
            Id = 1,
            Name = "Slider1",
            ValueEvaluation = 4,
            Value = 5,
            TemplateId = 1
        };

        // Act
        var sliderCriteriaEntity = Entities2Dto.Translator.ToEntity(sliderCriteriaDto);

        // Assert
        Assert.NotNull(sliderCriteriaEntity);
        Assert.Equal(sliderCriteriaDto.Id, sliderCriteriaEntity.Id);
        Assert.Equal(sliderCriteriaDto.Name, sliderCriteriaEntity.Name);
        Assert.Equal(sliderCriteriaDto.ValueEvaluation, sliderCriteriaEntity.ValueEvaluation);
        Assert.Equal(sliderCriteriaDto.Value, sliderCriteriaEntity.Value);
        Assert.Equal(sliderCriteriaDto.TemplateId, sliderCriteriaEntity.TemplateId);
    }
    
    [Fact]
    public void SliderCriteriaEntities_To_SliderCriteriaDtos()
    {
        // Arrange 
        var sliderCriteriaEntities = new List<SliderCriteriaEntity>
        {
            new SliderCriteriaEntity
            {
                Id = 1,
                Name = "Slider1",
                ValueEvaluation = 4,
                Value = 5,
                TemplateId = 1
            },
            new SliderCriteriaEntity
            {
                Id = 2,
                Name = "Slider2",
                ValueEvaluation = 3,
                Value = 4,
                TemplateId = 1
            }
        };
        
        // Act
        var sliderCriteriaDtos = Entities2Dto.Translator.ToDtos(sliderCriteriaEntities);
        
        // Assert
        Assert.NotNull(sliderCriteriaDtos);
        var criteriaDtos = sliderCriteriaDtos as SliderCriteriaDto[] ?? sliderCriteriaDtos.ToArray();
        Assert.Equal(sliderCriteriaEntities.Count, criteriaDtos.Length);
        foreach (var sliderCriteriaDto in criteriaDtos)
        {
            var sliderCriteriaEntity = sliderCriteriaEntities.FirstOrDefault(s => s.Id == sliderCriteriaDto.Id);
            Assert.NotNull(sliderCriteriaEntity);
            Assert.Equal(sliderCriteriaEntity.Id, sliderCriteriaDto.Id);
            Assert.Equal(sliderCriteriaEntity.Name, sliderCriteriaDto.Name);
            Assert.Equal(sliderCriteriaEntity.ValueEvaluation, sliderCriteriaDto.ValueEvaluation);
            Assert.Equal(sliderCriteriaEntity.Value, sliderCriteriaDto.Value);
            Assert.Equal(sliderCriteriaEntity.TemplateId, sliderCriteriaDto.TemplateId);
        }
    }
    
    [Fact]
    public void SliderCriteriaDtos_To_SliderCriteriaEntities()
    {
        // Arrange 
        var sliderCriteriaDtos = new List<SliderCriteriaDto>
        {
            new SliderCriteriaDto
            {
                Id = 1,
                Name = "Slider1",
                ValueEvaluation = 4,
                Value = 5,
                TemplateId = 1
            },
            new SliderCriteriaDto
            {
                Id = 2,
                Name = "Slider2",
                ValueEvaluation = 3,
                Value = 4,
                TemplateId = 1
            }
        };
        
        // Act
        var sliderCriteriaEntities = Entities2Dto.Translator.ToEntities(sliderCriteriaDtos);
        
        // Assert
        Assert.NotNull(sliderCriteriaEntities);
        var criteriaEntities = sliderCriteriaEntities as SliderCriteriaEntity[] ?? sliderCriteriaEntities.ToArray();
        Assert.Equal(sliderCriteriaDtos.Count, criteriaEntities.Length);
        foreach (var sliderCriteriaEntity in criteriaEntities)
        {
            var sliderCriteriaDto = sliderCriteriaDtos.FirstOrDefault(s => s.Id == sliderCriteriaEntity.Id);
            Assert.NotNull(sliderCriteriaDto);
            Assert.Equal(sliderCriteriaDto.Id, sliderCriteriaEntity.Id);
            Assert.Equal(sliderCriteriaDto.Name, sliderCriteriaEntity.Name);
            Assert.Equal(sliderCriteriaDto.ValueEvaluation, sliderCriteriaEntity.ValueEvaluation);
            Assert.Equal(sliderCriteriaDto.Value, sliderCriteriaEntity.Value);
            Assert.Equal(sliderCriteriaDto.TemplateId, sliderCriteriaEntity.TemplateId);
        }
    }

    [Fact]
    public void RadioCriteriaEntity_To_RadioCriteriaDto()
    {
        // Arrange
        var radioCriteriaEntity = new RadioCriteriaEntity
        {
            Id = 1,
            Name = "Radio1",
            ValueEvaluation = 4,
            Options = ["Option 1", "Option 2"],
            SelectedOption = "Option 1",
            TemplateId = 1
        };

        // Act
        var radioCriteriaDto = Entities2Dto.Translator.ToDto(radioCriteriaEntity);
        
        // Assert
        Assert.NotNull(radioCriteriaDto);
        Assert.Equal(radioCriteriaEntity.Id, radioCriteriaDto.Id);
        Assert.Equal(radioCriteriaEntity.Name, radioCriteriaDto.Name);
        Assert.Equal(radioCriteriaEntity.ValueEvaluation, radioCriteriaDto.ValueEvaluation);
        Assert.Equal(radioCriteriaEntity.Options, radioCriteriaDto.Options);
        Assert.Equal(radioCriteriaEntity.SelectedOption, radioCriteriaDto.SelectedOption);
        Assert.Equal(radioCriteriaEntity.TemplateId, radioCriteriaDto.TemplateId);
    }
    
    [Fact]
    public void RadioCriteriaDto_To_RadioCriteriaEntity()
    {
        // Arrange
        var radioCriteriaDto = new RadioCriteriaDto
        {
            Id = 1,
            Name = "Radio1",
            ValueEvaluation = 4,
            Options = ["Option 1", "Option 2"],
            SelectedOption = "Option 1",
            TemplateId = 1
        };

        // Act
        var radioCriteriaEntity = Entities2Dto.Translator.ToEntity(radioCriteriaDto);
        
        // Assert
        Assert.NotNull(radioCriteriaEntity);
        Assert.Equal(radioCriteriaDto.Id, radioCriteriaEntity.Id);
        Assert.Equal(radioCriteriaDto.Name, radioCriteriaEntity.Name);
        Assert.Equal(radioCriteriaDto.ValueEvaluation, radioCriteriaEntity.ValueEvaluation);
        Assert.Equal(radioCriteriaDto.Options, radioCriteriaEntity.Options);
        Assert.Equal(radioCriteriaDto.SelectedOption, radioCriteriaEntity.SelectedOption);
        Assert.Equal(radioCriteriaDto.TemplateId, radioCriteriaEntity.TemplateId);
    }
    
    [Fact]
    public void RadioCriteriaEntities_To_RadioCriteriaDtos()
    {
        // Arrange 
        var radioCriteriaEntities = new List<RadioCriteriaEntity>
        {
            new RadioCriteriaEntity
            {
                Id = 1,
                Name = "Radio1",
                ValueEvaluation = 4,
                Options = ["Option 1", "Option 2"],
                SelectedOption = "Option 1",
                TemplateId = 1
            },
            new RadioCriteriaEntity
            {
                Id = 2,
                Name = "Radio2",
                ValueEvaluation = 3,
                Options = ["Option 1", "Option 2"],
                SelectedOption = "Option 2",
                TemplateId = 1
            }
        };
        
        // Act
        var radioCriteriaDtos = Entities2Dto.Translator.ToDtos(radioCriteriaEntities);
        
        // Assert
        Assert.NotNull(radioCriteriaDtos);
        var criteriaDtos = radioCriteriaDtos as RadioCriteriaDto[] ?? radioCriteriaDtos.ToArray();
        Assert.Equal(radioCriteriaEntities.Count, criteriaDtos.Length);
        foreach (var radioCriteriaDto in criteriaDtos)
        {
            var radioCriteriaEntity = radioCriteriaEntities.FirstOrDefault(s => s.Id == radioCriteriaDto.Id);
            Assert.NotNull(radioCriteriaEntity);
            Assert.Equal(radioCriteriaEntity.Id, radioCriteriaDto.Id);
            Assert.Equal(radioCriteriaEntity.Name, radioCriteriaDto.Name);
            Assert.Equal(radioCriteriaEntity.ValueEvaluation, radioCriteriaDto.ValueEvaluation);
            Assert.Equal(radioCriteriaEntity.Options, radioCriteriaDto.Options);
            Assert.Equal(radioCriteriaEntity.SelectedOption, radioCriteriaDto.SelectedOption);
            Assert.Equal(radioCriteriaEntity.TemplateId, radioCriteriaDto.TemplateId);
        }
    }
    
    [Fact]
    public void RadioCriteriaDtos_To_RadioCriteriaEntities()
    {
        // Arrange 
        var radioCriteriaDtos = new List<RadioCriteriaDto>
        {
            new RadioCriteriaDto
            {
                Id = 1,
                Name = "Radio1",
                ValueEvaluation = 4,
                Options = ["Option 1", "Option 2"],
                SelectedOption = "Option 1",
                TemplateId = 1
            },
            new RadioCriteriaDto
            {
                Id = 2,
                Name = "Radio2",
                ValueEvaluation = 3,
                Options = ["Option 1", "Option 2"],
                SelectedOption = "Option 2",
                TemplateId = 1
            }
        };
        
        // Act
        var radioCriteriaEntities = Entities2Dto.Translator.ToEntities(radioCriteriaDtos);
        
        // Assert
        Assert.NotNull(radioCriteriaEntities);
        var criteriaEntities = radioCriteriaEntities as RadioCriteriaEntity[] ?? radioCriteriaEntities.ToArray();
        Assert.Equal(radioCriteriaDtos.Count, criteriaEntities.Length);
        foreach (var radioCriteriaEntity in criteriaEntities)
        {
            var radioCriteriaDto = radioCriteriaDtos.FirstOrDefault(s => s.Id == radioCriteriaEntity.Id);
            Assert.NotNull(radioCriteriaDto);
            Assert.Equal(radioCriteriaDto.Id, radioCriteriaEntity.Id);
            Assert.Equal(radioCriteriaDto.Name, radioCriteriaEntity.Name);
            Assert.Equal(radioCriteriaDto.ValueEvaluation, radioCriteriaEntity.ValueEvaluation);
            Assert.Equal(radioCriteriaDto.Options, radioCriteriaEntity.Options);
            Assert.Equal(radioCriteriaDto.SelectedOption, radioCriteriaEntity.SelectedOption);
            Assert.Equal(radioCriteriaDto.TemplateId, radioCriteriaEntity.TemplateId);
        }
    }

    [Fact]
    public void TemplateEntity_To_TemplateDto()
    {
        // Arrange
        var templateEntity = new TemplateEntity
        {
            Id = 1,
            Name = "Template1",
            Criteria = new List<CriteriaEntity>
            {
                new TextCriteriaEntity
                {
                    Id = 1,
                    Name = "Text1",
                    ValueEvaluation = 4,
                    Text = "This is a text",
                    TemplateId = 1
                },
                new TextCriteriaEntity
                {
                    Id = 2,
                    Name = "Text2",
                    ValueEvaluation = 3,
                    Text = "This is another text",
                    TemplateId = 1
                },
                new SliderCriteriaEntity
                {
                    Id = 1,
                    Name = "Slider1",
                    ValueEvaluation = 4,
                    Value = 5,
                    TemplateId = 1
                },
                new SliderCriteriaEntity
                {
                    Id = 2,
                    Name = "Slider2",
                    ValueEvaluation = 3,
                    Value = 4,
                    TemplateId = 1
                },
                new RadioCriteriaEntity
                {
                    Id = 1,
                    Name = "Radio1",
                    ValueEvaluation = 4,
                    Options = ["Option 1", "Option 2"],
                    SelectedOption = "Option 1",
                    TemplateId = 1
                },
                new RadioCriteriaEntity
                {
                    Id = 2,
                    Name = "Radio2",
                    ValueEvaluation = 3,
                    Options = ["Option 1", "Option 2"],
                    SelectedOption = "Option 2",
                    TemplateId = 1
                }
            }
        };

        // Act
        var templateDto = Entities2Dto.Translator.ToDto(templateEntity);

        // Assert
        Assert.NotNull(templateDto);
        Assert.Equal(templateEntity.Id, templateDto.Id);
        Assert.Equal(templateEntity.Name, templateDto.Name);
        Assert.NotNull(templateDto.Criterias);
        Assert.Equal(templateEntity.Criteria.Count(), templateDto.Criterias.Count());
    }

    [Fact]
    public void TemplateDto_To_TemplateEntity()
    {
        // Arrange
        var templateDto = new TemplateDto
        {
            Id = 1,
            Name = "Template1",
            Criterias =
            [
                new TextCriteriaDto
                {
                    Id = 1,
                    Name = "Text1",
                    ValueEvaluation = 4,
                    Text = "This is a text",
                    TemplateId = 1
                },

                new TextCriteriaDto
                {
                    Id = 2,
                    Name = "Text2",
                    ValueEvaluation = 3,
                    Text = "This is another text",
                    TemplateId = 1
                },

                new SliderCriteriaDto
                {
                    Id = 1,
                    Name = "Slider1",
                    ValueEvaluation = 4,
                    Value = 5,
                    TemplateId = 1
                },

                new SliderCriteriaDto
                {
                    Id = 2,
                    Name = "Slider2",
                    ValueEvaluation = 3,
                    Value = 4,
                    TemplateId = 1
                },

                new RadioCriteriaDto
                {
                    Id = 1,
                    Name = "Radio1",
                    ValueEvaluation = 4,
                    Options = ["Option 1", "Option 2"],
                    SelectedOption = "Option 1",
                    TemplateId = 1
                },

                new RadioCriteriaDto
                {
                    Id = 2,
                    Name = "Radio2",
                    ValueEvaluation = 3,
                    Options = ["Option 1", "Option 2"],
                    SelectedOption = "Option 2",
                    TemplateId = 1
                }
            ]
        };

        // Act
        var templateEntity = Entities2Dto.Translator.ToEntity(templateDto);

        // Assert
        Assert.NotNull(templateEntity);
        Assert.Equal(templateDto.Id, templateEntity.Id);
        Assert.Equal(templateDto.Name, templateEntity.Name);
        Assert.NotNull(templateEntity.Criteria);
        Assert.Equal(templateDto.Criterias.Count(), templateEntity.Criteria.Count());
    }

    [Fact]
    public void TemplateEntities_To_TemplateDtos()
    {
        // Arrange 
        List<TemplateEntity> templateEntities =
        [
            new TemplateEntity
            {
                Id = 1,
                Name = "Template1",
                Criteria = new List<CriteriaEntity>
                {
                    new TextCriteriaEntity
                    {
                        Id = 1,
                        Name = "Text1",
                        ValueEvaluation = 4,
                        Text = "This is a text",
                        TemplateId = 1
                    },
                    new TextCriteriaEntity
                    {
                        Id = 2,
                        Name = "Text2",
                        ValueEvaluation = 3,
                        Text = "This is another text",
                        TemplateId = 1
                    },
                    new SliderCriteriaEntity
                    {
                        Id = 1,
                        Name = "Slider1",
                        ValueEvaluation = 4,
                        Value = 5,
                        TemplateId = 1
                    },
                    new SliderCriteriaEntity
                    {
                        Id = 2,
                        Name = "Slider2",
                        ValueEvaluation = 3,
                        Value = 4,
                        TemplateId = 1
                    },
                    new RadioCriteriaEntity
                    {
                        Id = 1,
                        Name = "Radio1",
                        ValueEvaluation = 4,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 1",
                        TemplateId = 1
                    },
                    new RadioCriteriaEntity
                    {
                        Id = 2,
                        Name = "Radio2",
                        ValueEvaluation = 3,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 2",
                        TemplateId = 1
                    }
                }
            },

            new TemplateEntity
            {
                Id = 2,
                Name = "Template2",
                Criteria = new List<CriteriaEntity>
                {
                    new TextCriteriaEntity
                    {
                        Id = 3,
                        Name = "Text3",
                        ValueEvaluation = 4,
                        Text = "This is a text",
                        TemplateId = 2
                    },
                    new TextCriteriaEntity
                    {
                        Id = 4,
                        Name = "Text4",
                        ValueEvaluation = 3,
                        Text = "This is another text",
                        TemplateId = 2
                    },
                    new SliderCriteriaEntity
                    {
                        Id = 3,
                        Name = "Slider3",
                        ValueEvaluation = 4,
                        Value = 5,
                        TemplateId = 2
                    },
                    new SliderCriteriaEntity
                    {
                        Id = 4,
                        Name = "Slider4",
                        ValueEvaluation = 3,
                        Value = 4,
                        TemplateId = 2
                    },
                    new RadioCriteriaEntity
                    {
                        Id = 3,
                        Name = "Radio3",
                        ValueEvaluation = 4,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 1",
                        TemplateId = 2
                    },
                    new RadioCriteriaEntity
                    {
                        Id = 4,
                        Name = "Radio4",
                        ValueEvaluation = 3,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 2",
                        TemplateId = 2
                    }
                }
            }
        ];

        // Act
        IEnumerable<TemplateDto> templateDtos = Entities2Dto.Translator.ToDtos(templateEntities);
        
        // Assert
        Assert.NotNull(templateDtos);
        var enumerable = templateDtos as TemplateDto[] ?? templateDtos.ToArray();
        Assert.Equal(templateEntities.Count, enumerable.Length);
        foreach (TemplateDto templateDto in enumerable)
        {
            TemplateEntity? templateEntity = templateEntities.FirstOrDefault(s => s.Id == templateDto.Id);
            Assert.NotNull(templateEntity);
            Assert.Equal(templateEntity.Id, templateDto.Id);
            Assert.Equal(templateEntity.Name, templateDto.Name);
            Assert.NotNull(templateDto.Criterias);
            Debug.Assert(templateEntity.Criteria != null, "templateEntity.Criteria != null");
            Assert.Equal(templateEntity.Criteria.Count(), templateDto.Criterias.Count());
        }
    }


    [Fact]
    public void TemplateDtos_To_TemplateEntities()
    {
        // Arrange 
        var templateDtos = new List<TemplateDto>
        {
            new TemplateDto
            {
                Id = 1,
                Name = "Template1",
                Criterias =
                [
                    new TextCriteriaDto
                    {
                        Id = 1,
                        Name = "Text1",
                        ValueEvaluation = 4,
                        Text = "This is a text",
                        TemplateId = 1
                    },

                    new TextCriteriaDto
                    {
                        Id = 2,
                        Name = "Text2",
                        ValueEvaluation = 3,
                        Text = "This is another text",
                        TemplateId = 1
                    },

                    new SliderCriteriaDto
                    {
                        Id = 1,
                        Name = "Slider1",
                        ValueEvaluation = 4,
                        Value = 5,
                        TemplateId = 1
                    },

                    new SliderCriteriaDto
                    {
                        Id = 2,
                        Name = "Slider2",
                        ValueEvaluation = 3,
                        Value = 4,
                        TemplateId = 1
                    },

                    new RadioCriteriaDto
                    {
                        Id = 1,
                        Name = "Radio1",
                        ValueEvaluation = 4,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 1",
                        TemplateId = 1
                    },

                    new RadioCriteriaDto
                    {
                        Id = 2,
                        Name = "Radio2",
                        ValueEvaluation = 3,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 2",
                        TemplateId = 1
                    }
                ]
            },
            new TemplateDto
            {
                Id = 2,
                Name = "Template2",
                Criterias =
                [
                    new TextCriteriaDto
                    {
                        Id = 3,
                        Name = "Text3",
                        ValueEvaluation = 4,
                        Text = "This is a text",
                        TemplateId = 2
                    },

                    new TextCriteriaDto
                    {
                        Id = 4,
                        Name = "Text4",
                        ValueEvaluation = 3,
                        Text = "This is another text",
                        TemplateId = 2
                    },

                    new SliderCriteriaDto
                    {
                        Id = 5,
                        Name = "Slider3",
                        ValueEvaluation = 4,
                        Value = 5,
                        TemplateId = 2
                    },

                    new SliderCriteriaDto
                    {
                        Id = 6,
                        Name = "Slider4",
                        ValueEvaluation = 3,
                        Value = 4,
                        TemplateId = 2
                    },

                    new RadioCriteriaDto
                    {
                        Id = 7,
                        Name = "Radio3",
                        ValueEvaluation = 4,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 1",
                        TemplateId = 2
                    },

                    new RadioCriteriaDto
                    {
                        Id = 8,
                        Name = "Radio4",
                        ValueEvaluation = 3,
                        Options = ["Option 1", "Option 2"],
                        SelectedOption = "Option 2",
                        TemplateId = 2
                    }
                ]
            }
        };
        
        // Act
        var templateEntities = Entities2Dto.Translator.ToEntities(templateDtos);
        
        // Assert
        Assert.NotNull(templateEntities);
        var enumerable = templateEntities as TemplateEntity[] ?? templateEntities.ToArray();
        Assert.Equal(templateDtos.Count, enumerable.Length);
        foreach (var templateEntity in enumerable)
        {
            var templateDto = templateDtos.FirstOrDefault(s => s.Id == templateEntity.Id);
            Assert.NotNull(templateDto);
            Assert.Equal(templateEntity.Id, templateDto.Id);
            Assert.Equal(templateEntity.Name, templateDto.Name);
            Assert.NotNull(templateEntity.Criteria);
            Assert.Equal(templateDto.Criterias.Count(), templateEntity.Criteria.Count());
        }
    }
    
    [Fact]
    public void EvaluationEntity_To_EvaluationDto()
    {
        // Arrange
        var evaluationEntity = new EvaluationEntity
        {
            Id = 1,
            StudentId = 1,
            TemplateId = 1,
            TeacherId = 1,
            CourseName = "Course 1",
            Date = new DateTime(2024, 3, 16),
            Grade = 0,
            PairName = ""
        };

        // Act
        var evaluationDto = Entities2Dto.Translator.ToDto(evaluationEntity);

        // Assert
        Assert.NotNull(evaluationDto);
        Assert.Equal(evaluationEntity.Id, evaluationDto.Id);
        Assert.Equal(evaluationEntity.StudentId, evaluationDto.StudentId);
        Assert.Equal(evaluationEntity.TemplateId, evaluationDto.TemplateId);
        Assert.Equal(evaluationEntity.TeacherId, evaluationDto.TeacherId);
        Assert.Equal(evaluationEntity.CourseName, evaluationDto.CourseName);
        Assert.Equal(evaluationEntity.Date, evaluationDto.Date);
        Assert.Equal(evaluationEntity.Grade, evaluationDto.Grade);
        Assert.Equal(evaluationEntity.PairName, evaluationDto.PairName);
    }
    
    [Fact]
    public void EvaluationDto_To_EvaluationEntity()
    {
        // Arrange
        var evaluationDto = new EvaluationDto
        {
            Id = 1,
            StudentId = 1,
            TemplateId = 1,
            TeacherId = 1,
            CourseName = "Course 1",
            Date = new DateTime(2024, 3, 16),
            Grade = 0,
            PairName = ""
        };

        // Act
        var evaluationEntity = Entities2Dto.Translator.ToEntity(evaluationDto);
        
        // Assert
        Assert.NotNull(evaluationEntity);
        Assert.Equal(evaluationDto.Id, evaluationEntity.Id);
        Assert.Equal(evaluationDto.StudentId, evaluationEntity.StudentId);
        Assert.Equal(evaluationDto.TemplateId, evaluationEntity.TemplateId);
        Assert.Equal(evaluationDto.TeacherId, evaluationEntity.TeacherId);
        Assert.Equal(evaluationDto.CourseName, evaluationEntity.CourseName);
        Assert.Equal(evaluationDto.Date, evaluationEntity.Date);
        Assert.Equal(evaluationDto.Grade, evaluationEntity.Grade);
        Assert.Equal(evaluationDto.PairName, evaluationEntity.PairName);
    }
    
    
}
