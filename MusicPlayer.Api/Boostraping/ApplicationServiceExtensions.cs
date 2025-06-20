using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Core.Services;
using MusicPlayer.Infrastructure.Repositories;

namespace MusicPlayer.Api.Boostraping;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}
