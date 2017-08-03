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
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
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
        #region Constants

        private const string YellowJacket = "YellowJacket";

        #endregion

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

                CreateConfigurationFile(deploymentFolderLocation, testAssemblyName, packageLocation, packageName);

                Console.WriteLine("Package configuration file created...");
            }
            catch
            {
                Cleanup();
                throw;
            }
        }

        /// <summary>
        /// Gets the package configuration.
        /// </summary>
        /// <param name="packageFullName">Full name of the package.</param>
        /// <returns><see cref="PackageConfiguration"/>.</returns>
        public PackageConfiguration GetPackageConfiguration(string packageFullName)
        {
            string packageConfigurationFullName = $"{Path.GetFileName(packageFullName)}.json";

            return JsonConvert.DeserializeObject<PackageConfiguration>(File.ReadAllText(packageConfigurationFullName));
        }

        /// <summary>
        /// Extracts the package.
        /// </summary>
        /// <param name="packageFullName">Full name of the package.</param>
        /// <returns>The package full name.</returns>
        public string ExtractPackage(string packageFullName)
        {
            string packageName = "";

            if (!string.IsNullOrEmpty(packageFullName))
                packageName = Path.GetFileName(packageFullName);

            string extractedPackageLocation = Path.Combine(Path.GetTempPath(), YellowJacket, packageName);

            try
            {
                if (Directory.Exists(extractedPackageLocation))
                    Directory.Delete(extractedPackageLocation);

                ZipFile.ExtractToDirectory(packageFullName, extractedPackageLocation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return extractedPackageLocation;
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Creates the configuration file.
        /// </summary>
        /// <param name="deploymentFolderLocation">The deployment folder location.</param>
        /// <param name="testAssemblyName">Name of the test assembly.</param>
        /// <param name="packageLocation">The package location.</param>
        /// <param name="packageName">Name of the package.</param>
        private void CreateConfigurationFile(
            string deploymentFolderLocation, 
            string testAssemblyName, 
            string packageLocation, 
            string packageName)
        {
            PackageConfiguration packageConfiguration =
                new PackageConfiguration { TestAssemblyName = testAssemblyName };

            string configFileFullName = Path.Combine(packageLocation, $"{packageName}.json");

            TypeLocatorHelper typeLocatorHelper = new TypeLocatorHelper();

            List<string> features = 
                typeLocatorHelper.GetFeatureTypes(Assembly.LoadFrom(Path.Combine(deploymentFolderLocation, testAssemblyName)))
                .Select(x => x.Name.Substring(0, x.Name.Length - 7))
                .ToList();

            if (!features.Any())
                throw new Exception($"No feature have been found in the test assembly {Path.Combine(packageLocation, testAssemblyName)}");

            packageConfiguration.Features = features;

            File.WriteAllText(
                configFileFullName,
                JsonConvert.SerializeObject(packageConfiguration, Formatting.Indented));
        }

        /// <summary>
        /// Cleanups the files if something wrong happen.
        /// </summary>
        private void Cleanup()
        {
            // TODO: to implements
        }

        #endregion
    }
}
