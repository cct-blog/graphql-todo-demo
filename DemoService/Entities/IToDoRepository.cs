using System.Linq.Expressions;
using DemoService.Entities.Types;

namespace DemoService.Entities;

public interface IToDoRepository
{
    public Task<ToDo?> GetToDo(string id); 
    public Task<ToDo[]> GetToDos(); 
    public Task<ToDo[]> GetToDos(Expression<Func<ToDo, bool>> predicate);
    public Task<ToDo?> CreateToDo(string message, bool isFinished);
    public Task<ToDo?> UpdateToDo(ToDo todo);
    public Task<bool> DeleteToDo(ToDo todo);
}
