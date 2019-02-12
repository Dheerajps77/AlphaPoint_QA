using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    class AdvanceBuyLimitOrderIOCPage
    {
        IWebDriver driver;
        private readonly ProgressLogger logger;
 
        string exchangeMenuString = "Exchange";

        By advanceOrderButton = By.XPath("//div[@class='order-entry__item-button' and text()='« Advanced Orders']");
        By buyButton = By.XPath("//div[contains(@class,'advanced-order-sidepane__tab') and text()='Buy']");
        By sellButton = By.XPath("//div[text()='Sell']");
        By feesText = By.XPath("//span[contains(@class,'advanced-order-form__lwt-text') and @data-test='Fees:']");
        By orderTotalText = By.XPath("//span[contains(@class,'advanced-order-form__lwt-text') and @data-test='Order Total:']");
        By instrumentList = By.XPath("//select[@name='instrument']");
        By orderTypeList = By.XPath("//select[@name='orderType']");
        By ordersizeTextField = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='quantity']");
        By placeBuyOrderButton = By.XPath("//button[contains(@class,'advanced-order-form__btn') and text()='Place Buy Order']");
        By placeSellOrderButton = By.XPath("//button[contains(@class,'advanced-order-form__btn') and text()='Place Sell Order']");
        By buyButtonSelected = By.XPath("//div[@class='advanced-order-sidepane__tab advanced-order-sidepane__tab--buy-selected']");
        By exchangeMenuText = By.XPath("//span[@class='page-header-nav__label' and text()='Exchange']");
        By sellOrderEntryButton = By.XPath("//label[@data-test='Sell Side']");
        By sellAmountTextField = By.XPath("//input[@data-test='Sell Amount']");
        By limitOrderTypeButton = By.XPath("//label[@data-test='Limit Order Type']");
        By orderEntrySlectTimeInForce = By.XPath("//select[@name='timeInForce']");

        By advanceOrderSizeAmount = By.XPath("//input[@data-test='Order Size:']");
        By advanceOrderLimitAmount = By.XPath("//input[@data-test='Limit Price:']");
        By openOrderAllListValue = By.XPath("//div[@class='flex-table__body order-history-table__body']/div");
        By openOrderTabButton = By.XPath("//div[@data-test='Open Orders']");

        AdvancedOrderPage objAdvancedOrderPage;
        UserFunctions objUserFunctionality;

        public AdvanceBuyLimitOrderIOCPage(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
          
        }

        public IWebElement ExchangeMenuText()
        {
            return driver.FindElement(exchangeMenuText);
        }
        
        public IWebElement AdvanceOrderButton()
        {
            return driver.FindElement(advanceOrderButton);
        }

        public IWebElement BuyButton()
        {
            return driver.FindElement(buyButton);
        }

        public IWebElement SellButton()
        {
            return driver.FindElement(sellButton);
        }

        public IWebElement OpenOrderTabButton()
        {
            return driver.FindElement(openOrderTabButton);
        }

        public IWebElement OpenOrderAllListValue()
        {
            return driver.FindElement(openOrderAllListValue);
        }

        public IWebElement BuyButtonSelected()
        {
            return driver.FindElement(buyButtonSelected);
        }
        

        // This method verifies the Advance orders button
        // This method verifies the Buy Order button
        public bool AdvanceOrdersButton(IWebDriver driver)
        {
            bool flag = false;
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            string exchangeStringValueFromSite = ExchangeMenuText().Text;

            Thread.Sleep(3000);

            if (exchangeStringValueFromSite.Equals(exchangeMenuString))
            {
                logger.Info("Verification for exchangeMenu value has been passed.");
                
                UserSetFunctions.Click(AdvanceOrderButton());
                Thread.Sleep(2000);
                UserSetFunctions.Click(BuyButton());

                Thread.Sleep(2000);

                if (BuyButtonSelected().Displayed)
                {
                    logger.Info("Buy Button clicked in Advance Order section.");
                    flag = true;
                    return flag;
                }
                else
                {
                    logger.Info("Buy Button isn't clicked in Advance Order section.");
                    return flag;
                }
            }
            else
            {
                logger.Info("Verification for exchangeMenu value has been failed.");
                return flag;
            }
        }

        public void AdvanceBuyLimitOrderIOC(string orderType, string instrument, string orderSize, string limitPrice)
        {
            string buyMarketOrderTime;
            UserSetFunctions.Click(BuyButton());
            objAdvancedOrderPage = new AdvancedOrderPage(logger);
            objUserFunctionality = new UserFunctions(logger);
            objAdvancedOrderPage.SelectInstrumentsAndOrderType(instrument, orderType);
            objAdvancedOrderPage.PlaceBuyOrderWithImmediateOrCancelType(orderSize, limitPrice);


            buyMarketOrderTime = GenericUtils.GetCurrentTime();
            VerifyAdvanceBuyOrderTab(instrument, "Buy", 2, limitPrice, buyMarketOrderTime);
            Thread.Sleep(2000);
        }

        public void AdvanceSellLimitOrderIOC(string orderType, string instrument, string orderSize, string limitPrice)
        {
            string sellMarketOrderTime;
            UserSetFunctions.Click(SellButton());
            objAdvancedOrderPage = new AdvancedOrderPage(logger);
            objUserFunctionality = new UserFunctions(logger);

            objAdvancedOrderPage.SelectInstrumentsAndOrderType(instrument, orderType);
            objAdvancedOrderPage.PlaceSellOrderWithImmediateOrCancelType(orderSize, limitPrice);
            sellMarketOrderTime = GenericUtils.GetCurrentTime();
            Thread.Sleep(2000);         
            UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
            
            Thread.Sleep(2000);
            UserSetFunctions.Click(OpenOrderTabButton());
            VerifyAdvanceBuyOrderTab(instrument, "Sell", 2, limitPrice, sellMarketOrderTime);
        }

        public bool VerifyAdvanceBuyOrderTab(string instrument, string side, double size, string limitPrice, string buyMarketOrderTime)
        {
            bool flag = false;
            string buyAmountValue = GenericUtils.ConvertToDoubleFormat(size);
            string expectedRow = instrument + " || " + side + " || " + size + " || " + limitPrice + " || " + buyMarketOrderTime;

            if (GetListOfOpenOrders().Contains(expectedRow))
            {
                logger.LogCheckPoint("Matched Expected -> " + expectedRow + " Actual -> ");
                flag = true;
            }
            return flag;


        }

        public void VerifyAdvanceSellOrderTab()
        {

        }

        public ArrayList GetListOfOpenOrders()
        {
            ArrayList openOrderList = new ArrayList();
            int countOfOpenOrders = driver.FindElements(openOrderAllListValue).Count;
            for (int i = 1; i <= countOfOpenOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                logger.LogCheckPoint("countItems ---- " + countItems);
                for (int j = 1; j <= (countItems - 2); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    logger.LogCheckPoint("text ---- " + text);
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                openOrderList.Add(textFinal);
                logger.LogCheckPoint("Text FINAL---- " + textFinal);
            }
            return openOrderList;
        }
    }
}
