using DemoService.Common.Types;

namespace DemoService.Resolvers.Types;

public abstract record ToDoRelationWithAttributeBase : ToDoRelationWithIdBase
{
    [ID]
    public override string Id { get; set; }

    protected ToDoRelationWithAttributeBase() : base() => Id = string.Empty;
    protected ToDoRelationWithAttributeBase(ToDoRelationWithIdBase source) : base(source) => Id = source.Id;
}

public record ToDoRelationInput : ToDoRelationWithIdBase { }

public record ToDoRelationPayload : ToDoRelationWithAttributeBase
{
    public ToDoRelationPayload(ToDoRelationWithIdBase source) : base(source) { }
    public static ToDoRelationPayload FromEntity(ToDoRelationWithIdBase source)
        => new ToDoRelationPayload(source);
}
