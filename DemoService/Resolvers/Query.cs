using DemoService.Interfaces;
using DemoService.Resolvers.Types;

namespace DemoService.Resolvers;

public class Query
{
    public async Task<ToDoPayload> ToDo(string id, [Service] IToDoService service)
        => await service.GetToDo(id);

    public async Task<ToDoPayload[]> ToDos([Service] IToDoService service) 
        => await service.GetToDos();

    public async Task<ToDoRelationPayload> ToDoRelation(
        ToDoRelationInput input, [Service] IToDoRelationService service)
        => await service.GetToDoRelationAsync(input);

    public async Task<string[]> ParentIds(string childId, [Service] IToDoRelationService service)
        => await service.GetParentsAsync(childId);

    public async Task<string[]> ChildrenIds(string parentId, [Service] IToDoRelationService service)
        => await service.GetChildrenAsync(parentId);
}
