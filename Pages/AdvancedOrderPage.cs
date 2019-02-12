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

namespace AlphaPoint_QA.Pages
{
    class AdvancedOrderPage
    {
        ProgressLogger logger;
        //private readonly ITestOutputHelper output;
        static Config data;
        public static IWebDriver driver;

        By buyTab = By.XPath("//div[text()='Buy']");
        By sellTab = By.XPath("//div[@class='advanced-order-sidepane__tab-container']/div[2]");
        By instrument = By.Name("instrument");
        By orderType = By.XPath("//select[@name='orderType']");
        By orderSize = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='quantity']");
        By limitPrice = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='limitPrice']");
        By stopPrice = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='stopPrice']");
        By displayQuntity = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='displayQuantity']");
        By placeBuyOrder = By.XPath("//form[@class='advanced-order-form__body']//button[text()='Place Buy Order']");
        By placeSellOrder = By.XPath("//form[@class='advanced-order-form__body']//button[text()='Place Sell Order']");
        By askOrBidPrice = By.XPath("//div[@class='advanced-order-form__limit-price-block-value']");
        By askOrBidPriceLabel = By.XPath("//div[@class='advanced-order-form__limit-price-block']/div");
        By pegPrice = By.Name("pegPriceType");
        By trailingAmount = By.XPath("//input[@data-test='Trailing Amount:']");

        public AdvancedOrderPage(ProgressLogger logger)
        {
            
            this.logger = logger;
            logger.Info("Advance Order Page Started");
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }


        public void SelectBuyOrSellTab(string buyOrSell)
        {
            Thread.Sleep(1000);
            try
            {
                if (buyOrSell.Equals("Buy"))
                {
                    string labeltext = driver.FindElement(askOrBidPriceLabel).Text;
                    if (!labeltext.Contains("Ask Price"))
                    {
                        driver.FindElement(buyTab).Click();
                    }
                }
                else if (buyOrSell.Equals("Sell"))
                {
                    driver.FindElement(sellTab).Click();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IWebElement InstrumentDropDown(IWebDriver driver)
        {
            return driver.FindElement(instrument);
        }

        public IWebElement PegPriceDropDown(IWebDriver driver)
        {
            return driver.FindElement(pegPrice);
        }

        public IWebElement OrderTypeDropDown(IWebDriver driver)
        {
            return driver.FindElement(orderType);
        }

        public IWebElement OrderSizeEditBox(IWebDriver driver)
        {
            return driver.FindElement(orderSize);
        }

        public IWebElement LimitPriceEditBox(IWebDriver driver)
        {
            return driver.FindElement(limitPrice);
        }

        public IWebElement StopPriceEditBox(IWebDriver driver)
        {
            return driver.FindElement(stopPrice);
        }

        public IWebElement TrailingAmountEditBox(IWebDriver driver)
        {
            return driver.FindElement(trailingAmount);
        }

        public IWebElement DisplayQuntityEditBox(IWebDriver driver)
        {
            return driver.FindElement(displayQuntity);
        }

        public IWebElement PlaceBuyOrderButton(IWebDriver driver)
        {
            return driver.FindElement(placeBuyOrder);
        }

        public IWebElement PlaceSellOrderButton(IWebDriver driver)
        {
            return driver.FindElement(placeSellOrder);
        }

        public IWebElement AskOrBidPriceLabel(IWebDriver driver)
        {
            return driver.FindElement(askOrBidPrice);
        }


        public string GetAskOrBidPrice()
        {
            return driver.FindElement(askOrBidPrice).Text;
        }


        public void SelectInstrumentsAndOrderType(string instruments, string orderType)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(InstrumentDropDown(driver));
                UserSetFunctions.SelectDropdown(InstrumentDropDown(driver), instruments);
                UserSetFunctions.VerifyWebElement(OrderTypeDropDown(driver));
                UserSetFunctions.SelectDropdown(OrderTypeDropDown(driver), orderType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SelectPegPrice(string pegPriceType)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(PegPriceDropDown(driver));
                UserSetFunctions.SelectDropdown(PegPriceDropDown(driver), pegPriceType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceBuyOrderWithReserveOrderType(string orderSize, string limitPrice, string displayQuantity)
        {
            try
            {
                string placeOrderTime;
                string placeOrderTimePlusOneMin;
                string successMsg;
                Dictionary<string, string> reserveBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.VerifyWebElement(OrderSizeEditBox(driver));
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.VerifyWebElement(LimitPriceEditBox(driver));
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.VerifyWebElement(DisplayQuntityEditBox(driver));
                UserSetFunctions.EnterText(DisplayQuntityEditBox(driver), displayQuantity);
                UserSetFunctions.VerifyWebElement(PlaceBuyOrderButton(driver));
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                placeOrderTime = GenericUtils.GetCurrentTime();
                placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                reserveBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                reserveBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return reserveBuyOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Dictionary<string, string> PlaceSellOrderWithReserveOrderType(string orderSize, string limitPrice, string displayQuantity)
        {
            try
            {
                string placeOrderTime;
                string placeOrderTimePlusOneMin;
                string successMsg;
                Dictionary<string, string> reserveSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.VerifyWebElement(OrderSizeEditBox(driver));
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.VerifyWebElement(LimitPriceEditBox(driver));
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.VerifyWebElement(DisplayQuntityEditBox(driver));
                UserSetFunctions.EnterText(DisplayQuntityEditBox(driver), displayQuantity);
                UserSetFunctions.VerifyWebElement(PlaceSellOrderButton(driver));
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                placeOrderTime = GenericUtils.GetCurrentTime();
                placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                reserveSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                reserveSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return reserveSellOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceMarketBuyOrder(string orderSize)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeBuyOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceMarketSellOrder(string orderSize)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceBuyOrderWithImmediateOrCancelType(string orderSize, string limitPrice)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);

                return placeBuyOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceSellOrderWithImmediateOrCancelType(string orderSize, string limitPrice)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceStopMarketBuyOrder(string orderSize, string stopPrice)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(StopPriceEditBox(driver), stopPrice);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);

                return placeBuyOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceStopMarketSellOrder(string orderSize, string stopPrice)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(StopPriceEditBox(driver), stopPrice);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceTrailingStopMarketBuyOrder(string orderSize, string trailingAmount, string pegPriceValue)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(TrailingAmountEditBox(driver), trailingAmount);
                SelectPegPrice(pegPriceValue);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeBuyOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceTrailingStopMarketSellOrder(string orderSize, string trailingAmount, string pegPriceValue)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(TrailingAmountEditBox(driver), trailingAmount);
                SelectPegPrice(pegPriceValue);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}