using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Tasks;
using TaskFlow.Infrastructure.BL;

namespace TaskFlow.Infrastructure.UnitOfWork
{
    public class WorkflowUnitOfWork : IWorkflowUnitOfWork
    {
        private readonly WorkflowDBContext _dbContext;
        public IWorkflowGenericRepository<Project> Project { get; }
        public IWorkflowGenericRepository<Section> Sections { get; }
        public IWorkflowGenericRepository<UTask> Tasks { get; }


        public WorkflowUnitOfWork(IWorkflowGenericRepository<Project> project, IWorkflowGenericRepository<Section> sections, IWorkflowGenericRepository<UTask> tasks, WorkflowDBContext dbContext)
        {
            Project = project;
            Sections = sections;
            Tasks = tasks;
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }
    }
}
