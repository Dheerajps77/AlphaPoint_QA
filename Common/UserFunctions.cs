using System;
using System.Drawing;
using System.Threading;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;

namespace AlphaPoint_QA.Common
{

    public class UserFunctions
    {
        private ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;
        static string username;
        static string password;
        private string apexWebTitle;
       

        public UserFunctions(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        //Webelements defined here
        By selectServer = By.CssSelector("select[name = tradingServer"); //By.XPath("//select[@name='tradingServer']");
        By userLoginName = By.CssSelector("input[name = username]"); //By.XPath("//input[@name='username']");
        By userLoginPassword = By.CssSelector("input[name=password]");
        //By userLoginPassword = By.XPath("//input[@name='password']");
        By userLoginButton = By.XPath("//button[text()='Log In']");
        By loggedInUserName = By.XPath("//button[@class='user-summary__popover-menu-trigger page-header-user-summary__popover-menu-trigger']");
        By userSignOutButton = By.XPath("//span[contains(@class,'popover-menu__item-label') and text()='Sign Out']");

        //This method is used for User Login
        //If the user is already logged in, then this method logs out user and then logs in 

        public string LogIn(ProgressLogger logger, string userName = Const.USER5)
        {
            string username = null;
            apexWebTitle = TestData.GetData("HomePageTitle");
            string userUrl = data.UserPortal.PortalUrl;
            string userServerName = data.UserPortal.PortalServerUrl;

            driver.Navigate().GoToUrl(userUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Size = new Size(1366, 768);

            if (!(UserLoggedInStatus(driver)))
            {
                try
                {
                    username = UserLogin(logger, userName, userServerName);
                }
                catch (Exception e)
                {
                    logger.TakeScreenshot();
                    logger.Error(Const.UserLoginFailed, e);
                    throw e;
                }
            }
            else
            {
                try
                {
                    Thread.Sleep(1000);
                    UserSetFunctions.Click(driver.FindElement(loggedInUserName));
                    UserSetFunctions.Click(driver.FindElement(userSignOutButton));
                    Thread.Sleep(2000);
                    username=UserLogin(logger, userName, userServerName);
                }

                catch (Exception e)
                {
                    logger.Error(Const.UserLogoutFailed, e);
                    throw e;
                }
            }
            return username;
        }

        private string UserLogin(ProgressLogger logger, string userName, string userServerName)
        {
            username = data.UserPortal.Users[userName].UserName;
            password = data.UserPortal.Users[userName].Password;

            IWebElement serverWebElement = driver.FindElement(selectServer);

            UserSetFunctions.SelectDropdown(serverWebElement, userServerName);
            UserSetFunctions.EnterText(driver.FindElement(userLoginName), username);
            UserSetFunctions.EnterText(driver.FindElement(userLoginPassword), password);
            UserSetFunctions.Click(driver.FindElement(userLoginButton));
            Assert.Equal(driver.Title.ToLower(), apexWebTitle.ToLower());
            logger.LogCheckPoint(string.Format(LogMessage.UserLoggedInSuccessfully, username));
            Thread.Sleep(2000);
            return username;
        }

        //This method is used for User Logout
        public void LogOut()
        {
            try
            {
                Thread.Sleep(2000);
                UserCommonFunctions.ScrollingUpVertical(driver);
                UserSetFunctions.Click(driver.FindElement(loggedInUserName));
                UserSetFunctions.Click(driver.FindElement(userSignOutButton));
                logger.LogCheckPoint(string.Format(LogMessage.UserLoggedOutSuccessfully, username));
            }
            catch (Exception e)
            {
                logger.Error(Const.UserLogoutFailed, e);
                throw e;
            }
        }

        private bool UserLoggedInStatus(IWebDriver driver)
        {
            bool flag;
            try
            {
                flag = driver.FindElement(loggedInUserName).Enabled;
            }
            catch (NoSuchElementException)
            {
                flag = false;
            }
            return flag;
        }
    }
}