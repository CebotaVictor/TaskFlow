using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Service;
using TaskFlow.Helpers.Service;

namespace TaskFlow.Infrastructure.Servicies
{
    public class PasswordHandingService : IPassword
    {
        private readonly IConfiguration? _configuration;

        public PasswordHandingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string PasswordHashing(string Password)
        {
            string salt = SaltHelper.getSalt();
            string hash = PasswordHelper.HashPassword(Password, salt);
            string[] strings = SaltHelper.GetDevidedSalt(salt);

            return $"{strings[0]}{hash}{strings[1]}";
        }

        public bool VerifyPassword(string Hash, string Password)
        {
            SaltHelper.InitializeConfiguration(_configuration!);
            string[] strings = SaltHelper.GetSalt(Hash);

            if (strings[1] == PasswordHelper.HashPassword(Password, strings[0])) return true;

            return false;
        }
    }
}
