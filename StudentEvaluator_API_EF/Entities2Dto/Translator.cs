using API_Dto;
using EF_Entities;

namespace Entities2Dto
{
    public static class Translator
    {

        public static Mapper<StudentEntity,StudentDto> StudentMapper { get; set; } = new Mapper<StudentEntity, StudentDto>();
        public static Mapper<GroupEntity, GroupDto> GroupMapper { get; set; } = new Mapper<GroupEntity, GroupDto>();
        
        public static Mapper<CriteriaEntity, CriteriaDto> CriteriaMapper { get; set; } = new Mapper<CriteriaEntity, CriteriaDto>();
        public static Mapper<TextCriteriaEntity, TextCriteriaDto> TextCriteriaMapper { get; set; } = new Mapper<TextCriteriaEntity, TextCriteriaDto>();
        public static Mapper<TemplateEntity, TemplateDto> TemplateMapper { get; set; } = new Mapper<TemplateEntity, TemplateDto>();

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
                    Group = student.Group.ToDto(),
                   
                };
                StudentMapper.Set(student, studentDto);               
                return studentDto;
            }
            else return studentDto;
        }
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
            else return groupDto;
         
        }
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
                    Group = student.Group.ToEntity(),
                };
                StudentMapper.Set(studentEntity, student);
                return studentEntity;
            }
            else return studentEntity;
         
        }

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
            else return groupEntity;
        }

        public static IEnumerable<StudentDto> ToDtos(this IEnumerable<StudentEntity> entities)
        {
            IEnumerable<StudentDto> students = new List<StudentDto>();
            foreach (var entity in entities)
            {
                (students as List<StudentDto>).Add(entity.ToDto());
            }
            return students;
        }

        public static IEnumerable<GroupDto> ToDtos(this IEnumerable<GroupEntity> entities)
        {
            IEnumerable<GroupDto> groups = new List<GroupDto>();
            foreach (var entity in entities)
            {
                (groups as List<GroupDto>).Add(entity.ToDto());
            }
            return groups;
        }


        public static IEnumerable<StudentEntity> ToEntities(this IEnumerable<StudentDto> dtos)
        {
            IEnumerable<StudentEntity> students = new List<StudentEntity>();
            foreach (var dto in dtos)
            {
                (students as List<StudentEntity>).Add(dto.ToEntity());
            }
            return students;
        }
        
        
        public static IEnumerable<GroupEntity> ToEntities(this IEnumerable<GroupDto> dtos)
        {
            IEnumerable<GroupEntity> groups = new List<GroupEntity>();
            foreach (var dto in dtos)
            {
                (groups as List<GroupEntity>).Add(dto.ToEntity());
            }
            return groups;
        }
        
        public static CriteriaDto ToDto(this CriteriaEntity criteria)
        {
            var criteriaDto = CriteriaMapper.GetDto(criteria);
            if (criteriaDto == null)
            {
                criteriaDto = new CriteriaDto
                {
                    Id = criteria.Id,
                    Name = criteria.Name,
                    ValueEvaluation = criteria.ValueEvaluation,
                    TemplateId = criteria.TemplateId
                };
                CriteriaMapper.Set(criteria, criteriaDto);
                return criteriaDto;
            }
            else return criteriaDto;
        }
        
        
        public static CriteriaEntity ToEntity(this CriteriaDto criteria)
        {
            var criteriaEntity = CriteriaMapper.GetEntity(criteria);
            if (criteriaEntity == null)
            {
                return new CriteriaEntity
                {
                    Id = criteria.Id,
                    Name = criteria.Name,
                    ValueEvaluation = criteria.ValueEvaluation,
                    TemplateId = criteria.TemplateId,
                };
            }
            else return criteriaEntity;
        }
        
        public static IEnumerable<CriteriaDto> ToDtos(this IEnumerable<CriteriaEntity> entities)
        {
            IEnumerable<CriteriaDto> criterias = new List<CriteriaDto>();
            foreach (var entity in entities)
            {
                (criterias as List<CriteriaDto>).Add(entity.ToDto());
            }
            return criterias;
        }
        
        public static IEnumerable<CriteriaEntity> ToEntities(this IEnumerable<CriteriaDto> dtos)
        {
            IEnumerable<CriteriaEntity> criterias = new List<CriteriaEntity>();
            foreach (var dto in dtos)
            {
                (criterias as List<CriteriaEntity>).Add(dto.ToEntity());
            }
            return criterias;
        }
        
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
            else return textCriteriaDto;
        }
        
        
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
            else return textCriteriaEntity;
        }
        
        public static IEnumerable<TextCriteriaDto> ToDtos(this IEnumerable<TextCriteriaEntity> entities)
        {
            IEnumerable<TextCriteriaDto> textCriterias = new List<TextCriteriaDto>();
            foreach (var entity in entities)
            {
                (textCriterias as List<TextCriteriaDto>).Add(entity.ToDto());
            }
            return textCriterias;
        }
        
        public static IEnumerable<TextCriteriaEntity> ToEntities(this IEnumerable<TextCriteriaDto> dtos)
        {
            IEnumerable<TextCriteriaEntity> textCriterias = new List<TextCriteriaEntity>();
            foreach (var dto in dtos)
            {
                (textCriterias as List<TextCriteriaEntity>).Add(dto.ToEntity());
            }
            return textCriterias;
        }
        
        public static TemplateDto ToDto(this TemplateEntity template)
        {
            var templateDto = TemplateMapper.GetDto(template);
            if (templateDto == null)
            {
                templateDto = new TemplateDto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Criteria = template.Criteria.ToDtos(),
                    Teacher = template.Teacher.ToDto(),
                };
                TemplateMapper.Set(template, templateDto);
                return templateDto;
            }
            else return templateDto;
        }
        
        
        public static TemplateEntity ToEntity(this TemplateDto template)
        {
            var templateEntity = TemplateMapper.GetEntity(template);
            if (templateEntity == null)
            {
                return new TemplateEntity
                {
                    Id = template.Id,
                    Name = template.Name,
                    Criteria = template.Criteria.ToEntities(),
                };
            }
            else return templateEntity;
        }
        
        public static IEnumerable<TemplateDto> ToDtos(this IEnumerable<TemplateEntity> entities)
        {
            IEnumerable<TemplateDto> templates = new List<TemplateDto>();
            foreach (var entity in entities)
            {
                (templates as List<TemplateDto>).Add(entity.ToDto());
            }
            return templates;
        }
        
        public static IEnumerable<TemplateEntity> ToEntities(this IEnumerable<TemplateDto> dtos)
        {
            IEnumerable<TemplateEntity> templates = new List<TemplateEntity>();
            foreach (var dto in dtos)
            {
                (templates as List<TemplateEntity>).Add(dto.ToEntity());
            }
            return templates;
        }
        
        
        public static TeacherDto ToDto(this TeacherEntity teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                Username = teacher.Username,
                Password = teacher.Password,
                Templates = teacher.Templates.ToDtos(),
                roles = teacher.roles,
            };
        }
        
        public static TeacherEntity ToEntity(this TeacherDto teacher)
        {
            return new TeacherEntity
            {
                Id = teacher.Id,
                Username = teacher.Username,
                Password = teacher.Password,
                Templates = teacher.Templates.ToEntities(),
                roles = teacher.roles,
            };
        }
        
        public static IEnumerable<TeacherDto> ToDtos(this IEnumerable<TeacherEntity> entities)
        {
            IEnumerable<TeacherDto> teachers = new List<TeacherDto>();
            foreach (var entity in entities)
            {
                (teachers as List<TeacherDto>).Add(entity.ToDto());
            }
            return teachers;
        }
        
        
        public static IEnumerable<TeacherEntity> ToEntities(this IEnumerable<TeacherDto> dtos)
        {
            IEnumerable<TeacherEntity> teachers = new List<TeacherEntity>();
            foreach (var dto in dtos)
            {
                (teachers as List<TeacherEntity>).Add(dto.ToEntity());
            }
            return teachers;
        }

        public static LessonDto ToDto(this LessonEntity lesson)
        {
            return new LessonDto
            {
               Id=lesson.Id,
               Classroom=lesson.Classroom,
               CourseName=lesson.CourseName,
               Date=lesson.Date,
               Start=lesson.Start,
               End=lesson.End,
               Teacher=lesson.Teacher?.ToDto(),
               
            };
        }

        public static LessonEntity ToEntity(this LessonDto lessonDto)
        {
            return new LessonEntity
            {
                Id = lessonDto.Id,
                Classroom = lessonDto.Classroom,
                CourseName = lessonDto.CourseName,
                Date = lessonDto.Date,
                Start = lessonDto.Start,
                End = lessonDto.End,
                Teacher = lessonDto.Teacher.ToEntity(),
            };
        }

        public static IEnumerable<LessonDto> ToDtos(this IEnumerable<LessonEntity> entities)
        {
            IEnumerable<LessonDto> lessons = new List<LessonDto>();
            foreach (var entity in entities)
            {
                (lessons as List<LessonDto>).Add(entity.ToDto());
            }
            return lessons;
        }

    }
}
