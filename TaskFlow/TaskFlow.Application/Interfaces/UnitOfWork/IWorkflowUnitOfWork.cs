using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Application.Interfaces.UnitOfWork
{
    public interface IWorkflowUnitOfWork
    {
        public IWorkflowGenericRepository<Project> Project { get; }
        public IWorkflowGenericRepository<Section> Sections { get; }
        public IWorkflowGenericRepository<UTask> Tasks { get; }

        Task<int> SaveChangesAsync();
    }
}
