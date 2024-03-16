using API_Dto;
using EF_Entities;

namespace Entities2Dto
{

    /// <summary>
    /// Static class providing translation methods to convert between entity and DTO objects.
    /// </summary>
    public static class Translator
    {
        public static Mapper<StudentEntity,StudentDto> StudentMapper { get; set; } = new Mapper<StudentEntity, StudentDto>();
        public static Mapper<GroupEntity, GroupDto> GroupMapper { get; set; } = new Mapper<GroupEntity, GroupDto>();   
        public static Mapper<CriteriaEntity, CriteriaDto> CriteriaMapper { get; set; } = new Mapper<CriteriaEntity, CriteriaDto>();
        public static Mapper<TextCriteriaEntity, TextCriteriaDto> TextCriteriaMapper { get; set; } = new Mapper<TextCriteriaEntity, TextCriteriaDto>();
        public static Mapper<SliderCriteriaEntity, SliderCriteriaDto> SliderCriteriaMapper { get; set; } = new Mapper<SliderCriteriaEntity, SliderCriteriaDto>();
        public static Mapper<TemplateEntity, TemplateDto> TemplateMapper { get; set; } = new Mapper<TemplateEntity, TemplateDto>();
 
        public static Mapper<TeacherEntity, TeacherDto> TeacherMapper { get; set; } = new Mapper<TeacherEntity, TeacherDto>();
        public static Mapper<UserEntity, UserDto> UserMapper { get; set; } = new Mapper<UserEntity, UserDto>();
        public static Mapper<LessonEntity,LessonDto> LessonMapper { get; set; } = new Mapper<LessonEntity, LessonDto>();
        public static Mapper<LessonEntity, LessonReponseDto> LessonReponseMapper { get; set; } = new Mapper<LessonEntity, LessonReponseDto>();
        public static Mapper<EvaluationEntity, EvaluationDto> EvaluationMapper { get; set; } = new Mapper<EvaluationEntity, EvaluationDto>();
        public static Mapper<EvaluationEntity, EvaluationReponseDto> EvaluationReponseMapper { get; set; } = new Mapper<EvaluationEntity, EvaluationReponseDto>();

        //Student

        /// <summary>
        /// Extension method to convert StudentEntity to StudentDto.
        /// </summary>
        /// <param name="student">The student entity object to be converted.</param>
        /// <returns>The corresponding student DTO object.</returns>
        public static StudentDto ToDto (this StudentEntity student)
        {
            var studentDto = StudentMapper.GetDto(student);
            if ( studentDto == null)
            {
                if (student.Group == null)
                {
                    student.Group = new GroupEntity { GroupNumber = student.GroupNumber, GroupYear = student.GroupYear };

                }
                studentDto = new StudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Lastname = student.Lastname,
                    UrlPhoto = student.UrlPhoto,
                    GroupNumber = student.GroupNumber,
                    GroupYear = student.GroupYear,
                   
                };
                StudentMapper.Set(student, studentDto);               
                return studentDto;
            }

            return studentDto;
        }


