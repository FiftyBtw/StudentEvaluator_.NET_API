using API_Dto;
using API_Model;
using System.Net.Http.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace API_Dto2Model
{
    public class ApiDataManager : IDataManager
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

        public async Task<PageReponseModel<Student>> GetStudents(int index=0, int count=10)
        {
            var students = await _httpClient.GetFromJsonAsync<PageReponseDto<StudentDto>>($"{_httpClient.BaseAddress}api/Students?index={index}&count={count}");
            return await Task.FromResult(new PageReponseModel<Student>(students.nbElement,students.Data.ToModels()));
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
    }
}
