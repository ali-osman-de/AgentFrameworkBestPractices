using System;
using System.ComponentModel;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Data;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Model;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Tools;

public class ToDoManagerTool
{
    private static IServiceScopeFactory? _scopeFactory;

    public static void Configure(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    private static async Task<T> WithService<T>(Func<DatabaseService, Task<T>> action)
    {
        if (_scopeFactory == null)
            throw new InvalidOperationException("ScopeFactory has not been configured.");
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<DatabaseService>();
        return await action(service);
    }

    [Description("Add new To-Do in the database")]
    public static async Task<bool> AddNewToDo([Description("To-Do name")]string toDoName, [Description("To-Do description")] string toDoDescription)
    {
        var newToDo = new ToDo
        {
            Id = Guid.NewGuid(),
            Title = toDoName,
            Description = toDoDescription
        };

        var result = await WithService(service => service.AddToDo(newToDo));
        return result ? true : false;

    }

    [Description("List all To-Do in the database")]
    public static async Task<List<ToDo>> ListToDo()
    {
        var result = await WithService(service => service.ListTodo());
        return result;
    }

    [Description("Update To-Do in the database")]
    public static async Task<ToDo> UpdateToDo([Description("To-Do kimlik numarası")]Guid Id, [Description("updated To-Do name")]string toDoName, [Description("updated To-Do description")] string toDoDescription)
    {
        var result = await WithService(service => service.UpdateToDo(Id, toDoName, toDoDescription));
        return result;
    }

    [Description("Remove To-Do in the database")]
    public static async Task<bool> RemoveToDo([Description("To-Do kimlik numarası")]Guid Id)
    {
        var result = await WithService(service => service.RemoveTodo(Id));
        return result ? true : false;
    }
}
