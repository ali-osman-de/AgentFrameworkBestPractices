using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Model;
using Microsoft.EntityFrameworkCore;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Data;

public class DatabaseService
{
    private readonly AppDbContext _context;

    public DatabaseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddToDo(ToDo newToDo)
    {
        if (_context.ToDos == null)
        {
            return false;
        }
        await _context.ToDos.AddAsync(newToDo);
        var res = await _context.SaveChangesAsync() > 0;
        return res ? true : false;
    }

    public async Task<List<ToDo>> ListTodo()
    {
        if (_context.ToDos == null)
        {
            return new List<ToDo>();
        }
        var res = await _context.ToDos.ToListAsync();
        return res;
    }
        public async Task<ToDo> UpdateToDo(Guid Id, string title, string description)
    {
        if (_context.ToDos == null)
        {
            throw new InvalidOperationException("ToDo DbSet is not initialized.");
        }
        var entity = await _context.ToDos.FindAsync(Id);
        if (entity == null)
        {
            throw new InvalidOperationException($"ToDo item with Id {Id} was not found.");
        }
        entity.Title = title;
        entity.Description = description;
        await _context.SaveChangesAsync();
        return entity;
    }
        public async Task<bool> RemoveTodo(Guid Id)
    {
        if (_context.ToDos == null)
        {
            throw new InvalidOperationException("ToDo DbSet is not initialized.");
        }

        var affected = await _context.ToDos.Where(x => x.Id == Id).ExecuteDeleteAsync();
        return affected > 0;
    }
}
