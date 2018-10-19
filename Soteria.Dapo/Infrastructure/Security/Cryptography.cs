using Soteria.DataComponents.Resource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Infrastructure
{
    public static class Cryptography
    {
        private static readonly byte[] salt = Encoding.ASCII.GetBytes(Constants.CryptoKey);
        public static string Encrypt(string textToEncrypt)
        {
            var algorithm = GetAlgorithm();
            if (string.IsNullOrWhiteSpace(textToEncrypt)) return "";

            byte[] encryptedBytes;
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
            {
                byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
                encryptedBytes = InMemoryCrypt(bytesToEncrypt, encryptor);
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        public static string EncryptArray(string[] texts)
        {
            if (texts.Length <= 0) return "";
            return Encrypt(string.Join("|", texts));
        }
        public static string Decrypt(string encryptedText)
        {
            var algorithm = GetAlgorithm();
            if (string.IsNullOrWhiteSpace(encryptedText)) return "";
            byte[] descryptedBytes;
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                descryptedBytes = InMemoryCrypt(encryptedBytes, decryptor);
            }
            return Encoding.UTF8.GetString(descryptedBytes);
        }
        public static string[] DecryptArray(string encryptedText)
        {
            if (string.IsNullOrWhiteSpace(encryptedText)) return null;
            string ret = Decrypt(encryptedText);
            return ret.Split(new char[] { '|' });
        }
        private static byte[] InMemoryCrypt(byte[] data, ICryptoTransform transform)
        {
            MemoryStream memory = new MemoryStream();
            using (Stream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
            {
                stream.Write(data, 0, data.Length);
            }
            return memory.ToArray();
        }
        private static AesCryptoServiceProvider GetAlgorithm()
        {
            var key = new Rfc2898DeriveBytes(Constants.CryptoKey, salt);
            var algorithm = new AesCryptoServiceProvider();
            int bytesForKey = algorithm.KeySize / 8;
            int bytesForIV = algorithm.BlockSize / 8;
            algorithm.Key = key.GetBytes(bytesForKey);
            algorithm.IV = key.GetBytes(bytesForIV);
            return algorithm;
        }
    }
}
