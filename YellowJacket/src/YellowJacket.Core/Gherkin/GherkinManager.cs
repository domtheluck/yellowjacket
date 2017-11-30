using System;
using System.IO;
using System.Linq;
using System.Reflection;
using YellowJacket.Common.Helpers;

namespace YellowJacket.Core.Gherkin
{
    /// <summary>
    /// Handles the Gherkin operations.
    /// </summary>
    internal class GherkinManager
    {
        #region Private Members

        readonly ResourceHelper _resourceHelper = new ResourceHelper();

        #endregion

        /// <summary>
        /// Extracts the specified feature on the local file system.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> instance containing the event data.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="location">The location.</param>
        /// <exception cref="ArgumentException">You must provide a value for the location.</exception>
        public void ExtractFeature(Assembly assembly, string featureName, string location)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentException("You must provide a value for the location");

            if (!Directory.Exists(location))
                Directory.CreateDirectory(location);

            string feature = 
                ResourceHelper.GetEmbededResourceNames(assembly)
                    .FirstOrDefault(x => x.ToLowerInvariant().Contains(featureName.ToLowerInvariant()));

            if (string.IsNullOrEmpty(feature))
                throw new ArgumentException($"Feature {feature} not found in the assembly {assembly.FullName}");

            File.AppendAllText(Path.Combine(location, feature), ResourceHelper.ReadEmbededResource(assembly, feature));
        }

        /// <summary>
        /// Parses the feature.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        public Feature ParseFeature(string fullName)
        {
            return null;
        }
    }
}
