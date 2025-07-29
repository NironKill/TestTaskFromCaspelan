using Bookstore.Application.Interfaces;
using Bookstore.Persistence.Common;
using Bookstore.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseSet>(configuration.GetSection(DataBaseSet.Configuration));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Connection.GetOptionConfiguration(
                configuration.GetSection(DataBaseSet.Configuration).Get<DataBaseSet>().ConnectionString)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}
