using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE; 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AlphaPoint_QA.Utils
{
    
    public enum PlatformType
    {
        Linux,
        Window,
    }

    public enum BrowserType
    {
        Chrome,
        Firefox,
        IE
    }

    public class AlphaPointWebDriver : IWebDriver
    {

        public IWebDriver driver;

        public PlatformType Platform { get; private set; }

        public BrowserType Browser { get; private set; }

        // instance of singleton class
        private static AlphaPointWebDriver instanceOfAlphaPointWebDriver = null;

        private AlphaPointWebDriver()
        {
            Platform = Enum.Parse<PlatformType>(ConfigManager.Instance.Platform, true);
            Browser = Enum.Parse<BrowserType>(ConfigManager.Instance.Browser, true);

            driver = GetDriver(Platform, Browser);

        }

        // TO create instance of class
        public static IWebDriver GetInstanceOfAlphaPointWebDriver()
        {
            if (instanceOfAlphaPointWebDriver == null)
            {
                instanceOfAlphaPointWebDriver = new AlphaPointWebDriver();
            }
            return instanceOfAlphaPointWebDriver.driver;
        }


        //  To get driver
        private IWebDriver GetDriver(PlatformType platform, BrowserType browser)
        {
            switch (platform)
            {
                case PlatformType.Linux:
                    return GetLinuxDriver(browser);
                case PlatformType.Window:
                    return GetWindowDriver(browser);
                default:
                    return null;
            }
        }

        private IWebDriver GetLinuxDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    return GetChromeforLinux();
                case BrowserType.Firefox:
                    return GetFirefoxforLinux();
                case BrowserType.IE:
                    return GetIEforLinux();
                default:
                    return null;
            }
        }

        private IWebDriver GetChromeforLinux()
        {
            string chromelinuxPath = @"Drivers\Linux\Chrome\";
            driver = new ChromeDriver(chromelinuxPath);
            return driver;
        }

        private IWebDriver GetFirefoxforLinux()
        {
            string firefoxlinuxPath = @"Drivers\Linux\Firefox";
            driver = new FirefoxDriver(firefoxlinuxPath);
            return driver;
        }

        private IWebDriver GetIEforLinux()
        {
            string ielinuxPath = @"Drivers\Linux\IE";
            driver = new InternetExplorerDriver(ielinuxPath);
            return driver;
        }

        private IWebDriver GetWindowDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    return GetChromeforWindow();
                case BrowserType.Firefox:
                    return GetFirefoxforWindow();
                case BrowserType.IE:
                    return GetIEforWindow();
                default:
                    return null;
            }
        }

        private IWebDriver GetChromeforWindow()
        {
            string chromeWindowPath = @"Drivers\Windows\Chrome\";
            driver = new ChromeDriver(chromeWindowPath);
            return driver;
        }

        private IWebDriver GetFirefoxforWindow()
        {
            string firefoxWindowPath = @"Drivers\Windows\Firefox";
            driver = new FirefoxDriver(firefoxWindowPath);
            return driver;
        }

        private IWebDriver GetIEforWindow()
        {
            string ieWindowPath = @"Drivers\Windows\IE";
            driver = new InternetExplorerDriver(ieWindowPath);
            return driver;
        }


        public string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Title => throw new NotImplementedException();

        public string PageSource => throw new NotImplementedException();

        public string CurrentWindowHandle => throw new NotImplementedException();

        public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public IOptions Manage()
        {
            throw new NotImplementedException();
        }

        public INavigation Navigate()
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }
    }
}
