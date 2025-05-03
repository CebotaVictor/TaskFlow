using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskFlow.WebApi.Extensions.Service
{
    public class JwtService
    {
        private readonly IHttpContextAccessor ?_httpContextAccessor;

        public JwtService(IHttpContextAccessor? httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public int ExtractJwt()
        {
            string authorizationHeader = _httpContextAccessor!.HttpContext!.Request.Headers["Authorization"]!;
            string jwt = authorizationHeader.Split(' ')[1];


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.ReadToken(jwt);
            JwtSecurityToken jwtToken = (JwtSecurityToken)token;
            IEnumerable<Claim> claims = jwtToken.Claims;

            string subject = claims.First(c => c.Type == "sub").Value;

            return int.Parse(subject);
        }
    }
}
