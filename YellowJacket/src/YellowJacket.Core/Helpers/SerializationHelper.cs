﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace YellowJacket.Core.Helpers
{
    internal class SerializationHelper
    {
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();

                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}