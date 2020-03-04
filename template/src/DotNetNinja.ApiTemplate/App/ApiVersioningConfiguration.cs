using Microsoft.Extensions.DependencyInjection;

namespace DotNetNinja.ApiTemplate.App
{
    public static class ApiVersioningConfiguration
    {
        public static IServiceCollection AddUrlApiVersioning(this IServiceCollection services)
        {
            return services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                }).AddApiVersioning();
        }
    }
}