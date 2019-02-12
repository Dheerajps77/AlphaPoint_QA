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
    class AdvanceSellOrderPage
    {
        IWebDriver driver;
        
        ProgressLogger logger;
        string orderTypeValue = "Market Order";
        string exchangeMenuString = "Exchange";

        By advanceOrderButton = By.XPath("//div[@class='order-entry__item-button' and text()='« Advanced Orders']");
        By buyButton = By.XPath("//div[contains(@class,'advanced-order-sidepane__tab') and text()='Buy']");
        By sellButton = By.XPath("//div[@class='advanced-order-sidepane__tab-container']/div[2]");
        By feesText = By.XPath("//span[contains(@class,'advanced-order-form__lwt-text') and @data-test='Fees:']");
        By orderTotalText = By.XPath("//span[contains(@class,'advanced-order-form__lwt-text') and @data-test='Order Total:']");
        By instrumentList = By.XPath("//select[@name='instrument']");
        By orderTypeList = By.XPath("//select[@name='orderType']");
        By ordersizeTextField = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='quantity']");
        By placeSellOrderButton = By.XPath("//form[@class='advanced-order-form__body']//button[text()='Place Sell Order']");
        By CloseIconAdvancedOrder = By.XPath("//div[@class='ap-sidepane__close-button advanced-order-sidepane__close-button']/span");

        By exchangeMenuText = By.XPath("//span[@class='page-header-nav__label' and text()='Exchange']");

        public AdvanceSellOrderPage(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
           
            this.logger = logger;
        }

        

        public void VerifyAdvanceSellOrder(string instrument, IWebDriver driver, string orderSize)
        {
            Thread.Sleep(2000);
            UserCommonFunctions.DashBoardMenuButton(driver);
            Thread.Sleep(2000);
            string exchangeStringValueFromSite = driver.FindElement(exchangeMenuText).Text;
            Thread.Sleep(3000);

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

            Thread.Sleep(2000);
            UserSetFunctions.Click(driver.FindElement(sellButton));

            UserSetFunctions.SelectDropdown(driver.FindElement(instrumentList), instrument);
            UserSetFunctions.SelectDropdown(driver.FindElement(orderTypeList), orderTypeValue);

            UserSetFunctions.EnterText(driver.FindElement(ordersizeTextField), orderSize);

            if(driver.FindElement(orderTotalText).Enabled)
            {
                logger.Info("Fees and OrderTotal is displaying in the page.");
            }
            else
            {
                logger.Error("Fees and OrderTotal is displaying in the page.");
            }

            Thread.Sleep(2000);
            UserSetFunctions.Click(driver.FindElement(placeSellOrderButton));
        }
    }
}
