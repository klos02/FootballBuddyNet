using FootballBuddy.Profiles.Application.Abstractions;
using FootballBuddy.Profiles.Domain.Repositories;
using FootballBuddy.Profiles.Infrastructure.Persistence;
using FootballBuddy.Profiles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballBuddy.Profiles.Infrastructure;

public static class ProfilesInfrastructureExtensions
{
    public static IServiceCollection AddProfilesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProfilesDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("ProfilesDb"),
                postgres => postgres.MigrationsHistoryTable("__EFMigrationsHistory_Profiles")));
        
        
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProfileUnitOfWork, ProfileUnitOfWork>();

        return services;
    }
}
