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
using TechTalk.SpecFlow;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Factories;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Logging;

namespace YellowJacket.Core.Framework
{
    /// <summary>
    /// Base class to handle SpecFlow binding hooks.
    /// </summary>
    [Binding]
    internal class BaseHook
    {
        #region Public Methods

        /// <summary>
        /// Hook used after executing a feature.
        /// </summary>
        [AfterFeature]
        public static void AfterFeature()
        {
            HookProcessor.Process(HookType.AfterFeature);
        }

        /// <summary>
        /// Hook used after executing a scenario.
        /// </summary>
        [AfterScenario]
        public static void AfterScenario()
        {
            // TODO: need to check if an error happened
            LogManager.WriteLine($"Scenario {ScenarioContext.Current.ScenarioInfo.Title} completed");

            HookProcessor.Process(HookType.AfterScenario);
        }

        /// <summary>
        /// Hook used after executing a step.
        /// </summary>
        [AfterStep]
        public static void AfterStep()
        {
            HookProcessor.Process(HookType.AfterStep);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            LogManager.WriteLine($"Execution completed at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            HookProcessor.Process(HookType.AfterExecution);
        }

        /// <summary>
        /// Hook used before executing a feature.
        /// </summary>
        [BeforeFeature]
        public static void BeforeFeature()
        {
            LogManager.WriteLine($"Feature {FeatureContext.Current.FeatureInfo.Title}");

            HookProcessor.Process(HookType.BeforeFeature);
        }

        /// <summary>
        /// Hook used before executing a scenario.
        /// </summary>
        [BeforeScenario]
        public static void BeforeScenario()
        {
            LogManager.WriteLine($"Starting scenario {ScenarioContext.Current.ScenarioInfo.Title}");

            HookProcessor.Process(HookType.BeforeScenario);
        }

        /// <summary>
        /// Hook used before executing a step.
        /// </summary>
        [BeforeStep]
        public static void BeforeStep()
        {
            HookProcessor.Process(HookType.BeforeStep);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Initialize();

            LogManager.WriteLine($"Execution start at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            HookProcessor.Process(HookType.BeforeExecution);
        }

        [BeforeScenarioBlock]
        public static void BeforeScenarioBlock()
        {
            
        }

        [AfterScenarioBlock]
        public static void AfterScenarioBlock()
        {

        }

        #endregion

        /// <summary>
        /// Initializes the HookBase.
        /// </summary>
        private static void Initialize()
        {
            // TODO: Modify this code to get the right driver according to the execution context
            DriverContext.Driver = WebDriverFactory.Get(BrowserType.Chrome);
        }
    }
}
