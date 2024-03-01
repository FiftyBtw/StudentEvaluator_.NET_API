using API_Dto;
using API_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto2Model
{
    public static class Translator
    {
        public static Student ToModel(this StudentDto student)
        {
            return new Student
            {
                Id = student.Id,
                Name = student.Name,
                Lastname = student.Lastname,
                UrlPhoto = student.UrlPhoto,
                GroupNumber = student.GroupNumber,
                GroupYear = student.GroupYear,

            };
            
        }

        public static StudentDto ToDto(this Student student)
        { 
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Lastname = student.Lastname,
                UrlPhoto = student.UrlPhoto,
                GroupNumber = student.GroupNumber,
                GroupYear = student.GroupYear
            };
        }

        public static IEnumerable<Student> ToModels(this IEnumerable<StudentDto> dtos)
        {
            IEnumerable<Student> students = new List<Student>();
            foreach (var dto in dtos)
            {
                (students as List<Student>).Add(dto.ToModel());
            }
            return students;
        }

    }
}
