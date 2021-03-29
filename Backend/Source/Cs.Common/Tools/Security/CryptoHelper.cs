namespace Cs.Common.Tools.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public static class CryptoHelper
    {
        public static PasswordHash HashPassword(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashed = Hash(password, Encoding.Unicode.GetString(salt));

            var result = new PasswordHash
            {
                Salt = Encoding.Unicode.GetString(salt),
                Hash = hashed
            };

            return result;
        }

        public static bool CheckPassword(string password, string salt, string hash)
        {
            var hashed = Hash(password, salt);

            return hashed == hash;
        }

        private static string Hash(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.Unicode.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }

    public class PasswordHash
    {
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}