using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Tests.Acceptance
{
    internal static class WebBrowser
    {
        public static IWebDriver Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    ScenarioContext.Current["browser"] = new ChromeDriver();
                }

                return ScenarioContext.Current["browser"] as IWebDriver;
            }
        }
    }
}