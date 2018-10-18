using System;

namespace UserAuth.API.Helpers
{
    public static class PasswordHash
    {
        public static Tuple<byte[], byte[]> CreatePasswordHash(string passsword, byte[] passwordHash, byte[] passswordSalt)
        {
            using ( var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passswordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
                return Tuple.Create(passswordSalt, passwordHash);
            }
        }   
    }
}
