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

using YellowJacket.Core.Framework;

namespace YellowJacket.Core.Logging
{
    /// <summary>
    /// Used to log content to the different logger instances.
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// Logs the specified content.
        /// </summary>
        /// <param name="content">The value.</param>
        /// <param name="isNewLine">if set to <c>true</c> log the content as a new line; otherwise, <c>false</c>.</param>
        public static void Log(string content, bool isNewLine = true)
        {
            ExecutionContext.Current.GetLoggers().ForEach(x =>
            {
                if (isNewLine)
                    x.WriteLine(content);
                else
                    x.Write(content);
            });
        }
    }
}
