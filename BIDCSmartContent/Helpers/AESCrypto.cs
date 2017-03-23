using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BIDVSmartContent.Helpers
{
    public class AESCrypto
    {
        private const string IvAsString = "@5f2rv8pa1yW14I3";

        public static string EncryptToBase64String(string input, string password)
        {
            byte[] utfData = Encoding.UTF8.GetBytes(input);
            return EncryptToBase64String(utfData, password);
        }

        public static string EncryptToBase64String(byte[] utfData, string password)
        {
            var encryptBytes = EncryptToBytes(utfData, password);
            return Convert.ToBase64String(encryptBytes);
        }

        public static byte[] EncryptToBytes(string input, string password)
        {
            byte[] utfData = Encoding.UTF8.GetBytes(input);
            return EncryptToBytes(utfData, password);
        }

        public static byte[] EncryptToBytes(byte[] utfData, string password)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(password);

            if (saltBytes.Length > 16) return null;
            byte[] encryptedBytes;
            using (var aes = new AesManaged())
            {
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;

                var aeSKey = new byte[16];
                for (int i = 0; i < saltBytes.Length; i++)
                {
                    aeSKey[i] = saltBytes[i];
                }
                aes.Key = aeSKey;
                aes.IV = Encoding.UTF8.GetBytes(IvAsString);

                using (ICryptoTransform encryptTransform = aes.CreateEncryptor())
                {
                    using (var encryptedStream = new MemoryStream())
                    {
                        using (var encryptor = new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
                        {
                            encryptor.Write(utfData, 0, utfData.Length);
                            encryptor.Flush();
                            encryptor.Close();

                            encryptedBytes = encryptedStream.ToArray();
                        }
                    }
                }
            }
            return encryptedBytes;
        }

        public static string DecryptBase64String(string input, string password)
        {
            byte[] encryptedBytes = Convert.FromBase64String(input);
            var decryptBytes = DecryptFromBytes(encryptedBytes, password);
            return Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
        }

        public static byte[] Decrypt(string input, string password)
        {
            byte[] encryptedBytes = Convert.FromBase64String(input);
            return DecryptFromBytes(encryptedBytes, password);
        }

        public static string Decrypt(byte[] encryptedBytes, string password)
        {
            var decryptBytes = DecryptFromBytes(encryptedBytes, password);
            return Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
        }

        public static byte[] DecryptFromBytes(byte[] encryptedBytes, string password)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(password);
            if (saltBytes.Length > 16) return null;
            byte[] decryptBytes;
            using (var aes = new AesManaged())
            {
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;

                var aeSKey = new byte[16];
                for (int i = 0; i < saltBytes.Length; i++)
                {
                    aeSKey[i] = saltBytes[i];
                }
                aes.Key = aeSKey;
                aes.IV = Encoding.UTF8.GetBytes(IvAsString);
                using (ICryptoTransform decryptTransform = aes.CreateDecryptor())
                {
                    using (var decryptedStream = new MemoryStream())
                    {
                        var decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write);
                        decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
                        decryptor.Flush();
                        decryptor.Close();
                        decryptBytes = decryptedStream.ToArray();
                    }
                }
            }
            return decryptBytes;
        }
    }
}