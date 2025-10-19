using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Domain.Entities.WSections;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Tasks;
using TaskFlow.Infrastructure.BL;
using System.Diagnostics;

namespace TaskFlow.Infrastructure.UnitOfWork
{
    public class WorkflowUnitOfWork : IWorkflowUnitOfWork
    {
        private readonly WorkflowDBContext _dbContext;
        public IWorkflowGenericRepository<Project> Project { get; }
        public IWorkflowGenericRepository<Section> Sections { get; }
        public IWorkflowGenericRepository<UTask> Tasks { get; }
        public ITaskRepository TaskRepo { get; }
        public IProjectRepository ProjectRepo { get; }
        public ISectionRepository SectionRepo { get; }

        [DebuggerStepThrough]
        public WorkflowUnitOfWork(IWorkflowGenericRepository<Project> project, IWorkflowGenericRepository<Section> sections, 
            IWorkflowGenericRepository<UTask> tasks, ITaskRepository taskRepo, IProjectRepository projectRepo, ISectionRepository sectionRepo, WorkflowDBContext dbContext)
        {
            Project = project;
            Sections = sections;
            Tasks = tasks;
            TaskRepo = taskRepo;
            ProjectRepo = projectRepo;
            SectionRepo = sectionRepo;
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }
    }
}
