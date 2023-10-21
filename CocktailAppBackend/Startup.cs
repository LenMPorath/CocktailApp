using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CocktailAppBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            var outputPath = @"C:\Users\lenmp\output.txt";
            File.WriteAllText(outputPath, "Connection String:" + connectionString);
            services.AddDbContext<CocktailAppDBContext>(options =>
                {
                    options.UseMySql(connectionString, new MariaDbServerVersion("10.9.0"));
                });

            // Weitere Service-Konfigurationen
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Weitere Konfigurationen

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CocktailAppDBContext>();
                // Fügen Sie hier Initialisierungslogik hinzu, falls erforderlich
            }
        }
    }
}