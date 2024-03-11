
using API_Dto;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using API_Dto2Model;
using API_Model;
using Client_Model;
using EF_Entities;

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

//Lesson


//GetLessons
Console.WriteLine("Test GetLessons :\n");

var lessons = await apiDataManager.GetLessons();
Console.WriteLine($"Nombre d'éléments : {lessons.nbElement}");
foreach (var lesson in lessons.Data)
{
    Console.WriteLine(lesson);
}

//PostLesson

Console.WriteLine("Test PostLesson :\n");

var newLesson = new LessonCreation(new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)), new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(17, 0)), "Apprentissage Automatique", "Amphi A", 1, 1, 5);
var lessonReponse = await apiDataManager.PostLesson(newLesson);

Console.WriteLine(lessonReponse);

//PutLesson
Console.WriteLine("Test PutLesson :\n");
newLesson.Classroom = "Amphi B";
lessonReponse = await apiDataManager.PutLesson(lessonReponse.Id, newLesson);

Console.WriteLine(lessonReponse);
//GetLessonById

Console.WriteLine("Test GetLessonById (id=1) :\n");
var lessbyId = await apiDataManager.GetLessonById(1);

Console.WriteLine(lessbyId);

//GetLessonByTeacherId

Console.WriteLine("Test GetLessonByTeacherId (id=1) :\n");

var lessonsByTeacherId=await apiDataManager.GetLessonsByTeacherId(1);
Console.WriteLine($"Nombre d'éléments : {lessonsByTeacherId.nbElement}");
foreach (var lesson in lessonsByTeacherId.Data)
{
    Console.WriteLine(lesson);
}
//DeleteLesson

Console.WriteLine("Test DeleteLesson :\n");

repDelete = await apiDataManager.DeleteLesson(lessonReponse.Id);
Console.WriteLine(repDelete);

lessons = await apiDataManager.GetLessons();
Console.WriteLine($"Nombre d'éléments : {lessons.nbElement}");
foreach (var lesson in lessons.Data)
{
    Console.WriteLine(lesson);
}

//Evaluation

//GetEvaluations
Console.WriteLine("Test GetEvaluations :\n");

var evals = await apiDataManager.GetEvaluations();
Console.WriteLine($"Nombre d'éléments : {evals.nbElement}");
foreach (var eval in evals.Data)
{
    Console.WriteLine(eval);
}

//PostEvaluation

Console.WriteLine("Test PostEvaluation :\n");

var newEval = new EvaluationCreation(new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)), "JavaScript", 13, null, 1, 10, 1);
var evalReponse = await apiDataManager.PostEvaluation(newEval);

Console.WriteLine(evalReponse);

//PutEvaluation
Console.WriteLine("Test PutEvaluation :\n");
newEval.CourseName= "PHP";
evalReponse = await apiDataManager.PutEvaluation(evalReponse.Id, newEval);

Console.WriteLine(evalReponse);
//GetEvaluationById

Console.WriteLine("Test GetEvaluationById (id=1) :\n");
var evalById = await apiDataManager.GetEvaluationById(1);

Console.WriteLine(evalById);

//GetLessonByTeacherId

Console.WriteLine("Test GetEvaluationsByTeacherId (id=1) :\n");

var evalsByTeacherId = await apiDataManager.GetEvaluationsByTeacherId(1);
Console.WriteLine($"Nombre d'éléments : {evalsByTeacherId.nbElement}");
foreach (var eval in evalsByTeacherId.Data)
{
    Console.WriteLine(eval);
}
//DeleteEvalation

Console.WriteLine("Test DeleteEvaluation :\n");

repDelete = await apiDataManager.DeleteEvaluation(lessonReponse.Id);
Console.WriteLine(repDelete);

evals = await apiDataManager.GetEvaluations();
Console.WriteLine($"Nombre d'éléments : {evals.nbElement}");
foreach (var eval in evals.Data)
{
    Console.WriteLine(eval);
}