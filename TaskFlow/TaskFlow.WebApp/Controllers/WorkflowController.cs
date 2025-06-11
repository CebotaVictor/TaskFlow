using MediatR;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using TaskFlow.Application.WorkFlow.Projects.Queries;
using TaskFlow.WebApp.API.Interfaces;

namespace TaskFlow.WebApp.Controllers
{
    public class WorkflowController : Controller
    {
        private readonly IProject _projectApiService;


        public WorkflowController(IProject projectApiService)
        {
            _projectApiService = projectApiService ?? throw new NullReferenceException("IProject API Service is null");
        }
        public ActionResult ProjectBoardView() { return View(); }

        [HttpGet]
        public async Task<IActionResult> ProjectListView(CancellationToken token)
        {
            try
            {
                var result = await _projectApiService.GetAllProjectsAsync(token);
                if (result == null || !result.Any()) return NotFound(result);
                return View(result);
            }
            catch
            {
                return BadRequest("Failed to retrieve project list");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewPage(CancellationToken token) 
        {
            try
            {
                var result = await _projectApiService.GetAllProjectsAsync(token);
                if (result == null || !result.Any()) return NotFound(result);
                return View(result);
            }
            catch
            {
                return BadRequest("Failed to retrieve project list");
            }
        }
    }
}
