using System;
using YellowJacket.Core.Hook;

namespace YellowJacket.WebApp.Automation
{
    [HookPriority(1)]
    public class Hook : IHook
    {
        public void AfterFeature()
        {
            throw new NotImplementedException();
        }

        public void AfterScenario()
        {
            throw new NotImplementedException();
        }

        public void AfterStep()
        {
            throw new NotImplementedException();
        }

        public void BeforeFeature()
        {
            throw new NotImplementedException();
        }

        public void BeforeScenario()
        {
            throw new NotImplementedException();
        }

        public void BeforeStep()
        {
            throw new NotImplementedException();
        }
    }
}
