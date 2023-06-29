using DemoService.Interfaces;
using DemoService.Resolvers.Types;

namespace DemoService.Resolvers;

public class Mutation
{
    public async Task<ToDoPayload> CreateToDo(ToDoCreatorInput input, [Service] IToDoService service)
        => await service.CreateToDo(input);

    public async Task<ToDoPayload> UpdateToDo(ToDoUpdaterInput input, [Service] IToDoService service)
        => await service.UpdateToDo(input);

    public async Task<bool> DeleteToDo(string id, [Service] IToDoService service)
        => await service.DeleteToDo(id);

    public async Task<ToDoRelationPayload> CreateToDoRelation(
        ToDoRelationInput input, [Service] IToDoRelationService service)
        => await service.CreateToDoRelasitonAsync(input);

    public async Task<bool> DeleteToDoRelation(
        ToDoRelationInput input, [Service] IToDoRelationService service)
        => await service.DeleteToDoRelasitonAsync(input);
}