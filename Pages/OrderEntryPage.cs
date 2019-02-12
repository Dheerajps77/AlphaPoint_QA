using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;


namespace AlphaPoint_QA.pages
{
    public class OrderEntryPage
    {
        IWebDriver driver;

        ProgressLogger logger;

        public OrderEntryPage(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
        }

        /// <summary>
        /// Locators for elements
        /// </summary>
        public By orderEntryButton = By.XPath("//div[@data-test='Order Entry']");
        public By buyOrderEntryButton = By.XPath("//label[@data-test='Buy Side']");

        // Order Type Market
        public By marketOrderTypeButton = By.XPath("//label[@data-test='Market Order Type']");
        public By buyAmountTextField = By.XPath("//input[@data-test='Buy Amount']");
        public By placeBuyOrderButton = By.XPath("//button[text()='Place Buy Order']");
        public By lastPrice = By.XPath("//span[@class='instrument-table__value instrument-table__value--last-price']");
        public By placeSellOrderButton = By.XPath("//button[text()='Place Sell Order']");
        public By stopOrderTypeButton = By.XPath("//label[@data-test='Stop Order Type']");
        public By sellAmountTextField = By.XPath("//input[@data-test='Sell Amount']");
        public By limitOrderTypeButton = By.XPath("//label[@data-test='Limit Order Type']");
        public By sellOrderEntryButton = By.XPath("//label[@data-test='Sell Side']");
        public By limitPriceTextBox = By.XPath("//div[@class='ap-input__input-box order-entry__input-box']//input[@name='limitPrice']");
        public By timeInForce = By.XPath("//select[@name='timeInForce']");

        public By feesText = By.XPath("//div[contains(@class,'ap-label-with-text')]//label[contains(@class,'order-entry__lwt-label') and text()='Fees']");
        public By orderTotalText = By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Order Total']//following::span[@class='ap-label-with-text__text order-entry__lwt-text']");
        public By netText = By.XPath("//div[contains(@class,'ap-label-with-text')]//label[contains(@class,'order-entry__lwt-label') and text()='Net']");
        public By marketPriceText = By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Market Price']//following::span[@data-test='Market Price']");
        public By stopPriceTextField = By.XPath("//input[@data-test='Stop Price']");
        public By exchangeMenuText = By.XPath("//span[@class='page-header-nav__label' and text()='Exchange']");
        public By successfullymsg = By.XPath("//div[contains(@class,'snackbar snackbar')]/div");

        public IWebElement PlaceBuyOrderButton()
        {
            return driver.FindElement(placeBuyOrderButton);
        }

        public IWebElement TransactionMessage()
        {
            return driver.FindElement(successfullymsg);
        }

        public IWebElement PlaceSellOrderButton()
        {
            return driver.FindElement(placeSellOrderButton);
        }

        public IWebElement StopOrderTypeButton()
        {
            return driver.FindElement(stopOrderTypeButton);
        }

        public IWebElement BuyAmountTextField()
        {
            return driver.FindElement(buyAmountTextField);
        }

        public IWebElement SellAmountTextField()
        {
            return driver.FindElement(sellAmountTextField);
        }

        public IWebElement MarketPriceText()
        {
            return driver.FindElement(marketPriceText);
        }

        public IWebElement MarketOrderTypeButton()
        {
            return driver.FindElement(marketOrderTypeButton);
        }
        public IWebElement StopPriceTextField()
        {
            return driver.FindElement(stopPriceTextField);
        }

        public IWebElement LimitPriceTextBox()
        {
            return driver.FindElement(limitPriceTextBox);
        }

        public IWebElement TimeInForce()
        {
            return driver.FindElement(timeInForce);
        }

        public IWebElement LimitOrderTypeButton()
        {
            return driver.FindElement(limitOrderTypeButton);
        }

        public IWebElement BuyOrderEntryButton()
        {
            return driver.FindElement(buyOrderEntryButton);
        }

