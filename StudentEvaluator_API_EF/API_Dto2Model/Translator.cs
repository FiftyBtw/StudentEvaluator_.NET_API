using API_Dto;
using API_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto2Model
{
    /// <summary>
    /// A static class containing methods to translate between DTOs and models.
    /// </summary>
    public static class Translator
    {
        //Student

        /// <summary>
        /// Converts a StudentDto object to a Student model object.
        /// </summary>
        public static Student ToModel(this StudentDto student)
        {
            return new Student(student.Id, student.Name, student.Lastname, student.UrlPhoto, student.GroupYear, student.GroupNumber);     
        }

        /// <summary>
        /// Converts a Student model object to a StudentDto object.
        /// </summary>
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

        /// <summary>
        /// Converts a collection of StudentDto objects to a collection of Student model objects.
        /// </summary>
        public static IEnumerable<Student> ToModels(this IEnumerable<StudentDto> dtos)
        {
            IEnumerable<Student> students = new List<Student>();
            foreach (var dto in dtos)
            {
                (students as List<Student>).Add(dto.ToModel());
            }
            return students;
        }

        /// <summary>
        /// Converts a collection of Student model objects to a collection of StudentDto objects.
        /// </summary>
        public static IEnumerable<StudentDto> ToDtos(this IEnumerable<Student> dtos)
        {
            IEnumerable<StudentDto> students = new List<StudentDto>();
            foreach (var dto in dtos)
            {
                (students as List<StudentDto>).Add(dto.ToDto());
            }
            return students;
        }

        //Group

        /// <summary>
        /// Converts a GroupDto object to a Group model object.
        /// </summary>
        public static Group ToModel(this GroupDto group)
        {
            if (group.Students != null) return new Group(group.GroupYear, group.GroupNumber, group.Students.ToModels());
            else return new Group(group.GroupYear, group.GroupNumber);
        }

        /// <summary>
        /// Converts a Group model object to a GroupDto object.
        /// </summary>
        public static GroupDto ToDto(this Group group)
        {
            return new GroupDto
            {
               GroupNumber = group.GroupNumber,
               GroupYear= group.GroupYear,
               Students = group.Students.ToDtos(),
            };
        }

        /// <summary>
        /// Converts a collection of GroupDto objects to a collection of Group model objects.
        /// </summary>
        public static IEnumerable<Group> ToModels(this IEnumerable<GroupDto> dtos)
        {
            IEnumerable<Group> groups = new List<Group>();
            foreach (GroupDto dto in dtos)
            {
                (groups as List<Group>).Add(dto.ToModel());
            }
            return groups;
        }

    }
}
