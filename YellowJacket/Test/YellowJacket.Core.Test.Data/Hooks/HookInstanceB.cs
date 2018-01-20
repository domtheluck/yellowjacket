using YellowJacket.Core.Hook;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Test.Data.Hooks
{
    [HookPriority(2)]
    public class HookInstanceB : IHook
    {
        public void BeforeFeature()
        {
      
        }

        public void AfterFeature()
        {
          
        }

        public void BeforeScenario()
        {
 
        }

        public void AfterScenario()
        {

        }

        public void BeforeStep()
        {

        }

        public void AfterStep()
        {

        }

        public void BeforeExecution()
        {

        }

        public void AfterExecution()
        {

        }
    }
}
