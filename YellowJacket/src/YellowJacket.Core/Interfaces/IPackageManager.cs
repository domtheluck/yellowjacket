using System.Collections.Generic;

namespace YellowJacket.Core.Interfaces
{
    /// <summary>
    /// Interface definition for the package manager.
    /// </summary>
    public interface IPackageManager
    {
        /// <summary>
        /// Creates a test package.
        /// </summary>
        /// <param name="deploymentFolderLocation">The deployment folder location.</param>
        /// <param name="testAssemblyName">Name of the test assembly.</param>
        /// <param name="packageLocation">The package location.</param>
        /// <param name="plugins">The plugins.</param>
        void Create(string deploymentFolderLocation, string testAssemblyName, string packageLocation, List<string> plugins);
    }
}
