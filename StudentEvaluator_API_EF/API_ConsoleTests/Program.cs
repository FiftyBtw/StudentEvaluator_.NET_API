
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

var newStudent = new Student(0,"Mathieu", "Berger", "photo", 1, 1);

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


//Group 

//GetGroups
Console.WriteLine("Test GetGroups :\n");

var groups = await apiDataManager.GetGroups();
Console.WriteLine($"Nombre d'éléments : {groups.nbElement}");
foreach (var group in groups.Data)
{
    Console.WriteLine(group);
}

//PostGroup

Console.WriteLine("Test PostGroup :\n");

var newGroup = new Group(3, 9, new List<Student>());
newGroup= await apiDataManager.PostGroup(newGroup);

Console.WriteLine(newGroup);

//PutGroup


//GetGroupById

Console.WriteLine("Test GetGroupById (gyear =1 & gnumber=1) :\n");
var groupById = await apiDataManager.GetGroupByIds(1, 1);
Console.WriteLine(groupById);

//DeleteGroup

Console.WriteLine("Test DeleteGroup :\n");

 repDelete = await apiDataManager.DeleteGroup(newGroup.GroupYear, newGroup.GroupNumber);
Console.WriteLine(repDelete);

groups = await apiDataManager.GetGroups();
Console.WriteLine($"Nombre d'éléments : {groups.nbElement}");
foreach (var group in groups.Data)
{
    Console.WriteLine(group);
}
