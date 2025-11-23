using AgentFrameworkBestPractices.API.Extensions;
using AgentFrameworkBestPractices.Common.Extensions;
using AgentFrameworkBestPractices.FunctionCalling.Extensions;
using AgentFrameworkBestPractices.MultiConversation.Extensions;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExtensions();
builder.Services.AddServiceExtensions();
builder.Services.AddMultiConversationService();
builder.Services.AddFunctionToolService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
