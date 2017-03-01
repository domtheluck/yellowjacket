using System;
using TechTalk.SpecFlow;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Infrastructure;

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
            HookProcessor.Process(HookType.AfterExecution);

        }

        /// <summary>
        /// Hook used before executing a feature.
        /// </summary>
        [BeforeFeature]
        public static void BeforeFeature()
        {
            ExecutionContext.Current.Log($"Feature: {FeatureContext.Current.FeatureInfo.Title}");

            HookProcessor.Process(HookType.BeforeFeature);
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

            ExecutionContext.Current.Log($"Execution start at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            HookProcessor.Process(HookType.BeforeExecution);
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
