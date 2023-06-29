using System.Diagnostics.CodeAnalysis;

using DemoService.Entities;
using DemoService.Entities.Types;
using DemoService.Interfaces;
using DemoService.Resolvers.Types;

namespace DemoService.Services;

public class ToDoRelationService : IToDoRelationService
{
    private readonly ToDoRepository _toDoRepository;
    private readonly ToDoRelationRepository _relationRepository;

    private ToDoRelationPayload ToPayload(ToDoRelation? entity)
        => ToDoRelationPayload.FromEntity(entity ?? throw new GraphQLException("todo relation not found."));

    private async Task<ToDo> GetToDoAsync(string id) => (await _toDoRepository.GetToDo(id))
        ?? throw new GraphQLException("todo not found.");

    private async Task<ToDoRelation> GetToDoRelationAsync(string parentId, string childId)
        => (await _relationRepository.GetRelationAsync(
                await GetToDoAsync(parentId),
                await GetToDoAsync(childId)))
                ?? throw new GraphQLException("todo relation not found.");

    public ToDoRelationService(ToDoDbContext context) 
        => (_toDoRepository, _relationRepository) 
            = (new ToDoRepository(context), new ToDoRelationRepository(context));

    public async Task<string[]> GetChildrenAsync(string parentId)
        => (await _relationRepository.GetChildrenAsync(await GetToDoAsync(parentId)))
            .Select(each => each.ChildId).ToArray();

    public async Task<string[]> GetParentsAsync(string childId)
        => (await _relationRepository.GetParentsAsync(await GetToDoAsync(childId)))
            .Select(each => each.ParentId).ToArray();

    public async Task<ToDoRelationPayload> GetToDoRelationAsync(ToDoRelationInput input)
        => ToPayload(await GetToDoRelationAsync(input.ParentId, input.ChildId));

    public async Task<ToDoRelationPayload> CreateToDoRelasitonAsync(ToDoRelationInput input)
    {
        var parent = await GetToDoAsync(input.ParentId);
        var child = await GetToDoAsync(input.ChildId);
        return ToPayload((await _relationRepository.GetRelationAsync(parent, child)) 
            ?? (await _relationRepository.CreateRelationAsync(parent, child)));
    }

    public async Task<bool> DeleteToDoRelasitonAsync(ToDoRelationInput input)
        => await _relationRepository.DeleteRelationAsync(
            await GetToDoRelationAsync(input.ParentId, input.ChildId));
}