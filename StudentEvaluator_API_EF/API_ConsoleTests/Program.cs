
using API_Dto;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
HttpClient _httpClient = new HttpClient();


//Students :
_httpClient.BaseAddress = new Uri("https://localhost:7140/api/Students");

//GetStudents :
Console.WriteLine("Test GetStudents :");

var students = await _httpClient.GetFromJsonAsync<PageReponseDto<StudentDto>>(_httpClient.BaseAddress);
Console.WriteLine($"Nombre d'éléments : {students.nbElement}");
foreach (var student in students.Data)
{
    Console.WriteLine(student);
}

//PostStudent :
Console.WriteLine("Test PostStudent :");

var newStudent = new StudentDto
{
    Name="Mathieu",
    Lastname="Berger",
    UrlPhoto="photo",
    GroupNumber=1,
    GroupYear=1,
};

var reponse = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, newStudent);
newStudent = await reponse.Content.ReadFromJsonAsync<StudentDto>();

Console.WriteLine(newStudent);
//PutStudent :
Console.WriteLine("Test PutStudent :");

newStudent.Name = "Pierre";

reponse = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}?id={newStudent.Id}",newStudent);
newStudent = await reponse.Content.ReadFromJsonAsync<StudentDto>();


Console.WriteLine(newStudent);
//GetStudentById :
Console.WriteLine("Test GetStudentById (id=1):");

var studentById = await _httpClient.GetFromJsonAsync<StudentDto>($"{_httpClient.BaseAddress}/{1}");

Console.WriteLine(studentById);

//DeleteStudent :
Console.WriteLine("Test DeleteStudent :");

var repDelete = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}?id={newStudent?.Id}");
Console.WriteLine(repDelete.IsSuccessStatusCode);

students = await _httpClient.GetFromJsonAsync<PageReponseDto<StudentDto>>(_httpClient.BaseAddress);
Console.WriteLine($"Nombre d'éléments : {students.nbElement}");
foreach (var student in students.Data)
{
    Console.WriteLine(student);
}


