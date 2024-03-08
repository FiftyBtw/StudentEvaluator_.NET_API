using API_Model;
using EF_DbContextLib;
using EF_StubbedContextLib;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Model2Entities
{
    public class DbEntitiesManager : IStudentService<Student>, IGroupService<Group>
    {
        private readonly LibraryContext _libraryContext;

        public DbEntitiesManager(StubbedContext context)
        {
            _libraryContext = context;
        }

        //Student 
        public Task<bool> DeleteStudent(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);
            if (student == null) return Task.FromResult(false);

            _libraryContext.StudentSet.Where(b => b.Id == id).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            student = _libraryContext.StudentSet.FirstOrDefault(b => b.Id == id);

            if (student == null) return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public async Task<Student?> GetStudentById(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(s => s.Id == id)?.ToModel();
            return await Task.FromResult(student);
        }

        public async Task<PageReponse<Student>> GetStudents(int index, int count)
        {
            var students = _libraryContext.StudentSet.ToModels();
            return await Task.FromResult(new PageReponse<Student>(students.Count(), students.Skip(index * count).Take(count)));
        }

        public async Task<Student?> PostStudent(Student student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.StudentSet.FirstOrDefault(s => s.Name.Equals(student.Name) && s.Lastname.Equals(student.Lastname))?.ToModel());
        }

        public async Task<Student?> PutStudent(long id, Student student)
        {
            throw new NotImplementedException();
        }

        //Group
        public async Task<PageReponse<Group>> GetGroups(int index, int count)
        {
            var groups = _libraryContext.GroupSet.Include(g => g.Students).ToModels();
            return await Task.FromResult(new PageReponse<Group>(groups.Count(), groups.Skip(index * count).Take(count)));
        }

        public async Task<Group?> GetGroupByIds(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.Include(g => g.Students).FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber)
                ?.ToModel();
            return await Task.FromResult(group);
        }
        public async Task<Group?> PostGroup(Group group)
        {
            var groupTest = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber);
            if (groupTest != null) return await Task.FromResult(groupTest.ToModel());
            _libraryContext.GroupSet.AddAsync(group.ToEntity());
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber)?.ToModel());
        }

        public async Task<Group?> PutGroup(int gyear, int gnumber, Group newGroup)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);
            if (group == null) return await Task.FromResult<Group?>(null);
            group.GroupNumber = newGroup.GroupNumber;
            group.GroupYear = newGroup.GroupYear;
            //group.Students = newGroup.Students;
            _libraryContext.SaveChanges();
            return await Task.FromResult(group.ToModel());
        }

        public async Task<bool> DeleteGroup(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);
            if (group == null) return await Task.FromResult(false);

            _libraryContext.GroupSet.Where(g => g.GroupYear == gyear && g.GroupNumber == gnumber).ExecuteDelete();
            _libraryContext.SaveChangesAsync();

            group = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber);

            if (group == null) return await Task.FromResult(true);

            return await Task.FromResult(false);
        }
    }
    
}
