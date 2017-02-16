using System;
using OpenQA.Selenium;
using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Framework
{
    public class WebDriverFactory
    {
        public static IWebDriver Get(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.InternetExplorer32:
                    break;

                case BrowserType.InternetExplorer64:
                    break;

                case BrowserType.Firefox:
                    break;

                case BrowserType.Chrome:
                    break;

                default:

                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }
        }
    }
}
