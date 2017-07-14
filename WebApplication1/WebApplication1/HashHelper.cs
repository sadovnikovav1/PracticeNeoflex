using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1
{
    public class HashHelper
    {
        private static HashAlgorithm hasher;

        static HashHelper()
        {
            hasher = SHA512.Create();
        }

        public static string GetPasswordFromHash(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] hashBytes = hasher.ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashBytes);
        }
    }
}