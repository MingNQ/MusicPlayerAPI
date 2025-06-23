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
        services.AddScoped<IArtistService, ArtistService>();
        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<ITrackService, TrackService>();

        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IArtistRepository, ArtistRepository>();
        services.AddTransient<IAlbumRepository, AlbumRepository>();
        services.AddTransient<ITrackRepository, TrackRepository>();

        return services;
    }
}
