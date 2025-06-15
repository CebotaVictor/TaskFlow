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
using TaskFlow.Application.Users.Admins.Commands;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Admins.Handlers
{
    public class AddAdminHandler : IRequestHandler<AddAdminCommand, UserResponse>
    {
        private IUsersUnitOfWork _unitOfWork;

        public AddAdminHandler(IUsersUnitOfWork UnitOfWork)
        {   
            _unitOfWork = UnitOfWork ?? throw new NullReferenceException("IGenericRepository is null in AddMemberHandler");
        }

        public async Task<UserResponse> Handle(AddAdminCommand request, CancellationToken token)
        {
            if (request == null) { return new UserResponse(-1, ""); }

            try
            {
                if (request.AdminField == null) return new UserResponse(-1, "");
                Admin newMember = new Admin
                {
                    Username = request.AdminField.Username,
                    Email = request.AdminField.Email,
                    Password = request.AdminField.Password,
                    DataAdded = DateTime.Now
                };
                await _unitOfWork.Admins.AddGeneric(newMember);
                
                if(await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new UserResponse(1,newMember.Email);
                }

                throw new Exception("Save changes did not worked as indended for AddAdminHandler");
            }
            catch (Exception ex)
            {
                return new UserResponse(-1, ex.Message);
            }       
        }

    }
}
