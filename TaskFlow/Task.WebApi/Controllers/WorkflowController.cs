using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Application.WorkFlow.Projects.Queries;
using TaskFlow.Application.WorkFlow.Sections.Commands;
using TaskFlow.Application.WorkFlow.Sections.Queries;
using TaskFlow.Application.WorkFlow.UserTask.Command;
using TaskFlow.Application.WorkFlow.UserTask.Handler;
using TaskFlow.Application.WorkFlow.UserTask.Queries;
using TaskFlow.Infrastructure.UserTask.Command;


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

        [HttpGet("GetProjectById")]
        public async Task<IActionResult> GetAllProject(ushort Id,CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetProjectByIdQuery { Id = Id }, token);
                if (result == null) return NotFound(result);
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
                var result = await _mediator.Send(request, token);
                if(result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create project {ex.Message}");
            }
        }

        [HttpDelete("DeleteProject")]
        public async Task<IActionResult> DeleteProject(ushort Id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new DeleteProjectCommand {Id = Id}, token);
                if (result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete project {ex.Message}");
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


        [HttpGet("GetAllSectionsFromProjectId")]
        public async Task<IActionResult> GetAllSectionsFromProjectId(ushort Id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetProjectByIdQuery { Id = Id}, token);
                if (result == null) return NotFound(result);

                return Ok(result.Sections);
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
                var result = await _mediator.Send(request, token);
                if (result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create project {ex.Message}");
            }
        }

        [HttpDelete("DeleteSection")]
        public async Task<IActionResult> DeleteSection(ushort Id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new DeleteSectionCommand { Id = Id }, token);
                if (result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete section {ex.Message}");
            }
        }



        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(request, token);
                if (result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create task {ex.Message}");
            }
        }

        [HttpPost("CreateSubTask")]
        public async Task<IActionResult> CreateSybTask([FromBody] CreateSubTaskCommand request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(request, token);
                if (result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create subtask {ex.Message}");
            }
        }

        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteTask(ushort Id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new DeleteTaskCommand { Id = Id }, token);
                if (result == null) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete task {ex.Message}");
            }
        }



        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks(CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetAllTasksQuery(), token);
                if (result == null || !result.Any()) return NotFound(result);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to retrieve task list");
            }
        }



    }
}
