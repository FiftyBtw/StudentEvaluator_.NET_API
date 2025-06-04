using API_Dto;
using Client_Model;
using Shared;
using System.Net.Http.Json;

namespace Dto2Model
{
    /// <summary>
    /// Class for managing API data related to students and groups.
    /// </summary>
    public class ApiDataManager : IStudentService<Student>, IGroupService<Group>, ILessonService<LessonCreation, Lesson>, IEvaluationService<EvaluationCreation, Evaluation>,/*IUserService<User,LoginRequest,LoginResponse>,*/ITemplateService<Template>
    {
      
    

    private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor for the ApiDataManager class.
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for API requests.</param>
        /// <param name="version">The version of the API to use.</param>
        private readonly int _version;

        public ApiDataManager(HttpClient httpClient,int version=1)
        {
            _httpClient = httpClient;
            _version = version;
        }

        //Student

        /// <summary>
        /// Deletes a student with the specified ID.
        /// </summary>
        public async Task<bool> DeleteStudent(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{_version}/Students?id={id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);

        }

        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        public async Task<Student?> GetStudentById(long id)
        {
            var studentById = await _httpClient.GetFromJsonAsync<StudentDto>($"{_httpClient.BaseAddress}api/v{_version}/Students/{id}");
            return await Task.FromResult(studentById?.ToModel());
        }

