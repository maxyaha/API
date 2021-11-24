using Shareds.Setting;
using Shareds.Utilizing;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Shareds.Formatting
{
    public static class GuidFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static Guid ToGuid(this Enum @enum)
        {
            return @enum.ToUInt().ToGuid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static Guid ToGuid(this int @enum)
        {
            return @enum.ToUInt().ToGuid();
        }

        public static Guid ToGuid_ForUnitTest(this int @enum,string key)
        {
            return @enum.ToUInt().ToGuid_ForUnitTest(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Guid ToGuid(this uint number)
        {
            return number.ToGuid(Guid.Empty);
        }

        public static Guid ToGuid_ForUnitTest(this uint number, string key)
        {
            return number.ToGuid_ForUnitTest(Guid.Empty,  key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Guid ToGuid(this uint number, Guid id)
        {
            string uid = id.ToString();
            uid = CharFormatter.Remove(uid, "-{}");
            byte[] bytes = BitConverter.GetBytes(number);
            var encrypted = Cryptography.Encrypt(bytes, SecretServices.Config.Key1);

            uid = StringWriter(uid, encrypted, 0);
            Guid.TryParse(uid, out id);

            return id;
        }


        public static Guid ToGuid_ForUnitTest(this uint number, Guid id, string key)
        {
            string uid = id.ToString();
            uid = CharFormatter.Remove(uid, "-{}");
            byte[] bytes = BitConverter.GetBytes(number);
            var encrypted = Cryptography.Encrypt(bytes, key);

            uid = StringWriter(uid, encrypted, 0);
            Guid.TryParse(uid, out id);

            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private static string StringWriter(this string uid, byte[] bytes, int start)
        {
            StringBuilder build = new StringBuilder(uid);
            string temp;
            int count = 0;
            int sequence = start;

            for (int i = 0; i < (int)bytes.LongLength; i++)
            {
                temp = string.Format("{0:x2}", bytes[count++]);
                build[sequence++] = (temp.ToCharArray())[0];
                build[sequence++] = (temp.ToCharArray())[1];
            }
            return build.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        internal static class Cryptography
        {

            /// <summary>
            /// Encrypt a byte array into a byte array using a key and an IV 
            /// </summary>
            /// <param name="clear"></param>
            /// <param name="key"></param>
            /// <param name="iv"></param>
            /// <returns></returns>
            public static byte[] Encrypt(byte[] clear, byte[] key, byte[] iv)
            {
                if (clear.Length <= 0)
                    // Throw error messages, If no information.
                    throw new ArgumentNullException(Exceper.Format(Exceper.ArgumentNullDataFormat, nameof(clear)));
                if (key.Length <= 0)
                    // Throw error messages, If no information.
                    throw new ArgumentNullException(Exceper.Format(Exceper.ArgumentNullDataFormat, nameof(key)));
                if (iv.Length <= 0)
                    // Throw error messages, If no information.
                    throw new ArgumentNullException(Exceper.Format(Exceper.ArgumentNullDataFormat, nameof(iv)));
                // Set encryption settings.key Use password for both key and init. vector 
                using (var algorithm = new AesCryptoServiceProvider())
                {
                    var transform = algorithm.CreateEncryptor(key, iv);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        var crypto = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                        crypto.Write(clear, 0, clear.Length);
                        crypto.Close();
                        return stream.ToArray();
                    }
                }
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

                return Encrypt(clear, key.GetBytes(32), key.GetBytes(16));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="cipher"></param>
            /// <param name="Key"></param>
            /// <param name="iv"></param>
            /// <returns></returns>
            public static byte[] Decrypt(byte[] cipher, byte[] key, byte[] iv)
            {
                if (cipher.Length <= 0)
                    // Throw error messages, If no information.
                    throw new ArgumentNullException(Exceper.Format(Exceper.ArgumentNullDataFormat, nameof(cipher)));
                if (key.Length <= 0)
                    // Throw error messages, If no information.
                    throw new ArgumentNullException(Exceper.Format(Exceper.ArgumentNullDataFormat, nameof(key)));
                if (iv.Length <= 0)
                    // Throw error messages, If no information.
                    throw new ArgumentNullException(Exceper.Format(Exceper.ArgumentNullDataFormat, nameof(iv)));
                // Set dncryption settings.key Use password for both key and init. vector 
                using (var algorithm = new AesCryptoServiceProvider())
                {
                    var transform = algorithm.CreateDecryptor(key, iv);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        var crypto = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                        crypto.Write(cipher, 0, cipher.Length);
                        crypto.Close();
                        return stream.ToArray();
                    }
                }
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

                return Decrypt(cipher, key.GetBytes(32), key.GetBytes(16));
            }
        }
    }
}
