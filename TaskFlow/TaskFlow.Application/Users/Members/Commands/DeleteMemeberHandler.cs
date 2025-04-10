using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Members.Commands
{
    public class DeleteMemeberHandler
    {
        private IGenericRepository<Member> _userRepository;
        private IUnitOfWork<Member> _unitOfWork;

        public DeleteMemeberHandler(IGenericRepository<Member> user, IUnitOfWork<Member> UnitOfWork)
        {
            _userRepository = user;
            _unitOfWork = UnitOfWork;
        }


        public async Task<UserResponse> Handle(int id,CreateMemeberCommand request, CancellationToken token)
        {
            if (request == null) { return new UserResponse(-1, "");}
            try
            {
                await _unitOfWork.Users.DeletByIdGenericAsync(id);
                return new UserResponse(1, request.Email);
            }
            catch (Exception ex)
            { 
                return new UserResponse(-1, ex.Message);
            }
        }
    }
}
