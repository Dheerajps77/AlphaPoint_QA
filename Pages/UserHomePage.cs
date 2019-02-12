using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    class UserHomePage
    {

        private readonly ITestOutputHelper output;
        static ILog logger;
        static Config data;
        static string username;
        static string password;

        IWebDriver driver;
        public UserHomePage(IWebDriver driver, ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        /// <summary>
        /// Locators for elements
        /// </summary>
        By loggedInUserName = By.XPath("//button[@class='user-summary__popover-menu-trigger page-header-user-summary__popover-menu-trigger']");
        By signOutButton = By.XPath("//span[contains(@class,'popover-menu__item-label') and text()='Sign Out']");

        //This method Navigates to Exchange selects the Instrument
        public void SelectInstrumentFromExchange()
        {

        }


        //This method Logs out the User
        public void Logout()
        {
            Thread.Sleep(2000);
            UserSetFunctions.Click(driver.FindElement(loggedInUserName));
            UserSetFunctions.Click(driver.FindElement(signOutButton));
        }
    }
}
