using DotNetNinja.ApiTemplate.App;
using DotNetNinja.ApiTemplate.Configuration;
using DotNetNinja.AutoBoundConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNinja.ApiTemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var openApiSettings = services
                .AddAutoBoundConfigurations(Configuration).FromAssembly(typeof(Program).Assembly).Provider.Get<OpenApiSettings>();
            services
                .AddControllers()
                .Services
                .AddUrlApiVersioning()
                .AddOpenApi(openApiSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionInfo, OpenApiSettings openApiSettings)
        {
            app
                .UseDeveloperExceptionPageInDevelopment(env)
                .UseHttpsRedirection()
                .UseOpenApi(versionInfo, openApiSettings)
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
