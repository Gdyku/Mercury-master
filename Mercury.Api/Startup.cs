using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mercury.BLL.Concretes;
using Mercury.BLL.Dtos;
using Mercury.BLL.Intefaces;
using Mercury.BLL.Settings;
using Mercury.DAL.Data;
using Mercury.DAL.Repository;
using Mercury.DAL.Repository.Concretes;
using Mercury.DAL.Repository.Interfaces;
using Mercury.Infrastructure.Concretes;
using Mercury.Infrastructure.Interfaces;
using Mercury.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Mercury.Api
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
            var appSettings = Configuration.GetSection("AuthSettings");
            services.Configure<AuthorizationSettings>(appSettings);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var key = Encoding.ASCII.GetBytes(appSettings.Get<AuthorizationSettings>().Secret);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.Events = new JwtBearerEvents
            //    {
            //        OnTokenValidated = async context =>
            //        {
            //            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserLogic>();
            //            var userId = int.Parse(context.Principal.Identity.Name);
            //            var user = await userService.GetUser(userId);

            //            if (user == null)
            //            {
            //                context.Fail("Unauthorized");
            //            }
            //        }
            //    };
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = "http://localhost:5000";
                   options.RequireHttpsMetadata = false;

                   options.Audience = "mercuryApi";
               });

            services.AddCors(x => x.AddDefaultPolicy(y =>
                y.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
            );

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
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
