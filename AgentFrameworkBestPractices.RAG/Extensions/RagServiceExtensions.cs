using System;
using AgentFrameworkBestPractices.RAG.Interfaces;
using AgentFrameworkBestPractices.RAG.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.RAG.Extensions;

public static class RagServiceExtensions
{
    public static void AddRagServiceExtensions(this IServiceCollection services){

        services.AddScoped<IRagService, RagService>();

    }

}
