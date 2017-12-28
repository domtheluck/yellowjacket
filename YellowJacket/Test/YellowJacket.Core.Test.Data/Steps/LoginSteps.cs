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

using TechTalk.SpecFlow;

namespace YellowJacket.Core.Test.Data.Steps
{
    [Binding]
    public class LoginSteps
    {
        #region Public Methods

        [Given(@"I enter '(.*)' in '(.*)' textbox")]
        public void GivenIEnterInTextbox(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I click on '(.*)' button")]
        public void WhenIClickOnButton(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I see my profile page")]
        public void ThenISeeMyProfilePage()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I see invalid password message")]
        public void ThenISeeInvalidPasswordMessage()
        {
            ScenarioContext.Current.Pending();
        }


        #endregion
    }
}
