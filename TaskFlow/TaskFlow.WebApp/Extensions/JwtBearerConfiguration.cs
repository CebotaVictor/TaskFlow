﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
using TaskFlow.Infrastructure.Authentication;
using System.Threading.Tasks;

namespace TaskFlow.WebApi.Extentions
{
    public static class JwtBearerConfiguration
    {
        internal static IServiceCollection AddJwtBearer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    JwtSettings settings = new JwtSettings();
                    configuration.GetSection("JwtSettings").Bind(settings);
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
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

            return service;
        }
    }
}
