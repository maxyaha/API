using System.IO;
using System.Security.Cryptography;

namespace Shareds.Utilizing
{
    /// <summary>
    /// 
    /// </summary>
    public static class Cypher
    {
        /// <summary>
        /// 
        /// </summary>
        public static string Key = "Asd9847Fg85ihkn52s";

        /// <summary>
        /// Encrypt a byte array into a byte array using a key and an IV 
        /// </summary>
        /// <param name="clear"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clear, byte[] key, byte[] iv)
        {
            // Create a MemoryStream to accept the encrypted bytes 
            MemoryStream memory = new MemoryStream();

            TripleDES algorithm = TripleDES.Create();
            algorithm.Key = key;
            algorithm.IV = iv;

            CryptoStream crypto = new CryptoStream(memory, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            crypto.Write(clear, 0, clear.Length);
            crypto.Close();

            return memory.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clear"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clear, string password)
        {
            PasswordDeriveBytes key = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            return Encrypt(clear, key.GetBytes(24), key.GetBytes(8));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="Key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] cipher, byte[] Key, byte[] iv)
        {
            MemoryStream memory = new MemoryStream();

            TripleDES algorithm = TripleDES.Create();
            algorithm.Key = Key;
            algorithm.IV = iv;

            CryptoStream crypto = new CryptoStream(memory, algorithm.CreateDecryptor(), CryptoStreamMode.Write);
            crypto.Write(cipher, 0, cipher.Length);
            crypto.Close();

            return memory.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] cipher, string password)
        {
            PasswordDeriveBytes key = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            return Decrypt(cipher, key.GetBytes(24), key.GetBytes(8));
        }
    }
}