        /// <summary>
        /// Retrieves a page of students.
        /// </summary>
        public async Task<PageReponse<Student>> GetStudents(int index=0, int count=10)
        {
            var students = await _httpClient.GetFromJsonAsync<PageReponse<StudentDto>>($"{_httpClient.BaseAddress}api/v{_version}/Students?index={index}&count={count}");
            if (students != null)
            {
                return await Task.FromResult(new PageReponse<Student>(students.NbElement, students.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Student>(0, new List<Student>()));
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        public async Task<Student?> PostStudent(Student student)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Students", student.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Student>());
        }


        /// <summary>
        /// Updates an existing student.
        /// </summary>
        public async Task<Student?> PutStudent(long id, Student student)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Students?id={student.Id}", student.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Student>());
        }

        //Group

        /// <summary>
        /// Retrieves a page of groups.
        /// </summary>
        public async Task<PageReponse<Group>> GetGroups(int index=0, int count=10)
        {

            var groups = await _httpClient.GetFromJsonAsync<PageReponse<GroupDto>>($"{_httpClient.BaseAddress}api/v{_version}/Groups?index={index}&count={count}");
            if (groups != null)
            {
                return await Task.FromResult(new PageReponse<Group>(groups.NbElement, groups.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Group>(0, new List<Group>()));
        }

        /// <summary>
        /// Retrieves a group by year and number.
        /// </summary>
        public async Task<Group?> GetGroupByIds(int gyear, int gnumber)
        {
            var groupById = await _httpClient.GetFromJsonAsync<GroupDto>($"{_httpClient.BaseAddress}api/v{_version}/Groups/{gyear}/{gnumber}");
            return await Task.FromResult(groupById?.ToModel());
        }

        /// <summary>
        /// Adds a new group.
        /// </summary>
        public async Task<Group?> PostGroup(Group group)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Groups", group.ToDto());
            var groupRep = await reponse.Content.ReadFromJsonAsync<GroupDto>();
            if (groupRep != null)
            {
                return await Task.FromResult(groupRep.ToModel());
            }
            return await Task.FromResult<Group?>(null);
        }

        /// <summary>
        /// Deletes a group by year and number.
        /// </summary>
        public async Task<bool> DeleteGroup(int gyear, int gnumber)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{_version}/Groups?gyear={gyear}&gnumber={gnumber}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }

        //Lesson
        public async Task<PageReponse<Lesson>> GetLessons(int index=0, int count=10)
        {
            var lessons = await _httpClient.GetFromJsonAsync<PageReponse<LessonReponseDto>>($"{_httpClient.BaseAddress}api/v{_version}/Lessons?index={index}&count={count}");
            if (lessons != null)
            {
            return await Task.FromResult(new PageReponse<Lesson>(lessons.NbElement, lessons.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Lesson>(0, new List<Lesson>()));
        }

        public async Task<Lesson?> GetLessonById(long id)
        {
            var lessonById = await _httpClient.GetFromJsonAsync<LessonReponseDto>($"{_httpClient.BaseAddress}api/v{_version}/Lessons/{id}");
            return await Task.FromResult(lessonById?.ToModel());
        }

        public async Task<PageReponse<Lesson>> GetLessonsByTeacherId(string id, int index=0, int count = 10)
        {
            var lessonsByTeacherId = await _httpClient.GetFromJsonAsync<PageReponse<LessonReponseDto>>($"{_httpClient.BaseAddress}api/v{_version}/Lessons/teacher?index={index}&count={count}");
            if (lessonsByTeacherId != null)
            {
                return await Task.FromResult(new PageReponse<Lesson>(lessonsByTeacherId.NbElement, lessonsByTeacherId.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Lesson>(0, new List<Lesson>()));
        }

        public async Task<Lesson?> PostLesson(LessonCreation lesson)
        {
            var lessonDto = lesson.ToDto();
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Lessons", lessonDto);
            if(reponse.IsSuccessStatusCode)
            {
                var lessonRep = await reponse.Content.ReadFromJsonAsync<LessonReponseDto>();
                if (lessonRep != null)
                {
                    return await Task.FromResult(lessonRep.ToModel());
                }
            }
            return await Task.FromResult<Lesson?>(null);
        }
        
        public async Task<Lesson?> PutLesson(long id, LessonCreation lesson)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Lessons?id={id}", lesson.ToDto());
            if(reponse.IsSuccessStatusCode)
            {
                var lessonRep = await reponse.Content.ReadFromJsonAsync<LessonReponseDto>();
                if (lessonRep != null)
                {
                    return await Task.FromResult(lessonRep.ToModel());
                }
            }   
            return await Task.FromResult<Lesson?>(null);
        }
        public async Task<bool> DeleteLesson(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{_version}/Lessons?id={id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);

        }

        //Evaluation

        public async Task<PageReponse<Evaluation>> GetEvaluations(int index=0, int count=10)
        {
            var evals = await _httpClient.GetFromJsonAsync<PageReponse<EvaluationReponseDto>>($"{_httpClient.BaseAddress}api/v{_version}/Evaluations?index={index}&count={count}");
            if (evals != null)
            {
                return await Task.FromResult(new PageReponse<Evaluation>(evals.NbElement, evals.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Evaluation>(0, new List<Evaluation>()));
        }

        public async Task<Evaluation?> GetEvaluationById(long id)
        {
            var evalById = await _httpClient.GetFromJsonAsync<EvaluationReponseDto>($"{_httpClient.BaseAddress}api/v{_version}/Evaluations/{id}");
            return await Task.FromResult(evalById?.ToModel());
        }

        public async Task<PageReponse<Evaluation>> GetEvaluationsByTeacherId(string id, int index=0, int count=10)
        {
            var evalsByTeacherId = await _httpClient.GetFromJsonAsync<PageReponse<EvaluationReponseDto>>($"{_httpClient.BaseAddress}api/v{_version}/Evaluations/teacher?index={index}&count={count}");
            if (evalsByTeacherId != null)
            {
                return await Task.FromResult(new PageReponse<Evaluation>(evalsByTeacherId.NbElement, evalsByTeacherId.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Evaluation>(0, new List<Evaluation>()));
        }

        public async Task<Evaluation?> PostEvaluation(EvaluationCreation eval)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Evaluations", eval.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var evalRep = await reponse.Content.ReadFromJsonAsync<EvaluationReponseDto>();
                if (evalRep != null)
                {
                    return await Task.FromResult(evalRep.ToModel());
                }
            }
            return await Task.FromResult<Evaluation?>(null);
        }

        public async Task<Evaluation?> PutEvaluation(long id, EvaluationCreation eval)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Evaluations?id={id}", eval.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var evalRep = await reponse.Content.ReadFromJsonAsync<EvaluationReponseDto>();
                if (evalRep != null)
                {
                    return await Task.FromResult(evalRep.ToModel());
                }
            }
            return await Task.FromResult<Evaluation?>(null);
        }

        public async Task<bool> DeleteEvaluation(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{_version}/Evaluations?id={id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }
        //User
    /*
        public async Task<PageReponse<User>> GetUsers(int index = 0, int count = 10)
        {
            var users = await _httpClient.GetFromJsonAsync<PageReponse<UserDto>>(
                $"{_httpClient.BaseAddress}api/v{_version}/Users?index={index}&count={count}");
            if (users != null)
            {
                return await Task.FromResult(new PageReponse<User>(users.nbElement, users.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<User>(0, new List<User>()));
        }
    
        
        public async Task<User?> GetUserById(long id)
        {
            var userById = await _httpClient.GetFromJsonAsync<UserDto>($"{_httpClient.BaseAddress}api/v{_version}/Users/{id}");
            return await Task.FromResult(userById?.ToModel());
        }

        public async Task<User?> PostUser(User user)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Users", user.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var userRep = await reponse.Content.ReadFromJsonAsync<UserDto>();
                if (userRep != null)
                {
                    return await Task.FromResult(userRep.ToModel());
                }
            }
            return await Task.FromResult<User?>(null);
        }

        public async Task<LoginResponse?> Login(LoginRequest loginRequest)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Users/login", loginRequest.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var userRep = await reponse.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (userRep != null)
                {
                    return await Task.FromResult(userRep.ToModel());
                }
            }
            return await Task.FromResult<LoginResponse?>(null);
        }

        public async Task<User?> PutUser(long id, User user)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Users/{id}", user.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var userRep = await reponse.Content.ReadFromJsonAsync<UserDto>();
                if (userRep != null)
                {
                    return await Task.FromResult(userRep.ToModel());
                }
            }
            return await Task.FromResult<User?>(null);
        }

        public async Task<bool> DeleteUser(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{_version}/Users/{id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }
        */
        //Template
        public async Task<PageReponse<Template>> GetTemplatesByUserId(string userId, int index=0, int count = 10)
        {
            var templatesByUserId = await _httpClient.GetFromJsonAsync<PageReponse<TemplateDto>>($"{_httpClient.BaseAddress}api/v{_version}/Templates/teacher?index={index}&count={count}");
            if (templatesByUserId != null)
            {
                return await Task.FromResult(new PageReponse<Template>(templatesByUserId.NbElement, templatesByUserId.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Template>(0, new List<Template>()));
        }

        public async Task<PageReponse<Template>> GetEmptyTemplatesByUserId(string userId, int index=0, int count=10)
        {
            var emptyTemplatesByUserId = await _httpClient.GetFromJsonAsync<PageReponse<TemplateDto>>($"{_httpClient.BaseAddress}api/v{_version}/Templates/teacher/models?index={index}&count={count}");
            if (emptyTemplatesByUserId != null)
            {
                return await Task.FromResult(new PageReponse<Template>(emptyTemplatesByUserId.NbElement, emptyTemplatesByUserId.Data.ToModels()));
            }
            return await Task.FromResult(new PageReponse<Template>(0, new List<Template>()));
        }

        public async Task<Template?> GetTemplateById(long templateId)
        {
            var templateById = await _httpClient.GetFromJsonAsync<TemplateDto>($"{_httpClient.BaseAddress}api/v{_version}/Templates/{templateId}");
            return await Task.FromResult(templateById?.ToModel());
        }

        public async Task<Template?> PostTemplate(string userId, Template template)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Templates", template.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var templateRep = await reponse.Content.ReadFromJsonAsync<TemplateDto>();
                if (templateRep != null)
                {
                    return await Task.FromResult(templateRep.ToModel());
                }
            }
            return await Task.FromResult<Template?>(null);
        }

        public async Task<Template?> PutTemplate(long templateId, Template template)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{_version}/Templates/{templateId}", template.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var templateRep = await reponse.Content.ReadFromJsonAsync<TemplateDto>();
                if (templateRep != null)
                {
                    return await Task.FromResult(templateRep.ToModel());
                }
            }
            return await Task.FromResult<Template?>(null);
        }

        public async Task<bool> DeleteTemplate(long templateId)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{_version}/Templates/{templateId}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }
    }
}