        public IWebElement SellOrderEntryButton()
        {
            return driver.FindElement(sellOrderEntryButton);
        }

        public IWebElement OrderEntryButton()
        {
            return driver.FindElement(orderEntryButton);
        }

        public IWebElement FeesText()
        {
            return driver.FindElement(feesText);
        }

        public IWebElement OrderTotalText()
        {
            return driver.FindElement(orderTotalText);
        }

        public IWebElement NetText()
        {
            return driver.FindElement(netText);
        }

        public IWebElement ExchangeMenuText()
        {
            return driver.FindElement(exchangeMenuText);
        }


        public void ClickPlaceBuyOrder()
        {
            UserSetFunctions.Click(PlaceBuyOrderButton());
        }

        public void ClickPlaceSellOrder()
        {
            UserSetFunctions.Click(PlaceSellOrderButton());
        }

        // This method fetches the Last Price value
        public string GetLastPrice()
        {
            logger.LogCheckPoint("Last Price" + driver.FindElement(lastPrice).Text);
            return driver.FindElement(lastPrice).Text;
        }

        // This method selects Limit button for Buy side
        public void SelectBuyLimitButton()
        {
            UserSetFunctions.Click(BuyOrderEntryButton());
            UserSetFunctions.Click(LimitOrderTypeButton());
        }

        // This method selects Limit button for Sell side
        public void SelectSellLimitButton()
        {
            UserSetFunctions.Click(SellOrderEntryButton());
            UserSetFunctions.Click(LimitOrderTypeButton());
        }

