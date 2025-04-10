using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Members.Commands
{
    public class AddMemberHandler 
    {
        private IGenericRepository<Member> _userRepository;
        private IUnitOfWork<Member> _unitOfWork;

        public AddMemberHandler(IGenericRepository<Member> user, IUnitOfWork<Member> UnitOfWork)
        {   
            _userRepository = user;
            _unitOfWork = UnitOfWork; 
        }

        public async Task<UserResponse> Handle(CreateMemeberCommand request, CancellationToken token)
        {
            if (request == null) { return new UserResponse(-1, ""); }

            try
            {
                Member newMember = new Member
                {
                    Username = request.Username,
                    Email = request.Email,
                    Password = request.Password,
                    DataAdded = DateTime.Now
                };
                await _unitOfWork.Users.AddGeneric(newMember);
                return new UserResponse(1,newMember.Email);
            }
            catch (Exception ex)
            {
                return new UserResponse(-1, ex.Message);
            }       
        }

    }
}
