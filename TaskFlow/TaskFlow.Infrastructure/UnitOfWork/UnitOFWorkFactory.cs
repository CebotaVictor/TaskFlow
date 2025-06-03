using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.UnitOfWork;

namespace TaskFlow.Infrastructure.UnitOfWork
{
    public class UnitOFWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOFWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUsersUnitOfWork CreateUsersUnitOfWork() =>
            _serviceProvider.GetRequiredService<IUsersUnitOfWork>();

        public IWorkflowUnitOfWork CreateWorkFlowUnitOfWork() =>
            _serviceProvider.GetRequiredService<IWorkflowUnitOfWork>();
    }
}
