using System;
using TechTalk.SpecFlow;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Infrastructure;
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
            LogManager.Log($"Scenario {ScenarioContext.Current.ScenarioInfo.Title} completed");

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
            LogManager.Log($"Execution completed at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            HookProcessor.Process(HookType.AfterExecution);
        }

        /// <summary>
        /// Hook used before executing a feature.
        /// </summary>
        [BeforeFeature]
        public static void BeforeFeature()
        {
            LogManager.Log($"Feature {FeatureContext.Current.FeatureInfo.Title}");

            HookProcessor.Process(HookType.BeforeFeature);
        }

        /// <summary>
        /// Hook used before executing a scenario.
        /// </summary>
        [BeforeScenario]
        public static void BeforeScenario()
        {
            LogManager.Log($"Starting scenario {ScenarioContext.Current.ScenarioInfo.Title}");

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

            LogManager.Log($"Execution start at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

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
