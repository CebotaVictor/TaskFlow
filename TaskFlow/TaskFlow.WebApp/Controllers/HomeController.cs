using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.WebApp.API.Interfaces;
using TaskFlow1.Models;

namespace TaskFlow1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser _user;
        private ProfileDTO ?_profile;

        public HomeController(ILogger<HomeController> logger, IUser user)
        {
            _logger = logger;
            _user = user;
        }

        public IActionResult Biling()
        {
            return View();
        }


        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Template()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile(CancellationToken token)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var role = User.FindFirst(ClaimTypes.Role)?.Value;


                if (userId == null) return View("Error", new ErrorViewModel { RequestId = "User ID not found" });

                ushort id = ushort.Parse(userId!);

                var member = await _user.GetMemberById(id, token);
                Admin admin = null!;

                if (member == null)
                {
                    admin = await _user.GetAdminById(id, token);
                    _profile = new ProfileDTO
                    {
                        Id = id,
                        Email = email,
                        Role = role,
                        Username = admin?.Username,
                    };
                    return View(_profile);  
                }

                _profile = new ProfileDTO
                {
                    Id = ushort.Parse(userId!),
                    Email = email,
                    Role = role,
                    Username = member?.Username,
                };
                return View(_profile);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error retrieving profile information");
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
