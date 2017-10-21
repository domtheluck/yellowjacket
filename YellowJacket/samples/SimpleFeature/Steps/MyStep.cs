// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SimpleFeature.Steps
{
    [Binding]
    internal class MyStep
    {
        #region Steps

        [Given(@"I have entered '(.*)' into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            Calculator.Instance.EnterNumber(number);
        }

        [When(@"I press '(.*)'")]
        public void WhenIPress(string buttonName)
        {
            if (buttonName.ToLowerInvariant().Equals("add"))
                Calculator.Instance.ComputeAdd();
        }

        [Then(@"the result should be '(.*)' on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            int result = Calculator.Instance.GetResult();

            Assert.AreEqual(expectedResult, result);
        }

        #endregion
    }
}
