using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentat.Repository.Options;
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

            // CardDatabaseOptions section of appsecrets.json mapped to CardDatabaseOptions class.
            services.Configure<CardDatabaseOptions>(Configuration.GetSection(nameof(CardDatabaseOptions)));

            // IdentityDatabaseOptions section of appsecrets.json mapped to IdentityDatabaseOptions class.
            services.Configure<IdentityDatabaseOptions>(Configuration.GetSection(nameof(IdentityDatabaseOptions)));

            // SetDatabaseOptions section of appsectets.json mapped to SetDatabaseOptions class.
            services.Configure<SetDatabaseOptions>(Configuration.GetSection(nameof(SetDatabaseOptions)));

            // Configure Dependency Injection classes here
            services.AddSingleton<CardDatabaseOptions>();
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetValue<string>("CardDatabaseOptions:ConnectionString")));

            services.AddSingleton<CardDatabaseOptions>();
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetValue<string>("SetDatabaseOptions:ConnectionString")));

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ISetService, SetService>();

            // add an identity module using user and role models, add the mongodb stores, and add the default token providers alongside UI
            services.AddIdentity<MentatUser, MentatUserRole>()
            .AddMongoDbStores<MentatUser, MentatUserRole, Guid>
                (
                    Configuration.GetValue<string>("IdentityDatabaseOptions:ConnectionString"),
                    Configuration.GetValue<string>("IdentityDatabaseOptions:DatabaseName")
                )
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

                /*endpoints.MapControllerRoute(
                    name: "mentor",
                    pattern: "MentorController/SubmitForm",
                    defaults: new { controller = "Mentor", action = "SubmitForm" }
                    );*/



                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
