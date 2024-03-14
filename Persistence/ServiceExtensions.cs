using Application.Common.Contexts.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IDb, Db>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
