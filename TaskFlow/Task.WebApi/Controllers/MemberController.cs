﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Users.Admins.Commands;
using TaskFlow.Application.Users.Admins.Queries;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;

namespace Task.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMemberById(ushort id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetMemberByIdQuery { id = id }, token);
                if (result == null) return NotFound("Member by id not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateMember([FromBody] UserDTO Request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new AddMemberCommand { MemberField = Request }, token);
                if (result == null) return BadRequest(result);
                return Ok(Request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create the member {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditMember([FromBody] UserDTO Request, ushort id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new UpdateMemberCommand { Id = id, MemberField = Request }, token);
                if (result != null) return BadRequest("Failed to update member");
                return Ok(result);
            }
            catch
            {
                return BadRequest("Failed to update Member");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(ushort id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new DeleteMemberCommand { Id = id }, token);
                if (result != null) return Ok(result);
                throw new Exception($"Error while deleting user with id {id} in MemberController");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
