using API_Dto;
using EF_Entities;

namespace Entities2Dto
{
    public static class Translator
    {

        public static Mapper<StudentEntity,StudentDto> StudentMapper { get; set; } = new Mapper<StudentEntity, StudentDto>();
        public static Mapper<GroupEntity, GroupDto> GroupMapper { get; set; } = new Mapper<GroupEntity, GroupDto>();

        public static StudentDto ToDto (this StudentEntity student)
        {
            var studentDto = StudentMapper.GetDto(student);
            if ( studentDto == null)
            {
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


    }
}
