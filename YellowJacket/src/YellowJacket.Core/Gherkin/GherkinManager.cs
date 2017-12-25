using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Gherkin;
using Gherkin.Ast;
using YellowJacket.Common.Helpers;

namespace YellowJacket.Core.Gherkin
{
    /// <summary>
    /// Handles the Gherkin operations.
    /// </summary>
    internal class GherkinManager
    {
        #region Private Members

        private readonly ResourceHelper _resourceHelper = new ResourceHelper();

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses the feature.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="tempLocation">The temporary location.</param>
        /// <returns><see cref="GherkinDocument"/>.</returns>
        public GherkinDocument ParseFeature(Assembly assembly, string featureName, string tempLocation)
        {
            if (string.IsNullOrEmpty(tempLocation))
                throw new ArgumentException("You must provide a value for the location");

            if (!Directory.Exists(tempLocation))
                Directory.CreateDirectory(tempLocation);

            string featureFullname = ExtractFeature(assembly, featureName, tempLocation);

            return new Parser().Parse(featureFullname);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Extracts the specified feature on the local file system.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly" /> instance containing the event data.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="tempLocation">The temporary location.</param>
        /// <returns>The feature full name.</returns>
        /// <exception cref="ArgumentException">You must provide a value for the location.</exception>
        private string ExtractFeature(Assembly assembly, string featureName, string tempLocation)
        {
            string feature =
                ResourceHelper.GetEmbededResourceNames(assembly)
                    .FirstOrDefault(x => x.ToLowerInvariant().Contains(featureName.ToLowerInvariant()));

            if (string.IsNullOrEmpty(feature))
                throw new ArgumentException($"Feature {feature} not found in the assembly {assembly.FullName}");

            string featureFullname = Path.Combine(tempLocation, $"{featureName}.feature");

            File.AppendAllText(featureFullname, ResourceHelper.ReadEmbededResource(assembly, feature));

            return featureFullname;
        }

        #endregion
    }
}
