using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Microsoft.OpenApi.Models;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.DataAccess;
using NSC_TournamentGen.DataAccess.Repositories;
using NSC_TournamentGen.Domain.IRepositories;
using NSC_TournamentGen.Domain.Services;
using NSC_TournamentGen.Security;
using NSC_TournamentGen.Security.IRepositories;
using NSC_TournamentGen.Security.IServices;
using NSC_TournamentGen.Security.Models;
using NSC_TournamentGen.Security.Repositories;
using NSC_TournamentGen.Security.Services;

namespace NSC_TournamentGen
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var identifier = Configuration["JwtConfig:SecretIdentifier"];
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NSC_TournamentGen", Version = "v1" });


                // Authentication.
                c.AddSecurityDefinition(identifier, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = $"Enter the jwt token after the {identifier}.\n\nExample: {identifier} 123456.",
                });

                // Make the api remember the logged in user.
                // Without this piece of code, the logged in user won't be
                // able to use any parts of the api that require authorization.
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = identifier
                            },
                        },
                        new string[] {}
                    }
                });
            });

            // JWT authentication.
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"])),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtConfig:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtConfig:Audience"],
                    ValidateLifetime = true,
                };
            });

            // Make role-based authentication work.
            // Policy name can be anything you want bro. <3
            // Roles are defined in the Role class.
            services.AddAuthorization(options =>
             {
                 // Admin role.
                 options.AddPolicy("RequireAdministratorRole",
                      policy => policy.RequireRole(Role.Administrator));

                 // User role.
                 options.AddPolicy("RequireUserRole",
                      policy => policy.RequireRole(Role.User));
             });

            services.AddCors(
                opt => opt
                    .AddPolicy("dev-policy", policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ISecurityRepository, SecurityRepository>();
            services.AddScoped<IDbSeeder, DbSeeder>();
            services.AddScoped<IAuthDbSeeder, AuthDbSeeder>();

            services.AddDbContext<MainDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=main.db");
            });

            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=auth.db");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbSeeder dbSeeder, IAuthDbSeeder authDbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSC_TournamentGen v1"));
                app.UseCors("dev-policy");
                dbSeeder.SeedDevelopment();
                authDbSeeder.SeedDevelopment();
            }
            else
            {
                dbSeeder.SeedProduction();
                authDbSeeder.SeedProduction();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}