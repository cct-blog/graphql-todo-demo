using DemoService.Common.Types;

namespace DemoService.Resolvers.Types;

public record ToDoGraphBase : ToDoWithIdBase
{
    [ID]
    public override string Id { get; set; }

    protected ToDoGraphBase() : base() => Id = string.Empty;
    protected ToDoGraphBase(ToDoWithIdBase source) : base(source) => Id = source.Id;
}
public record ToDoCreatorInput : ToDoBase { } // IDなし

public record ToDoUpdaterInput : ToDoGraphBase { }

public record ToDoPayload : ToDoGraphBase
{
    protected ToDoPayload(ToDoWithIdBase source) : base(source) {}
    public static ToDoPayload FromEntity(ToDoWithIdBase source)
        => new ToDoPayload(source);
}
