using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DotNetNinja.ApiTemplate.App
{
    public static class ExceptionPageConfiguration
    {
        public static IApplicationBuilder UseDeveloperExceptionPageInDevelopment(this IApplicationBuilder app,
            IWebHostEnvironment env, DeveloperExceptionPageOptions options = null)
        {
            if (env.IsDevelopment())
            {
                if (options != null)
                {
                    return app.UseDeveloperExceptionPage(options);
                }

                return app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }
}