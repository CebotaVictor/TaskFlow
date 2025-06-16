using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Autentication.Queries;
using TaskFlow.Application.Contracts.Authentication;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.Authentication;
using TaskFlow.Application.Interfaces.Service;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Autentication.Handlers
{
    public class LoginCommandHandler (IUsersUnitOfWork unitOfWork, IMediator mediator, IJwtTokenGenerator jwtTokenGenerator, IPassword paswdHandling) : IRequestHandler<LoginQuery, AutenticationResponse>
    {
        private readonly IUsersUnitOfWork ?_unitOfWork = unitOfWork ?? throw new NullReferenceException("Unit of work is null");
        private readonly IMediator ?_mediator = mediator;
        private readonly IJwtTokenGenerator ?_jwtTokenGenerator = jwtTokenGenerator ?? throw new NullReferenceException("Token Generator is null");
        private readonly IPassword? _paswdHandling = paswdHandling;
        private string ?token;
        public async Task<AutenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var member = await _unitOfWork!.Members.GetMemberByEmail(request.Email);
                Admin admin = null!;
                if (member == null) // the member is null, check if admin exists with the same email
                {
                    admin = await _unitOfWork!.AdminRepo.GetAdminByEmail(request.Email);
                    if (admin == null) return new AutenticationResponse(null!, null!); // No member or admin found with this email

                    if (_paswdHandling!.VerifyPassword(admin!.Password, request.password) != true) return null!;

                    token = _jwtTokenGenerator!.GenerateToken(new JwtUserDTO(admin.Id, admin.Email, admin.Role));

                    return new AutenticationResponse(request.Email, token);
                }
                
                if(_paswdHandling!.VerifyPassword(member!.Password, request.password) != true) return null!; 

                token = _jwtTokenGenerator!.GenerateToken(new JwtUserDTO ( member.Id, member.Email, member.Role));

                return new AutenticationResponse ( request.Email, token);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new AutenticationResponse(null!, null!);
            }
        }
    }
}
