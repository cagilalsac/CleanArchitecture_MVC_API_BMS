using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
