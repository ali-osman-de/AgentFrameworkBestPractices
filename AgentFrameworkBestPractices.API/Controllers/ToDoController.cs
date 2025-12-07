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
        private readonly IRagService _ragService;
        public ToDoController(IToDoService toDoService, IRagService ragService)
        {
            _toDoService = toDoService;
            _ragService = ragService;
        }
        [HttpPost]
        public async Task<IActionResult> ToDoChat(string message)
        {
           var result = await _toDoService.ToDoChat(message);
           return Ok(result);
        }

        public async Task<IActionResult> RagClientChat(string message)
        {
            var result = await _ragService.RagChat(message);
            return Ok(result);
        }

        public async Task<IActionResult> RagQdrantChat(string message)
        {
            var result = await _ragService.RagChatWithQdrant(message);
            return Ok(result);
        }
    }
}
