using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace UnitOfWork;

public class UnitOfWork : IDisposable
{
    public UnitOfWork(DbContextOptions<LibraryContext> options)
        : this(new LibraryContext(options))
    {
    }

    public UnitOfWork(LibraryContext context)
    {
        Context = context;

        Context.Database.EnsureCreated();
    }

    public UnitOfWork()
        : this(new LibraryContext())
    {
    }

    private LibraryContext Context { get; }
    
    public GroupRepository GroupsRepository
    {
        get
        {
            if(_groupsRepository == null)
            {
                _groupsRepository = new GroupRepository(Context);
            }
            return _groupsRepository;
        }
    }

    private GroupRepository? _groupsRepository;
    
    public GenericRepository<StudentEntity> StudentsRepository
    {
        get
        {
            if(_studentsRepository == null)
            {
                _studentsRepository = new GenericRepository<StudentEntity>(Context);
            }
            return _studentsRepository;
        }
    }
    
    private GenericRepository<StudentEntity>? _studentsRepository;
    
    public GenericRepository<TeacherEntity> TeachersRepository
    {
        get
        {
            if(_teachersRepository == null)
            {
                _teachersRepository = new GenericRepository<TeacherEntity>(Context);
            }
            return _teachersRepository;
        }
    }
    
    private GenericRepository<TeacherEntity>? _teachersRepository;
    
    public GenericRepository<TemplateEntity> TemplatesRepository
    {
        get
        {
            if(_templatesRepository == null)
            {
                _templatesRepository = new GenericRepository<TemplateEntity>(Context);
            }
            return _templatesRepository;
        }
    }
    
    private GenericRepository<TemplateEntity>? _templatesRepository;
    
    public GenericRepository<EvaluationEntity> EvaluationsRepository
    {
        get
        {
            if(_evaluationsRepository == null)
            {
                _evaluationsRepository = new GenericRepository<EvaluationEntity>(Context);
            }
            return _evaluationsRepository;
        }
    }
    
    private GenericRepository<EvaluationEntity>? _evaluationsRepository;
    
    public GenericRepository<LessonEntity> LessonsRepository
    {
        get
        {
            if(_lessonsRepository == null)
            {
                _lessonsRepository = new GenericRepository<LessonEntity>(Context);
            }
            return _lessonsRepository;
        }
    }
    
    private GenericRepository<LessonEntity>? _lessonsRepository;
    
    public GenericRepository<CriteriaEntity> CriteriasRepository
    {
        get
        {
            if(_criteriasRepository == null)
            {
                _criteriasRepository = new GenericRepository<CriteriaEntity>(Context);
            }
            return _criteriasRepository;
        }
    }
    
    private GenericRepository<CriteriaEntity>? _criteriasRepository;

    
    private async Task RejectChangesAsync()
    {
        foreach (var entry in Context.ChangeTracker.Entries()
                     .Where(e => e.State != EntityState.Unchanged))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    await entry.ReloadAsync();
                    break;
            }
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        int result = 0;
        try
        {
            result = await Context.SaveChangesAsync();
        }
        catch
        {
            await RejectChangesAsync();
            return -1;
        }
        foreach (var entity in Context.ChangeTracker.Entries()
                     .Where(e => e.State != EntityState.Detached))
        {
            entity.State = EntityState.Detached;
        }
        return result;
    }

    private bool _disposed = false;

    protected virtual async Task Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            await Context.DisposeAsync();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}