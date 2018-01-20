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

using YellowJacket.Core.Gherkin;

namespace YellowJacket.Core.Engine
{
    /// <summary>
    /// Contains the run summary.
    /// </summary>
    public class RunSummary
    {
        #region Public Properties

        /// <summary>
        /// Gets the previous feature.
        /// </summary>
        /// <value>
        /// The previous feature.
        /// </value>
        public GherkinFeature PreviousFeature { get; internal set; }

        /// <summary>
        /// Gets the current feature.
        /// </summary>
        /// <value>
        /// The current feature.
        /// </value>
        public GherkinFeature CurrentFeature { get; internal set; }

        /// <summary>
        /// Gets the next feature.
        /// </summary>
        /// <value>
        /// The next feature.
        /// </value>
        public GherkinFeature NextFeature { get; internal set; }

        /// <summary>
        /// Gets the previous scenario.
        /// </summary>
        /// <value>
        /// The previous scenario.
        /// </value>
        public GherkinScenario PreviousScenario { get; internal set; }

        /// <summary>
        /// Gets the current scenario.
        /// </summary>
        /// <value>
        /// The current scenario.
        /// </value>
        public GherkinScenario CurrentScenario { get; internal set; }

        /// <summary>
        /// Gets the next scenario.
        /// </summary>
        /// <value>
        /// The next scenario.
        /// </value>
        public GherkinScenario NextScenario { get; internal set; }

        /// <summary>
        /// Gets the previous step.
        /// </summary>
        /// <value>
        /// The previous step.
        /// </value>
        public GherkinStep PreviousStep { get; internal set; }

        /// <summary>
        /// Gets the current step.
        /// </summary>
        /// <value>
        /// The current step.
        /// </value>
        public GherkinStep CurrentStep { get; internal set; }

        /// <summary>
        /// Gets the next step.
        /// </summary>
        /// <value>
        /// The next step.
        /// </value>
        public GherkinStep NextStep { get; internal set; }

        /// <summary>
        /// Gets the step execution percentage.
        /// </summary>
        /// <value>
        /// The step execution percentage.
        /// </value>
        public double StepExecutionPercentage { get; internal set; }

        /// <summary>
        /// Gets the feature execution percentage.
        /// </summary>
        /// <value>
        /// The feature execution percentage.
        /// </value>
        public double FeatureExecutionPercentage { get; internal set; }

        /// <summary>
        /// Gets the scenario execution percentage.
        /// </summary>
        /// <value>
        /// The scenario execution percentage.
        /// </value>
        public double ScenarioExecutionPercentage { get; internal set; }

        /// <summary>
        /// Gets the feature count.
        /// </summary>
        /// <value>
        /// The feature count.
        /// </value>
        public int FeatureCount { get; internal set; }

        /// <summary>
        /// Gets the scenario count.
        /// </summary>
        /// <value>
        /// The scenario count.
        /// </value>
        public int ScenarioCount { get; internal set; }

        /// <summary>
        /// Gets the step count.
        /// </summary>
        /// <value>
        /// The step count.
        /// </value>
        public int StepCount { get; internal set; }

        #endregion
    }
}
