using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DemoService.Entities.Types;

namespace DemoService.Entities;

public class ToDoRelationRepository : IToDoRelationRepository
{
    private readonly ToDoDbContext _context;

    public ToDoRelationRepository(ToDoDbContext context) => _context = context;

    public async Task<ToDoRelation[]> GetChildrenAsync(ToDo parent)
        => await _context.ToDoRelations
        .Where(each => each.ParentId == parent.Id).ToArrayAsync();

    public async Task<ToDoRelation[]> GetParentsAsync(ToDo child)
        => await _context.ToDoRelations.Where(each => each.ChildId == child.Id).ToArrayAsync();

    public async Task<ToDoRelation?> GetRelationAsync(ToDo parent, ToDo child)
        => await _context.ToDoRelations
        .Where(each => each.ChildId == child.Id && each.ParentId == parent.Id).FirstOrDefaultAsync();

    public async Task<bool> HasRelations(ToDo toDo)
        => await _context.ToDoRelations
            .Where(each => each.ParentId == toDo.Id || each.ChildId == toDo.Id).AnyAsync();

    public async Task<ToDoRelation?> CreateRelationAsync(ToDo parent, ToDo child)
    {
        var result = await _context.ToDoRelations.AddAsync(ToDoRelation.NewToDoRelation(parent, child));
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteRelationAsync(ToDoRelation relation)
    {
        var result = _context.ToDoRelations.Remove(relation);
        await _context.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }
}