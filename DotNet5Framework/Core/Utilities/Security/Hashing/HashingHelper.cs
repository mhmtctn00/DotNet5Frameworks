﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hmac.Key = passwordSalt;
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
        public static string GetJSSHA(string origpw)
        {
            var sha = new System.Security.Cryptography.SHA256Managed();

            sha.ComputeHash(new UTF8Encoding().GetBytes(origpw));
            var password = "";
            foreach (byte b in sha.Hash)
            {
                var x = BitConverter.ToString(new byte[] { b });
                password = password + x;
            }

            password = password.ToLower();
            return password;
        }
    }
}
