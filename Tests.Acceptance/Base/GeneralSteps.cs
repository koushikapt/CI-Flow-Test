using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Tests.Acceptance
{
    [Binding]
    public class GeneralSteps
    {
        #region Fields
        private const string Path = "http://localhost:59725/";
        private static readonly Dictionary<string, string> Pages = new Dictionary<string, string>()
        {
            { "Login", "Account/Login" },
            { "Home", string.Empty }
        };
        #endregion

        #region Step Definitions
        [Given(@"I am at the (.+) page")]
        public void GivenIAmAtPage(string page)
        {
            WebBrowser.Current.Navigate().GoToUrl(Path + Pages[page]);
        }

        [When(@"I fill in the following form")]
        public void WhenIFillInTheForm(Table fields)
        {
            foreach(var field in fields.Rows)
            {
                var textField = FindOne(WebBrowser.Current, By.Name(field["field"]), "Expected to find a text field with the name of {0}.", field["field"]);
                textField.SendKeys(field["value"]);
            }
        }

        [When(@"I click the (.+) button")]
        public void WhenIPress(string name)
        {
            FindOne(WebBrowser.Current, By.Name(name), "Expected to find a button with the name of {0}.", name).Click();
        }

        [Then(@"an error should appear showing '(.+)'")]
        public void ThenAnErrorShouldAppearShowing(string errorText)
        {
            var div = FindOne(WebBrowser.Current, By.ClassName("validation-summary-errors"), "Expected to find an errors container.");
            var text = FindOne(div, By.XPath("//ul/li[text()=\"" + errorText + "\"]"), "Expected to find an error.");
            Assert.IsTrue(text.Displayed);
        }

        [Then("I should be at the (.+) page")]
        public void ThenIShouldBeAtTheXPage(string expectedPage)
        {
            var expectedURL = Path + Pages[expectedPage];
            var actualURL = WebBrowser.Current.Url;
            Assert.AreEqual(expectedURL, actualURL, "Expected to be on the " + expectedPage + " page, but was not.");
        }

        [AfterScenario]
        public void Destroy()
        {
            WebBrowser.Current.Quit();
        }
        #endregion

        #region Inner Methods
        private static IWebElement FindOne(ISearchContext context, By by, string message, params object[] args)
        {
            try
            {
                return context.FindElement(by);
            }
            catch
            {
                Assert.Fail(message, args);
            }

            return null;
        }
        #endregion
    }
}