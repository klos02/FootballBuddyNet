using System.Reflection;
using FluentValidation;
using FootballBuddy.Auth.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace FootballBuddy.Auth.Application;

public static class AuthApplicationExtensions
{
    public static IServiceCollection AddAuthApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

        });
            
            
        
        
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
    
}