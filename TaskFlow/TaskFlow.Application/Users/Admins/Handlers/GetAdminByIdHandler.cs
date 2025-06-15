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
using TaskFlow.Application.Users.Admins.Queries;

namespace TaskFlow.Application.Users.Admins.Handlers
{
    public class GetAdminByIdHandler : IRequestHandler<GetAdminByIdQuery, Admin>
    {
        public IUsersUnitOfWork ?_unitOfWork;
        public ILogger<GetAdminByIdHandler> _logger;
        
        public GetAdminByIdHandler(IUsersUnitOfWork unitOfWork, ILogger<GetAdminByIdHandler> logger) 
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("unit o work is null in GetAdminByIdHandler");
            _logger = logger ?? throw new NullReferenceException("unit o work is null in GetAdminByIdHandler");
        }

        public Task<Admin> Handle(GetAdminByIdQuery request, CancellationToken token)
        {
            try
            {
                if (request == null) { throw new NullReferenceException("request is null in GetAdminByIdHandler"); }
                if (_unitOfWork == null) 
                { 
                    _logger.LogError("unit o work is null in GetAdminByIdHandler");
                    return null!;
                }
                var result = _unitOfWork.Admins.GetEntityById(request.id);
                if(result == null) throw new NullReferenceException("GetEntityById returned null in the GetAdminByIdHandler");  
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetEntityById returned null in the GetAdminByIdHandler {ex.Message}");
                return null!;
            }
        }
    }
}
