using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoService.Entities;

public abstract class RepositoryBase<TEntity>
    where TEntity : class
{
    protected ToDoDbContext Context { get; }

    protected RepositoryBase(ToDoDbContext context) => Context = context;

    protected async Task<EntityEntry<TEntity>> ExecuteChange(Func<Task<EntityEntry<TEntity>>> func)
    {
        var result = await func();
        await Context.SaveChangesAsync();
        return result;
    }  

    protected async Task<EntityEntry<TEntity>> ExecuteChange(Func<EntityEntry<TEntity>> func)
    {
        var result = func();
        await Context.SaveChangesAsync();
        return result;
    }  
}
