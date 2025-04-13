using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Queries;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Members.Handlers
{
    public class GetMembersHandler : IRequestHandler<GetMemberQuery, IEnumerable<Member>>
    {
        private readonly IUnitOfWork<Member> _unitOfWork;
        
        public GetMembersHandler(IUnitOfWork<Member> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IUnitOfWork is null in GetMemberHandler");
        }

        public async Task<IEnumerable<Member>> Handle(GetMemberQuery request, CancellationToken token) 
        {
            return await _unitOfWork.Users.GetAllEntities();   
        }
    }
}
