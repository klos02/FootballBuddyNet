using FootballBuddy.Profiles.Infrastructure.Persistence;
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

        return services;
    }
}
