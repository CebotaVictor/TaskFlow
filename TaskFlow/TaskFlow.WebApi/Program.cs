using Microsoft.OpenApi.Models;
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
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TaskFlow API",
                    Version = "v1"
                });
            });

            builder.Services.AddWebOptimizerConfig();

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

            app.UseStaticFiles();
            app.UseWebOptimizer();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.AddRouteConfig();
            });

            app.Run();
        }
    }
}
