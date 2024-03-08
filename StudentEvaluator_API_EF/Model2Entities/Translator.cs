using API_Model;
using EF_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model2Entities
{
    /// <summary>
    /// Provides translation methods to convert between model objects and entity objects.
    /// </summary>
    public static class Translator
    {
        //Student


        /// <summary>
        /// Converts a StudentEntity object to a Student model object.
        /// </summary>
        public static Student ToModel(this StudentEntity student)
        {
            return new Student(student.Id, student.Name, student.Lastname, student.UrlPhoto, student.GroupYear, student.GroupNumber);
        }


        /// <summary>
        /// Converts a Student model object to a StudentEntity object.
        /// </summary>
        public static StudentEntity ToEntity(this Student student)
        {
            return new StudentEntity
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
        /// Converts a collection of StudentEntity objects to a collection of Student model objects.
        /// </summary>
        public static IEnumerable<Student> ToModels(this IEnumerable<StudentEntity> entities)
        {
            IEnumerable<Student> students = new List<Student>();
            foreach (var entity in entities)
            {
                (students as List<Student>).Add(entity.ToModel());
            }
            return students;
        }


        /// <summary>
        /// Converts a collection of Student model objects to a collection of StudentEntity objects.
        /// </summary>
        public static IEnumerable<StudentEntity> ToEntities(this IEnumerable<Student> models)
        {
            IEnumerable<StudentEntity> students = new List<StudentEntity>();
            foreach (var model in models)
            {
                (students as List<StudentEntity>).Add(model.ToEntity());
            }
            return students;
        }

        //Group


        /// <summary>
        /// Converts a GroupEntity object to a Group model object.
        /// </summary>
        public static Group ToModel(this GroupEntity group)
        {
            if (group.Students != null) return new Group(group.GroupYear, group.GroupNumber, group.Students.ToModels());
            else return new Group(group.GroupYear, group.GroupNumber);
        }


        /// <summary>
        /// Converts a Group model object to a GroupEntity object.
        /// </summary>
        public static GroupEntity ToEntity(this Group group)
        {
            return new GroupEntity
            {
                GroupNumber = group.GroupNumber,
                GroupYear = group.GroupYear,
                Students = group.Students.ToEntities(),
            };
        }


        /// <summary>
        /// Converts a collection of GroupEntity objects to a collection of Group model objects.
        /// </summary>
        public static IEnumerable<Group> ToModels(this IEnumerable<GroupEntity> entities)
        {
            IEnumerable<Group> groups = new List<Group>();
            foreach (GroupEntity entity in entities)
            {
                (groups as List<Group>).Add(entity.ToModel());
            }
            return groups;
        }

    }
}
