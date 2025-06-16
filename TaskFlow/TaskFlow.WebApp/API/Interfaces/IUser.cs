using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.WebApp.API.Interfaces
{
    public interface IUser
    {
        public Task<Member> GetMemberById(ushort Id, CancellationToken token);
        public Task<Admin> GetAdminById(ushort Id, CancellationToken token);
    }
}
