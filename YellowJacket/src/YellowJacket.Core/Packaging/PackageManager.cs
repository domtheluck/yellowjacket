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

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using YellowJacket.Core.Helpers;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Packaging
{
    /// <summary>
    /// Handles the packaging operations.
    /// </summary>
    /// <seealso cref="IPackageManager" />
    public class PackageManager : IPackageManager
    {
        #region Public Methods

        /// <summary>
        /// Creates a test package.
        /// </summary>
        /// <param name="deploymentFolderLocation">The deployment folder location.</param>
        /// <param name="testAssemblyName">Name of the test assembly.</param>
        /// <param name="packageLocation">The package location.</param>
        /// <param name="plugins">The plugins.</param>
        /// <exception cref="ArgumentException">The test assembly name must be valid.</exception>
        public void Create(
            string deploymentFolderLocation, 
            string testAssemblyName, 
            string packageLocation, 
            List<string> plugins)
        {
            string packageName = Path.GetFileNameWithoutExtension(testAssemblyName);

            if (packageName == null)
                throw new ArgumentException("The test assembly name must be valid");

            string packageFullName = Path.Combine(packageLocation, $"{packageName}.zip");

            try
            {
                if (File.Exists(packageFullName))
                    File.Delete(packageFullName);

                Console.WriteLine($"Creates the package {packageFullName} from the deployment folder {deploymentFolderLocation}...");

                ZipFile.CreateFromDirectory(
                    deploymentFolderLocation,
                    packageFullName,
                    CompressionLevel.Optimal,
                    false);

                Console.WriteLine("Package creation completed...");

                Console.WriteLine("Creates the package configuration file...");

                CreateConfigurationFile(testAssemblyName, packageLocation, packageName);

                Console.WriteLine("Package configuration file created...");

                TypeLocatorHelper typeLocatorHelper = new TypeLocatorHelper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }  
        }

        #endregion

        #region Private Methods 

        private void CreateConfigurationFile(string testAssemblyName, string packageLocationstring, string packageName)
        {

            PackageConfiguration packageConfiguration = 
                new PackageConfiguration {TestAssemblyName = testAssemblyName};



        }

        #endregion
    }
}
