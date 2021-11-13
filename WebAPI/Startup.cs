using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using Domain.Entities.Accounts.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Services;
using Application._Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Application._Common.Options;
using System.Net.Http.Headers;
using System.Net;
using Application.Entities.Accounts.Cmds.RegisterAccountCommand;
using Application._Common.MappingsProfiles;

namespace WebAPI
{
    public class Startup
    {
        public const string AUTH_KEY = "AUTH_KEY";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authOptions = Configuration.GetSection("AuthOptions");
            services.Configure<AuthOptions>(authOptions);

            services.AddControllers();

            services.AddAutoMapper(typeof(AccountMappingProfile).Assembly);

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "AnySolutionTemplate API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Nikita Vedernikov",
                        Email = "Nikita.VedernikoB@yandex.ru",
                        Url = "https://vk.com/oldpegion"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });

            services.AddMediatR(typeof(RegisterAccountCommand).Assembly);

            services.AddEntityFrameworkMySql().AddDbContext<ApplicationDbContext>(config => 
                config.UseMySql(
                    Configuration.GetConnectionString("my_sql"), 
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("my_sql"))));

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateAudience = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions["IssuerKey"])),
                        ValidAlgorithms = new string[] { SecurityAlgorithms.HmacSha256 },
                    };

                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.Audience = authOptions["Audience"];
                    config.Authority = authOptions["Authority"];
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy("Api", apiPolicy =>
                    apiPolicy
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .RequireClaim(ClaimsIdentity.DefaultNameClaimType)
                        .RequireClaim("Id"));
            });

            services.AddScoped<ITokenService, JwtTokenService>();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3(config =>
            {
                config.Path = "/api/swagger";
            });

            app.UseRouting();

            app.Use((context, next) =>
            {
                if (context.Request.Cookies.Any(pair => pair.Key == Startup.AUTH_KEY))
                {
                    context.Request.Headers.Add(nameof(HttpRequestHeader.Authorization), "Bearer " + context.Request.Cookies[Startup.AUTH_KEY]);
                }

                return next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
