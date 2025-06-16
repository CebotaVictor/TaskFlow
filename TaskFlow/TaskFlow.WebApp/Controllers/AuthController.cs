using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.ComponentModel.Design.Serialization;
using TaskFlow.Application.Autentication.Handlers;
using TaskFlow.Application.Contracts.Authentication;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.WebApp.API.Interfaces;

namespace TaskFlow.WebApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuth _authService;
        private readonly CookieGenerator _cookieGenerator;
        public AuthController(IAuth authService, CookieGenerator cookieGenerator)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService), "IAuth service is null");
            _cookieGenerator = cookieGenerator ?? throw new ArgumentNullException(nameof(cookieGenerator), "CookieGenerator is null");
        }

        // GET: AuthController
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestApi request,CancellationToken token)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            var response = await _authService.Register(request, token);

            if (response == null)
            {
                ModelState.AddModelError("", "Registration failed. Please try again.");
                return View(request);
            }

            return RedirectToAction("Login");

        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = await _authService.Login(request, token);

            if (response == null)
            {
                ModelState.AddModelError("", "Login failed. Please try again.");
                return View(request);
            }

            _cookieGenerator.CreateCookie(response);

            return RedirectToAction("Profile", "Home");
        }


        // GET: AuthController/Details/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterRequest request, CancellationToken token)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(request);
        //    }

        //    var member = _mapper.Map<Member>(request);
        //    var result = await _userManager.CreateAsync(member, request.Password);
        //    if (!result.Succeeded)
        //    {
        //        foreach(var error in result.Errors)
        //        {
        //            ModelState.TryAddModelError(error.Code, error.Description);
        //        }

        //        return View(request);
        //    }
        //    return View(request);
        //}

        // GET: AuthController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AuthController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
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

        //// GET: AuthController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AuthController/Edit/5
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

        //// GET: AuthController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AuthController/Delete/5
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
