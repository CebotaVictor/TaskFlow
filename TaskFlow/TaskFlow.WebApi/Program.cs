using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TaskFlow.Infrastructure.BL;
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

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always; // use CookieSecurePolicy for better security over https 
            });

            builder.Services.AddResponseCompression();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddDbContext<UsersDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            app.UseResponseCompression(); // for better performance optimisation 

            app.UseStaticFiles(); //for bundeling and minification
            app.UseWebOptimizer();

            app.UseRouting(); // enables routing 

            app.UseAuthorization();

            app.MapStaticAssets();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.AddRouteConfig(); //use external method AddRouteConfig() for configuring routes 
            });

            app.Run();
        }
    }
}
