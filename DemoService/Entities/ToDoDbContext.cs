using Microsoft.EntityFrameworkCore;
using DemoService.Entities.Types;

namespace DemoService.Entities;

public class ToDoDbContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; } = default!;
    public DbSet<ToDoRelation> ToDoRelations { get; set; } = default!;

    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }
}
