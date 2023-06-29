namespace DemoService.Common.Types;

public record ToDoRelationBase
{
    public string ParentId { get; init; }
    public string ChildId { get; init; }

    protected ToDoRelationBase(string parentId, string childId)
        => (ParentId, ChildId) = (parentId, childId);

    protected ToDoRelationBase() : this(string.Empty, string.Empty) { }
}

public record ToDoRelationWithIdBase : ToDoRelationBase
{
    public virtual string Id { get; set; }

    protected ToDoRelationWithIdBase(string id, string parentId, string childId) : base(string.Empty, string.Empty) => Id = id;
    protected ToDoRelationWithIdBase() : this(string.Empty, string.Empty, string.Empty) { }
}