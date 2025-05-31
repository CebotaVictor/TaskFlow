using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Reflection;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.BL;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.UnitOfWork;
using TaskFlow.WebApi.Extensions;
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
