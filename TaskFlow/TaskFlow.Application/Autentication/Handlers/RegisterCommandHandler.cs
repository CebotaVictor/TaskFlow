using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Autentication.Commands;
using TaskFlow.Application.Contracts.Authentication;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.Authentication;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.Service;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Helpers.Service;

namespace TaskFlow.Application.Autentication.Handlers
{
    
    public class RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUsersUnitOfWork unitOfWork, IMediator mediator, IConfiguration configuration, IPassword paswdHandling) : IRequestHandler<RegisterCommand, UserResponse>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUsersUnitOfWork? _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator;
        private readonly IConfiguration _configuration = configuration;
        private readonly IPassword ? _paswdHandling = paswdHandling;

        public async Task<UserResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                if (await _unitOfWork!.Members.GetMemberByEmail(request.Email) is not null)
                {
                    throw new Exception("Dublicate email");
                }

                string Hash = _paswdHandling!.PasswordHashing(request.Password);

                var user = await _mediator.Send(new AddMemberCommand { MemberField = new UserDTO(request.Username, request.Email, Hash) });
                
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UserResponse(-1, null!);
            }

        }
    }
}
