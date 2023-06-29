using System.Diagnostics.CodeAnalysis;
using DemoService.Entities;
using DemoService.Entities.Types;
using DemoService.Interfaces;
using DemoService.Resolvers.Types;

namespace DemoService.Services;

public class ToDoService : IToDoService
{
    private readonly ToDoRepository _toDoRepository;
    private readonly ToDoRelationRepository _relationRepository;

    private bool TryToPayload(ToDo? entity, [NotNullWhen(true)]out ToDoPayload? payload)
        => (payload = entity is null ? null : ToDoPayload.FromEntity(entity)) is not null;

    public ToDoService(ToDoDbContext context) 
        => (_toDoRepository, _relationRepository)
            = (new ToDoRepository(context), new ToDoRelationRepository(context));


    public async Task<ToDoPayload> GetToDo(string id)
        => TryToPayload(await _toDoRepository.GetToDo(id), out var payload)
            ? payload
            : throw new GraphQLException("invalid id.");

    public async Task<ToDoPayload[]> GetToDos()
        => (await _toDoRepository.GetToDos()).Select(ToDoPayload.FromEntity).ToArray();

    public async Task<ToDoPayload> CreateToDo(ToDoCreatorInput input)
        => TryToPayload(await _toDoRepository.CreateToDo(input.Message, input.IsFinished), out var payload)
            ? payload
            : throw new GraphQLException("failed to create.");

    public async Task<ToDoPayload> UpdateToDo(ToDoUpdaterInput input)
        => TryToPayload(await _toDoRepository.UpdateToDo(ToDo.Copy(input)), out var payload)
            ? payload
            : throw new GraphQLException("failed to create.");

    public async Task<bool> DeleteToDo(string id)
    {
        var toDo = await _toDoRepository.GetToDo(id);
        if (toDo is null) throw new GraphQLException("invalid id.");
        if (await _relationRepository.HasRelations(toDo))
            throw new GraphQLException("todo has relations.");
        return await _toDoRepository.DeleteToDo(toDo);
    }
}
