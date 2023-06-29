using DemoService.Common.Types;

namespace DemoService.Entities.Types;

public record ToDoRelation : ToDoRelationWithIdBase
{
    protected ToDoRelation(string id, string parentId, string childId)
        : base(id, parentId, childId) { }
    protected ToDoRelation(ToDoRelationWithIdBase source) : base(source) { }
    public ToDoRelation()
        : this(Ulid.Empty.ToString(), Ulid.Empty.ToString(), Ulid.Empty.ToString()) { }
    public static ToDoRelation Copy(ToDoRelation source)
        => new ToDoRelation(source);
    public static ToDoRelation ToEntity(ToDoRelationWithIdBase source)
        => new ToDoRelation(source);
    public static ToDoRelation NewToDoRelation(ToDo parent, ToDo child)
        => new ToDoRelation(Ulid.NewUlid().ToString(), parent.Id, child.Id);
}
