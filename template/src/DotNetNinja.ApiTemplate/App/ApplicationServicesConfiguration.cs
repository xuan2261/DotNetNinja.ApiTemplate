using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNinja.ApiTemplate.App
{
    public static class ApplicationServicesConfiguration
    {
        /// <summary>
        /// Add your application services here.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(Program));
        }
    }
}