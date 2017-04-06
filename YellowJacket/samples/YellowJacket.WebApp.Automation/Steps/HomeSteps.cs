using System.Threading;
using TechTalk.SpecFlow;
using YellowJacketCore.Framework;

namespace YellowJacket.WebApp.Automation.Steps
{
    [Binding]
    public class HomeSteps: BaseStep
    {
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            Thread.Sleep(1000);
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            Thread.Sleep(1000);
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Thread.Sleep(1000);
        }
    }
}
