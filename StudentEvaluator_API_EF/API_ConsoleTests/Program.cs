using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Client_Model;
using Dto2Model;

HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7140");

var registerData = new
{
    username = "newUser",
    password = "newUserPassword1234$",
};

var registerContent = new StringContent(JsonSerializer.Serialize(registerData), Encoding.UTF8, "application/json");
var registerResponse = await httpClient.PostAsync("/api/v1/Account/register", registerContent);

if (!registerResponse.IsSuccessStatusCode)
{
    Console.WriteLine("Error registering new user. Maybe the user already exists.");
}

var loginData = new
{
    username = "newUser",
    password = "newUserPassword1234$"
};

var loginContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
var loginResponse = await httpClient.PostAsync("/api/v1/Account/login", loginContent);

if (!loginResponse.IsSuccessStatusCode)
{
    Console.WriteLine("Error logging in.");
    return;
}

var loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
string token;
using (var document = JsonDocument.Parse(loginResponseBody))
{
    var root = document.RootElement;
    token = root.GetProperty("token").GetString(); 
}
if (string.IsNullOrEmpty(token))
{
    Console.WriteLine("Error getting token.");
    return;
}

var handler = new JwtSecurityTokenHandler();
var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

var userId = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

httpClient.DefaultRequestHeaders.Authorization = 
    new AuthenticationHeaderValue("Bearer", token);


ApiDataManager apiDataManager = new(httpClient);


//Group 

//GetGroups
Console.WriteLine("Test GetGroups :\n");

var groups = await apiDataManager.GetGroups();
Console.WriteLine($"Nombre d'éléments : {groups.NbElement}");
foreach (var group in groups.Data)
{
    Console.WriteLine(group);
}

//PostGroup

Console.WriteLine("Test PostGroup :\n");

var newGroup = new Group(3, 9, new List<Student>());
newGroup= await apiDataManager.PostGroup(newGroup);

Console.WriteLine(newGroup);

//GetGroupById

Console.WriteLine("Test GetGroupById (gyear =1 & gnumber=1) :\n");
var groupById = await apiDataManager.GetGroupByIds(1, 1);
Console.WriteLine(groupById);

//DeleteGroup

Console.WriteLine("Test DeleteGroup :\n");
if (newGroup != null)
{
    var repDelete = await apiDataManager.DeleteGroup(newGroup.GroupYear, newGroup.GroupNumber);
    Console.WriteLine(repDelete);
}

groups = await apiDataManager.GetGroups();
Console.WriteLine($"Nombre d'éléments : {groups.NbElement}");
foreach (var group in groups.Data)
{
    Console.WriteLine(group);
}

//Students :

//GetStudents :
Console.WriteLine("Test GetStudents :");

var students = await apiDataManager.GetStudents();
Console.WriteLine($"Nombre d'éléments : {students.NbElement}");
foreach (var student in students.Data)
{
    Console.WriteLine(student);
}

//PostStudent :
Console.WriteLine("Test PostStudent :");

var newStudent = new Student(0,"Mathieu", "Berger", "photo", 1, 1);

newStudent = await apiDataManager.PostStudent(newStudent);

if (newStudent != null)
{
    Console.WriteLine(newStudent);
}
else
{
    Console.WriteLine("PostStudent returned null.");
}

//PutStudent :
Console.WriteLine("Test PutStudent :");

if (newStudent != null)
{
    newStudent.Name = "Pierre";
}

if (newStudent != null)
{
    newStudent = await apiDataManager.PutStudent(newStudent.Id, newStudent);
}

Console.WriteLine(newStudent);
//GetStudentById :
Console.WriteLine("Test GetStudentById (id=1):");

var studentById = await apiDataManager.GetStudentById(1);

Console.WriteLine(studentById);

//DeleteStudent :
Console.WriteLine("Test DeleteStudent :");
if (newStudent != null)
{
    var repDelete = await apiDataManager.DeleteStudent(newStudent.Id);
    Console.WriteLine(repDelete);
}

students = await apiDataManager.GetStudents();
Console.WriteLine($"Nombre d'éléments : {students.NbElement}");
foreach (var student in students.Data)
{
    Console.WriteLine(student);
}

//Lesson


//GetLessons
Console.WriteLine("Test GetLessons :\n");

var lessons = await apiDataManager.GetLessons();
Console.WriteLine($"Nombre d'éléments : {lessons.NbElement}");
foreach (var lesson in lessons.Data)
{
    Console.WriteLine(lesson);
}

//PostLesson

Console.WriteLine("Test PostLesson :\n");

var newLesson = new LessonCreation(new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)), new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(17, 0)), "Apprentissage Automatique", "Amphi A", "1", 1, 5);
var lessonReponse = await apiDataManager.PostLesson(newLesson);

Console.WriteLine(lessonReponse);

//PutLesson
Console.WriteLine("Test PutLesson :\n");
newLesson.Classroom = "Amphi B";
if (lessonReponse != null)
{
    lessonReponse = await apiDataManager.PutLesson(lessonReponse.Id, newLesson);
    Console.WriteLine(lessonReponse);
}
//GetLessonById

