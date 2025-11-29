using System;
using System.ComponentModel;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Data;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Tools;

public class ToDoManagerTool
{
    private readonly AppDbContext _context;

    public ToDoManagerTool(AppDbContext context)
    {
        _context = context;
    }

    [Description("Add new To-Do in the database")]
    public async Task<bool> AddNewToDo([Description("To-Do name")]string toDoName, [Description("To-Do description")] string toDoDescription)
    {
        var newToDo = new ToDo
        {
            Id = Guid.NewGuid(),
            Title = toDoName,
            Description = toDoDescription
        };

        _context.ToDos.Add(newToDo);
        var result = await _context.SaveChangesAsync() > 0;
        return result ? true : false;
    }

    [Description("List all To-Do in the database")]
    public async Task<List<ToDo>> ListToDo()
    {
        var result = await _context.ToDos.ToListAsync();
        return result;
    }

    [Description("Update To-Do in the database")]
    public async Task<ToDo> UpdateToDo([Description("To-Do kimlik numarası")]Guid Id, [Description("updated To-Do name")]string toDoName, [Description("updated To-Do description")] string toDoDescription)
    {
        var entity = await _context.ToDos.FindAsync(Id);
        if (entity == null)
        {
            throw new InvalidOperationException($"To-Do item with Id '{Id}' not found.");
        }
        entity.Title = toDoName;
        entity.Description = toDoDescription;
        _context.ToDos.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    [Description("Remove To-Do in the database")]
    public async Task<bool> RemoveToDo([Description("To-Do kimlik numarası")]Guid Id)
    {
        var entity = await _context.ToDos.FindAsync(Id);
        if (entity == null)
        {
            return false;
        }
        _context.ToDos.Remove(entity);
        var result = await _context.SaveChangesAsync() > 0;
        return result ? true : false;
    }
}
