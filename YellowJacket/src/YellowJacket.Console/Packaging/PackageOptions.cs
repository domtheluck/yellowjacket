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

namespace YellowJacket.Console.Packaging
{
    /// <summary>
    /// Contains all package options.
    /// </summary>
    public class PackageOptions
    {
        #region Properties

        /// <summary>
        /// Gets or sets the deployment folder location.
        /// </summary>
        /// <value>
        /// The deployment folder location.
        /// </value>
        public string DeploymentFolderLocation { get; set; }

        /// <summary>
        /// Gets or sets the name of the test assembly.
        /// </summary>
        /// <value>
        /// The name of the test assembly.
        /// </value>
        public string TestAssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the package location.
        /// </summary>
        /// <value>
        /// The package location.
        /// </value>
        public string PackageLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we want to overwrite the configuration.
        /// </summary>
        /// <value>
        ///   <c>true</c> if we want to overwrite the configuration; otherwise, <c>false</c>.
        /// </value>
        public bool OverwriteConfiguration { get; set; }

        #endregion
    }
}
