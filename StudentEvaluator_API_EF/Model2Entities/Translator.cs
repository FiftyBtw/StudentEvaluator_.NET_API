using Client_Model;
using EF_Entities;
using Model2Entities;

namespace Model2Entities;


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
    public static ICollection<StudentEntity> ToEntities(this IEnumerable<Student> models)
    {
        ICollection<StudentEntity> students = new List<StudentEntity>();
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
    
    
    // Teacher 
    
    /// <summary>
    /// Converts a <see cref="TeacherEntity"/> object to a <see cref="Teacher"/> model.
    /// </summary>
    /// <param name="teacher">The teacher entity to convert.</param>
    /// <returns>The converted teacher model.</returns>
    public static Teacher ToModel(this TeacherEntity teacher)
    {
        return new Teacher(teacher.Id, teacher.UserName, teacher.PasswordHash);
    }
    
    /// <summary>
    /// Converts a <see cref="Teacher"/> model to a <see cref="TeacherEntity"/> object.
    /// </summary>
    /// <param name="teacher">The teacher model to convert.</param>
    /// <returns>The converted teacher entity.</returns>
    public static TeacherEntity ToEntity(this Teacher teacher)
    {
        return new TeacherEntity
        {
            Id = teacher.Id,
            UserName = teacher.Username,
            PasswordHash = teacher.Password,
        };
    }
    
    // Lesson
    
    /// <summary>
    /// Converts a <see cref="LessonEntity"/> object to a <see cref="Lesson"/> model.
    /// </summary>
    /// <param name="lesson">The lesson entity to convert.</param>
    /// <returns>The converted lesson model.</returns>
    public static Lesson ToModel(this LessonEntity lesson)
    {
        return new Lesson(lesson.Id, lesson.Start, lesson.End, lesson.CourseName, lesson.Classroom, lesson.Teacher.ToModel(), lesson.Group.ToModel());
    }
    
    /// <summary>
    /// Converts a <see cref="Lesson"/> model to a <see cref="LessonEntity"/> object.
    /// </summary>
    /// <param name="lesson">The lesson model to convert.</param>
    /// <returns>The converted lesson entity.</returns>
    public static LessonEntity ToEntity(this LessonCreation lesson)
    {
        return new LessonEntity
        {
            Start = lesson.Start,
            End = lesson.End,
            CourseName = lesson.CourseName,
            Classroom = lesson.Classroom,
            TeacherEntityId = lesson.TeacherId,
            GroupYear = lesson.GroupYear,
            GroupNumber = lesson.GroupNumber
        };
    }
    
    /// <summary>
    /// Converts a collection of <see cref="LessonEntity"/> objects to a collection of <see cref="Lesson"/> models.
    /// </summary>
    /// <param name="entities">The collection of lesson entities to convert.</param>
    /// <returns>The converted collection of lesson models.</returns>
    public static IEnumerable<Lesson> ToModels(this IEnumerable<LessonEntity> entities)
    {
        IEnumerable<Lesson> lessons = new List<Lesson>();
        foreach (var entity in entities)
        {
            (lessons as List<Lesson>).Add(entity.ToModel());
        }
        return lessons;
    }
    
    /// <summary>
    /// Converts a collection of <see cref="Lesson"/> models to a collection of <see cref="LessonEntity"/> objects.
    /// </summary>
    /// <param name="models">The collection of lesson models to convert.</param>
    /// <returns>The converted collection of lesson entities.</returns>
    public static IEnumerable<LessonEntity> ToEntities(this IEnumerable<LessonCreation> models)
    {
        IEnumerable<LessonEntity> lessons = new List<LessonEntity>();
        foreach (var model in models)
        {
            (lessons as List<LessonEntity>).Add(model.ToEntity());
        }
        return lessons;
    }
    
    // Template
    
    public static Template ToModel(this TemplateEntity template)
    {
        return new Template(template.Id, template.Name, template.Criteria.Select(CriteriaEntityConverter.ConvertToModel).ToList());
    }
    
   public static TemplateEntity ToEntity(this Template template)
    {
        return new TemplateEntity
        {
            Id = template.Id,
            Name = template.Name,
            Criteria = template.Criterias.Select(CriteriaEntityConverter.ConvertToEntity).ToList()
        };
    }
   
   public static IEnumerable<Template> ToModels(this IEnumerable<TemplateEntity> entities)
    {
        IEnumerable<Template> templates = new List<Template>();
        foreach (var entity in entities)
        {
            (templates as List<Template>).Add(entity.ToModel());
        }
        return templates;
    }
    
    // Evaluation
    
    public static Evaluation ToModel(this EvaluationEntity evaluation)
    {
        return new Evaluation(evaluation.Id, evaluation.Date, evaluation.CourseName, evaluation.Grade, evaluation.PairName, evaluation.Teacher.ToModel(), evaluation.Template?.ToModel(), evaluation.Student.ToModel());
    }
    
    public static EvaluationEntity ToEntity(this EvaluationCreation evaluation)
    {
        return new EvaluationEntity
        {
            Date = evaluation.Date,
            CourseName = evaluation.CourseName,
            Grade = evaluation.Grade,
            PairName = evaluation.PairName,
            TeacherId = evaluation.TeacherId,
            TemplateId = evaluation.TemplateId,
            StudentId = evaluation.StudentId
        };
    }
    
    public static IEnumerable<Evaluation> ToModels(this IEnumerable<EvaluationEntity> entities)
    {
        IEnumerable<Evaluation> evaluations = new List<Evaluation>();
        foreach (var entity in entities)
        {
            (evaluations as List<Evaluation>).Add(entity.ToModel());
        }
        return evaluations;
    }
    
    public static IEnumerable<EvaluationEntity> ToEntities(this IEnumerable<EvaluationCreation> models)
    {
        IEnumerable<EvaluationEntity> evaluations = new List<EvaluationEntity>();
        foreach (var model in models)
        {
            (evaluations as List<EvaluationEntity>).Add(model.ToEntity());
        }
        return evaluations;
    }
}