using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentat.Repository.Models;
using Mentat.Repository.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mentat.Domain.Interfaces;
using Mentat.Domain.IService;
using Mentat.Domain.Service;
using Microsoft.EntityFrameworkCore;
// using Mentat.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mentat.UI
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

            // Mapping to CardDatabaseSettings no longer works on NET 6.0; need to update Startup.cs to reflect migration to 6.0
            // For now, mainly entered CardBaseSettings into the class - when migration complete, abstract with updated Configure
            services.Configure<CardDatabaseSettings>(Configuration.GetSection(nameof(CardDatabaseSettings)));


            services.AddSingleton<ICardDatabaseSettings, CardDatabaseSettings>();
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetValue<string>("CardDatabaseSettings:ConnectionString")));
            services.AddScoped<ICardService, CardService>();
            
            services.AddControllersWithViews();

            // Configure Dependency Injection classes here
            services.AddScoped<IStudentService, StudentService>();

            // services.AddScoped<IBashTestConfig, BashTestConfig>();
            // services.AddScoped<IBashTestDriver, BashTestDriver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
