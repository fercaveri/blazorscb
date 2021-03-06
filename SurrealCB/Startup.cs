using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using SurrealCB.CommonUI;
using SurrealCB.CommonUI.Services;
using SurrealCB.Data;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;
using SurrealCB.Data.Shared;
using SurrealCB.Server.Authorization;
using SurrealCB.Server.Mappings;
using SurrealCB.Server.Middlewares;
using SurrealCB.Server.Services;

namespace SurrealCB.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddScoped<AppState>();
            services.AddScoped<IUserProfileApi, UserProfileApi>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBattleService, BattleService>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IMetricService, MetricService>();
            services.AddScoped<IUserSession, UserSession>();

            //services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            //    .AddRoles<IdentityRole<Guid>>()
            //    .AddEntityFrameworkStores<SCBDbContext>()
            //    .AddDefaultTokenProviders();

            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
            //    AdditionalUserClaimsPrincipalFactory>();

            services.AddTransient<IApiLogService, ApiLogService>();
            services.AddTransient<ISeeder, SCBSeeder>();

            var automapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new MappingProfile());
            });

            var autoMapper = automapperConfig.CreateMapper();

            services.AddSingleton(autoMapper);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers().AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.Converters.Add(new StringEnumConverter());
                setup.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSingleton<IRepositoryConfiguration, SCBNHibernateConfiguration>();
            services.AddScoped<IRepository, NHibernateRepository>();
            //services.AddDbContext<SCBDbContext>(options =>
            //        options.UseLazyLoadingProxies()
            //               .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseMiddleware<ApiResponseMiddleware>(true);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/index");
            });

            var nh = provider.GetService<IRepositoryConfiguration>();

            nh.Configure(Configuration["NhibernateConfig:ConnectionString"],
                Configuration.GetValue("NhibernateConfig:ShowSQL", false),
                Configuration.GetValue<System.Data.IsolationLevel>("NhibernateConfig:Isolation"),
                Configuration.GetValue<string>("NhibernateConfig:SQLiteFileName", null));

            // loading initial data and updating schema only if specified in appsettings.json
            if (Configuration.GetValue<bool>("NHibernateConfig:UpdateSchema", false))
            {
                nh.UpdateSchema();

                var seeder = provider.GetService<ISeeder>();
                seeder.SeedAsync().GetAwaiter().GetResult();
            }
        }
    }
}
