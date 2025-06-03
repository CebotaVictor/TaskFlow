using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;
using TaskFlow.Application.Autentication.Commands;
using TaskFlow.Application.Autentication.Queries;
using TaskFlow.Application.Contracts.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly IMediator ?_mediator;
        private readonly ILogger<ApiAuthController> ?_logger;

        public ApiAuthController(IMediator mediator, ILogger<ApiAuthController> logger)
        {
            _mediator = mediator ?? throw new NullReferenceException("Mediator is null in ApiAuthController");
            _logger = logger;
        }

        // POST api/<ApiAuthController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestApi request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegisterCommand command = new RegisterCommand
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };
            var response = await _mediator!.Send(command, token);

            if(response.UserEmail is null)
            {
                return Conflict();
            }

            return Ok(response.UserEmail);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoginQuery query = new LoginQuery(request.Email,request.password);

            var response = await _mediator!.Send(query,token);

            if(response.Email == null)
            {
                return NotFound();
            }

            return Ok(response.Token);
        }

        //// PUT api/<ApiAuthController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ApiAuthController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
