using AgentFrameworkBestPractices.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddServiceExtensions();

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
