using AgentFrameworkBestPractices.AgentAsFunctionTool.Interfaces;
using AgentFrameworkBestPractices.API.Interfaces;
using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.FunctionCalling.Interfaces;
using AgentFrameworkBestPractices.MultiConversation.Interfaces;
using AgentFrameworkBestPractices.Plugins.Interfaces;
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
        private readonly IMultiChatService _multiChatService;
        private readonly IFuncToolChatService _funcToolChatService;
        private readonly IAgentAsToolService _agentAsToolService;
        private readonly IPlugInChatService _plugInChatService;

        public ChatController(IChatService chatService, IMultiChatService multiChatService, IFuncToolChatService funcToolChatService, IAgentAsToolService agentAsToolService, IPlugInChatService plugInChatService)
        {
            _chatService = chatService;
            _multiChatService = multiChatService;
            _funcToolChatService = funcToolChatService;
            _agentAsToolService = agentAsToolService;
            _plugInChatService = plugInChatService;
        }

        [HttpPost]
        public async Task<ActionResult> Chat(string message)
        {
            var chat = await _chatService.SendChatMessage(message, CancellationToken.None);
            return Ok(chat);
        }

        public async Task<ActionResult> MultiChat(string message)
        {
            var multiChat = await _multiChatService.ChatWithThreads(message);
            return Ok(multiChat);
        }

        public async Task<ActionResult> FuncToolChat(string message)
        {
            var funcChat = await _funcToolChatService.ChatFuncTool(message);
            return Ok(funcChat);
        }
        public async Task<ActionResult> AgentAsFuncToolChat(string message)
        {
            var asFuncChat = await _agentAsToolService.AgentAsToolChat(message);
            return Ok(asFuncChat);
        }
        public async Task<ActionResult> PluginsChat(string message)
        {
            var pluginsChat = await _plugInChatService.PlugInChat(message);
            return Ok(pluginsChat);
        }
    }
}
