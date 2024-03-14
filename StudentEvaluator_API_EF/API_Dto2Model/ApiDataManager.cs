using API_Dto;
using API_Model;
using Client_Model;
using Shared;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace API_Dto2Model
{
    /// <summary>
    /// Class for managing API data related to students and groups.
    /// </summary>
    public class ApiDataManager : IStudentService<Student>, IGroupService<Group>, ILessonService<LessonCreation, Lesson>, IEvaluationService<EvaluationCreation, Evaluation>,IUserService<User,LoginRequest,LoginReponse>,ITemplateService<Template>
    {
      
    

    private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor for the ApiDataManager class.
        /// </summary>
        /// <param name="httpClient">HttpClient object to make HTTP requests.</param>
        /// 
        private readonly int Version;

        public ApiDataManager(HttpClient httpClient,int version=1)
        {
            _httpClient = httpClient;
            Version = version;
        }

        //Student

        /// <summary>
        /// Deletes a student with the specified ID.
        /// </summary>
        public async Task<bool> DeleteStudent(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{Version}/Students?id={id}");
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
            var studentById = await _httpClient.GetFromJsonAsync<StudentDto>($"{_httpClient.BaseAddress}api/v{Version}/Students/{id}");
            return await Task.FromResult(studentById?.ToModel());
        }

        /// <summary>
        /// Retrieves a page of students.
        /// </summary>
        public async Task<PageReponse<Student>> GetStudents(int index=0, int count=10)
        {
            var students = await _httpClient.GetFromJsonAsync<PageReponse<StudentDto>>($"{_httpClient.BaseAddress}api/v{Version}/Students?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Student>(students.nbElement,students.Data.ToModels()));
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        public async Task<Student?> PostStudent(Student student)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Students", student.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Student>());
        }


        /// <summary>
        /// Updates an existing student.
        /// </summary>
        public async Task<Student?> PutStudent(long id, Student student)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Students?id={student.Id}", student.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Student>());
        }

        //Group

        /// <summary>
        /// Retrieves a page of groups.
        /// </summary>
        public async Task<PageReponse<Group>> GetGroups(int index=0, int count=10)
        {

            var groups = await _httpClient.GetFromJsonAsync<PageReponse<GroupDto>>($"{_httpClient.BaseAddress}api/v{Version}/Groups?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Group>(groups.nbElement, groups.Data.ToModels()));
        }

        /// <summary>
        /// Retrieves a group by year and number.
        /// </summary>
        public async Task<Group?> GetGroupByIds(int gyear, int gnumber)
        {
            var groupById = await _httpClient.GetFromJsonAsync<GroupDto>($"{_httpClient.BaseAddress}api/v{Version}/Groups/{gyear}/{gnumber}");
            return await Task.FromResult(groupById?.ToModel());
        }

        /// <summary>
        /// Adds a new group.
        /// </summary>
        public async Task<Group?> PostGroup(Group group)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Groups", group.ToDto());
            var groupRep = await reponse.Content.ReadFromJsonAsync<GroupDto>();
            return await Task.FromResult(groupRep.ToModel());
        }

        /// <summary>
        /// Updates an existing group.
        /// </summary>
        public async Task<Group?> PutGroup(int gyear, int gnumber, Group group)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Groups?gyear={group.GroupYear}&gnumber={group.GroupNumber}", group.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Group>());
        }

        /// <summary>
        /// Deletes a group by year and number.
        /// </summary>
        public async Task<bool> DeleteGroup(int gyear, int gnumber)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{Version}/Groups?gyear={gyear}&gnumber={gnumber}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }

        //Lesson
        public async Task<PageReponse<Lesson>> GetLessons(int index=0, int count=10)
        {
            var lessons = await _httpClient.GetFromJsonAsync<PageReponse<LessonReponseDto>>($"{_httpClient.BaseAddress}api/v{Version}/Lessons?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Lesson>(lessons.nbElement, lessons.Data.ToModels()));
        }

        public async Task<Lesson?> GetLessonById(long id)
        {
            var lessonById = await _httpClient.GetFromJsonAsync<LessonReponseDto>($"{_httpClient.BaseAddress}api/v{Version}/Lessons/id/{id}");
            return await Task.FromResult(lessonById?.ToModel());
        }

        public async Task<PageReponse<Lesson>> GetLessonsByTeacherId(long id, int index=0, int count = 10)
        {
            var lessonsByTeacherId = await _httpClient.GetFromJsonAsync<PageReponse<LessonReponseDto>>($"{_httpClient.BaseAddress}api/v{Version}/Lessons/teacher/{id}?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Lesson>(lessonsByTeacherId.nbElement, lessonsByTeacherId.Data.ToModels()));
        }

        public async Task<Lesson?> PostLesson(LessonCreation lesson)
        {
            var lessonDto = lesson.ToDto();
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Lessons", lessonDto);
            if(reponse.IsSuccessStatusCode)
            {
                var lessonRep = await reponse.Content.ReadFromJsonAsync<LessonReponseDto>();
                return await Task.FromResult(lessonRep.ToModel());
            }
            return await Task.FromResult<Lesson?>(null);
        }
        
        public async Task<Lesson?> PutLesson(long id, LessonCreation lesson)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Lessons?id={id}", lesson.ToDto());
            if(reponse.IsSuccessStatusCode)
            {
                var lessonRep = await reponse.Content.ReadFromJsonAsync<LessonReponseDto>();
                return await Task.FromResult(lessonRep.ToModel());
            }   
            return await Task.FromResult<Lesson?>(null);
        }
        public async Task<bool> DeleteLesson(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{Version}/Lessons?id={id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);

        }

        //Evaluation

        public async Task<PageReponse<Evaluation>> GetEvaluations(int index=0, int count=10)
        {
            var evals = await _httpClient.GetFromJsonAsync<PageReponse<EvaluationReponseDto>>($"{_httpClient.BaseAddress}api/v{Version}/Evaluations?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Evaluation>(evals.nbElement, evals.Data.ToModels()));
        }

        public async Task<Evaluation?> GetEvaluationById(long id)
        {
            var evalById = await _httpClient.GetFromJsonAsync<EvaluationReponseDto>($"{_httpClient.BaseAddress}api/v{Version}/Evaluations/{id}");
            return await Task.FromResult(evalById?.ToModel());
        }

        public async Task<PageReponse<Evaluation>> GetEvaluationsByTeacherId(long id, int index=0, int count=10)
        {
            var evalsByTeacherId = await _httpClient.GetFromJsonAsync<PageReponse<EvaluationReponseDto>>($"{_httpClient.BaseAddress}api/v{Version}/Evaluations/teacher/{id}?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Evaluation>(evalsByTeacherId.nbElement, evalsByTeacherId.Data.ToModels()));
        }

        public async Task<Evaluation?> PostEvaluation(EvaluationCreation eval)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Evaluations", eval.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var evalRep = await reponse.Content.ReadFromJsonAsync<EvaluationReponseDto>();
                return await Task.FromResult(evalRep.ToModel());
            }
            return await Task.FromResult<Evaluation?>(null);
        }

        public async Task<Evaluation?> PutEvaluation(long id, EvaluationCreation eval)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Evaluations?id={id}", eval.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var evalRep = await reponse.Content.ReadFromJsonAsync<EvaluationReponseDto>();
                return await Task.FromResult(evalRep.ToModel());
            }
            return await Task.FromResult<Evaluation?>(null);
        }

        public async Task<bool> DeleteEvaluation(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{Version}/Evaluations?id={id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }
        //User

        public async Task<PageReponse<User>> GetUsers(int index=0, int count = 10)
        {
            var users = await _httpClient.GetFromJsonAsync<PageReponse<UserDto>>($"{_httpClient.BaseAddress}api/v{Version}/Users?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<User>(users.nbElement, users.Data.ToModels()));
        }

        public async Task<User?> GetUserById(long id)
        {
            var userById = await _httpClient.GetFromJsonAsync<UserDto>($"{_httpClient.BaseAddress}api/v{Version}/Users/{id}");
            return await Task.FromResult(userById?.ToModel());
        }

        public async Task<User?> PostUser(User user)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Users", user.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var userRep = await reponse.Content.ReadFromJsonAsync<UserDto>();
                return await Task.FromResult(userRep.ToModel());
            }
            return await Task.FromResult<User?>(null);
        }

        public async Task<LoginReponse?> Login(LoginRequest loginRequest)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Users/login", loginRequest.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var userRep = await reponse.Content.ReadFromJsonAsync<LoginResponseDto>();
                return await Task.FromResult(userRep.ToModel());
            }
            return await Task.FromResult<LoginReponse?>(null);
        }

        public async Task<User?> PutUser(long id, User user)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Users/{id}", user.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var userRep = await reponse.Content.ReadFromJsonAsync<UserDto>();
                return await Task.FromResult(userRep.ToModel());
            }
            return await Task.FromResult<User?>(null);
        }

        public async Task<bool> DeleteUser(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{Version}/Users/{id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }

        //Template
        public async Task<PageReponse<Template>> GetTemplatesByUserId(long userId, int index=0, int count = 10)
        {
            var templatesByUserId = await _httpClient.GetFromJsonAsync<PageReponse<TemplateDto>>($"{_httpClient.BaseAddress}api/v{Version}/Templates/user/{userId}?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Template>(templatesByUserId.nbElement, templatesByUserId.Data.ToModels()));
        }

        public async Task<PageReponse<Template>> GetEmptyTemplatesByUserId(long userId, int index=0, int count=10)
        {
            var emptyTemplatesByUserId = await _httpClient.GetFromJsonAsync<PageReponse<TemplateDto>>($"{_httpClient.BaseAddress}api/v{Version}/Templates/user/{userId}/models?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Template>(emptyTemplatesByUserId.nbElement, emptyTemplatesByUserId.Data.ToModels()));
        }

        public async Task<Template?> GetTemplateById(long userId, long templateId)
        {
            var templateById = await _httpClient.GetFromJsonAsync<TemplateDto>($"{_httpClient.BaseAddress}api/v{Version}/Template/{templateId}/user/{userId}");
            return await Task.FromResult(templateById?.ToModel());
        }

        public async Task<Template?> PostTemplate(long userId, Template template)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Templates?userId={userId}", template.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var templateRep = await reponse.Content.ReadFromJsonAsync<TemplateDto>();
                return await Task.FromResult(templateRep.ToModel());
            }
            return await Task.FromResult<Template?>(null);
        }

        public async Task<Template?> PutTemplate(long templateId, Template template)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/v{Version}/Templates?id={templateId}", template.ToDto());
            if (reponse.IsSuccessStatusCode)
            {
                var templateRep = await reponse.Content.ReadFromJsonAsync<TemplateDto>();
                return await Task.FromResult(templateRep.ToModel());
            }
            return await Task.FromResult<Template?>(null);
        }

        public async Task<bool> DeleteTemplate(long templateId)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/v{Version}/Templates?id={templateId}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }
    }
}
