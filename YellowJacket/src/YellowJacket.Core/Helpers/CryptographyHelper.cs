using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YellowJacket.Core.Helpers
{
    public class CryptographyHelper
    {
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        /// <summary>
        ///H ash an input string and return the hash as a 32 character hexadecimal string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            foreach (byte item in data)
            {
                sBuilder.Append(item.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