        // This method verifies the persistence of Amount entered in the Order Size field
        public bool VerifyOrderEntryAmountPersistence(string instrument, string amountEntered)
        {
            bool flag = false;
            string exchangeStringValueFromSite;
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);

            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserSetFunctions.Click(MarketOrderTypeButton());
                UserSetFunctions.EnterText(BuyAmountTextField(), amountEntered);
                UserSetFunctions.Click(LimitOrderTypeButton());
                UserSetFunctions.Click(StopOrderTypeButton());
                UserSetFunctions.Click(MarketOrderTypeButton());
                Thread.Sleep(2000);
                string amountPersisted = BuyAmountTextField().GetAttribute("value");
                if (amountEntered.Equals(amountPersisted))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckSuccessMsg, TestData.GetData("BuyTab")));
                    flag = true;
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckFailureMsg, TestData.GetData("BuyTab")));
                    flag = false;
                }

                UserSetFunctions.Click(SellOrderEntryButton());
                UserSetFunctions.EnterText(SellAmountTextField(), amountEntered);
                UserSetFunctions.Click(MarketOrderTypeButton());
                UserSetFunctions.Click(LimitOrderTypeButton());
                UserSetFunctions.Click(StopOrderTypeButton());

                Thread.Sleep(2000);
                if (amountEntered.Equals(amountPersisted))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckSuccessMsg, TestData.GetData("SellTab")));
                    flag = true;
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckFailureMsg, TestData.GetData("SellTab")));
                    flag = false;
                }
            }
            else
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                flag = false;
            }
            return flag;
        }


        public Dictionary<string, string> PlaceMarketBuyOrder(string instrument, string side, double buyAmount)
        {
            try
            {
                string exchangeStringValueFromSite;
                //string fee = (buyAmount * feeComponent).ToString();
                string buyAmountValue = buyAmount.ToString();
                string successMsg;
                string placeOrderTime;
                string placeOrderTimePlusOneMin;

                Dictionary<string, string> marketBuyOrderData = new Dictionary<string, string>();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Thread.Sleep(2000);
                exchangeStringValueFromSite = ExchangeMenuText().Text;
                Thread.Sleep(2000);

                if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                    UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                    UserCommonFunctions.CancelAllOrders(driver);
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(BuyOrderEntryButton());
                    UserSetFunctions.Click(MarketOrderTypeButton());
                    UserSetFunctions.EnterText(BuyAmountTextField(), buyAmountValue);
                    Thread.Sleep(2000);
                    UserSetFunctions.Click(PlaceBuyOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, buyAmount));
                    marketBuyOrderData.Add("Instrument", instrument);
                    marketBuyOrderData.Add("Side", side);
                    marketBuyOrderData.Add("BuyAmount", buyAmountValue);
                    //marketBuyOrderData.Add("Fee", fee);
                    marketBuyOrderData.Add("PlaceOrderTime", placeOrderTime);
                    marketBuyOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
                UserCommonFunctions.ScrollingDownVertical(driver);
                Thread.Sleep(3000);
                return marketBuyOrderData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceMarketSellOrder(string instrument, string side, double sellAmount, double feeComponent)
        {
            try
            {
                string exchangeStringValueFromSite;
                string fee = (sellAmount * feeComponent).ToString();
                string sellAmountValue = sellAmount.ToString();
                string successMsg;
                string placeOrderTime;
                string placeOrderTimePlusOneMin;

                Dictionary<string, string> marketSellOrderData = new Dictionary<string, string>();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Thread.Sleep(2000);
                exchangeStringValueFromSite = ExchangeMenuText().Text;
                Thread.Sleep(2000);

                if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                    UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                    UserCommonFunctions.CancelAllOrders(driver);
                    UserSetFunctions.Click(driver.FindElement(orderEntryButton));
                    UserSetFunctions.Click(driver.FindElement(sellOrderEntryButton));
                    UserSetFunctions.Click(driver.FindElement(marketOrderTypeButton));
                    UserSetFunctions.EnterText(SellAmountTextField(), sellAmount.ToString());
                    Thread.Sleep(2000);
                    UserSetFunctions.Click(PlaceSellOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, sellAmount));
                    marketSellOrderData.Add("Instrument", instrument);
                    marketSellOrderData.Add("Side", side);
                    marketSellOrderData.Add("SellAmount", sellAmountValue);
                    marketSellOrderData.Add("Fee", fee);
                    marketSellOrderData.Add("PlaceOrderTime", placeOrderTime);
                    marketSellOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
                UserCommonFunctions.ScrollingDownVertical(driver);
                Thread.Sleep(3000);
                return marketSellOrderData;
            }
            catch (Exception e)
            {
                logger.Error(Const.MarketSellOrderFailureMsg);
                throw e;
            }

        }

        public Dictionary<string, string> PlaceStopBuyOrder(string instrument, string side, double buyAmount, double feeComponent, double stopPrice)
        {
            string exchangeStringValueFromSite;
            string fee = (buyAmount * feeComponent).ToString();
            string buyAmountValue = buyAmount.ToString();
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;
            string stopPriceValue = stopPrice.ToString();

            Dictionary<string, string> stopBuyOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);

            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(BuyOrderEntryButton());
                    UserSetFunctions.Click(StopOrderTypeButton());
                    UserSetFunctions.EnterText(BuyAmountTextField(), buyAmount.ToString());
                    UserSetFunctions.EnterText(StopPriceTextField(), stopPrice.ToString());
                    Thread.Sleep(2000);

                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        logger.LogCheckPoint(string.Format(LogMessage.StopOrderBalanceStoredSuccessfully, side));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.StopOrderNotPresent, side, OrderTotalText().Text, MarketPriceText().Text));
                    }
                    UserSetFunctions.Click(PlaceBuyOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, buyAmount));
                    stopBuyOrderData.Add("Instrument", instrument);
                    stopBuyOrderData.Add("Side", side);
                    stopBuyOrderData.Add("BuyAmount", buyAmountValue);
                    stopBuyOrderData.Add("StopPrice", stopPriceValue);
                    stopBuyOrderData.Add("PlaceOrderTime", placeOrderTime);
                    stopBuyOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    Thread.Sleep(2000);
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
            }
            UserCommonFunctions.ScrollingDownVertical(driver);
            Thread.Sleep(3000);
            return stopBuyOrderData;
        }

        public Dictionary<string, string> PlaceStopSellOrder(string instrument, string side, double sellAmount, double feeComponent, double stopPrice)
        {
            string exchangeStringValueFromSite;
            string fee = (sellAmount * feeComponent).ToString();
            string sellAmountValue = sellAmount.ToString();
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;
            string stopPriceValue = stopPrice.ToString();

            Dictionary<string, string> stopSellOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);

            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                if (SellOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(SellOrderEntryButton());
                    UserSetFunctions.Click(StopOrderTypeButton());
                    UserSetFunctions.EnterText(SellAmountTextField(), sellAmount.ToString());
                    UserSetFunctions.EnterText(StopPriceTextField(), stopPrice.ToString());
                    Thread.Sleep(2000);

                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        logger.LogCheckPoint(string.Format(LogMessage.StopOrderBalanceStoredSuccessfully, side));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.StopOrderNotPresent, side, OrderTotalText().Text, MarketPriceText().Text));
                    }
                    UserSetFunctions.Click(PlaceSellOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, sellAmount));
                    stopSellOrderData.Add("Instrument", instrument);
                    stopSellOrderData.Add("Side", side);
                    stopSellOrderData.Add("SellAmount", sellAmountValue);
                    stopSellOrderData.Add("StopPrice", stopPriceValue);
                    stopSellOrderData.Add("PlaceOrderTime", placeOrderTime);
                    stopSellOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    Thread.Sleep(2000);
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
            }
            UserCommonFunctions.ScrollingDownVertical(driver);
            Thread.Sleep(3000);
            return stopSellOrderData;
        }

        //Limit buy Order
        public Dictionary<string, string> PlaceLimitBuyOrder(string instrument, string side, string buyAmount, string limitPrice, string timeinforce)
        {
            string exchangeStringValueFromSite;
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;

            Dictionary<string, string> limitBuyOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);
            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(BuyOrderEntryButton());
                    UserSetFunctions.Click(LimitOrderTypeButton());
                    UserSetFunctions.EnterText(BuyAmountTextField(), buyAmount);
                    UserSetFunctions.EnterText(LimitPriceTextBox(), limitPrice);
                    UserSetFunctions.SelectDropdown(TimeInForce(), timeinforce);

                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        logger.LogCheckPoint(string.Format("", side));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format("", side, OrderTotalText().Text, MarketPriceText().Text));
                    }
                    UserSetFunctions.Click(PlaceBuyOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, buyAmount));
                    limitBuyOrderData.Add("PlaceOrderTime", placeOrderTime);
                    limitBuyOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    logger.LogCheckPoint(String.Format(LogMessage.LimitOrderSuccessMsg, side, buyAmount, limitPrice));
                }
            }
            else
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
            }
            return limitBuyOrderData;
        }

        //Limit sell Order
        public Dictionary<string, string> PlaceLimitSellOrder(string instrument, string side, string sellAmount, string limitPrice, string timeinforce)
        {
            string exchangeStringValueFromSite;
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;

            Dictionary<string, string> limitSellOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);
            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(SellOrderEntryButton());
                    UserSetFunctions.Click(LimitOrderTypeButton());
                    UserSetFunctions.EnterText(SellAmountTextField(), sellAmount);
                    UserSetFunctions.EnterText(LimitPriceTextBox(), limitPrice);
                    UserSetFunctions.SelectDropdown(TimeInForce(), timeinforce);
                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        logger.LogCheckPoint(string.Format("", side));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format("", side, OrderTotalText().Text, MarketPriceText().Text));
                    }
                    UserSetFunctions.Click(PlaceSellOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, sellAmount));
                    limitSellOrderData.Add("PlaceOrderTime", placeOrderTime);
                    limitSellOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    logger.LogCheckPoint(String.Format(LogMessage.LimitOrderSuccessMsg, side, sellAmount, limitPrice));
                }
            }
            return limitSellOrderData;
        }
    }
}