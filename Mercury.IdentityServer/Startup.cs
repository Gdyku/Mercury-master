using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4;
using Mercury.BLL.Concretes;
using Mercury.BLL.Dtos;
using Mercury.BLL.Intefaces;
using Mercury.BLL.Settings;
using Mercury.DAL.Data;
using Mercury.DAL.Repository;
using Mercury.DAL.Repository.Concretes;
using Mercury.DAL.Repository.Interfaces;
using Mercury.IdentityServer.Infrastructure;
using Mercury.Infrastructure.Concretes;
using Mercury.Infrastructure.Interfaces;
using Mercury.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mercury.IdentityServer
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
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Default")))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMarkerRepository, MarkerRepository>()
                .AddScoped<IUserLogic, UserLogic>()
                .AddScoped<IMarkerLogiccs, MarkerLogic>()
                .AddScoped<IAuthLogic, AuthLogic>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IScheduleLogic, ScheduleLogic>()
                .AddScoped<IScheduleRepository, ScheduleRepository>()
                .AddControllersWithViews();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddCustomTokenRequestValidator<CustomTokenRequestValidator>()
                .AddProfileService<ProfileService>();

            var appSettings = Configuration.GetSection("AuthSettings");
            services.Configure<AuthorizationSettings>(appSettings);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
            })
               .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
               {
                   options.SignInScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                   options.SignOutScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                   options.SaveTokens = true;

                   options.Authority = "http://localhost:5000";
                   options.ClientId = "implicitClient";
                   options.ResponseType = "code id_token";
                   options.RequireHttpsMetadata = false;
               });


            builder.AddDeveloperSigningCredential();

            services.AddCors(x => x.AddDefaultPolicy(y =>
                 y.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod())
             );
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

            app.UseCors();

            app.UseIdentityServer();
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
