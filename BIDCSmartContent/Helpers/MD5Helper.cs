using System.Security.Cryptography;
using System.Text;

namespace BIDVSmartContent.Helpers
{
    public static class MD5Helper
    {
        public static string GetMd5(string text)
        {
            var md5Crypto = new MD5CryptoServiceProvider();
            md5Crypto.ComputeHash(UTF8Encoding.UTF8.GetBytes(text));
            byte[] results = md5Crypto.Hash;
            StringBuilder str = new StringBuilder();
            foreach (var result in results)
            {
                str.Append(result.ToString("x2"));
            }
            return str.ToString().ToUpper();
        }

        public static string CreateMD5(string text)
        {
            var md5Crypto = new MD5CryptoServiceProvider();
            md5Crypto.ComputeHash(UTF8Encoding.UTF8.GetBytes(text));
            byte[] results = md5Crypto.Hash;
            StringBuilder str = new StringBuilder();
            foreach (var result in results)
            {
                str.Append(result.ToString("X2"));
            }
            return str.ToString().ToUpper();
        }
    }
}