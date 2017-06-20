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
using System.IO;
using YellowJacket.Console.Interfaces;

namespace YellowJacket.Console.Packaging
{
    /// <summary>
    /// Handles the packaging.
    /// </summary>
    /// <seealso cref="IPackageManager" />
    public class PackageManager : IPackageManager
    {
        #region Public Methods

        public bool CreatePackage(
            PackageOptions packageOptions)
        {
            try
            {
                if (!ValidateOptions(packageOptions))
                    return false;


            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);

                return false;
            }

            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validates the package options.
        /// </summary>
        /// <param name="packageOptions">The package options.</param>
        /// <returns><c>true</c> if the package options are valid; otherwise, <c>false</c>.</returns>
        private bool ValidateOptions(PackageOptions packageOptions)
        {
            if (!Directory.Exists(packageOptions.DeploymentFolderLocation))
            {
                System.Console.WriteLine($"The location {packageOptions.DeploymentFolderLocation} does not exist");
                return false;
            }

            if (File.Exists(Path.Combine(packageOptions.DeploymentFolderLocation,
                packageOptions.TestAssemblyName)))
                return true;

            System.Console.WriteLine(
                $"The assembly {packageOptions.TestAssemblyName} does not exist in location {packageOptions.DeploymentFolderLocation}");
            return false;
        }

        #endregion
    }
}
