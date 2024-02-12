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
            };
        }

        public static IEnumerable<StudentDto> ToDtos(this IEnumerable<StudentEntity> entities)
        {
            IEnumerable<StudentDto> books = new List<StudentDto>();
            foreach (var entity in entities)
            {
                (books as List<StudentDto>).Add(entity.ToDto());
            }
            return books;
        }

    }
}
