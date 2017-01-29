using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace YellowJacket.Core.Base
{
    /// <summary>
    /// Hook for the test initialization.
    /// </summary>
    public abstract class InitializeHook : Base
    {
        #region Public Members

        //public readonly BrowserType Browser;

        #endregion

        #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the <see cref="InitializeHook"/> class.
        ///// </summary>
        ///// <param name="browser">The browser.</param>
        //protected InitializeHook(BrowserType browser)
        //{
        //    Browser = browser;
        //}

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the settings.
        /// </summary>
        public void InitializeSettings()
        {
            //string content = GetRunSettings();

            //if (string.IsNullOrEmpty(content))
            //    throw new Exception("The RunSettings file is invalid");

            //RunSettings runSettings = JsonConvert.DeserializeObject<RunSettings>(content);

            //Settings.BrowserType = runSettings.BrowserType;
            //Settings.Environment = runSettings.Environment;
            //Settings.Language = runSettings.Language;
            //Settings.Product = runSettings.Product;

            //Settings.OutputPath = runSettings.OutputPath;
            //Settings.StepLogPath = runSettings.StepLogPath;

            //OpenBrowser(Settings.BrowserType);
        }

        ///// <summary>
        ///// Navigates to the site.
        ///// </summary>
        //public override void NavigateSite()
        //{
        //    DriverContext.Browser.GoToUrl(EnvironmentHelper.GetLoginUrl(Settings.Environment));
        //}

        #endregion

        #region Private Methods

        ///// <summary>
        ///// Gets the run settings.
        ///// </summary>
        ///// <returns></returns>
        //private string GetRunSettings()
        //{
            //CleanupRunSettingFiles();

            //string path = $"{AssemblyHelper.GetExecutingAssemblyDirectory()}\\runs";

            //DirectoryInfo di = new DirectoryInfo(path);

            //string filePath = di.GetFiles("*.json").OrderByDescending(x => x.CreationTime).FirstOrDefault()?.FullName;

            //return File.ReadAllText(filePath);
        //}

        /// <summary>
        /// Cleanups the run setting files.
        /// </summary>
        private void CleanupRunSettingFiles()
        {
            //string path = $"{AssemblyHelper.GetExecutingAssemblyDirectory()}\\runs";

            //DirectoryInfo di = new DirectoryInfo(path);

            //FileInfo[] files = di.GetFiles("*.json").OrderByDescending(x => x.CreationTime).ToArray();

            //if (files.Length <= 1) return;

            //for (int cpt = 1; cpt < files.Length - 1; cpt++)
            //    File.Delete(files[cpt].FullName);
        }

        ///// <summary>
        ///// Opens the browser.
        ///// </summary>
        ///// <param name="browserType">Type of the browser.</param>
        //private void OpenBrowser(BrowserType browserType = BrowserType.Firefox)
        //{
        //    switch (browserType)
        //    {
        //        case BrowserType.InternetExplorer32:
        //        case BrowserType.InternetExplorer64:
        //            InternetExplorerOptions options = new InternetExplorerOptions
        //            {
        //                IntroduceInstabilityByIgnoringProtectedModeSettings = true
        //            };


        //            DriverContext.Driver = new InternetExplorerDriver(options);

        //            break;

        //        case BrowserType.Firefox:
        //            FirefoxProfile firefoxProfile = new FirefoxProfile
        //            {
        //                AcceptUntrustedCertificates = true,
        //                AssumeUntrustedCertificateIssuer = true
        //            };

        //            firefoxProfile.SetPreference("plugin.state.flash", 0);
        //            firefoxProfile.SetPreference("dom.ipc.plugins.enabled.libflashplayer.so", "false");

        //            FirefoxBinary firefoxBinary =
        //                new FirefoxBinary(
        //                    Path.Combine(
        //                        AssemblyHelper.GetExecutingAssemblyDirectory(),
        //                        @"Browsers\Firefox\FirefoxPortable.exe"));

        //            DriverContext.Driver = new FirefoxDriver(firefoxBinary, firefoxProfile, TimeSpan.FromSeconds(300));

        //            break;

        //        case BrowserType.Chrome:
        //            ChromeDriverService chromeDriverService =
        //                ChromeDriverService.CreateDefaultService(AssemblyHelper.GetExecutingAssemblyDirectory());

        //            ChromeOptions chromeOptions = new ChromeOptions();

        //            chromeOptions.AddArgument("--disable-popup-blocking");
        //            chromeOptions.AddArgument("--disable-translate");
        //            chromeOptions.AddArgument("--disable-gpu");
        //            chromeOptions.AddArgument("--ignore-certificate-errors");

        //            chromeOptions.BinaryLocation = Path.Combine(AssemblyHelper.GetExecutingAssemblyDirectory(),
        //                @"Browsers\Chrome\Chrome.exe");

        //            DriverContext.Driver = new ChromeDriver(chromeDriverService, chromeOptions, TimeSpan.FromSeconds(300));

        //            break;

        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
        //    }

        //    DriverContext.Browser = new Browser(DriverContext.Driver);

        //    DriverContext.Driver.Manage().Cookies.DeleteAllCookies();

        //    DriverContext.Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(5));
        //    DriverContext.Driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromMinutes(5));

        //    DriverContext.Driver.Manage().Window.Maximize();
        //}

        #endregion
    }
}
