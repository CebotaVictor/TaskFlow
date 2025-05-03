using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.BL;
using System.Linq;
using TaskFlow.Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Helpers.Service;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;

namespace TaskFlow.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GenericRepository<Member> ?_repository;
        private readonly UsersDBContext ?_context;
        private readonly ILogger<MemberRepository> _logger;

        public MemberRepository(GenericRepository<Member> repository, UsersDBContext context, ILogger<MemberRepository> logger, IConfiguration configuration)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<Member> GetMemberByEmail(string Email)
        {
            var fetchedmember = await _context!.Members.FirstOrDefaultAsync(x => x.Email == Email);
            if (fetchedmember == null) { _logger.LogError("Fail to fetch member by email");  return null!; }
            return fetchedmember;
        }
    }
}
