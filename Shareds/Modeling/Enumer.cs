using Shareds.Utilizing;
using System;
using System.Text;

namespace Shareds.Modeling
{
    /// <summary>
    /// 
    /// </summary>
    public static class Enumer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="length"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private static byte[] ByteWriter(this string uid, int length, int start)
        {
            StringBuilder build = new StringBuilder(uid);
            byte[] bytes = new byte[length];
            string temp;
            int sequence = start;

            for (int i = 0; i < length; i++)
            {
                temp = build[sequence++].ToString();
                temp += build[sequence++].ToString();
                bytes[i] = byte.Parse(temp, System.Globalization.NumberStyles.HexNumber);
            }
            return bytes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static uint ToUInt(this Guid id)
        {
            string uid = id.ToString();
            uid = Character.Remove(uid, "-{}");
            byte[] bytes = ByteWriter(uid, 8, 9);
            var decrypted = Cypher.Decrypt(bytes, Cypher.Key);
            uint number = BitConverter.ToUInt32(decrypted, 0);
            return number;
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
        /// <param name="number"></param>
        /// <returns></returns>
        public static Guid ToGuid(this uint number)
        {
            return number.ToGuid(Guid.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Guid ToGuid(this uint number, Guid id)
        {
            if (number >= Math.Pow(2, 24))
                throw new ArgumentOutOfRangeException("Unsigned integer is greater than 24bit");

            string uid = id.ToString();
            uid = Character.Remove(uid, "-{}");//Remove any '-' and '{}' characters
            byte[] bytes = BitConverter.GetBytes(number);
            var encrypted = Cypher.Encrypt(bytes, Cypher.Key);

            uid = StringWriter(uid, encrypted, 9);
            Guid.TryParse(uid, out id);

            return id;
        }
    }
}
