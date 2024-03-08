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
        //Student
        public static Student ToModel(this StudentDto student)
        {
            return new Student(student.Id, student.Name, student.Lastname, student.UrlPhoto, student.GroupYear, student.GroupNumber);     
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
        public static Group ToModel(this GroupDto group)
        {
            if (group.Students != null) return new Group(group.GroupYear, group.GroupNumber, group.Students.ToModels());
            else return new Group(group.GroupYear, group.GroupNumber);
        }

        public static GroupDto ToDto(this Group group)
        {
            return new GroupDto
            {
               GroupNumber = group.GroupNumber,
               GroupYear= group.GroupYear,
               Students = group.Students.ToDtos(),
            };
        }

        public static IEnumerable<Group> ToModels(this IEnumerable<GroupDto> dtos)
        {
            IEnumerable<Group> groups = new List<Group>();
            foreach (GroupDto dto in dtos)
            {
                (groups as List<Group>).Add(dto.ToModel());
            }
            return groups;
        }
        //Teacher
        public static Teacher ToModel(this TeacherDto dto)
        {
            return new Teacher();
        }

        public static TeacherDto ToDto(this Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                Username = teacher.Username,
                Password = teacher.Password,
                roles = teacher.roles

            };
        }
        //Lesson
        public static Lesson ToModel(this LessonDto lesson)
        {
            return new Lesson(lesson.Id,lesson.Date,lesson.Start,lesson.End,lesson.CourseName,lesson.Classroom,lesson.Teacher.ToModel(),lesson.Group.ToModel());
        }

        public static LessonDto ToDto(this Lesson lesson)
        {
            return new LessonDto
            {
                Id = lesson.Id,
                Date = lesson.Date,
                Start = lesson.Start,
                End = lesson.End,
                CourseName = lesson.CourseName,
                Classroom = lesson.Classroom,
                Teacher = lesson.Teacher.ToDto(),
                Group = lesson.Group.ToDto(),
            };
        }

        public static IEnumerable<Lesson> ToModels(this IEnumerable<LessonDto> dtos)
        {
            IEnumerable<Lesson> lessons = new List<Lesson>();
            foreach (LessonDto dto in dtos)
            {
                (lessons as List<Lesson>).Add(dto.ToModel());
            }
            return lessons;
        }

    }
}
