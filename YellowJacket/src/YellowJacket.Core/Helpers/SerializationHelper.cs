// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            // TODO: probably need to pass an additional parameters for that
            if (File.Exists(filePath))
                File.Delete(filePath);

            using (Stream stream = File.Open(filePath, FileMode.CreateNew))
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
