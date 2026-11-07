using BuildingBlocks.Application.Abstractions;
using FootballBuddy.Auth.Contracts.IntegrationEvents;
using FootballBuddy.Profiles.Application.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace FootballBuddy.Profiles.Application;

public static class ProfilesApplicationExtensions
{
    public static IServiceCollection AddProfilesApplication(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<UserRegisteredV1>, UserRegisteredHandler>();
        
        return services;
    }
}