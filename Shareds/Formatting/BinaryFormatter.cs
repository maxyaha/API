using Shareds.Utilizing;
using System.IO;

namespace Shareds.Formatting
{
    /// <summary>
    /// 
    /// </summary>
    public static class BinaryFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T entity)
        {
            byte[] bytes;

            var eventer = Converter.ChangeTo(entity, entity.GetType());

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, eventer);
                bytes = stream.ToArray();
            }
            return bytes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static dynamic Deserialize(byte[] bytes)
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] bytes)
        {
            T entity;

            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                entity = (T)formatter.Deserialize(stream);
            }
            return entity;
        }
    }
}
