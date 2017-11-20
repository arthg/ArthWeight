using ArthWeight.Data;
using ArthWeight.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ArthWeight
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                    .AddEntityFrameworkStores<ArthwindsContext>();

            services.AddDbContext<ArthwindsContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("ArthwindsConnectionString"));
            });

            services.AddAutoMapper();

            services.AddTransient<ArthwindsSeeder>();

            services.AddScoped<IArthwindsRepository, ArthwindsRepository>();

            services.AddMvc(opt =>
            {
                if (_hostingEnvironment.IsProduction())
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {         
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            // must be BEFORE app.UseMvc
            app.UseAuthentication();

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                  "{controller}/{action}/{id?}",
                  new { controller = "App", Action = "Index" });
            });

            if (env.IsDevelopment())
            {
                // Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<ArthwindsSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }
    }
}