Console.WriteLine("Test GetLessonById (id=1) :\n");
var lessbyId = await apiDataManager.GetLessonById(1);

Console.WriteLine(lessbyId);

//GetLessonByTeacherId

Console.WriteLine($"Test GetLessonByTeacherId (id={userId}) :\n");

var lessonsByTeacherId=await apiDataManager.GetLessonsByTeacherId(userId);
Console.WriteLine($"Nombre d'éléments : {lessonsByTeacherId.NbElement}");
foreach (var lesson in lessonsByTeacherId.Data)
{
    Console.WriteLine(lesson);
}
//DeleteLesson

Console.WriteLine("Test DeleteLesson :\n");
if (lessonReponse != null)
{
    var repDelete = await apiDataManager.DeleteLesson(lessonReponse.Id);
    Console.WriteLine(repDelete);
}

lessons = await apiDataManager.GetLessons();
Console.WriteLine($"Nombre d'éléments : {lessons.NbElement}");
foreach (var lesson in lessons.Data)
{
    Console.WriteLine(lesson);
}

//Evaluation

//GetEvaluations
Console.WriteLine("Test GetEvaluations :\n");

var evals = await apiDataManager.GetEvaluations();
Console.WriteLine($"Nombre d'éléments : {evals.NbElement}");
foreach (var eval in evals.Data)
{
    Console.WriteLine(eval);
}

//PostEvaluation

Console.WriteLine("Test PostEvaluation :\n");

var newEval = new EvaluationCreation(new DateOnly(2023, 11, 26).ToDateTime(new TimeOnly(15, 0)), "JavaScript", 13, null, userId, 10, 1);
var evalReponse = await apiDataManager.PostEvaluation(newEval);

Console.WriteLine(evalReponse);

//PutEvaluation
Console.WriteLine("Test PutEvaluation :\n");
newEval.CourseName= "PHP";
if (evalReponse != null)
{
    evalReponse = await apiDataManager.PutEvaluation(evalReponse.Id, newEval);
}
Console.WriteLine(evalReponse);
//GetEvaluationById

Console.WriteLine("Test GetEvaluationById (id=1) :\n");
var evalById = await apiDataManager.GetEvaluationById(1);

Console.WriteLine(evalById);

//GetLessonByTeacherId

Console.WriteLine("Test GetEvaluationsByTeacherId (id=1) :\n");

var evalsByTeacherId = await apiDataManager.GetEvaluationsByTeacherId(userId);
Console.WriteLine($"Nombre d'éléments : {evalsByTeacherId.NbElement}");
foreach (var eval in evalsByTeacherId.Data)
{
    Console.WriteLine(eval);
}
//DeleteEvaluation

Console.WriteLine("Test DeleteEvaluation :\n");
if (evalReponse != null)
{
    var repDelete = await apiDataManager.DeleteEvaluation(evalReponse.Id);
    Console.WriteLine(repDelete);
}

evals = await apiDataManager.GetEvaluations();
Console.WriteLine($"Nombre d'éléments : {evals.NbElement}");
foreach (var eval in evals.Data)
{
    Console.WriteLine(eval);
}

//Template

//GetTemplateByUserId
Console.WriteLine("Test GetTemplatesByUserId :\n");

var templates = await apiDataManager.GetTemplatesByUserId(userId);
Console.WriteLine($"Nombre d'éléments : {evals.NbElement}");
foreach (var template in templates.Data)
{
    Console.WriteLine(template);
}

//GetEmptyTemplateByUserId
Console.WriteLine("Test GetEmptyTemplatesByUserId :\n");

var emptyTemplates = await apiDataManager.GetEmptyTemplatesByUserId(userId);
Console.WriteLine($"Nombre d'éléments : {emptyTemplates.NbElement}");
foreach (var template in emptyTemplates.Data)
{
    Console.WriteLine(template);
}

//PostTemplate

Console.WriteLine("Test PostTemplate :\n");

var newTemplate = new Template(0, "Exam de Crypto", new List<Criteria>());
var templateRep = await apiDataManager.PostTemplate(userId, newTemplate);

Console.WriteLine(templateRep);

//PutTemplate
Console.WriteLine("Test PutTemplate :\n");
newTemplate.Name = "Exam de Proba";
if (templateRep != null)
{
    templateRep = await apiDataManager.PutTemplate(templateRep.Id, newTemplate);
}
Console.WriteLine(templateRep);
//GetTemplateById

Console.WriteLine("Test GetEvaluationById ( userid=1 ,templateid=1) :\n");
var templateId = await apiDataManager.GetTemplateById(1);

Console.WriteLine(templateId);


//DeleteTemplate
Console.WriteLine("Test DeleteTemplate :\n");
if (templateRep != null)
{
    var repDelete = await apiDataManager.DeleteTemplate(templateRep.Id);
    Console.WriteLine(repDelete);
}

templates = await apiDataManager.GetEmptyTemplatesByUserId(userId);
Console.WriteLine($"Nombre d'éléments : {templates.NbElement}");
foreach (var template in templates.Data)
{
    Console.WriteLine(template);
}