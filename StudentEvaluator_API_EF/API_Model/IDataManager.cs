using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Model
{
    public interface IDataManager
    {
        //Student
        public Task<PageReponseModel<Student>> GetStudents(int index, int count);
        public Task<Student?> GetStudentById(long id);
        public Task<Student?> PostStudent(Student student);
        public Task<Student?> PutStudent(long id, Student student);
        public Task<bool> DeleteStudent(long id);
        //Group
        public Task<PageReponseModel<Group>> GetGroups(int index, int count);
        public Task<Group?> GetGroupByIds(int gyear, int gnumber);
        public Task<Group?> PostGroup(Group group);
        public Task<Group?> PutGroup(int gyear, int gnumber, Group group);
        public Task<bool> DeleteGroup(int gyear, int gnumber);
        //Lesson
        public Task<PageReponseModel<Lesson>> GetLessons(int index, int count);
        public Task<Lesson?> GetLessonById(long id);
        public Task<PageReponseModel<Lesson>> GetLessonsByTeacherId(long id, int index, int count);
        public Task<Lesson?> PostLesson(Lesson lesson);
        public Task<Lesson?> PutLesson(long id, Lesson lesson);
        public Task<bool> DeleteLesson(long id);
    }
}
