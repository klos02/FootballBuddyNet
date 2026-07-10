using Microsoft.Extensions.DependencyInjection;

namespace FootballBuddy.Auth.Api;

public static class AuthApiExtensions
{
    public static IMvcBuilder AddAuthApi(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.AddApplicationPart(typeof(AuthApiExtensions).Assembly);
    }
}