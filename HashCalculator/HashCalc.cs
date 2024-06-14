using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace HashCalculator
{
    public static class HashCalc
    {
        public static string H256(string filePath)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha256.ComputeHash(fs);
                    return BitConverter.ToString(hashBytes);
                }
            }
        }

        public static string H512(string filePath)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha512.ComputeHash(fs);
                    return BitConverter.ToString(hashBytes);
                }
            }
        }

    }
}
