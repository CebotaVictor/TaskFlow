using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Application.WorkFlow.Projects.Queries;
using TaskFlow.Application.WorkFlow.Sections.Commands;
using TaskFlow.Application.WorkFlow.Sections.Queries;


namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private IMediator _mediator;
        public WorkflowController(IMediator mediator)
        {
            _mediator = mediator ?? throw new NullReferenceException("IMediator is null");
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProject(CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetAllProjectsQuery(), token);
                if (result == null || !result.Any()) return NotFound(result);
                foreach(var project in result)
                {
                    if (project.Sections.Count() != 0)
                    {
                        Console.WriteLine($"Project: {project.Name}, Sections Count: {project.Sections.Count()}");
                    }
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to retrieve project list");
            }
        }


        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(request);
                if(result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create project {ex.Message}");
            }
        }


        [HttpGet("GetAllSections")]
        public async Task<IActionResult> GetAllSections(CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetAllSectionQuery(), token);
                if (result == null || !result.Any()) return NotFound(result);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to retrieve project list");
            }
        }


        [HttpPost("CreateSection")]
        public async Task<IActionResult> CreateSection([FromBody] CreateSectionCommand request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(request);
                if (result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create project {ex.Message}");
            }
        }
    }
}
