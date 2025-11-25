using AgentFrameworkBestPractices.AgentAsFunctionTool.Extensions;
using AgentFrameworkBestPractices.API.Extensions;
using AgentFrameworkBestPractices.Common.Extensions;
using AgentFrameworkBestPractices.FunctionCalling.Extensions;
using AgentFrameworkBestPractices.McpClientAsFunctionTool.Extensions;
using AgentFrameworkBestPractices.MultiConversation.Extensions;
using AgentFrameworkBestPractices.Plugins.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExtensions(); // single olmasý lazým agent üretimi
builder.Services.AddServiceExtensions(); // diðer servisler addscoped olabilir!!
builder.Services.AddMultiConversationService();
builder.Services.AddFunctionToolService();
builder.Services.AddAsToolService();
builder.Services.AddPluginService();
builder.Services.AddMcpClientAsTool();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(x => x.AllowAnyHeader().AllowCredentials().AllowAnyOrigin());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
