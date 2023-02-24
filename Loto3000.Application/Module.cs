
using Loto3000Application.Services.Implementation;
using Loto3000Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Loto3000.Application.AutoMapper;

namespace Loto3000.Application
{
    public static class Module
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDrawService, DrawService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
