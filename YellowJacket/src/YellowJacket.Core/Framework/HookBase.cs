using TechTalk.SpecFlow;
using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Framework
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
            Initialize();
        }

        /// <summary>
        /// Hook used before executing a scenario.
        /// </summary>
        [BeforeScenario()]
        public static void BeforeScenario()
        {

        }

        /// <summary>
        /// Hook used before executing a step.
        /// </summary>
        [BeforeStep()]
        public static void BeforeStep()
        {

        }

        #endregion

        private static void Initialize()
        {
            DriverContext.Driver = WebDriverFactory.Get(BrowserType.Chrome);
        }
    }
}
