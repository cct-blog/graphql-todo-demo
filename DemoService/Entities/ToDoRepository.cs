using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DemoService.Entities.Types;

namespace DemoService.Entities;

public class ToDoRepository : RepositoryBase<ToDo>, IToDoRepository
{
    public ToDoRepository(ToDoDbContext context) : base(context) { }

    public async Task<ToDo?> GetToDo(string id)
        => await Context.ToDos.FirstOrDefaultAsync(each => each.Id == id);

    public async Task<ToDo[]> GetToDos()
        => await Context.ToDos.ToArrayAsync();

    public async Task<ToDo[]> GetToDos(Expression<Func<ToDo, bool>> predicate)
        => await Context.ToDos.Where(predicate).ToArrayAsync();

    public async Task<ToDo?> CreateToDo(string message, bool isFinished)
        => (await ExecuteChange(
            async () => await Context.ToDos.AddAsync(ToDo.NewToDo(message, isFinished)))).Entity;

    public async Task<ToDo?> UpdateToDo(ToDo todo)
        => (await ExecuteChange(() => Context.ToDos.Update(todo))).Entity;

    public async Task<bool> DeleteToDo(ToDo todo)
        => (await ExecuteChange(() => Context.ToDos.Remove(todo))).State == EntityState.Deleted;
}