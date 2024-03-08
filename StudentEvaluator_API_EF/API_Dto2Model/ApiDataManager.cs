using API_Dto;
using API_Model;
using Shared;
using System.Net.Http.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace API_Dto2Model
{
    public class ApiDataManager : IStudentService<Student>,IGroupService<Group>
    {
        private readonly HttpClient _httpClient;

        public ApiDataManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Student
        public async Task<bool> DeleteStudent(long id)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/Students?id={id}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);

        }
        public async Task<Student?> GetStudentById(long id)
        {
            var studentById = await _httpClient.GetFromJsonAsync<StudentDto>($"{_httpClient.BaseAddress}api/Students/{id}");
            return await Task.FromResult(studentById?.ToModel());
        }

        public async Task<PageReponse<Student>> GetStudents(int index=0, int count=10)
        {
            var students = await _httpClient.GetFromJsonAsync<PageReponse<StudentDto>>($"{_httpClient.BaseAddress}api/Students?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Student>(students.nbElement,students.Data.ToModels()));
        }

        public async Task<Student?> PostStudent(Student student)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/Students", student.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Student>());
        }

        public async Task<Student?> PutStudent(long id, Student student)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/Students?id={student.Id}", student.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Student>());
        }

        //Group
        public async Task<PageReponse<Group>> GetGroups(int index=0, int count=10)
        {

            var groups = await _httpClient.GetFromJsonAsync<PageReponse<GroupDto>>($"{_httpClient.BaseAddress}api/Groups?index={index}&count={count}");
            return await Task.FromResult(new PageReponse<Group>(groups.nbElement, groups.Data.ToModels()));
        }
        public async Task<Group?> GetGroupByIds(int gyear, int gnumber)
        {
            var groupById = await _httpClient.GetFromJsonAsync<GroupDto>($"{_httpClient.BaseAddress}api/Groups/{gyear}/{gnumber}");
            return await Task.FromResult(groupById?.ToModel());
        }
        public async Task<Group?> PostGroup(Group group)
        {
            var reponse = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}api/Groups", group.ToDto());
            var groupRep = await reponse.Content.ReadFromJsonAsync<GroupDto>();
            return await Task.FromResult(groupRep.ToModel());
        }
        public async Task<Group?> PutGroup(int gyear, int gnumber, Group group)
        {
            var reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}api/Groups?gyear={group.GroupYear}&gnumber={group.GroupNumber}", group.ToDto());
            return await Task.FromResult(await reponse.Content.ReadFromJsonAsync<Group>());
        }
        public async Task<bool> DeleteGroup(int gyear, int gnumber)
        {
            var b = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}api/Groups?gyear={gyear}&gnumber={gnumber}");
            if (b.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else return await Task.FromResult(false);
        }

        //Lesson
        public Task<PageReponseModel<Lesson>> GetLessons(int index=0, int count=10)
        {
            var lessons = await _httpClient.GetFromJsonAsync<PageReponseDto<Lesson>>($"{_httpClient.BaseAddress}api/Lessons?index={index}&count={count}");
            return await Task.FromResult(new PageReponseModel<Lesson>(lessons.nbElement, lessons.Data.ToModels()));
        }

        public Task<Lesson?> GetLessonById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PageReponseModel<Lesson>> GetLessonsByTeacherId(long id, int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<Lesson?> PostLesson(Lesson lesson)
        {
            throw new NotImplementedException();
        }

        public Task<Lesson?> PutLesson(long id, Lesson lesson)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLesson(long id)
        {
            throw new NotImplementedException();
        }
    }
}
