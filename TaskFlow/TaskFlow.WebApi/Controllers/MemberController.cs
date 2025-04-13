using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using NuGet.Common;
using System.Reflection;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;



namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private readonly IMediator _mediator;

       public MemberController(IMediator mediator) 

        {
            _mediator = mediator;
        }
        // GET: MemberController
        //public ActionResult CreateMemberView()
        //{
        //    return View();
        //}

        // GET: MemberController/Details/5
        [HttpGet]
        public async Task<ActionResult> GetAllMembers(CancellationToken token)

        {
            try
            {
                var result = await _mediator.Send(new GetMemberDTO(), token);
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

        // POST: MemberController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MemberDTO Request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var result = await _mediator.Send(new AddMemberCommand{MemberField = Request}, token);   
                if(result != null) return BadRequest(result);
                return Ok(Request);
            }
            catch(Exception ex) 
            {
                return BadRequest("Failed to retreive member list");
            }
        }


        // POST: MemberController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddMemberCommand Request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(Request, token);   
                return Ok(Request);
            }
            catch
            {

                return BadRequest("Failed to create Member");
            }
        }



        //POST: MemberController/Edit/5
        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] MemberDTO Request, ushort id, CancellationToken token)
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

        // GET: MemberController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: MemberController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        // GET: MemberController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: MemberController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
