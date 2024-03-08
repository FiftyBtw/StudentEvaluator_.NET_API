using API_Model;
using EF_DbContextLib;
using EF_StubbedContextLib;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Model2Entities
{
    /// <summary>
    /// Manages database entities for student and group operations.
    /// </summary>
    public class DbEntitiesManager : IStudentService<Student>, IGroupService<Group>
    {
        private readonly LibraryContext _libraryContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="DbEntitiesManager"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DbEntitiesManager(StubbedContext context)
        {
            _libraryContext = context;
        }

        //Student 


        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
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


        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved student, or null if not found.</returns>
        public async Task<Student?> GetStudentById(long id)
        {
            var student = _libraryContext.StudentSet.FirstOrDefault(s => s.Id == id)?.ToModel();
            return await Task.FromResult(student);
        }


        /// <summary>
        /// Retrieves a page of students.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of students per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a page of students.</returns>
        public async Task<PageReponse<Student>> GetStudents(int index, int count)
        {
            var students = _libraryContext.StudentSet.ToModels();
            return await Task.FromResult(new PageReponse<Student>(students.Count(), students.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="student">The student to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added student.</returns>
        public async Task<Student?> PostStudent(Student student)
        {
            _libraryContext.StudentSet.AddAsync(student.ToEntity());
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.StudentSet.FirstOrDefault(s => s.Name.Equals(student.Name) && s.Lastname.Equals(student.Lastname))?.ToModel());
        }


        /// <summary>
        /// Updates a student.
        /// </summary>
        /// <param name="id">The ID of the student to update.</param>
        /// <param name="student">The updated student.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated student.</returns>
        public async Task<Student?> PutStudent(long id, Student student)
        {
            throw new NotImplementedException();
        }

        //Group


        /// <summary>
        /// Retrieves a page of groups.
        /// </summary>
        /// <param name="index">The index of the page.</param>
        /// <param name="count">The number of groups per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a page of groups.</returns>
        public async Task<PageReponse<Group>> GetGroups(int index, int count)
        {
            var groups = _libraryContext.GroupSet.Include(g => g.Students).ToModels();
            return await Task.FromResult(new PageReponse<Group>(groups.Count(), groups.Skip(index * count).Take(count)));
        }


        /// <summary>
        /// Retrieves a group by its year and number.
        /// </summary>
        /// <param name="gyear">The year of the group.</param>
        /// <param name="gnumber">The number of the group.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved group, or null if not found.</returns>
        public async Task<Group?> GetGroupByIds(int gyear, int gnumber)
        {
            var group = _libraryContext.GroupSet.Include(g => g.Students).FirstOrDefault(g => g.GroupYear == gyear && g.GroupNumber == gnumber)
                ?.ToModel();
            return await Task.FromResult(group);
        }


        /// <summary>
        /// Adds a new group.
        /// </summary>
        /// <param name="group">The group to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added group.</returns>
        public async Task<Group?> PostGroup(Group group)
        {
            var groupTest = _libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber);
            if (groupTest != null) return await Task.FromResult(groupTest.ToModel());
            _libraryContext.GroupSet.AddAsync(group.ToEntity());
            _libraryContext.SaveChanges();
            return await Task.FromResult(_libraryContext.GroupSet.FirstOrDefault(g => g.GroupYear == group.GroupYear && g.GroupNumber == group.GroupNumber)?.ToModel());
        }


        /// <summary>
        /// Updates a group.
        /// </summary>
        /// <param name="gyear">The year of the group to update.</param>
        /// <param name="gnumber">The number of the group to update.</param>
        /// <param name="newGroup">The updated group.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated group.</returns>
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


        /// <summary>
        /// Deletes a group by its year and number.
        /// </summary>
        /// <param name="gyear">The year of the group to delete.</param>
        /// <param name="gnumber">The number of the group to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
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
