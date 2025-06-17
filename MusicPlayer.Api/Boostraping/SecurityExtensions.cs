using Microsoft.AspNetCore.Identity;

namespace MusicPlayer.Api.Boostraping;

public static class SecurityExtensions
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        //services.AddIdentity<IdentityUser, IdentityRole>();

        return services;
    }
}
