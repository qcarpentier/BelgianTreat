using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BelgianTreat.Services;
using BelgianTreat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BelgianTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BelgianContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("BelgianConnectionString"));
            });

            services.AddTransient<IMailService, NullMailService>();
            // Support for real mail service 
            // TODO
            services.AddTransient<BelgianSeeder>();
            services.AddScoped<IBelgianRepository, BelgianRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Seed the db
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<BelgianSeeder>();
                    seeder.Seed();
                }
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            // Configure routes
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });
            });
        }
    }
}
