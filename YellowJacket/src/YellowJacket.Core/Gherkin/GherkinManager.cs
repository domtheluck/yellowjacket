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
        #region Public Methods

        /// <summary>
        /// Parses the feature.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="workingFolder">The working folder.</param>
        /// <returns><see cref="GherkinFeature"/>.</returns>
        public GherkinFeature ParseFeature(Assembly assembly, string featureName, string workingFolder)
        {
            if (string.IsNullOrEmpty(workingFolder))
                throw new ArgumentException("You must provide a value for the location");

            if (!Directory.Exists(workingFolder))
                Directory.CreateDirectory(workingFolder);

            string featureFullname = ExtractFeature(assembly, featureName, workingFolder);

            GherkinDocument gherkinDocument = new Parser().Parse(featureFullname);

            GherkinFeature feature = new GherkinFeature
            {
                Name = gherkinDocument.Feature.Name,
                Description = gherkinDocument.Feature.Description,
                Tags = ParseTags(gherkinDocument.Feature.Tags),
                Scenarios = ParseScenarioDefinitions(gherkinDocument.Feature.Children)
            };

            return feature;
        }

        #endregion

        #region Private Methods

        private IEnumerable<GherkinScenario> ParseScenarioDefinitions(
            IEnumerable<ScenarioDefinition> scenarioDefinitions)
        {
            return 
                scenarioDefinitions
                    .Select(scenarioDefinition => 
                        new GherkinScenario
                        {
                            Name = scenarioDefinition.Name,
                            Steps = ParseSteps(scenarioDefinition.Steps)
                        })
                    .ToList();
        }

        private IEnumerable<GherkinStep> ParseSteps(IEnumerable<Step> steps)
        {
            return steps
                .Select(step => 
                    new GherkinStep
                    {
                        Text = step.Text
                    })
                .ToList();
        }

        private IEnumerable<GherkinTag> ParseTags(IEnumerable<Tag> tags)
        {
            return tags
                .Select(tag => 
                    new GherkinTag
                    {
                        Name = tag.Name
                    })
                .ToList();
        }

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
