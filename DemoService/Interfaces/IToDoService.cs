using DemoService.Resolvers.Types;

namespace DemoService.Interfaces;

public interface IToDoService
{
    public Task<ToDoPayload> GetToDo(string id);
    public Task<ToDoPayload[]> GetToDos();

    public Task<ToDoPayload> CreateToDo(ToDoCreatorInput input);
    public Task<ToDoPayload> UpdateToDo(ToDoUpdaterInput input);
    public Task<bool> DeleteToDo(string id);
}