using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{

    [Collection("Alphapoint_QA_USER")]
    public class OrderEntryTest:TestBase
    {
        private string instrument;
        private string orderType;
        private string menuTab;
        private string buyTab;
        private string sellTab;
        private string orderSize;
        private string limitPrice;
        private string timeInForce;
        private string marketOrderBuyAmount;
        private string marketOrderSellAmount;
        private string feeComponent;
        private string stopPrice;
        private string sellStopPrice;
        private string sellOrderSize;
        private string buyOrderSize;
        private string incSellOrderSize;
        private string decSellOrderSize;

        public OrderEntryTest(ITestOutputHelper output):base(output)
        {

        }

        // Test Secenario - 2
        [Fact]
        public void TC2_VerifyAmountPersistence()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                Assert.True(orderEntryPage.VerifyOrderEntryAmountPersistence(instrument, TestData.GetData("PersistenceTestAmount")));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceSuccessMsg, buyTab, sellTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.AmountPersistenceFailureMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        //Test Secenario - 3
        [Fact]
        public void TC3_VerifyBuyMarketOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                marketOrderBuyAmount = TestData.GetData("TC3_MarketOrderBuyAmount");
                feeComponent = TestData.GetData("FeeComponent");
                sellOrderSize = TestData.GetData("TC3_SellOrderSize");
                limitPrice = TestData.GetData("TC3_LimitPrice");
                timeInForce = TestData.GetData("TC3_TimeInForce");

                string feeValue = GenericUtils.FeeAmount(marketOrderBuyAmount, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER5);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));


                userFunctions.LogIn(TestProgressLogger, Const.USER6);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                Dictionary<string, string> placeMarketBuyOrder = orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(marketOrderBuyAmount));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(placeMarketBuyOrder["Instrument"], placeMarketBuyOrder["Side"], Double.Parse(placeMarketBuyOrder["BuyAmount"]), feeValue, placeMarketBuyOrder["PlaceOrderTime"], placeMarketBuyOrder["PlaceOrderTimePlusOneMin"]), Const.MarketOrderVerifiedInFilledOrders);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketOrderTestPassed, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.MarketOrderTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        //Test Secenario - 4
        [Fact]
        public void TC4_VerifySellMarketOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                marketOrderSellAmount = TestData.GetData("TC4_MarketOrderSellAmount");
                feeComponent = TestData.GetData("FeeComponent");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC4_MarketOrderBuyAmount");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");

                string feeValue = GenericUtils.SellFeeAmount(buyOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                Dictionary<string, string> placeMarketSellOrder = orderEntryPage.PlaceMarketSellOrder(instrument, sellTab, Double.Parse(marketOrderSellAmount), Double.Parse(feeComponent));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(placeMarketSellOrder["Instrument"], placeMarketSellOrder["Side"], Double.Parse(placeMarketSellOrder["SellAmount"]), feeValue, placeMarketSellOrder["PlaceOrderTime"], placeMarketSellOrder["PlaceOrderTimePlusOneMin"]), Const.MarketOrderVerifiedInFilledOrders);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.SellMarketOrderSuccessMsg));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.MarketOrderTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        //Test Secenario - 5
        [Fact]
        public void VerifyBuyLimitOrder()
        {
            try
            {
                string type;
                string feeValue;
                string placeBuyOrderTime;
                string placeBuyOrderTimePlusOneMin;
                string placeSellOrderTime;
                string placeSellOrderTimePlusOneMin;
                string successMsg;
                string askPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC5_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC5_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC5_IncreasedSellOrderSize");
                decSellOrderSize = TestData.GetData("TC5_DecreasedSellOrderSize");
                limitPrice = TestData.GetData("TC5_LimitPrice");
                timeInForce = TestData.GetData("TC5_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                type = Const.Limit;
                feeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                // Creating Buy and Sell Order to get the last price
                TestProgressLogger.StartTest();
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.SelectLinkOnKYCPage(driver);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                Dictionary<string, string> placeLimitBuyOrder = orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, orderSize, limitPrice, timeInForce);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSize), limitPrice, placeLimitBuyOrder["PlaceOrderTime"], placeLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, buyTab));
                userFunctions.LogOut();
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                Dictionary<string, string> placeLimitSellOrder = orderEntryPage.PlaceLimitSellOrder(instrument, buyTab, orderSize, limitPrice, timeInForce);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(orderSize), feeValue, placeLimitSellOrder["PlaceOrderTime"], placeLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, sellTab));
                userFunctions.LogOut();


            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.MarketOrderTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        //Test Secenario - 7
        [Fact]
        public void TC7_VerifyBuyStopOrder()
        {
            try
            {
                string type = Const.StopMarket;
                instrument = TestData.GetData("Instrument");
                marketOrderSellAmount = TestData.GetData("MarketOrderSellAmount");
                feeComponent = TestData.GetData("FeeComponent");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                stopPrice = TestData.GetData("StopPrice");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);

                userFunctions.LogIn(TestProgressLogger, Const.USER6);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));


                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                Dictionary<string, string> placeStopBuyOrder = orderEntryPage.PlaceStopBuyOrder(instrument, buyTab, Double.Parse(orderSize), Double.Parse(feeComponent), Double.Parse(stopPrice));

               
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(placeStopBuyOrder["Instrument"], placeStopBuyOrder["Side"], type, Double.Parse(placeStopBuyOrder["BuyAmount"]), placeStopBuyOrder["StopPrice"], placeStopBuyOrder["PlaceOrderTime"], placeStopBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.BuyStopOrderSuccessMsg, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.MarketOrderTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }

        }

        //Test Secenario - 8
        [Fact]
        public void TC8_VerifySellStopOrder()
        {
            try
            {
                string type = Const.StopMarket;
                instrument = TestData.GetData("Instrument");
                marketOrderSellAmount = TestData.GetData("MarketOrderSellAmount");
                feeComponent = TestData.GetData("FeeComponent");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                sellStopPrice = TestData.GetData("SellStopPrice");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);

                userFunctions.LogIn(TestProgressLogger, Const.USER6);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));


                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                Dictionary<string, string> placeStopSellOrder = orderEntryPage.PlaceStopSellOrder(instrument, sellTab, Double.Parse(orderSize), Double.Parse(feeComponent), Double.Parse(sellStopPrice));


                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(placeStopSellOrder["Instrument"], placeStopSellOrder["Side"], type, Double.Parse(placeStopSellOrder["SellAmount"]), placeStopSellOrder["StopPrice"], placeStopSellOrder["PlaceOrderTime"], placeStopSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.BuyStopOrderSuccessMsg, sellTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.MarketOrderTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

    }
}
