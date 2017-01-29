using TechTalk.SpecFlow;

namespace YellowJacket.Core.Base
{
    [Binding]
    public class HookBase
    {
        #region Public Methods

        /// <summary>
        /// Hook used after executing a feature.
        /// </summary>
        [AfterFeature()]
        public static void AfterFeature()
        {

        }

        /// <summary>
        /// Hook used after executing a scenario.
        /// </summary>
        [AfterScenario()]
        public static void AfterScenario()
        {
            //if (ScenarioContext.Current.TestError != null)
            //    new ScreenshotHelper().TakeScreenshot(Settings.LogPath);
        }

        /// <summary>
        /// Hook used after executing a step.
        /// </summary>
        [AfterStep()]
        public static void AfterStep()
        {

        }

        /// <summary>
        /// Hook used before executing a feature.
        /// </summary>
        [BeforeFeature()]
        public static void BeforeFeature()
        {

        }

        /// <summary>
        /// Hook used before executing a scenario.
        /// </summary>
        [BeforeScenario()]
        public static void BeforeScenario()
        {
            //if (ScenarioContext.Current.TestError != null)
            //    new ScreenshotHelper().TakeScreenshot(Settings.LogPath);
        }

        /// <summary>
        /// Hook used before executing a step.
        /// </summary>
        [BeforeStep()]
        public static void BeforeStep()
        {

        }

        #endregion
    }
}
