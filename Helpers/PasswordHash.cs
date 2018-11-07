using System;

namespace UserAuth.API.Helpers
{
    public static class PasswordHash
    {
        public static (byte[], byte[]) CreatePasswordHash(string passsword, byte[] passwordHash, byte[] passswordSalt)
        {
            using ( var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passswordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
                return (passswordSalt, passwordHash);
            }
        }

        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(key: passwordSalt))
            {
                var newHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (newHash[i] != passwordHash[i]) 
                        return false;
                }
                return true;
            }
        }
    }   
}
