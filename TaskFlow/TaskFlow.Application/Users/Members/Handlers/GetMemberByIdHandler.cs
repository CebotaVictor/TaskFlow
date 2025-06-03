using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Queries;
using Microsoft.Extensions.Logging;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Members.Handlers
{
    public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, Member>
    {
        public IUsersUnitOfWork ?_unitOfWork;
        public ILogger<GetMemberByIdHandler> _logger;
        
        public GetMemberByIdHandler(IUsersUnitOfWork unitOfWork, ILogger<GetMemberByIdHandler> logger) 
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("unit o work is null in GetMemberByIdHandler");
            _logger = logger ?? throw new NullReferenceException("unit o work is null in GetMemberByIdHandler");
        }

        public Task<Member> Handle(GetMemberByIdQuery request, CancellationToken token)
        {
            try
            {
                if (request == null) { throw new NullReferenceException("request is null in GetMemberByIdHandler"); }
                if (_unitOfWork == null) 
                { 
                    _logger.LogError("unit o work is null in GetMemberByIdHandler");
                    return null!;
                }
                var result = _unitOfWork.Users.GetEntityById(request.id);
                if(result == null) throw new NullReferenceException("GetEntityById returned null in the GetMemberByIdHandler");  
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetEntityById returned null in the GetMemberByIdHandler {ex.Message}");
                return null!;
            }
        }
    }
}
