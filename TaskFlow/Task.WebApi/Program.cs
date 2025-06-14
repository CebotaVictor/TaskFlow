
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TaskFlow.Application.Autentication.Handlers;
using TaskFlow.Application.Interfaces.Authentication;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.Authentication;
using TaskFlow.Infrastructure.BL;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.UnitOfWork;
using TaskFlow.WebApi.Extentions;
namespace Task.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGenWithAuth();


            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always; // use CookieSecurePolicy for better security over https 
            });

            builder.Services.AddResponseCompression();
            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

            builder.Services.AddDbContext<UsersDBContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<WorkflowDBContext>(options =>
                options.UseSqlServer(connectionString));


            builder.Services.AddMediator(builder.Configuration, builder.Configuration);
            builder.Services.AddJwtBearer(builder.Configuration);



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5176")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow API v1");
                    c.RoutePrefix = "swagger";  // Access Swagger at /swagger
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.MapControllers();
            app.UseResponseCompression();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
