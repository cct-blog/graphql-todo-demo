using DemoService.Interfaces;
using DemoService.Resolvers.Types;

namespace DemoService.Resolvers;

[ExtendObjectType(typeof(ToDoPayload))]
public class ToDoPayloadExtend
{
    public async Task<IEnumerable<string>> GetChildren(
        [Parent] ToDoPayload toDo, [Service] IToDoRelationService service)
        => await service.GetChildrenAsync(toDo.Id); 

    public async Task<IEnumerable<string>> GetParents(
        [Parent] ToDoPayload toDo, [Service] IToDoRelationService service)
        => await service.GetParentsAsync(toDo.Id); 
}
