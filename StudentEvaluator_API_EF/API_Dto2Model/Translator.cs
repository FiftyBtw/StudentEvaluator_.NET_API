using API_Dto;
using Client_Model;

namespace Dto2Model
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
        //Teacher
        public static Teacher ToModel(this TeacherDto dto)
        {
            return new Teacher(dto.Id, dto.Username, dto.Password,dto.roles) ;
        }

        public static TeacherDto ToDto(this Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                Username = teacher.Username,
                Password = teacher.Password,
                roles = teacher.Roles

            };
        }

        public static IEnumerable<Teacher> ToModels(this IEnumerable<TeacherDto> dtos)
        {
            IEnumerable<Teacher> teachers = new List<Teacher>();
            foreach (TeacherDto dto in dtos)
            {
                (teachers as List<Teacher>).Add(dto.ToModel());
            }
            return teachers;
        }
        //Lesson
        public static Lesson ToModel(this LessonReponseDto lesson)
        {
            return new Lesson(lesson.Id,lesson.Start,lesson.End,lesson.CourseName,lesson.Classroom,lesson.Teacher.ToModel(),lesson.Group.ToModel());
        }


        public static LessonDto ToDto(this LessonCreation lesson)
        {
            return new LessonDto
            { 
                Start = lesson.Start,
                End = lesson.End,
                CourseName = lesson.CourseName,
                Classroom = lesson.Classroom,
                TeacherId = lesson.TeacherId,
                GroupNumber = lesson.GroupNumber,
                GroupYear = lesson.GroupYear,
            };
        }

        public static LessonReponseDto ToReponseDto(this Lesson lesson)
        {
            return new LessonReponseDto
            {
                Id = lesson.Id,
                Start = lesson.Start,
                End = lesson.End,
                CourseName = lesson.CourseName,
                Classroom = lesson.Classroom,
                Teacher = lesson.Teacher.ToDto(),
                Group = lesson.Group.ToDto(),
           
            };
        }

        public static IEnumerable<Lesson> ToModels(this IEnumerable<LessonReponseDto> dtos)
        {
            IEnumerable<Lesson> lessons = new List<Lesson>();
            foreach (LessonReponseDto dto in dtos)
            {
                (lessons as List<Lesson>).Add(dto.ToModel());
            }
            return lessons;
        }

        //Criteria 

        //public static Criteria ToModel(this CriteriaDto criteriaDto)
        //{
        //    return new Criteria(criteriaDto.Id, criteriaDto.Name, criteriaDto.ValueEvaluation, criteriaDto.TemplateId);
        //}


        //public static CriteriaDto ToDto(this Criteria criteria)
        //{
        //    return new CriteriaDto
        //    {
        //        Id= criteria.Id,
        //        Name= criteria.Name,
        //        ValueEvaluation= criteria.ValueEvaluation,
        //        TemplateId= criteria.TemplateId

        //    };
        //}

 
        //Template 

        public static Template ToModel(this TemplateDto template)
        {
            return new Template(template.Id, template.Name, (template.Criterias??= []).Select(CriteriaModelConverter.ConvertToModel).ToList()) ;
        }


        public static TemplateDto ToDto(this Template template)
        {
            return new TemplateDto
            {
                Id = template.Id,
                Name = template.Name,
                Criterias = template.Criterias.Select(CriteriaModelConverter.ConvertToDto).ToList(),
            };
        }

        public static IEnumerable<Template> ToModels(this IEnumerable<TemplateDto> dtos)
        {
            IEnumerable<Template> templates = new List<Template>();
            foreach (TemplateDto dto in dtos)
            {
                (templates as List<Template>).Add(dto.ToModel());
            }
            return templates;
        }

        //Evaluation

        public static Evaluation ToModel(this EvaluationReponseDto evalRepDto)
        {
            return new Evaluation(evalRepDto.Id,evalRepDto.Date,evalRepDto.CourseName,evalRepDto.Grade,evalRepDto.PairName,evalRepDto.Teacher.ToModel(),evalRepDto.Template?.ToModel(),evalRepDto.Student.ToModel());
        }


        public static EvaluationDto ToDto(this EvaluationCreation eval)
        {
            return new EvaluationDto
            {
                Date = eval.Date,
                CourseName = eval.CourseName,
                Grade = eval.Grade,
                PairName = eval.PairName,
                StudentId = eval.StudentId,
                TeacherId = eval.TeacherId,
                TemplateId =eval.TemplateId,

            };
        }

        public static EvaluationReponseDto ToReponseDto(this Evaluation evaluation)
        {
            return new EvaluationReponseDto
            {
                Id = evaluation.Id,
                Date = evaluation.Date,
                CourseName= evaluation.CourseName,
                Grade = evaluation.Grade,
                PairName = evaluation.PairName,
                Teacher= evaluation.Teacher.ToDto(),
                Template = evaluation.Template.ToDto(),
                Student = evaluation.Student.ToDto(), 
            };
        }

        public static IEnumerable<Evaluation> ToModels(this IEnumerable<EvaluationReponseDto> dtos)
        {
            IEnumerable<Evaluation> evals = new List<Evaluation>();
            foreach (EvaluationReponseDto dto in dtos)
            {
                (evals as List<Evaluation>).Add(dto.ToModel());
            }
            return evals;
        }

        //Users
        public static User ToModel(this UserDto dto)
        {
            return new User(dto.Id, dto.Username, dto.Password, dto.roles);
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                roles = user.Roles,
            };
        }

        public static LoginRequestDto ToDto(this LoginRequest loginRequest)
        {
            return new LoginRequestDto
            {        
                Username = loginRequest.Username,
                Password = loginRequest.Password,
            };
        }

        public static LoginResponse ToModel(this LoginResponseDto loginReponse)
        {
            return new LoginResponse(loginReponse.Id,loginReponse.Username,loginReponse.Roles);
        }

        public static IEnumerable<User> ToModels(this IEnumerable<UserDto> dtos)
        {
            IEnumerable<User> users = new List<User>();
            foreach (UserDto dto in dtos)
            {
                (users as List<User>).Add(dto.ToModel());
            }
            return users;
        }
    }
}

