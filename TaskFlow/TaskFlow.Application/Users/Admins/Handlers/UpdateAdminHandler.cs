using MediatR;
using System;
using System.Collections.Generic;
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
    public class UpdateAdminHandler : IRequestHandler<UpdateAdminCommand, UserResponse>
    {
        private IUsersUnitOfWork _unitOfWork;

        public UpdateAdminHandler(IUsersGenericRepository<Admin> user, IUsersUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork ?? throw new NullReferenceException("IGeneriRepository is null in UpdateAdminHandler");
        }
        public async Task<UserResponse> Handle(UpdateAdminCommand request, CancellationToken token)
        {
            try
            {
                Admin admin = await _unitOfWork.Admins.GetEntityById(request.Id);

                if (admin == null || request.AdminField == null) throw new NullReferenceException($"User with id {request.Id} is non existent");
                admin.Email = request.AdminField.Email;
                admin.Username = request.AdminField.Username;
                admin.Password = request.AdminField.Password;
                admin.Id = request.Id;
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new UserResponse(1, admin.Email);
                }

                throw new Exception("Save changes did not worked as intended for UpdateAdminHandler");
            }
            catch (Exception ex)
            {
                return new UserResponse(1, ex.Message);
            }
        }
    }
}
