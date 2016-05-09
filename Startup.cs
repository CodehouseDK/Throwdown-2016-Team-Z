using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamZ.Services;

namespace TeamX
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<INameService, NameService>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            
            app.UseIISPlatformHandler();
            
            app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();
            
            app.UseStaticFiles();
            
        }
    }
}