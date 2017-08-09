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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace YellowJacket.Core.Helpers
{
    /// <summary>
    /// Helper class for embeded resources manipulation.
    /// </summary>
    public class ResourceHelper
    {
        /// <summary>
        /// Gets the embeded resources.
        /// </summary>
        /// <typeparam name="TAssembly">The type of the assembly.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns><see cref="IEnumerable{String}"/>.</returns>
        /// <exception cref="ArgumentNullException">predicate.</exception>
        public static IEnumerable<string> GetEmbededResources<TAssembly>(Func<string, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return
                GetEmbededResourceNames<TAssembly>()
                    .Where(predicate)
                    .Select(name => ReadEmbededResource(typeof(TAssembly), name))
                    .Where(x => !string.IsNullOrEmpty(x));
        }

        /// <summary>
        /// Gets the embeded resource names.
        /// </summary>
        /// <typeparam name="TAssembly">The type of the assembly.</typeparam>
        /// <returns><see cref="IEnumerable{String}"/>.</returns>
        public static IEnumerable<string> GetEmbededResourceNames<TAssembly>()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(TAssembly));

            return assembly.GetManifestResourceNames();
        }

        /// <summary>
        /// Gets the embeded resource.
        /// </summary>
        /// <typeparam name="TAssembly">The type of the assembly.</typeparam>
        /// <typeparam name="TNamespace">The type of the namespace.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The embeded resource.</returns>
        /// <exception cref="ArgumentNullException">name</exception>
        public static string GetEmbededResource<TAssembly, TNamespace>(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return ReadEmbededResource(typeof(TAssembly), typeof(TNamespace), name);
        }

        /// <summary>
        /// Reads the embeded resource.
        /// </summary>
        /// <param name="assemblyType">Type of the assembly.</param>
        /// <param name="namespaceType">Type of the namespace.</param>
        /// <param name="name">The name.</param>
        /// <returns>The embeded resource.</returns>
        /// <exception cref="ArgumentNullException">
        /// assemblyType
        /// or
        /// namespaceType
        /// or
        /// name
        /// </exception>
        public static string ReadEmbededResource(Type assemblyType, Type namespaceType, string name)
        {
            if (assemblyType == null)
                throw new ArgumentNullException(nameof(assemblyType));

            if (namespaceType == null)
                throw new ArgumentNullException(nameof(namespaceType));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return ReadEmbededResource(assemblyType, $"{namespaceType.Namespace}.{name}");
        }

        /// <summary>
        /// Reads the embeded resource.
        /// </summary>
        /// <param name="assemblyType">Type of the assembly.</param>
        /// <param name="name">The name.</param>
        /// <returns>The embeded resource.</returns>
        /// <exception cref="ArgumentNullException">
        /// assemblyType
        /// or
        /// name
        /// </exception>
        public static string ReadEmbededResource(Type assemblyType, string name)
        {
            if (assemblyType == null)
                throw new ArgumentNullException(nameof(assemblyType));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));


            Assembly assembly = Assembly.GetAssembly(assemblyType);

            using (Stream resourceStream = assembly.GetManifestResourceStream(name))
            {
                if (resourceStream == null)
                    return null;

                using (StreamReader streamReader = new StreamReader(resourceStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
