using System;
using AgentFrameworkBestPractices.RAG.Interfaces;
using AgentFrameworkBestPractices.RAG.Providers;
using AgentFrameworkBestPractices.RAG.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.RAG.Extensions;

public static class RagServiceExtensions
{
    public static void AddRagServiceExtensions(this IServiceCollection services){
        services.AddSingleton<ITextProviders, TextProviders>();
        services.AddSingleton<IRagService, RagService>();

    }

}
