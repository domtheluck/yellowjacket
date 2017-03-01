using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace YellowJacket.Core.Helpers
{
    /// <summary>
    /// Helper class for object serialization.
    /// </summary>
    internal class SerializationHelper
    {
        /// <summary>
        /// Serialize and write the specified object as binary.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="objectToWrite">The object to write.</param>
        /// <param name="append">if set to <c>true</c> [append].</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            // TODO: probably need to pass an additional parameters for that
            if (File.Exists(filePath))
                File.Delete(filePath);

            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.CreateNew))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads the specified binary file and deserialize it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
