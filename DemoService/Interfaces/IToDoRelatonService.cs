using DemoService.Resolvers.Types;

namespace DemoService.Interfaces;

public interface IToDoRelationService
{
    public Task<string[]> GetChildrenAsync(string id);
    public Task<string[]> GetParentsAsync(string id);
    public Task<ToDoRelationPayload> GetToDoRelationAsync(ToDoRelationInput input);
    public Task<ToDoRelationPayload> CreateToDoRelasitonAsync(ToDoRelationInput input);
    public Task<bool> DeleteToDoRelasitonAsync(ToDoRelationInput input);
}