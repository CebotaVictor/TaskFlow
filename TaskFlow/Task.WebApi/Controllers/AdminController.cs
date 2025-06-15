using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Users.Admins.Commands;
using TaskFlow.Application.Users.Admins.Queries;

namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAdmins(CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetAdminQuery(), token);
                if (result == null || !result.Any()) return NotFound(result);

                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to retrieve admin list");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAdminById(ushort id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetAdminByIdQuery { id = id }, token);
                if (result == null) return NotFound("Admin by id not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdmin([FromBody] UserDTO Request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new AddAdminCommand { AdminField = Request }, token);
                if (result == null) return BadRequest(result);
                return Ok(Request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create the admin {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAdmin([FromBody] UserDTO Request, ushort id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new UpdateAdminCommand { Id = id, AdminField = Request }, token);
                if (result != null) return BadRequest("Failed to update admin");
                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to update Admin");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdmin(ushort id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAdminCommand { Id = id }, token);
                if (result != null) return Ok(result);
                throw new Exception($"Error while deleting user with id {id} in AdminController");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}

