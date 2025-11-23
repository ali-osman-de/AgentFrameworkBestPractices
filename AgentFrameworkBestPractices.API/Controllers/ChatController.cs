using AgentFrameworkBestPractices.API.Interfaces;
using AgentFrameworkBestPractices.Common.Interface;
using Microsoft.Agents.AI;
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using System.Threading.Tasks;

namespace AgentFrameworkBestPractices.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult> Chat(string message)
        {
            var chat = await _chatService.SendChatMessage(message, CancellationToken.None);
            return Ok(chat);
        }
    }
}
