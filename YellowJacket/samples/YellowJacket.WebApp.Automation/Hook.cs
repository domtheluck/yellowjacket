using System;
using YellowJacket.Core.Hook;

namespace YellowJacket.WebApp.Automation
{
    [HookPriority(1)]
    [Serializable]
    public class Hook : IHook
    {
        /// <summary>
        /// Called after a feature execution.
        /// </summary>
        public void AfterFeature()
        {

        }

        /// <summary>
        /// Called after a scenario execution.
        /// </summary>

        public void AfterScenario()
        {

        }

        /// <summary>
        /// Called after a step execution.
        /// </summary>
        public void AfterStep()
        {

        }

        /// <summary>
        /// Called before the execution.
        /// </summary>
        public void BeforeExecution()
        {

        }

        /// <summary>
        /// Called after the execution.
        /// </summary>
        public void AfterExecution()
        {

        }

        /// <summary>
        /// Called mefore a feature execution.
        /// </summary>
        public void BeforeFeature()
        {
            Console.WriteLine("IHook - BeforeFeature");
        }

        /// <summary>
        /// Called before a scenario execution.
        /// </summary>
        public void BeforeScenario()
        {

        }

        /// <summary>
        /// Called before a step execution.
        /// </summary>
        public void BeforeStep()
        {

        }
    }
}
