using System;

namespace YellowJacket.Core.Hook
{
    /// <summary>
    /// Interface use to handle hooks.
    /// </summary>
    public interface IHook
    {
        /// <summary>
        /// Called mefore a feature execution.
        /// </summary>
        void BeforeFeature();

        /// <summary>
        /// Called after a feature execution.
        /// </summary>
        void AfterFeature();

        /// <summary>
        /// Called before a scenario execution.
        /// </summary>
        void BeforeScenario();

        /// <summary>
        /// Called after a scenario execution.
        /// </summary>
        void AfterScenario();

        /// <summary>
        /// Called before a step execution.
        /// </summary>
        void BeforeStep();

        /// <summary>
        /// Called after a step execution.
        /// </summary>
        void AfterStep();

        /// <summary>
        /// Called before the execution.
        /// </summary>
        void BeforeExecution();

        /// <summary>
        /// Called after the execution.
        /// </summary>
        void AfterExecution();
    }
}
