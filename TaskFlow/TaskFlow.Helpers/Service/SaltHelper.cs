using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TaskFlow.Helpers.Service
{
    public class SaltHelper
    {
        private static string ?_saltLength;
        private static string? _passLength;

        public static void InitializeConfiguration(IConfiguration configuration)
        {
            _saltLength = configuration!["Salt:Half"];
            _passLength = configuration!["Pasw:Size"];
        }

        public static string getSalt()
        {
            byte[] bytes = new byte[16];

            using (var KeyGenerator = RandomNumberGenerator.Create())
            {
                KeyGenerator.GetBytes(bytes);

                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Devide the salt into two equally parts
        /// </summary>
        /// <param name="salt"></param>
        /// <returns>A array of two strings that represent the halved salt</returns>
        public static string[] GetDevidedSalt(string salt)
        {
            int initialSize = (salt.Length / 2);

            string a = salt.Substring(0, initialSize);
            string b = salt.Substring(initialSize, initialSize);
            string[] strings = new string[2];
            strings[0] = a;
            strings[1] = b;

            return new string[] {a,b};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>A array that consists from the salt and hash</returns>
        public static string[] GetSalt(string hash) 
        {
            string paswLength = _passLength!;
            string saltLength = _saltLength!;

            int sLength, pLength;
            int.TryParse(saltLength, out sLength);
            int.TryParse(paswLength, out pLength);

            string salt = hash.Substring(0, sLength) + hash.Substring(sLength+pLength, sLength);

            string onlyHash = hash.Substring(sLength, pLength);

            return new string[] { salt, onlyHash };
        }
    }
}
