using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DemoService.Entities;

[TestClass]
public class DemoRepositoryTest
{
    private readonly IToDoRepository _repository; 

    public DemoRepositoryTest()
    {
        var builder = new DbContextOptionsBuilder<ToDoDbContext>();
        builder.UseInMemoryDatabase("test");
        var context = new ToDoDbContext(builder.Options);
        _repository = new ToDoRepository(context);
    }

    private static void ThrowWhenNull([DoesNotReturnIf(true)] bool isNull)
    {
        if (isNull) throw new NullReferenceException();
    }

    [TestMethod]
    public async Task CreateAndReadToDoTest()
    {
        var newToDo = await _repository.CreateToDo("Test Message 1");
        ThrowWhenNull(newToDo is null);
        var toDo = await _repository.GetToDo(newToDo.Id);
        ThrowWhenNull(toDo is null);
        toDo.Message.Should().Be("Test Message 1");
    }

    [TestMethod]
    public async Task CreateAndUpdateAndReadToDoTest()
    {
        var newToDo = await _repository.CreateToDo("Test Message 2");
        ThrowWhenNull(newToDo is null);
        var toDo1 = await _repository.GetToDo(newToDo.Id);
        ThrowWhenNull(toDo1 is null);
        toDo1.Message.Should().Be("Test Message 2");
        newToDo.Message = "Test Message 3";
        var updatedToDo = await _repository.UpdateToDo(newToDo);
        ThrowWhenNull(updatedToDo is null);
        (updatedToDo.Id == newToDo.Id).Should().BeTrue();
        (updatedToDo == newToDo).Should().BeTrue();
        var toDo2 = await _repository.GetToDo(updatedToDo.Id);
        ThrowWhenNull(toDo2 is null);
        toDo2.Message.Should().Be("Test Message 3");
        (updatedToDo == toDo2).Should().BeTrue();
    }
}