        /// <summary>
        /// Extension method to convert StudentDto to StudentEntity.
        /// </summary>
        /// <param name="student">The student DTO object to be converted.</param>
        /// <returns>The corresponding student entity object.</returns>
        public static StudentEntity ToEntity(this StudentDto student)
        {
            var studentEntity = StudentMapper.GetEntity(student);
            if (studentEntity == null)
            {
                studentEntity= new StudentEntity
                {
                    Id = student.Id,
                    Name = student.Name,
                    Lastname = student.Lastname,
                    UrlPhoto = student.UrlPhoto,
                    GroupNumber = student.GroupNumber,
                    GroupYear = student.GroupYear
                };
                StudentMapper.Set(studentEntity, student);
                return studentEntity;
            }
            return studentEntity;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of StudentEntity to IEnumerable of StudentDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of student entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of student DTOs.</returns>
        public static IEnumerable<StudentDto> ToDtos(this IEnumerable<StudentEntity> entities)
        {
            IEnumerable<StudentDto> students = new List<StudentDto>();
            foreach (var entity in entities)
            {
                (students as List<StudentDto>).Add(entity.ToDto());
            }
            return students;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of StudentDto to IEnumerable of StudentEntity.
        /// </summary>
        /// <param name="dtos">The IEnumerable of student DTOs to be converted.</param>
        /// <returns>The corresponding IEnumerable of student entities.</returns>
        public static IEnumerable<StudentEntity> ToEntities(this IEnumerable<StudentDto> dtos)
        {
            IEnumerable<StudentEntity> students = new List<StudentEntity>();
            foreach (var dto in dtos)
            {
                (students as List<StudentEntity>).Add(dto.ToEntity());
            }
            return students;
        }

        //Group

        /// <summary>
        /// Extension method to convert GroupEntity to GroupDto.
        /// </summary>
        /// <param name="group">The group entity object to be converted.</param>
        /// <returns>The corresponding group DTO object.</returns>
        public static GroupDto ToDto(this GroupEntity group)
        {
            var groupDto = GroupMapper.GetDto(group);
            if (groupDto == null)
            {
                groupDto = new GroupDto
                {
                    GroupNumber = group.GroupNumber,
                    GroupYear = group.GroupYear,
                    Students = group.Students?.ToDtos(),
                };
                GroupMapper.Set(group, groupDto);
                return groupDto;
            }

            return groupDto;

        }

        /// <summary>
        /// Extension method to convert GroupDto to GroupEntity.
        /// </summary>
        /// <param name="group">The group DTO object to be converted.</param>
        /// <returns>The corresponding group entity object.</returns>
        public static GroupEntity ToEntity(this GroupDto group)
        {
            var groupEntity = GroupMapper.GetEntity(group);
            if (groupEntity == null)
            {
                return new GroupEntity
                {
                    GroupNumber = group.GroupNumber,
                    GroupYear = group.GroupYear,
                    Students = group.Students.ToEntities(),
                };
            }

            return groupEntity;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of GroupEntity to IEnumerable of GroupDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of group entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of group DTOs.</returns>
        public static IEnumerable<GroupDto> ToDtos(this IEnumerable<GroupEntity> entities)
        {
            IEnumerable<GroupDto> groups = new List<GroupDto>();
            foreach (var entity in entities)
            {
                (groups as List<GroupDto>).Add(entity.ToDto());
            }
            return groups;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of GroupDto to IEnumerable of GroupEntity.
        /// </summary>
        /// <param name="dtos">The IEnumerable of group DTOs to be converted.</param>
        /// <returns>The corresponding IEnumerable of group entities.</returns>
        public static IEnumerable<GroupEntity> ToEntities(this IEnumerable<GroupDto> dtos)
        {
            IEnumerable<GroupEntity> groups = new List<GroupEntity>();
            foreach (var dto in dtos)
            {
                (groups as List<GroupEntity>).Add(dto.ToEntity());
            }
            return groups;
        }


        // TextCriteria


        /// <summary>
        /// Extension method to convert TextCriteriaEntity to TextCriteriaDto.
        /// </summary>
        /// <param name="textCriteria">The text criteria entity object to be converted.</param>
        /// <returns>The corresponding text criteria DTO object.</returns>
        public static TextCriteriaDto ToDto(this TextCriteriaEntity textCriteria)
        {
            var textCriteriaDto = TextCriteriaMapper.GetDto(textCriteria);
            if (textCriteriaDto == null)
            {
                textCriteriaDto = new TextCriteriaDto
                {
                    Id = textCriteria.Id,
                    Name = textCriteria.Name,
                    ValueEvaluation = textCriteria.ValueEvaluation,
                    TemplateId = textCriteria.TemplateId,
                    Text = textCriteria.Text,
                };
                TextCriteriaMapper.Set(textCriteria, textCriteriaDto);
                return textCriteriaDto;
            }

            return textCriteriaDto;
        }


        /// <summary>
        /// Extension method to convert TextCriteriaDto to TextCriteriaEntity.
        /// </summary>
        /// <param name="textCriteria">The text criteria DTO object to be converted.</param>
        /// <returns>The corresponding text criteria entity object.</returns>
        public static TextCriteriaEntity ToEntity(this TextCriteriaDto textCriteria)
        {
            var textCriteriaEntity = TextCriteriaMapper.GetEntity(textCriteria);
            if (textCriteriaEntity == null)
            {
                return new TextCriteriaEntity
                {
                    Id = textCriteria.Id,
                    Name = textCriteria.Name,
                    ValueEvaluation = textCriteria.ValueEvaluation, 
                    TemplateId = textCriteria.TemplateId,
                    Text = textCriteria.Text,
                };
            }

            return textCriteriaEntity;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of TextCriteriaEntity to IEnumerable of TextCriteriaDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of text criteria entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of text criteria DTOs.</returns>
        public static IEnumerable<TextCriteriaDto> ToDtos(this IEnumerable<TextCriteriaEntity> entities)
        {
            IEnumerable<TextCriteriaDto> textCriterias = new List<TextCriteriaDto>();
            foreach (var entity in entities)
            {
                (textCriterias as List<TextCriteriaDto>).Add(entity.ToDto());
            }
            return textCriterias;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of TextCriteriaDto to IEnumerable of TextCriteriaEntity.
        /// </summary>
        /// <param name="dtos">The IEnumerable of text criteria DTOs to be converted.</param>
        /// <returns>The corresponding IEnumerable of text criteria entities.</returns>
        public static IEnumerable<TextCriteriaEntity> ToEntities(this IEnumerable<TextCriteriaDto> dtos)
        {
            IEnumerable<TextCriteriaEntity> textCriterias = new List<TextCriteriaEntity>();
            foreach (var dto in dtos)
            {
                (textCriterias as List<TextCriteriaEntity>).Add(dto.ToEntity());
            }
            return textCriterias;
        }


        // SliderCriteria


        /// <summary>
        /// Extension method to convert SliderCriteriaEntity to SliderCriteriaDto.
        /// </summary>
        /// <param name="sliderCriteria">The slider criteria entity object to be converted.</param>
        /// <returns>The corresponding slider criteria DTO object.</returns>
        public static SliderCriteriaDto ToDto(this SliderCriteriaEntity sliderCriteria)
        {
           var sliderCriteriaDto = SliderCriteriaMapper.GetDto(sliderCriteria);
            if (sliderCriteriaDto == null)
            {
             sliderCriteriaDto = new SliderCriteriaDto
             {
                  Id = sliderCriteria.Id,
                  Name = sliderCriteria.Name,
                  ValueEvaluation = sliderCriteria.ValueEvaluation,
                  TemplateId = sliderCriteria.TemplateId,
                  Value = sliderCriteria.Value
             };
             SliderCriteriaMapper.Set(sliderCriteria, sliderCriteriaDto);
             return sliderCriteriaDto;
            }

            return sliderCriteriaDto;
        }


        /// <summary>
        /// Extension method to convert SliderCriteriaDto to SliderCriteriaEntity.
        /// </summary>
        /// <param name="sliderCriteria">The slider criteria DTO object to be converted.</param>
        /// <returns>The corresponding slider criteria entity object.</returns>
        public static SliderCriteriaEntity ToEntity(this SliderCriteriaDto sliderCriteria)
        {
            var sliderCriteriaEntity = SliderCriteriaMapper.GetEntity(sliderCriteria);
            if (sliderCriteriaEntity == null)
            {
                return new SliderCriteriaEntity
                {
                    Id = sliderCriteria.Id,
                    Name = sliderCriteria.Name,
                    ValueEvaluation = sliderCriteria.ValueEvaluation,
                    TemplateId = sliderCriteria.TemplateId,
                    Value = sliderCriteria.Value
                };
            }

            return sliderCriteriaEntity;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of SliderCriteriaEntity to IEnumerable of SliderCriteriaDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of slider criteria entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of slider criteria DTOs.</returns>
        public static IEnumerable<SliderCriteriaDto> ToDtos(this IEnumerable<SliderCriteriaEntity> entities)
        {
            IEnumerable<SliderCriteriaDto> sliderCriterias = new List<SliderCriteriaDto>();
            foreach (var entity in entities)
            {
                (sliderCriterias as List<SliderCriteriaDto>).Add(entity.ToDto());
            }
            return sliderCriterias;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of SliderCriteriaDto to IEnumerable of SliderCriteriaEntity.
        /// </summary>
        /// <param name="dtos">The IEnumerable of slider criteria DTOs to be converted.</param>
        /// <returns>The corresponding IEnumerable of slider criteria entities.</returns>
        public static IEnumerable<SliderCriteriaEntity> ToEntities(this IEnumerable<SliderCriteriaDto> dtos)
        {
            IEnumerable<SliderCriteriaEntity> sliderCriterias = new List<SliderCriteriaEntity>();
            foreach (var dto in dtos)
            {
                (sliderCriterias as List<SliderCriteriaEntity>).Add(dto.ToEntity());
            }
            return sliderCriterias;
        }

        // RadioCriteria


        /// <summary>
        /// Extension method to convert RadioCriteriaEntity to RadioCriteriaDto.
        /// </summary>
        /// <param name="radioCriteria">The radio criteria entity object to be converted.</param>
        /// <returns>The corresponding radio criteria DTO object.</returns>
        public static RadioCriteriaDto ToDto(this RadioCriteriaEntity radioCriteria)
        {
            return new RadioCriteriaDto
            {
                Id = radioCriteria.Id,
                Name = radioCriteria.Name,
                ValueEvaluation = radioCriteria.ValueEvaluation,
                TemplateId = radioCriteria.TemplateId,
                Options = radioCriteria.Options,
                SelectedOption = radioCriteria.SelectedOption
            };
        }


        /// <summary>
        /// Extension method to convert RadioCriteriaDto to RadioCriteriaEntity.
        /// </summary>
        /// <param name="radioCriteria">The radio criteria DTO object to be converted.</param>
        /// <returns>The corresponding radio criteria entity object.</returns>
        public static RadioCriteriaEntity ToEntity(this RadioCriteriaDto radioCriteria)
        {
            return new RadioCriteriaEntity
            {
                Id = radioCriteria.Id,
                Name = radioCriteria.Name,
                ValueEvaluation = radioCriteria.ValueEvaluation,
                TemplateId = radioCriteria.TemplateId,
                Options = radioCriteria.Options,
                SelectedOption = radioCriteria.SelectedOption
            };
        }


        /// <summary>
        /// Extension method to convert IEnumerable of RadioCriteriaEntity to IEnumerable of RadioCriteriaDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of radio criteria entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of radio criteria DTOs.</returns>
        public static IEnumerable<RadioCriteriaDto> ToDtos(this IEnumerable<RadioCriteriaEntity> entities)
        {
            IEnumerable<RadioCriteriaDto> radioCriterias = new List<RadioCriteriaDto>();
            foreach (var entity in entities)
            {
                (radioCriterias as List<RadioCriteriaDto>).Add(entity.ToDto());
            }
            return radioCriterias;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of RadioCriteriaDto to IEnumerable of RadioCriteriaEntity.
        /// </summary>
        /// <param name="dtos">The IEnumerable of radio criteria DTOs to be converted.</param>
        /// <returns>The corresponding IEnumerable of radio criteria entities.</returns>
        public static IEnumerable<RadioCriteriaEntity> ToEntities(this IEnumerable<RadioCriteriaDto> dtos)
        {
            IEnumerable<RadioCriteriaEntity> radioCriterias = new List<RadioCriteriaEntity>();
            foreach (var dto in dtos)
            {
                (radioCriterias as List<RadioCriteriaEntity>).Add(dto.ToEntity());
            }
            return radioCriterias;
        }

        // Template


        /// <summary>
        /// Extension method to convert TemplateDto to TemplateEntity.
        /// </summary>
        /// <param name="template">The template DTO object to be converted.</param>
        /// <returns>The corresponding template entity object.</returns>
        public static TemplateEntity ToEntity(this TemplateDto template)
        {
            var templateEntity = TemplateMapper.GetEntity(template);
            if (templateEntity == null)
            {
                return new TemplateEntity
                {
                    Id = template.Id,
                    Name = template.Name,
                    Criteria = template.Criterias?.Select(CriteriaDtoConverter.ConvertToEntity).ToList()
                };
            }
            TemplateMapper.Set(templateEntity, template);
            return templateEntity;
        }



        /// <summary>
        /// Extension method to convert IEnumerable of TemplateDto to IEnumerable of TemplateEntity.
        /// </summary>
        /// <param name="dtos">The IEnumerable of template DTOs to be converted.</param>
        /// <returns>The corresponding IEnumerable of template entities.</returns>
        public static IEnumerable<TemplateEntity> ToEntities(this IEnumerable<TemplateDto> dtos)
        {
            IEnumerable<TemplateEntity> templates = new List<TemplateEntity>();
            foreach (var dto in dtos)
            {
                (templates as List<TemplateEntity>).Add(dto.ToEntity());
            }
            return templates;
        }
        
        public static TemplateDto ToDto(this TemplateEntity template)
        {
            var templateDto = new TemplateDto
            {
                Id = template.Id,
                Name = template.Name,
                Criterias = template.Criteria?.Select(CriteriaDtoConverter.ConvertToDto).ToList()
            };
            return templateDto;
        }
        
        public static  IEnumerable<TemplateDto> ToDtos(this IEnumerable<TemplateEntity> entities)
        {
            IEnumerable<TemplateDto> templates = new List<TemplateDto>();
            foreach (var entity in entities)
            {
                (templates as List<TemplateDto>).Add(entity.ToDto());
            }
            return templates;
        }
        
        //Teacher

        /// <summary>
        /// Extension method to convert TeacherEntity to TeacherDto.
        /// </summary>
        /// <param name="teacher">The teacher entity object to be converted.</param>
        /// <returns>The corresponding teacher DTO object.</returns>
        public static TeacherDto ToDto(this TeacherEntity teacher)
        {
            var teacherDto = TeacherMapper.GetDto(teacher);
            if(teacherDto == null)
            {
                teacherDto = new TeacherDto
                {
                    Id = teacher.Id,
                    Username = teacher.Username,
                    Password = teacher.Password,
                    Templates = teacher.Templates?.ToDtos(),
                    roles = teacher.Roles,
                };
                TeacherMapper.Set(teacher, teacherDto);
            }
            return teacherDto;
        }


        /// <summary>
        /// Extension method to convert TeacherDto to TeacherEntity.
        /// </summary>
        /// <param name="teacher">The teacher DTO object to be converted.</param>
        /// <returns>The corresponding teacher entity object.</returns>
        public static TeacherEntity ToEntity(this TeacherDto teacher)
        {
            var teacherEntity = TeacherMapper.GetEntity(teacher);
            if(teacherEntity == null)
            {
                teacherEntity = new TeacherEntity
                {
                    Id = teacher.Id,
                    Username = teacher.Username,
                    Password = teacher.Password,
                    Templates = teacher.Templates?.ToEntities(),
                    Roles = teacher.roles,
                };
                TeacherMapper.Set(teacherEntity, teacher);
            }
            return teacherEntity; 
        }


        /// <summary>
        /// Extension method to convert IEnumerable of TeacherEntity to IEnumerable of TeacherDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of teacher entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of teacher DTOs.</returns>
        public static IEnumerable<TeacherDto> ToDtos(this IEnumerable<TeacherEntity> entities)
        {
            IEnumerable<TeacherDto> teachers = new List<TeacherDto>();
            foreach (var entity in entities)
            {
                (teachers as List<TeacherDto>).Add(entity.ToDto());
            }
            return teachers;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of TeacherDto to IEnumerable of TeacherEntity.
        /// </summary>
        /// <param name="dtos">The IEnumerable of teacher DTOs to be converted.</param>
        /// <returns>The corresponding IEnumerable of teacher entities.</returns>
        public static IEnumerable<TeacherEntity> ToEntities(this IEnumerable<TeacherDto> dtos)
        {
            IEnumerable<TeacherEntity> teachers = new List<TeacherEntity>();
            foreach (var dto in dtos)
            {
                (teachers as List<TeacherEntity>).Add(dto.ToEntity());
            }
            return teachers;
        }


        /// <summary>
        /// Extension method to convert LessonEntity to LessonDto.
        /// </summary>
        /// <param name="lesson">The lesson entity object to be converted.</param>
        /// <returns>The corresponding lesson DTO object.</returns>
        public static LessonDto ToDto(this LessonEntity lesson)
        {
            var lessonDto = LessonMapper.GetDto(lesson);
            if (lessonDto == null)
            {
                lessonDto=  new LessonDto
                {
                    Classroom = lesson.Classroom,
                    CourseName = lesson.CourseName,
                    Start = lesson.Start,
                    End = lesson.End,
                    TeacherId = lesson.TeacherEntityId,
                    GroupNumber = lesson.GroupNumber,
                    GroupYear = lesson.GroupYear,

                };
                LessonMapper.Set(lesson, lessonDto);
            
            }
            return lessonDto;
        }

        /// <summary>
        /// Extension method to convert LessonEntity to LessonReponseDto.
        /// </summary>
        /// <param name="lesson">The lesson entity object to be converted.</param>
        /// <returns>The corresponding lesson response DTO object.</returns>
        public static LessonReponseDto ToReponseDto(this LessonEntity lesson)
        {
            var lessonDto = LessonReponseMapper.GetDto(lesson);
            if (lessonDto == null)
            {
                lessonDto = new LessonReponseDto
                {
                    Id = lesson.Id,
                    Classroom = lesson.Classroom,
                    CourseName = lesson.CourseName,
                    Start = lesson.Start,
                    End = lesson.End,
                    Teacher = lesson.Teacher.ToDto(),
                    Group = lesson.Group.ToDto(),

                };
                LessonReponseMapper.Set(lesson, lessonDto);

            }
            return lessonDto;
        }


        /// <summary>
        /// Extension method to convert LessonDto to LessonEntity.
        /// </summary>
        /// <param name="lessonDto">The lesson DTO object to be converted.</param>
        /// <returns>The corresponding lesson entity object.</returns>
        public static LessonEntity ToEntity(this LessonDto lessonDto)
        {
            var lessonEntity = LessonMapper.GetEntity(lessonDto);
            if (lessonEntity == null)
            {
                lessonEntity = new LessonEntity
                {
                    Classroom = lessonDto.Classroom,
                    CourseName = lessonDto.CourseName,
                    Start = lessonDto.Start,
                    End = lessonDto.End,
                    TeacherEntityId = lessonDto.TeacherId,
                    GroupNumber = lessonDto.GroupNumber,
                    GroupYear= lessonDto.GroupYear,
                };
                LessonMapper.Set(lessonEntity, lessonDto);
            }
            return lessonEntity;
           
        }

        /// <summary>
        /// Extension method to convert IEnumerable of LessonEntity to IEnumerable of LessonDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of lesson entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of lesson DTOs.</returns>
        public static IEnumerable<LessonDto> ToDtos(this IEnumerable<LessonEntity> entities)
        {
            IEnumerable<LessonDto> lessons = new List<LessonDto>();
            foreach (var entity in entities)
            {
                (lessons as List<LessonDto>).Add(entity.ToDto());
            }
            return lessons;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of LessonEntity to IEnumerable of LessonReponseDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of lesson entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of lesson response DTOs.</returns>
        public static IEnumerable<LessonReponseDto> ToReponseDtos(this IEnumerable<LessonEntity> entities)
        {
            IEnumerable<LessonReponseDto> lessons = new List<LessonReponseDto>();
            foreach (var entity in entities)
            {
                (lessons as List<LessonReponseDto>).Add(entity.ToReponseDto());
            }
            return lessons;
        }

        /// <summary>
        /// Extension method to convert UserEntity to UserDto.
        /// </summary>
        /// <param name="user">The user entity object to be converted.</param>
        /// <returns>The corresponding user DTO object.</returns>
        public static UserDto ToDto(this UserEntity user)
        {
           var userDto = UserMapper.GetDto(user);
            if (userDto == null)
            {
                userDto= new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    roles = user.Roles,
                };
                UserMapper.Set(user, userDto);      
            }
            return userDto; 
            
        }


        /// <summary>
        /// Extension method to convert UserDto to UserEntity.
        /// </summary>
        /// <param name="user">The user DTO object to be converted.</param>
        /// <returns>The corresponding user entity object.</returns>
        public static UserEntity ToEntity(this UserDto user)
        {
            var userEntity = UserMapper.GetEntity(user);
            if(userEntity == null)
            {
                userEntity= new UserEntity
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Roles = user.roles,
                };
                UserMapper.Set(userEntity, user);
            }
            return userEntity;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of UserEntity to IEnumerable of UserDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of user entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of user DTOs.</returns>
        public static IEnumerable<UserDto> ToDtos(this IEnumerable<UserEntity> entities)
        {
            IEnumerable<UserDto> users = new List<UserDto>();
            foreach (var entity in entities)
            {
                (users as List<UserDto>).Add(entity.ToDto());
            }
            return users;
        }


        /// <summary>
        /// Extension method to convert EvaluationEntity to EvaluationDto.
        /// </summary>
        /// <param name="eval">The evaluation entity object to be converted.</param>
        /// <returns>The corresponding evaluation DTO object.</returns>
        public static EvaluationDto ToDto(this EvaluationEntity eval)
        {
            var evalDto = EvaluationMapper.GetDto(eval);
            if(evalDto == null)
            {
                evalDto = new EvaluationDto
                {
                    Id = eval.Id,
                    CourseName = eval.CourseName,
                    Grade = eval.Grade,
                    PairName = eval.PairName,
                    Date = eval.Date,

                    StudentId = eval.StudentId,
                    TeacherId = eval.TeacherId,
                    TemplateId = eval.TemplateId,

                };
                EvaluationMapper.Set(eval,evalDto);
            }
            return evalDto;
        }

        /// <summary>
        /// Extension method to convert EvaluationEntity to EvaluationReponseDto.
        /// </summary>
        /// <param name="eval">The evaluation entity object to be converted.</param>
        /// <returns>The corresponding evaluation response DTO object.</returns>
        public static EvaluationReponseDto ToReponseDto(this EvaluationEntity eval)
        {
            var evalDto = EvaluationReponseMapper.GetDto(eval);
            if (evalDto == null)
            {
                evalDto = new EvaluationReponseDto
                {
                    Id = eval.Id,
                    CourseName = eval.CourseName,
                    Grade = eval.Grade,
                    PairName = eval.PairName,
                    Date = eval.Date,

                    Student = eval.Student?.ToDto(),
                    Template = eval.Template?.ToDto(),
                    Teacher = eval.Teacher?.ToDto(),

                };
                EvaluationReponseMapper.Set(eval, evalDto);
            }
            return evalDto;
        }


        /// <summary>
        /// Extension method to convert EvaluationDto to EvaluationEntity.
        /// </summary>
        /// <param name="evalDto">The evaluation DTO object to be converted.</param>
        /// <returns>The corresponding evaluation entity object.</returns>
        public static EvaluationEntity ToEntity(this EvaluationDto evalDto)
        {
            var evalEntity = EvaluationMapper.GetEntity(evalDto);
            if(evalEntity == null)
            {
                evalEntity = new EvaluationEntity
                {
                    Id = evalDto.Id,
                    CourseName = evalDto.CourseName,
                    Grade = evalDto.Grade,
                    PairName = evalDto.PairName,
                    Date = evalDto.Date,

                    StudentId = evalDto.StudentId,
                    TemplateId = evalDto.TemplateId,
                    TeacherId = evalDto.TeacherId,

                };
                EvaluationMapper.Set(evalEntity, evalDto);
            }
            return evalEntity;
          
        }


        /// <summary>
        /// Extension method to convert EvaluationReponseDto to EvaluationEntity.
        /// </summary>
        /// <param name="evalDto">The evaluation response DTO object to be converted.</param>
        /// <returns>The corresponding evaluation entity object.</returns>
        public static EvaluationEntity ToEntity(this EvaluationReponseDto evalDto)
        {
            var evalEntity = EvaluationReponseMapper.GetEntity(evalDto);
            if (evalEntity == null)
            {
                evalEntity = new EvaluationEntity
                {
                    Id = evalDto.Id,
                    CourseName = evalDto.CourseName,
                    Grade = evalDto.Grade,
                    PairName = evalDto.PairName,
                    Date = evalDto.Date,

                    Student = evalDto.Student?.ToEntity(),
                    Template = evalDto.Template?.ToEntity(),
                    Teacher = evalDto.Teacher?.ToEntity(),

                };
                EvaluationReponseMapper.Set(evalEntity, evalDto);
            }
            return evalEntity;

        }


        /// <summary>
        /// Extension method to convert IEnumerable of EvaluationEntity to IEnumerable of EvaluationDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of evaluation entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of evaluation DTOs.</returns>
        public static IEnumerable<EvaluationDto> ToDtos(this IEnumerable<EvaluationEntity> entities)
        {
            IEnumerable<EvaluationDto> evals = new List<EvaluationDto>();
            foreach (var entity in entities)
            {
                (evals as List<EvaluationDto>).Add(entity.ToDto());
            }
            return evals;
        }


        /// <summary>
        /// Extension method to convert IEnumerable of EvaluationEntity to IEnumerable of EvaluationReponseDto.
        /// </summary>
        /// <param name="entities">The IEnumerable of evaluation entities to be converted.</param>
        /// <returns>The corresponding IEnumerable of evaluation response DTOs.</returns>
        public static IEnumerable<EvaluationReponseDto> ToReponseDtos(this IEnumerable<EvaluationEntity> entities)
        {
            IEnumerable<EvaluationReponseDto> evals = new List<EvaluationReponseDto>();
            foreach (var entity in entities)
            {
                (evals as List<EvaluationReponseDto>).Add(entity.ToReponseDto());
            }
            return evals;
        }
    }
}
