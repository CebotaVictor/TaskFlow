using System.Reflection;
using TaskFlow.Application.Autentication.Handlers;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.Authentication;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.Service;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Application.WorkFlow.Projects.Handler;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Tasks;
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
                cfg.RegisterServicesFromAssemblyContaining<CreateProjectCommand>();
                cfg.RegisterServicesFromAssemblyContaining<CreateProjectCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<WorkflowResponse>();
            });

            service.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            service.AddScoped<UsersGenericRepository<Member>>();
            service.AddScoped<UsersGenericRepository<Admin>>();
            service.AddScoped<IPassword, PasswordHandingService>();
            service.AddScoped<IUsersGenericRepository<Member>, UsersGenericRepository<Member>>();
            service.AddScoped<IUsersGenericRepository<Admin>, UsersGenericRepository<Admin>>();
            service.AddScoped<IWorkflowGenericRepository<Project>, WorkflowGenericRepository<Project>>();
            service.AddScoped<IWorkflowGenericRepository<Section>, WorkflowGenericRepository<Section>>();
            service.AddScoped<IWorkflowGenericRepository<UTask>, WorkflowGenericRepository<UTask>>();
            service.AddScoped<IMemberRepository, MemberRepository>();
            service.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();
            service.AddScoped<IWorkflowUnitOfWork, WorkflowUnitOfWork>();
            service.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            builderConfiguration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

            return service;
        }
    }
}
