using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using TaskFlow.Application.Autentication.Handlers;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.Authentication;
using TaskFlow.Infrastructure.BL;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.UnitOfWork;
using TaskFlow.WebApi.Extensions;
using TaskFlow.WebApi.Extentions;
using TaskFlow.WebApp.API.Interfaces;
using TaskFlow.WebApp.API.Services;
namespace TaskFlow1
{                                                   
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddWebOptimizerConfig();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always; // use CookieSecurePolicy for better security over https 
            });

            builder.Services.AddDbContext<UsersDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<WorkflowDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(Program));
                
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = false;
                //options.Providers.Add<BrotliCompressionProvider>();
                //options.Providers.Add<GzipCompressionProvider>();
            });

            //builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            //{
            //    options.Level = CompressionLevel.Fastest;
            //});

            //builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            //{
            //    options.Level = CompressionLevel.SmallestSize;
            //});

            builder.Services.AddHttpClient<IProject, ProjectAPIService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7006/api/Workflow/");
            });

            builder.Services.AddHttpClient<IAuth, AuthAPIService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7006/api/ApiAuth/");
                });

            builder.Services.AddHttpClient<IUser, UserApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7006/api/");
            });


            builder.Services.AddScoped<CookieGenerator>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                JwtSettings settings = new JwtSettings();
                builder.Configuration.GetSection("JwtSettings").Bind(settings);
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("my_token"))
                        {
                            context.Token = context.Request.Cookies["my_token"];
                        }
                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:7129", "http://localhost:5176")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseResponseCompression();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowFrontend");

            app.UseResponseCompression(); 
            app.UseRouting(); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebOptimizer();
            app.MapStaticAssets();

            app.AddRouteConfig(); 
            app.Run();
        }
    }
}
