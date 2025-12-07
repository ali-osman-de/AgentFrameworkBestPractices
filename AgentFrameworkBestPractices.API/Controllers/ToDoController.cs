using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Interfaces;
using AgentFrameworkBestPractices.RAG.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgentFrameworkBestPractices.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        [HttpPost]
        public async Task<IActionResult> ToDoChat(string message)
        {
           var result = await _toDoService.ToDoChat(message);
           return Ok(result);
        }
    }
}
