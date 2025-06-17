using MusicPlayer.Core.Interfaces;
using MusicPlayer.Core.Services;
using MusicPlayer.Infrastructure.Repositories;

namespace MusicPlayer.Api.Boostraping;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddTransient<IAuthRepository, AuthRepository>();

        return services;
    }
}
