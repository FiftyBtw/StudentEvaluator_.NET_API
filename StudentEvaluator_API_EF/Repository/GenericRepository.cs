using System.Linq.Expressions;
using EF_DbContextLib;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class GenericRepository <TEntity> where TEntity  :  class
{
    private  LibraryContext Context { get; set; }
    private DbSet<TEntity> Set { get; set; }
    
    public GenericRepository(LibraryContext context)
    {
        this.Context = context;
        this.Set = context.Set<TEntity>();
    }
    
    public async Task<TEntity>  GetByID(object id)
    {
        return await Set.FindAsync(id);
    }

    public virtual async  Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        int index = 0,
        int count = 10)
    {
        IQueryable<TEntity> query = Set;

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
    
    public virtual async  Task Insert(TEntity entity)
    {
        await Set.AddAsync(entity);
    }

    public async Task Delete(object id)
    {
        TEntity entityToDelete = await Set.FindAsync(id);
        Delete(entityToDelete);
    }

    public virtual  Task Delete(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            Set.Attach(entityToDelete);
        }
        Set.Remove(entityToDelete);
        return Task.CompletedTask;
    }

    public virtual  Task  Update(TEntity entityToUpdate)
    {
        Set.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}