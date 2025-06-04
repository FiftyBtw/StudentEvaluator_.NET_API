using Client_Model;
using EF_DbContextLib;
using EF_StubbedContextLib;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Model2Entities
{
    /// <summary>
    /// Manages database entities for student and group operations.
    /// </summary>
    public class DbEntitiesManager : IStudentService<Student>, IGroupService<Group>,
        ILessonService<LessonCreation, Lesson>, IEvaluationService<EvaluationCreation, Evaluation>,
        /*IUserService<User, LoginRequest, LoginResponse>, */ITemplateService<Template>
    {
        private readonly LibraryContext _libraryContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbEntitiesManager"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DbEntitiesManager(StubbedContext context)
        {
            _libraryContext = context;
        }

        //Student 


        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public async Task<bool> DeleteStudent(long id)
        {
            var student = await _libraryContext.StudentSet.FirstOrDefaultAsync(b => b.Id == id);
            if (student == null) return false;

            _libraryContext.StudentSet.Remove(student);
            await _libraryContext.SaveChangesAsync();

            return true;
        }


        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved student, or null if not found.</returns>
        public async Task<Student?> GetStudentById(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(s => s.Id == id)?.ToModel();
            return await Task.FromResult(student);
        }


        /// <summary>
        /// Retrieves a page of students.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of students per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a page of students.</returns>
        public async Task<PageReponse<Student>> GetStudents(int index, int count)
        {
            var students = _libraryContext.StudentSet.ToModels();
            return await Task.FromResult(new PageReponse<Student>(students.Count(),
                students.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="student">The student to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added student.</returns>
        public async Task<Student?> PostStudent(Student student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.StudentSet
                .FirstOrDefault(s => s.Name.Equals(student.Name) && s.Lastname.Equals(student.Lastname))?.ToModel());
        }


        /// <summary>
        /// Updates a student.
        /// </summary>
        /// <param name="id">The ID of the student to update.</param>
        /// <param name="student">The updated student.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated student.</returns>
        public async Task<Student?> PutStudent(long id, Student student)
        {
            var existingStudent = await _libraryContext.StudentSet.FindAsync(id);
            if (existingStudent == null) return null;

            _libraryContext.Entry(existingStudent).CurrentValues.SetValues(student);

            await _libraryContext.SaveChangesAsync();
            return existingStudent.ToModel();
        }


        //Group


        /// <summary>
        /// Retrieves a page of groups.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of groups per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a page of groups.</returns>
        public async Task<PageReponse<Group>> GetGroups(int index, int count)
        {
            var groups = _libraryContext.GroupSet.Include(g => g.Students).ToModels();
            return await Task.FromResult(new PageReponse<Group>(groups.Count(),
                groups.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Retrieves a group by its year and number.
        /// </summary>
        /// <param name="gyear">The year of the group.</param>
        /// <param name="gnumber">The number of the group.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved group, or null if not found.</returns>
        public async Task<Group?> GetGroupByIds(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.Include(g => g.Students)
                .FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber)
                ?.ToModel();
            return await Task.FromResult(group);
        }


        /// <summary>
        /// Adds a new group.
        /// </summary>
        /// <param name="group">The group to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added group.</returns>
        public async Task<Group?> PostGroup(Group group)
        {
            var groupTest = _libraryContext.GroupSet.FirstOrDefault(g =>
                g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber);
            if (groupTest != null) return await Task.FromResult(groupTest.ToModel());
            _libraryContext.GroupSet.AddAsync(group.ToEntity());
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.GroupSet
                .FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber)?.ToModel());
        }

        /// <summary>
        /// Deletes a group by its year and number.
        /// </summary>
        /// <param name="gyear">The year of the group to delete.</param>
        /// <param name="gnumber">The number of the group to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public async Task<bool> DeleteGroup(int gyear, int gnumber)
        {
            var group = await _libraryContext.GroupSet
                .FirstOrDefaultAsync(g => g.GroupYear == gyear && g.GroupNumber == gnumber);
            if (group == null) return false;
            _libraryContext.GroupSet.Remove(group);
            await _libraryContext.SaveChangesAsync();
            return true;
        }

        // Lesson

        public async Task<PageReponse<Lesson>> GetLessons(int index = 0, int count = 10)
        {
            var lessons = _libraryContext.LessonSet.ToModels();
            return await Task.FromResult(new PageReponse<Lesson>(lessons.Count(),
                lessons.Skip(index * count).Take(count)));
        }

        public async Task<Lesson?> GetLessonById(long id)
        {
            var lesson = _libraryContext.LessonSet.FirstOrDefault(l => l.Id == id)?.ToModel();
            return await Task.FromResult(lesson);
        }

        public async Task<PageReponse<Lesson>> GetLessonsByTeacherId(string id, int index = 0, int count = 10)
        {
            var lessons = _libraryContext.LessonSet.Where(l => l.TeacherEntityId == id).ToModels();
            return await Task.FromResult(new PageReponse<Lesson>(lessons.Count(),
                lessons.Skip(index * count).Take(count)));
        }

        public async Task<Lesson?> PostLesson(LessonCreation lesson)
        {
            var lessonEntity = lesson.ToEntity();
            await _libraryContext.LessonSet.AddAsync(lessonEntity);
            await _libraryContext.SaveChangesAsync();
            return await Task.FromResult(_libraryContext.LessonSet.FirstOrDefault(l => l.Id == lessonEntity.Id)
                ?.ToModel());
        }

        public async Task<Lesson?> PutLesson(long id, LessonCreation lesson)
        {
            var existingLesson = await _libraryContext.LessonSet.FindAsync(id);
            if (existingLesson == null) return null;
            existingLesson.CourseName = lesson.CourseName;
            await _libraryContext.SaveChangesAsync();
            return existingLesson.ToModel();
        }


        public async Task<bool> DeleteLesson(long id)
        {
            var lesson = await _libraryContext.LessonSet.FindAsync(id);
            if (lesson == null) return false;
            _libraryContext.LessonSet.Remove(lesson);
            await _libraryContext.SaveChangesAsync();
            return true;
        }


        // Evaluation

        public async Task<PageReponse<Evaluation>> GetEvaluations(int index = 0, int count = 10)
        {
            var evaluations = _libraryContext.EvaluationSet.ToModels();
            return await Task.FromResult(new PageReponse<Evaluation>(evaluations.Count(),
                evaluations.Skip(index * count).Take(count)));
        }

        public async Task<Evaluation?> GetEvaluationById(long id)
        {
            var evaluation = _libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == id)?.ToModel();
            return await Task.FromResult(evaluation);
        }

        public async Task<PageReponse<Evaluation>> GetEvaluationsByTeacherId(string id, int index = 0, int count = 10)
        {
            var evaluations = _libraryContext.EvaluationSet.Where(e => e.TeacherId == id).ToModels();
            return await Task.FromResult(new PageReponse<Evaluation>(evaluations.Count(),
                evaluations.Skip(index * count).Take(count)));
        }

        public async Task<Evaluation?> PostEvaluation(EvaluationCreation evaluation)
        {
            var evaluationEntity = evaluation.ToEntity();
            _libraryContext.EvaluationSet.AddAsync(evaluationEntity);
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.EvaluationSet.FirstOrDefault(e => e.Id == evaluationEntity.Id)
                ?.ToModel());
        }

        public async Task<Evaluation?> PutEvaluation(long id, EvaluationCreation evaluation)
        {
            var existingEvaluation = await _libraryContext.EvaluationSet.FindAsync(id);
            if (existingEvaluation == null)
            {
                return null;
            }

            existingEvaluation.CourseName = evaluation.CourseName;
            existingEvaluation.Date = evaluation.Date;
            existingEvaluation.Grade = evaluation.Grade;
            existingEvaluation.PairName = evaluation.PairName;
            existingEvaluation.TeacherId = evaluation.TeacherId;
            existingEvaluation.TemplateId = evaluation.TemplateId;
            existingEvaluation.StudentId = evaluation.StudentId;
            await _libraryContext.SaveChangesAsync();
            return existingEvaluation.ToModel();
        }


        public async Task<bool> DeleteEvaluation(long id)
        {
            var evaluation = await _libraryContext.EvaluationSet.FindAsync(id);
            if (evaluation == null)
            {
                return false;
            }

            _libraryContext.EvaluationSet.Remove(evaluation);
            await _libraryContext.SaveChangesAsync();

            return true;
        }
    /*
        // User 

        public async Task<PageReponse<User>> GetUsers(int index = 0, int count = 10)
        {
            var users = _libraryContext.UserSet.ToModels();
            return await Task.FromResult(new PageReponse<User>(users.Count(),
                users.Skip(index * count).Take(count)));
        }

        public async Task<User?> GetUserById(long id)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Id == id)?.ToModel();
            return await Task.FromResult(user);
        }

        public async Task<User?> PostUser(User user)
        {
            var userEntity = user.ToEntity();
            _libraryContext.UserSet.AddAsync(userEntity);
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.UserSet.FirstOrDefault(u => u.Id == userEntity.Id)
                ?.ToModel());
        }

        public async Task<User?> PutUser(long id, User user)
        {
            var existingUser = await _libraryContext.UserSet.FindAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.Username = user.Username;
            existingUser.Password = user.Password;

            await _libraryContext.SaveChangesAsync();
            return existingUser.ToModel();
        }


        public async Task<bool> DeleteUser(long id)
        {
            var userEntity = await _libraryContext.UserSet.FindAsync(id);
            if (userEntity == null)
            {
                return false;
            }

            _libraryContext.UserSet.Remove(userEntity);
            await _libraryContext.SaveChangesAsync();
            return true;
        }


        public async Task<LoginResponse?> Login(LoginRequest loginRequest)
        {
            var user = _libraryContext.UserSet.FirstOrDefault(u => u.Username == loginRequest.Username);
            if (user == null) return await Task.FromResult<LoginResponse?>(null);
            if (user.Password != loginRequest.Password) return await Task.FromResult<LoginResponse?>(null);
            return await Task.FromResult(new LoginResponse(user.Id, user.Username, user.Roles));
        }

        public async Task<User?> PostTeacher(Teacher teacher)
        {
            var userEntity = teacher.ToEntity();
            _libraryContext.TeacherSet.AddAsync(userEntity);
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.TeacherSet.FirstOrDefault(u => u.Id == userEntity.Id)
                ?.ToModel());
        }
        */
        // Template

        public async Task<PageReponse<Template>> GetTemplatesByUserId(string id, int index = 0, int count = 10)
        {
            var templates = _libraryContext.TemplateSet.Where(t => t.TeacherId == id).ToModels();
            return await Task.FromResult(new PageReponse<Template>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }

        public async Task<PageReponse<Template>> GetEmptyTemplatesByUserId(string id, int index = 0, int count = 10)
        {
            var templates = _libraryContext.TemplateSet.Where(t => t.TeacherId == id && t.EvaluationId == null)
                .ToModels();
            return await Task.FromResult(new PageReponse<Template>(templates.Count(),
                templates.Skip(index * count).Take(count)));
        }


        public async Task<Template?> GetTemplateById(long templateId)
        {
            var template = _libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateId);
            return await Task.FromResult(template?.ToModel());
        }

        public async Task<Template?> PostTemplate(string userId, Template template)
        {
            var templateEntity = template.ToEntity();
            templateEntity.TeacherId = userId;
            _libraryContext.TemplateSet.AddAsync(templateEntity);
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.TemplateSet.FirstOrDefault(t => t.Id == templateEntity.Id)
                ?.ToModel());
        }

        public async Task<Template?> PutTemplate(long templateId, Template template)
        {
            var existingTemplate = await _libraryContext.TemplateSet.FindAsync(templateId);
            if (existingTemplate == null)
            {
                return null;
            }
            existingTemplate.Name = template.Name;
            existingTemplate.Criteria = template.Criterias.Select(CriteriaEntityConverter.ConvertToEntity).ToList();
            await _libraryContext.SaveChangesAsync();
            return existingTemplate.ToModel(); 
        }


        public async Task<bool> DeleteTemplate(long templateId)
        {
            var templateEntity = await _libraryContext.TemplateSet.FindAsync(templateId);
            if (templateEntity == null)
            {
                return false;
            }

            _libraryContext.TemplateSet.Remove(templateEntity);
            await _libraryContext.SaveChangesAsync();
            return true;
        }
    }
}
