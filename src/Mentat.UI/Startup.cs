using System;
using Mentat.Repository.Models;
using Mentat.Repository.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mentat.Domain.IService;
using Mentat.Domain.Service;
using MongoDB.Driver;
using Mentat.UI.Areas.Identity.Data;
using IdentityMongo.Settings;
using Microsoft.AspNetCore.Identity;

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
            services.AddControllersWithViews();

            // Mapping to CardDatabaseSettings no longer works on NET 6.0; need to update Startup.cs to reflect migration to 6.0
            // For now, mainly entered CardBaseSettings into the class - when migration complete, abstract with updated Configure
            services.Configure<CardDatabaseSettings>(Configuration.GetSection(nameof(CardDatabaseSettings)));

            // Configure Dependency Injection classes here
            services.AddSingleton<ICardDatabaseSettings, CardDatabaseSettings>();
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetValue<string>("CardDatabaseSettings:ConnectionString")));

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICardService, CardService>();

            // load in mongodb settings
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

            // add an identity module using user and role models, add the mongodb stores, and add the default token providers alongside UI
            services.AddIdentity<MentatUser, MentatUserRole>()
            .AddMongoDbStores<MentatUser, MentatUserRole, Guid>(mongoDbSettings.ConnectionString, mongoDbSettings.Name)
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            services.AddRazorPages();
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
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
