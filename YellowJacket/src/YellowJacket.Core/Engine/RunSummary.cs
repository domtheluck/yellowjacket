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

        public GherkinFeature PreviousFeature { get; internal set; }

        public GherkinFeature CurrentFeature { get; internal set; }

        public GherkinFeature NextFeature { get; internal set; }

        public GherkinScenario PreviousScenario { get; internal set; }

        public GherkinScenario CurrentScenario { get; internal set; }

        public GherkinScenario NextScenario { get; internal set; }

        public GherkinStep PreviousStep { get; internal set; }

        public GherkinStep CurrentStep { get; internal set; }

        public GherkinStep NextStep { get; internal set; }

        public double StepExecutionPercentage { get; internal set; }

        public double FeatureExecutionPercentage { get; internal set; }

        public double ScenarioExecutionPercentage { get; internal set; }

        public int FeatureCount { get; internal set; }

        public int ScenarioCount { get; internal set; }

        public int StepCount { get; internal set; }

        #endregion
    }
}
