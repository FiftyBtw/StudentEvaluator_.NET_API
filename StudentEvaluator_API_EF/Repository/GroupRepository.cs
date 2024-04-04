using System.Linq.Expressions;
using EF_DbContextLib;
using EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class GroupRepository
{
    private LibraryContext Context { get; set; }
    private DbSet<GroupEntity> Set { get; set; }
    
    public GroupRepository(LibraryContext context)
    {
        this.Context = context;
        this.Set = context.Set<GroupEntity>();
    }
    
    public async Task<GroupEntity>  GetById(IEnumerable<Expression<Func<GroupEntity, object>>> includes, params object[] keyValues)
    {
        IQueryable<GroupEntity> query = Set;

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query
            .FirstOrDefaultAsync(g => g.GroupYear == (int)keyValues[0] && g.GroupNumber == (int)keyValues[1]);
    }
    
    public virtual async  Task<IEnumerable<GroupEntity>> Get(
        Expression<Func<GroupEntity, bool>> filter = null,
        Func<IQueryable<GroupEntity>, IOrderedQueryable<GroupEntity>> orderBy = null,
        string includeProperties = "",
        int index = 0,
        int count = 10)
    {
        IQueryable<GroupEntity> query = Set;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await  query.ToListAsync();
        }
    }
    
    public virtual async  Task Insert(GroupEntity entity)
    {
        await Set.AddAsync(entity);
    }
    
    public async Task Delete(params object[] keyValues)
    {
        GroupEntity entityToDelete = await Set.FindAsync(keyValues);
        Set.Remove(entityToDelete);
    }
    
}