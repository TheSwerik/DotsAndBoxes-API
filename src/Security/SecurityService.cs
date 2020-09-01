using System;
using System.Security.Cryptography;

namespace API.Security
{
    public static class SecurityService
    {
        private const int Iterations = 1000;

        public static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[20]);
            return HashPassword(password, salt);
        }

        public static string HashPassword(string password, byte[] saltBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
            var hash = pbkdf2.GetBytes(20);

            var hashBytes = new byte[40];
            Array.Copy(saltBytes, 0, hashBytes, 0, 20);
            Array.Copy(hash, 0, hashBytes, 20, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}