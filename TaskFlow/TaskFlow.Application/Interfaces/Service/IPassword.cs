using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.Interfaces.Service
{
    public interface IPassword
    {
        string PasswordHashing(string Password);

        /// <summary>
        /// Veryfy the password of a member
        /// </summary>
        /// <param name="Hash">The hash from the database</param>
        /// <param name="Password">The password from the request</param>
        /// <returns>A bool that represents the result</returns>
        bool VerifyPassword(string Hash, string Password);
    }
}
