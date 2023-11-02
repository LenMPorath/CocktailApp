using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CocktailApp.Services
{
    public class PasswordService
    {
        public static string CreateSalt()
        {
            byte[] salt = new byte[32]; // You can adjust the length of the salt here
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
        public static string ComputeHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
