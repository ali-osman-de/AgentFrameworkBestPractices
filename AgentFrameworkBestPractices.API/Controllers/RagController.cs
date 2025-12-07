using AgentFrameworkBestPractices.A2A.Interfaces;
using AgentFrameworkBestPractices.RAG.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgentFrameworkBestPractices.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RagController : ControllerBase
    {
        private readonly IRagService _ragService;
        private readonly IA2AService _a2aService;

        public RagController(IRagService ragService, IA2AService a2aService)
        {
            _ragService = ragService;
            _a2aService = a2aService;
        }

        [HttpPost]
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
        public async Task<IActionResult> A2AagentAsFunctionChat(string message)
        {
            var result = await _a2aService.A2AAsFunctionTool(message);
            return Ok(result);
        }
    }
}
