using DemoService.Common.Types;

namespace DemoService.Entities.Types;

public record ToDo : ToDoWithIdBase
{
    protected ToDo(Ulid id, string message, bool isFinished)
        => (Id, Message, IsFinished) = (id.ToString(), message, isFinished);
    public ToDo()
        : this(Ulid.Empty, string.Empty, false) { }
    protected ToDo(ToDoWithIdBase source) : base(source) { }
    public static ToDo Copy(ToDoWithIdBase source)
        => new ToDo(source);
    public static ToDo NewToDo(string message, bool isFinished = false)
        => new ToDo(Ulid.NewUlid(), message, isFinished);
}
