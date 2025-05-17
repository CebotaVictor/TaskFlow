using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Members.Handlers
{
    public class AddMemberHandler : IRequestHandler<AddMemberCommand, UserResponse>
    {
        private IUnitOfWork<Member> _unitOfWork;

        public AddMemberHandler(IUnitOfWork<Member> UnitOfWork)
        {   
            _unitOfWork = UnitOfWork ?? throw new NullReferenceException("IGenericRepository is null in AddMemberHandler");
        }

        public async Task<UserResponse> Handle(AddMemberCommand request, CancellationToken token)
        {
            if (request == null) { return new UserResponse(-1, ""); }

            try
            {
                if (request.MemberField == null) return new UserResponse(-1, "");
                Member newMember = new Member
                {
                    Username = request.MemberField.Username,
                    Email = request.MemberField.Email,
                    Password = request.MemberField.Password,
                    DataAdded = DateTime.Now
                };
                await _unitOfWork.Users.AddGeneric(newMember);
                
                if(await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new UserResponse(1,newMember.Email);
                }

                throw new Exception("Save changes did not worked as indended for AddMemberHandler");
            }
            catch (Exception ex)
            {
                return new UserResponse(-1, ex.Message);
            }       
        }

    }
}
