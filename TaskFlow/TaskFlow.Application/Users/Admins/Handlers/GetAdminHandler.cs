using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Admins.Queries;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Admins.Handlers
{
    public class GetAdminHandler : IRequestHandler<GetAdminQuery, IEnumerable<Admin>>
    {
        private readonly IUsersUnitOfWork _unitOfWork;
        
        public GetAdminHandler(IUsersUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IUnitOfWork is null in GetAdminHandler");
        }

        public async Task<IEnumerable<Admin>> Handle(GetAdminQuery request, CancellationToken token) 
        {
            return await _unitOfWork.Admins.GetAllEntities();   
        }
    }
}
