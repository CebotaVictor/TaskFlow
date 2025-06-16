using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TaskFlow.Application.Autentication.Handlers
{
    internal class CookieGenerator
    {
        private readonly IHttpContextAccessor ?_httpContextAccessor;

        public CookieGenerator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor), "IHttpContextAccessor is null in CookieGenerator");
        }
        public void CreateCookie(string Token)
        {
            _httpContextAccessor!.HttpContext!.Response.Cookies.Append("access_token", Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });

        }
    }
}
