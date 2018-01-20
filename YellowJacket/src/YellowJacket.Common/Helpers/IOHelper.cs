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
// ***********************************************************************

using System.IO;

namespace YellowJacket.Common.Helpers
{
    /// <summary>
    /// Input/Output helper methods.
    /// </summary>
    public class IoHelper
    {
        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        public static void DeleteDirectory(string path, bool recursive)
        {
            if (recursive)
            {
                string[] subfolders = Directory.GetDirectories(path);

                foreach (string folder in subfolders)
                {
                    DeleteDirectory(folder, true);
                }
            }

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(file);

                    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(file, attr ^ FileAttributes.ReadOnly);
                    }

                    File.Delete(file);
                }
                catch
                {
                    // ignored
                }
            }

            Directory.Delete(path);
        }
    }
}
