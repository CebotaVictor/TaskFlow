using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskFlow.Application.WorkFlow.Projects.Command;

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

    }
}
