namespace DemoService.Common.Types;

public record ToDoBase
{
    public string Message{ get; set; } = string.Empty;

    public bool IsFinished { get; set; }
}

public record ToDoWithIdBase : ToDoBase
{
    public virtual string Id { get; set; } = string.Empty;

    protected ToDoWithIdBase(ToDoWithIdBase source) : base(source) {}
}

