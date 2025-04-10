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
    public class UpdateMemberHandler
    {
        private IGenericRepository<Member> _userRepository;
        private IUnitOfWork<Member> _unitOfWork;

        public UpdateMemberHandler(IGenericRepository<Member> user, IUnitOfWork<Member> UnitOfWork)
        {
            _userRepository = user;
            _unitOfWork = UnitOfWork;
        }
        public async Task<UserResponse> Handle(int id, CreateMemeberCommand request, CancellationToken token)
        {
            try
            {
                Member member = await _unitOfWork.Users.GetEntityById(id);

                if (member == null) throw new NullReferenceException($"User with id {id} is non existent");
                member.Email = request.Email;
                member.Username = request.Username;
                member.Password = request.Password;
                member.Id = id;
                return new UserResponse(1, member.Email);
            }
            catch (Exception ex)
            {
                return new UserResponse(1, ex.Message);
            }
        }
    }
}
