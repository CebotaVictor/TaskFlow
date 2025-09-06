using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.WSections;
using TaskFlow.Domain.Entities.Tasks;
using TaskFlow.Infrastructure.BL;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace TaskFlow.Infrastructure.Repositories
{
    public class SectionRepository : ISectionRepository
    {

        private readonly WorkflowGenericRepository<Section>? _repository;
        private readonly WorkflowDBContext? _context;
        private readonly ILogger<SectionRepository> _logger;
        private readonly ITaskRepository _taskRepository;


        public SectionRepository(WorkflowGenericRepository<Section>? repository, ITaskRepository taskRepository, WorkflowDBContext? context,ILogger<SectionRepository> logger)
        {
            _repository = repository ?? throw new NullReferenceException("The WorkflowGenericRepository is null in SectionRepository");
            _taskRepository = taskRepository ?? throw new NullReferenceException("The TaskRepository is null in SectionRepository");
            _context = context ?? throw new NullReferenceException("The WorkFlowDBContext is null in SectionRepository");
            _logger = logger ?? throw new NullReferenceException("The ILogger is null in SectionRepository");
        }

        public async Task<bool> DeleteSectionByIdAsync(ushort Id)
        {
            var section = _context?.Sections?.Include(s => s.Tasks).FirstOrDefault(s => s.Id == Id);
            if (section == null) return false;

            var tasks = section?.Tasks.ToList() ?? new List<UTask>(); // allways returns a list even if its empty 


            if (tasks.Any())
            {
                foreach (var task in tasks)
                {
                    await _taskRepository.DeleteTaskByIdAsync(task.Id);
                }
            }
                _context?.Sections?.Remove(section!);
                return true;
            
         }

        public async Task<IEnumerable<Section>> GetAllSectionsAsync()
        {
            try
            {
                if (_context != null)
                    return await _context.Sections.Include(t => t.Tasks).ToListAsync() ?? throw new NullReferenceException($"GetAllSections query returned null for the SectionRepository");
                throw new NullReferenceException("GetAllSections got a null _context for the SectionRepository");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllSections method \n{ex.Message}");
                return Enumerable.Empty<Section>();
            }
        }
    }
}
