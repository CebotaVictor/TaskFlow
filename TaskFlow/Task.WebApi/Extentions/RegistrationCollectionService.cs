using System.Reflection;
using TaskFlow.Application.Autentication.Handlers;
using TaskFlow.Application.Interfaces.Authentication;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.Service;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.Authentication;
using TaskFlow.Infrastructure.BL;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.Servicies;
using TaskFlow.Infrastructure.UnitOfWork;

namespace TaskFlow.WebApi.Extentions
{
    public static class RegistrationCollectionService
    {
        internal static IServiceCollection AddMediator(this IServiceCollection service, IConfiguration configuration, IConfigurationBuilder builderConfiguration)
        {
            service.AddResponseCompression();
            service.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegistrationCollectionService).Assembly);
                cfg.RegisterServicesFromAssemblyContaining<UpdateMemberCommand>();
                cfg.RegisterServicesFromAssemblyContaining<AddMemberCommand>();
                cfg.RegisterServicesFromAssemblyContaining<GetMemberQuery>();
                cfg.RegisterServicesFromAssemblyContaining<DeleteMemberCommand>();
                cfg.RegisterServicesFromAssemblyContaining<RegisterCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<LoginCommandHandler>();
            });

            service.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            service.AddScoped<GenericRepository<Member>>();
            service.AddScoped<IPassword, PasswordHandingService>();
            service.AddScoped<IGenericRepository<Member>, GenericRepository<Member>>();
            service.AddScoped<IMemberRepository, MemberRepository>();
            service.AddScoped<IUnitOfWork<Member>, UnitOfWork<Member>>();
            service.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            builderConfiguration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

            return service;
        }
    }
}
