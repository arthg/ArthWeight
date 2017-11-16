﻿using ArthWeight.Data;
using ArthWeight.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArthWeight
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
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

            services.AddMvc();
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
