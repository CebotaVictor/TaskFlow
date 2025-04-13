using MediatR;
using System;
using System.Collections.Generic;
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
    public class UpdateMemberHandler : IRequestHandler<UpdateMemberCommand, UserResponse>
    {
        private IUnitOfWork<Member> _unitOfWork;

        public UpdateMemberHandler(IGenericRepository<Member> user, IUnitOfWork<Member> UnitOfWork)
        {
            _unitOfWork = UnitOfWork ?? throw new NullReferenceException("IGeneriRepository is null in UpdateMemberHandler");
        }
        public async Task<UserResponse> Handle(UpdateMemberCommand request, CancellationToken token)
        {
            try
            {
                Member member = await _unitOfWork.Users.GetEntityById(request.Id);

                if (member == null || request.MemberField == null) throw new NullReferenceException($"User with id {request.Id} is non existent");
                member.Email = request.MemberField.Email;
                member.Username = request.MemberField.Username;
                member.Password = request.MemberField.Password;
                member.Id = request.Id;
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new UserResponse(1, member.Email);
                }

                throw new Exception("Save changes did not worked as indended for UpdateMemberHandler");
            }
            catch (Exception ex)
            {
                return new UserResponse(1, ex.Message);
            }
        }
    }
}
