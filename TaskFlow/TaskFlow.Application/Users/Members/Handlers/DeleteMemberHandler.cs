﻿using MediatR;
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
    public class DeleteAdminHandler : IRequestHandler<DeleteMemberCommand, UserResponse>
    {
        private IUsersUnitOfWork _unitOfWork;

        public DeleteAdminHandler(IUsersUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork ?? throw new NullReferenceException("IGeneriRepository is null in UpdateMemberHandler");
        }


        public async Task<UserResponse> Handle(DeleteMemberCommand request, CancellationToken token)
        {
            if (request == null) { return new UserResponse(-1, "");}
            try
            {
                await _unitOfWork.Users.DeletByIdGenericAsync(request.Id);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new UserResponse(1, "Successfully deleted"); 
                }
                throw new Exception($"Error while deleting the member with id {request.Id}");
            }
            catch (Exception ex)
            { 
                return new UserResponse(-1, ex.Message);
            }
        }
    }
}
