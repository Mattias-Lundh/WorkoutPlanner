using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace WorkoutPlanner.App.Models
{
    public class Security
    {
        private SHA256CryptoServiceProvider shaProvider;
        private RNGCryptoServiceProvider saltProvider;
        public string Salt { get; set; }
        public string HashedPassword { get; set; }

        public Security()
        {
            shaProvider = new SHA256CryptoServiceProvider();
            saltProvider = new RNGCryptoServiceProvider();
        }

        public void GeneratePasswordData(string password)
        {
            byte[] saltBytes = new byte[10];
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            saltProvider.GetNonZeroBytes(saltBytes);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];

            passwordBytes.CopyTo(saltedPasswordBytes, 0);
            saltBytes.CopyTo(saltedPasswordBytes, passwordBytes.Length);


            HashedPassword = Convert.ToBase64String(shaProvider.ComputeHash(saltedPasswordBytes));
            Salt = Convert.ToBase64String(saltBytes);
        }

        public bool ComparePassword(string hashedPassword, string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];

            passwordBytes.CopyTo(saltedPasswordBytes, 0);
            saltBytes.CopyTo(saltedPasswordBytes, passwordBytes.Length);

            return Convert.ToBase64String(shaProvider.ComputeHash(saltedPasswordBytes)) == hashedPassword;
        }

    }
}