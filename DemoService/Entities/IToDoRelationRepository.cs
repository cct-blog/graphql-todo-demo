using DemoService.Entities.Types;

namespace DemoService.Entities;

public interface IToDoRelationRepository
{
    public Task<ToDoRelation?> GetRelationAsync(ToDo parent, ToDo child);
    public Task<ToDoRelation[]> GetChildrenAsync(ToDo parent);
    public Task<ToDoRelation[]> GetParentsAsync(ToDo child);
    public Task<bool> HasRelations(ToDo toDo);
    public Task<ToDoRelation?> CreateRelationAsync(ToDo parent, ToDo child);
    public Task<bool> DeleteRelationAsync(ToDoRelation relation); 
}