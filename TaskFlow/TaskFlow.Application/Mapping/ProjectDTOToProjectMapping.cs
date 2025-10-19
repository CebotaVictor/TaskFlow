using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Workflow;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.Application.Mapping
{
    public static class ProjectDTOToProjectMapping
    {

        public static Project ToProject(ProjectDTO ProjojectDto)
        {
            var project = new Project
            {
                Id = ProjojectDto.Id,
                Name = ProjojectDto.Name,
                Description = ProjojectDto.Description,
                CreatedAt = ProjojectDto.CreatedAt,
                EndDate = ProjojectDto.EndDate,
                Sections = ProjojectDto.Sections
            };

            return project;
        }

        public static ProjectDTO ToProjectDTO(Project project)
        {
            var projectDto = new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                EndDate = project.EndDate
            };
            return projectDto;
        }
    }
}
