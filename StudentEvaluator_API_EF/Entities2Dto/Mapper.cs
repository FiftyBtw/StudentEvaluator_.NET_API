using API_Dto;
using EF_Entities;

namespace Entities2Dto
{
    public static class Mapper
    {
        public static StudentDto ToDto (this StudentEntity student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Lastname = student.Lastname,
                UrlPhoto = student.UrlPhoto,
                Group = new GroupDto { GroupYear = student.GroupYear, GroupNumber = student.GroupNumber},
            };
        }
        public static GroupDto ToDto(this GroupEntity group)
        {
            return new GroupDto
            {
                GroupNumber = group.GroupNumber,
                GroupYear = group.GroupYear,
                Students = group.Students?.ToDtos(),
            };
        }
        public static StudentEntity ToEntity(this StudentDto student)
        {
            return new StudentEntity
            {
                Id = student.Id,
                Name = student.Name,
                Lastname = student.Lastname,
                UrlPhoto = student.UrlPhoto,
                GroupNumber = student.Group.GroupNumber,
                GroupYear = student.Group.GroupYear,
            };
        }

        public static GroupEntity ToEntity(this GroupDto group)
        {
            return new GroupEntity
            {
               GroupNumber = group.GroupNumber,
               GroupYear = group.GroupYear,
            };
        }

        public static IEnumerable<StudentDto> ToDtos(this IEnumerable<StudentEntity> entities)
        {
            IEnumerable<StudentDto> groups = new List<StudentDto>();
            foreach (var entity in entities)
            {
                (groups as List<StudentDto>).Add(entity.ToDto());
            }
            return groups;
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


    }
}
