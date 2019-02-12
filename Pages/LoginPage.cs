using AlphaPoint_QA.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    public class LoginPage
    {
        IWebDriver driver;
        private readonly ITestOutputHelper output;

        public LoginPage(IWebDriver driver, ITestOutputHelper output)
        {
            this.driver = driver;
            this.output = output;
        }

        /// <summary>
        /// Locators for elements
        /// </summary>
        By selectServer = By.XPath("//*[@id='root']/div[1]/div/div[2]/form/div[3]/select");
        By loginName = By.XPath("//*[@id='root']/div[1]/div/div[2]/form/div[1]/div/input");
        By loginPassword = By.XPath("//*[@id='root']/div[1]/div/div[2]/form/div[2]/div/div/div[1]/input");
        By loginButton = By.XPath("//*[@id='root']/div[1]/div/div[2]/form/button");

        public void Login(string userName, string password)
        {
            UserSetFunctions.SelectDropdown(driver.FindElement(selectServer), "wss://apiapexqa2.alphapoint.com/WSGateway/");
            UserSetFunctions.EnterText(driver.FindElement(loginName), userName);
            UserSetFunctions.EnterText(driver.FindElement(loginPassword), password);
            UserSetFunctions.Click(driver.FindElement(loginButton));

            output.WriteLine("Title--------------------" + driver.Title);
            Assert.Equal(driver.Title.ToLower(), "APEX Web".ToLower());

            //This returns new object of Home page such that it initializes the parameters of Home page after login
            //return new UserHomePage(driver);
        }


        private void clickLogin()
        {
            
        }

        private void typeUserName(String username)
        {
            
        }

        private void typePassword(String password)
        {
            
        }

        public void LoginUser(String username, String password)
        {

            typeUserName(username);
            typePassword(password);
            clickLogin();
        }

    }
}
