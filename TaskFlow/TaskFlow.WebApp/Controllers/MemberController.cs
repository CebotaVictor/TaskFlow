using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    //[ApiExplorerSettings(GroupName = "member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

       public MemberController(IMediator mediator) 
        {
            _mediator = mediator;
        }
       
        [HttpGet]
        public async Task<ActionResult> GetAllMembers(CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetMemberQuery(), token);
                if (result == null || !result.Any()) return NotFound(result);

                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to retreive member list");
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult> GetMemberById(ushort id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetMemberByIdQuery{id = id}, token);
                if(result == null) return NotFound("Member by id not found");
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserDTO Request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new AddMemberCommand{MemberField = Request}, token);   
                if(result == null) return BadRequest(result);
                return Ok(Request);
            }
            catch(Exception ex) 
            {
                return BadRequest($"Failed to create the member {ex.Message}");
            }
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Edit([FromBody] UserDTO Request, ushort id, CancellationToken token)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new UpdateMemberCommand {Id = id, MemberField = Request }, token);
                if (result != null) return BadRequest("Failed to update member");
                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to update Member");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(ushort id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new DeleteMemberCommand { Id = id }, token);
                if(result != null) return Ok(result);
                throw new Exception($"Error while deleting user with id {id} in MemberController");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
