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

using TechTalk.SpecFlow;
using YellowJacket.Core.Contexts;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Hook;

namespace YellowJacket.Core.Engine
{
    /// <summary>
    /// Base class to handle SpecFlow binding hooks.
    /// </summary>
    [Binding]
    internal class BaseHook
    {
        #region Public Methods

        /// <summary>
        /// Hook used before a test run.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExecutionContext.Instance.FireBeforeExecutionEvent();

            HookProcessor.Process(HookType.BeforeExecution);
        }

        /// <summary>
        /// Hook used after a test run.
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExecutionContext.Instance.FireAfterExecutionEvent();

            HookProcessor.Process(HookType.AfterExecution);
        }

        /// <summary>
        /// Hook used before executing a feature.
        /// </summary>
        [BeforeFeature]
        public static void BeforeFeature()
        {
            HookProcessor.Process(HookType.BeforeFeature);
        }

        /// <summary>
        /// Hook used after executing a feature.
        /// </summary>
        [AfterFeature]
        public static void AfterFeature()
        {
            ExecutionContext.Instance.FireExecutionAfterFeatureEvent();

            HookProcessor.Process(HookType.AfterFeature);
        }

        /// <summary>
        /// Hook used before executing a scenario.
        /// </summary>
        [BeforeScenario]
        public static void BeforeScenario()
        {
            HookProcessor.Process(HookType.BeforeScenario);
        }

        /// <summary>
        /// Hook used after executing a scenario.
        /// </summary>
        [AfterScenario]
        public static void AfterScenario()
        {
            ExecutionContext.Instance.FireExecutionAfterScenarioEvent();

            HookProcessor.Process(HookType.AfterScenario);
        }

        /// <summary>
        /// Hook used before executing a step.
        /// </summary>
        [BeforeStep]
        public static void BeforeStep()
        {
            ExecutionContext.Instance.FireExecutionBeforeStepEvent();

            HookProcessor.Process(HookType.BeforeStep);
        }

        /// <summary>
        /// Hook used after executing a step.
        /// </summary>
        [AfterStep]
        public static void AfterStep()
        {
            ExecutionContext.Instance.FireExecutionAfterStepEvent();

            HookProcessor.Process(HookType.AfterStep);
        }

        #endregion
    }
}
