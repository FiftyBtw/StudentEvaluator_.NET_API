using API_Model;
using EF_Entities;

namespace EF_Entities2Model;


/// <summary>
/// Provides methods to translate entities to models and vice versa.
/// </summary>
public static class Translator
{
    // Student 

    /// <summary>
    /// Converts a <see cref="StudentEntity"/> object to a <see cref="Student"/> model.
    /// </summary>
    /// <param name="student">The student entity to convert.</param>
    /// <returns>The converted student model.</returns>
    public static Student ToModel(this StudentEntity student)
    {
        return new Student(student.Id, student.Name, student.Lastname, student.UrlPhoto, student.GroupYear, student.GroupNumber);
    }


    /// <summary>
    /// Converts a <see cref="Student"/> model to a <see cref="StudentEntity"/> object.
    /// </summary>
    /// <param name="student">The student model to convert.</param>
    /// <returns>The converted student entity.</returns>
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
    /// Converts a collection of <see cref="StudentEntity"/> objects to a collection of <see cref="Student"/> models.
    /// </summary>
    /// <param name="entities">The collection of student entities to convert.</param>
    /// <returns>The converted collection of student models.</returns>
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
    /// Converts a collection of <see cref="Student"/> models to a collection of <see cref="StudentEntity"/> objects.
    /// </summary>
    /// <param name="models">The collection of student models to convert.</param>
    /// <returns>The converted collection of student entities.</returns>
    public static IEnumerable<StudentEntity> ToEntities(this IEnumerable<Student> models)
    {
        IEnumerable<StudentEntity> students = new List<StudentEntity>();
        foreach (var model in models)
        {
            (students as List<StudentEntity>).Add(model.ToEntity());
        }
        return students;
    }

    // Group


    /// <summary>
    /// Converts a <see cref="GroupEntity"/> object to a <see cref="Group"/> model.
    /// </summary>
    /// <param name="group">The group entity to convert.</param>
    /// <returns>The converted group model.</returns>
    public static Group ToModel(this GroupEntity group)
    {
        if (group.Students != null) return new Group(group.GroupYear, group.GroupNumber, group.Students.ToModels());
        else return new Group(group.GroupYear, group.GroupNumber);
    }


    /// <summary>
    /// Converts a <see cref="Group"/> model to a <see cref="GroupEntity"/> object.
    /// </summary>
    /// <param name="group">The group model to convert.</param>
    /// <returns>The converted group entity.</returns>
    public static GroupEntity ToEntity(this Group group)
    {
        return new GroupEntity
        {
            GroupNumber = group.GroupNumber,
            GroupYear = group.GroupYear,
            Students = group.Students.ToEntities()
        };
    }


    /// <summary>
    /// Converts a collection of <see cref="GroupEntity"/> objects to a collection of <see cref="Group"/> models.
    /// </summary>
    /// <param name="entities">The collection of group entities to convert.</param>
    /// <returns>The converted collection of group models.</returns>
    public static IEnumerable<Group> ToModels(this IEnumerable<GroupEntity> entities)
    {
        IEnumerable<Group> groups = new List<Group>();
        foreach (var entity in entities)
        {
            (groups as List<Group>).Add(entity.ToModel());
        }
        return groups;
    }


    /// <summary>
    /// Converts a collection of <see cref="Group"/> models to a collection of <see cref="GroupEntity"/> objects.
    /// </summary>
    /// <param name="models">The collection of group models to convert.</param>
    /// <returns>The converted collection of group entities.</returns>
    public static IEnumerable<GroupEntity> ToEntities(this IEnumerable<Group> models)
    {
        IEnumerable<GroupEntity> groups = new List<GroupEntity>();
        foreach (var model in models)
        {
            (groups as List<GroupEntity>).Add(model.ToEntity());
        }
        return groups;
    }
}