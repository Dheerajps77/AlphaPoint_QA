using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    //Test Secenario --> 9
    class AdvanceBuyOrderPage
    {
        IWebDriver driver;
        private readonly ITestOutputHelper output;
        static ProgressLogger logger;
        string instrumentValue = "BTCUSD";
        string orderTypeValue = "Market Order";
        string exchangeMenuString = "Exchange";

        By advanceOrderButton = By.XPath("//div[@class='order-entry__item-button' and text()='« Advanced Orders']");
        By buyButton = By.XPath("//div[contains(@class,'advanced-order-sidepane__tab') and text()='Buy']");
        By sellButton = By.XPath("//div[text()='Sell']");
        By feesText = By.XPath("//span[contains(@class,'advanced-order-form__lwt-text') and @data-test='Fees:']");
        By orderTotalText = By.XPath("//span[contains(@class,'advanced-order-form__lwt-text') and @data-test='Order Total:']");
        By instrumentList = By.XPath("//select[@name='instrument']");
        By orderTypeList = By.XPath("//select[@name='orderType']");
        By ordersizeTextField = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='quantity']");
        By placeBuyOrderButton = By.XPath("//form[@class='advanced-order-form__body']//button[text()='Place Buy Order']");
        By closeIconAdvancedOrder = By.XPath("//div[@class='ap-sidepane__close-button advanced-order-sidepane__close-button']/span");
        
        public AdvanceBuyOrderPage(IWebDriver driver, ProgressLogger testProgressLogger)
        {
            this.driver = driver;
            this.output = output;
            logger = testProgressLogger;
        }

        By exchangeMenuText = By.XPath("//span[@class='page-header-nav__label' and text()='Exchange']");
       
        
        public void VerifyAdvanceBuyOrder(string instrument, IWebDriver driver, string orderSize)
        {
            
            Thread.Sleep(3000);
            UserCommonFunctions.DashBoardMenuButton(driver);
            Thread.Sleep(2000);


            string exchangeStringValueFromSite = driver.FindElement(exchangeMenuText).Text;
            Thread.Sleep(2000);

            if (exchangeStringValueFromSite.Equals(exchangeMenuString))
            {
                Assert.True(true, "Verification for exchangeMenu value has been passed.");
            }
            else
            {
                Assert.False(false, "Verification for exchangeMenu value has been failed.");
            }

            UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
            UserSetFunctions.Click(driver.FindElement(advanceOrderButton));

            UserSetFunctions.SelectDropdown(driver.FindElement(instrumentList), instrumentValue);
            UserSetFunctions.SelectDropdown(driver.FindElement(orderTypeList), orderTypeValue);

            UserSetFunctions.EnterText(driver.FindElement(ordersizeTextField), orderSize);

            Thread.Sleep(2000);
            if(driver.FindElement(ordersizeTextField).Displayed)
            {
                logger.Info("Fees and OrderTotal is displaying in the page.");
            }
            else
            {
                logger.Error("Fees and OrderTotal is displaying in the page.");
            }
            Thread.Sleep(3000);
            UserSetFunctions.Click(driver.FindElement(placeBuyOrderButton));
        }
    }
}
