using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipesGlossary.States;

namespace RecipesGlossary;

/// <summary />
public static class DependencyInjection
{
    /// <summary />
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddSingleton<SnackbarState>();
        services.AddSingleton<LoadingState>();

        return services;
    }
}