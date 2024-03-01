
using API_Dto;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using API_Dto2Model;
using API_Model;

HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7140");
ApiDataManager apiDataManager = new(httpClient);


//Students :

//GetStudents :
Console.WriteLine("Test GetStudents :");

var students = await apiDataManager.GetStudents();
Console.WriteLine($"Nombre d'éléments : {students.nbElement}");
foreach (var student in students.Data)
{
    Console.WriteLine(student);
}

//PostStudent :
Console.WriteLine("Test PostStudent :");

var newStudent = new Student
{
    Name="Mathieu",
    Lastname="Berger",
    UrlPhoto="photo",
    GroupNumber=1,
    GroupYear=1,
};


newStudent = await apiDataManager.PostStudent(newStudent);

Console.WriteLine(newStudent);
//PutStudent :
Console.WriteLine("Test PutStudent :");

newStudent.Name = "Pierre";


newStudent = await apiDataManager.PutStudent(newStudent.Id, newStudent);


Console.WriteLine(newStudent);
//GetStudentById :
Console.WriteLine("Test GetStudentById (id=1):");

var studentById = await apiDataManager.GetStudentById(1);

Console.WriteLine(studentById);

//DeleteStudent :
Console.WriteLine("Test DeleteStudent :");

var repDelete = await apiDataManager.DeleteStudent(newStudent.Id);
Console.WriteLine(repDelete);

students = await apiDataManager.GetStudents();
Console.WriteLine($"Nombre d'éléments : {students.nbElement}");
foreach (var student in students.Data)
{
    Console.WriteLine(student);
}


