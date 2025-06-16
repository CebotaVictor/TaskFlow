using TaskFlow.Application.Contracts.Authentication;
using TaskFlow.Application.Users.Members.Commands;

namespace TaskFlow.WebApp.API.Interfaces
{
    public interface IAuth
    {
        public Task<object> Register(RegisterRequestApi request, CancellationToken token);
        public Task<string> Login(LoginRequest request, CancellationToken token);
    }
}
