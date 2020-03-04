using System;
using ChaosMonkey.Guards;
using DotNetNinja.ApiTemplate.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DotNetNinja.ApiTemplate.App
{
    public static class OpenApiConfiguration
    {
        private const string ApiName = "DotNetNinja.ApiTemplate API";

        public static IServiceCollection AddOpenApi(this IServiceCollection services, OpenApiSettings settings)
        {
            Guard.IsNotNull(settings, nameof(settings));
            return services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, settings));
                }
            });
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, OpenApiSettings settings)
        {
            var contact = new OpenApiContact
            {
                Name = (string.IsNullOrWhiteSpace(settings.Contact?.Name))?null: settings.Contact.Name,
                Email = (string.IsNullOrWhiteSpace(settings.Contact?.Email))?null:settings.Contact.Email,
                Url = (string.IsNullOrWhiteSpace(settings.Contact?.Url)) ? null : new Uri(settings.Contact.Url)
            };
            var license = new OpenApiLicense
            {
                Name = (string.IsNullOrWhiteSpace(settings.License?.Name) ? null : settings.License.Name),
                Url = (string.IsNullOrWhiteSpace(settings.License?.Url)) ? null : new Uri(settings.License.Url)
            };
            var apiDescription = (string.IsNullOrWhiteSpace(settings.ServiceDescription))
                ? ApiName
                : settings.ServiceDescription;
            return new OpenApiInfo()
            {
                Title = $"{ApiName}",
                Version = description.ApiVersion.ToString(),
                Description = (description.IsDeprecated)
                            ? $"{apiDescription} This API version has been deprecated."
                            : apiDescription,
                Contact = contact,
                License = license,
                TermsOfService = (string.IsNullOrWhiteSpace(settings.TermsOfServiceUrl))?null:new Uri(settings.TermsOfServiceUrl)
            };
        }

        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, 
                        IApiVersionDescriptionProvider versionProvider, OpenApiSettings settings)
        {
            Guard.IsNotNull(versionProvider, nameof(versionProvider));
            Guard.IsNotNull(settings, nameof(settings));

            app.UseSwagger();

            if (settings.IsUserInterfaceEnabled)
            {
                app.UseSwaggerUI(options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in versionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", 
                                                $"{ApiName} {description.GroupName}");
                    }
                });
            }
            return app;
        }
    }
}