namespace YellowJacket.Core.Hook
{
    /// <summary>
    /// Interface use to handle hooks.
    /// </summary>
    public interface IHook
    {
        void BeforeFeature();

        void AfterFeature();

        void BeforeScenario();

        void AfterScenario();

        void BeforeStep();

        void AfterStep();
    }
}
