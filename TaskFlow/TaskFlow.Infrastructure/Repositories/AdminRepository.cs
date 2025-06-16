using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.BL;

namespace TaskFlow.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UsersGenericRepository<Member>? _repository;
        private readonly UsersDBContext? _context;
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(UsersGenericRepository<Member> repository, UsersDBContext context, ILogger<AdminRepository> logger, IConfiguration configuration)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<Admin> GetAdminByEmail(string Email)
        {
            var fetchedmember = await _context!.Admins.FirstOrDefaultAsync(x => x.Email == Email);
            if (fetchedmember == null) { _logger.LogInformation("Fail to fetch member by email"); return null!; }
            return fetchedmember;
        }
    }
}
