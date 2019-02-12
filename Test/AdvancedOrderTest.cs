using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class AdvancedOrderTest : TestBase
    {
        private string instrument;
        private string orderType;
        private string menuTab;
        private string buyTab;
        private string sellTab;
        private string orderSize;
        private string limitPrice;
        private string marketOrder;
        private string buyOrderLimitPrice;
        private string sellOrderLimitPrice;
        private string timeInForce;
        private string feeComponent;
        private string buyOrderSize;
        private string sellOrderSize;
        private string reserveOrder;
        private string buyOrderDisplayQty;
        private string sellOrderDisplayQty;
        private string stopPrice;
        private string orderTypeDropdown;
        private string limitPriceEqualsStop;
        private string trailingAmount;
        private string pegPriceDropdown;

        public AdvancedOrderTest(ITestOutputHelper output) : base(output)
        {

        }

        //Test Case-9 Testing Done
        [Fact]
        public void VerifyMarketOrderTypeAdvanceBuyOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                marketOrder = TestData.GetData("MarketOrder");
                menuTab = TestData.GetData("MenuTab");
                sellTab = TestData.GetData("SellTab");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TC9_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC9_SellOrderSize");
                limitPrice = TestData.GetData("TC9_LimitPrice");
                timeInForce = TestData.GetData("TC9_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER5);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER6);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(buyTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, marketOrder);
                var placeMarketBuyOrder = advanceorder.PlaceMarketBuyOrder(buyOrderSize);
                string buyOrderSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, buyOrderSuccessMsg);
                TestProgressLogger.LogCheckPoint(buyOrderSuccessMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), feeValue, placeMarketBuyOrder["PlaceOrderTime"], placeMarketBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceMarketOrderSuccessMsg, buyTab, buyOrderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, buyTab, buyOrderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }


        // Test Case- 10 Testing Done
        [Fact]
        public void VerifyMarketOrderTypeAdvanceSellOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                marketOrder = TestData.GetData("MarketOrder");
                menuTab = TestData.GetData("MenuTab");
                sellTab = TestData.GetData("SellTab");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TC10_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC10_SellOrderSize");
                limitPrice = TestData.GetData("TC10_LimitPrice");
                timeInForce = TestData.GetData("TC10_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.SellFeeAmount(buyOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER5);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER6);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(sellTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, marketOrder);
                var placeMarketSellOrder = advanceorder.PlaceMarketSellOrder(sellOrderSize);
                string sellOrderSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, sellOrderSuccessMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), feeValue, placeMarketSellOrder["PlaceOrderTime"], placeMarketSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.Info(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, sellTab, sellOrderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]   //25
        public void TC25_VerifyIOCAdvanceBuyOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrderTime = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(orderSize, limitPrice);

                string successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, orderSize, limitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(orderSize), feeValue, placeIOCBuyOrderTime["PlaceOrderTime"], placeIOCBuyOrderTime["PlaceOrderTimePlusOneMin"]);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, orderSize, placeIOCBuyOrderTime));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.IOCOrderTypeFailedMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]      //26 Testing Done
        public void VerifyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                buyOrderLimitPrice = TestData.GetData("LimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC26_SellOrderLimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, sellOrderLimitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, sellOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, sellOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrder = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(orderSize, buyOrderLimitPrice);

                string cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);

                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(orderSize), buyOrderLimitPrice, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab));
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }

        }

        [Fact]      //27 Testing Done 
        public void VerifyPartiallyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderLimitPrice = TestData.GetData("TC27_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC27_SellOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC27_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC27_SellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.FeeAmount(sellOrderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, sellOrderLimitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, sellOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, sellOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrder = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, buyOrderLimitPrice);

                string cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);

                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(sellOrderSize), feeValue, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"]);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(buyOrderSize), buyOrderLimitPrice, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();

            }

        }

        [Fact]   //28 Testing Done 
        public void VerifyIOCAdvanceSellOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.SellFeeAmount(orderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrderTime = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(orderSize, limitPrice);

                string successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, orderSize, limitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(orderSize), feeValue, placeIOCSellOrderTime["PlaceOrderTime"], placeIOCSellOrderTime["PlaceOrderTimePlusOneMin"]);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, sellTab, orderSize, placeIOCSellOrderTime));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, sellTab));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();

            }

        }

        [Fact]   //29 Testing Done
        public void VerifyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC29_OrderSize");
                buyOrderLimitPrice = TestData.GetData("TC29_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC29_SellOrderLimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.SellFeeAmount(orderSize, sellOrderLimitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, buyOrderLimitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, buyOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, buyOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrder = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(orderSize, sellOrderLimitPrice);

                string cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);

                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(orderSize), sellOrderLimitPrice, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, sellTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }

        }

        [Fact]   //30 Testing Done
        public void VerifyPartiallyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderLimitPrice = TestData.GetData("TC30_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC30_SellOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC30_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC30_SellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.SellFeeAmount(buyOrderSize, buyOrderLimitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyOrderLimitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, buyOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, buyOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrder = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, sellOrderLimitPrice);

                string cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);

                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(buyOrderSize), feeValue, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"]);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(sellOrderSize), sellOrderLimitPrice, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, sellTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }

        }

        [Fact]    //31 Testing Done
        public void VerifyPlaceBuyOrderWithReservOrderType()
        {
            try
            {
                string type = Const.Limit;
                instrument = TestData.GetData("Instrument");
                reserveOrder = TestData.GetData("ReserveOrder");
                buyTab = TestData.GetData("BuyTab");
                buyOrderLimitPrice = TestData.GetData("TC31_BuyOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC31_BuyOrderSize");
                buyOrderDisplayQty = TestData.GetData("TC31_BuyOrderDisplayQty");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(buyTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, reserveOrder);
                var placeReserveBuyOrder = advanceorder.PlaceBuyOrderWithReserveOrderType(buyOrderSize, buyOrderLimitPrice, buyOrderDisplayQty);

                string successmsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successmsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderSuccessMsg, buyTab, buyOrderSize, buyOrderLimitPrice, buyOrderDisplayQty));
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(buyOrderSize), buyOrderLimitPrice, placeReserveBuyOrder["PlaceOrderTime"], placeReserveBuyOrder["PlaceOrderTimePlusOneMin"]));


            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]    //32 Testing Done
        public void VerifyPlaceSellOrderWithReserveOrderType()
        {
            try
            {
                string type = Const.Limit;
                instrument = TestData.GetData("Instrument");
                reserveOrder = TestData.GetData("ReserveOrder");
                sellTab = TestData.GetData("SellTab");
                sellOrderLimitPrice = TestData.GetData("TC32_SellOrderLimitPrice");
                sellOrderSize = TestData.GetData("TC32_SellOrderSize");
                sellOrderDisplayQty = TestData.GetData("TC32_SellOrderDisplayQty");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER6);

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);

                UserCommonFunctions.CancelAllOrders(driver);

                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));



                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(sellTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, reserveOrder);
                var placeReserveSellOrder = advanceorder.PlaceSellOrderWithReserveOrderType(sellOrderSize, sellOrderLimitPrice, sellOrderDisplayQty);

                string successmsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successmsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderSuccessMsg, sellTab, sellOrderSize, sellOrderLimitPrice, sellOrderDisplayQty));
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(sellOrderSize), sellOrderLimitPrice, placeReserveSellOrder["PlaceOrderTime"], placeReserveSellOrder["PlaceOrderTimePlusOneMin"]));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceReserveOrderSuccessMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // TC 13 Testing Done
        public void VerifyStopMarketBuyOrder()
        {
            try
            {
                string type = Const.StopMarket;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC13_SellOrderSize");
                limitPrice = TestData.GetData("TC13_LimitPrice");
                timeInForce = TestData.GetData("TC13_TimeInForce");
                stopPrice = TestData.GetData("TC13_StopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("StopMarketOrder");
                limitPriceEqualsStop = TestData.GetData("TC13_LimitPriceEqualsStop");

                string feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);
                string placeBuyOrderTime;
                string placeBuyOrderTimePlusOneMin;
                string placeSellOrderTime;
                string placeSellOrderTimePlusOneMin;
                string successMsg;

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);

                // Setting up market with User 8 - Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));

                // Creating Advance Stop Market Order with User 9 - Buy Order
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeStopMarketBuyOrder = advanceOrder.PlaceStopMarketBuyOrder(orderSize, stopPrice);
                placeBuyOrderTime = placeStopMarketBuyOrder["PlaceOrderTime"];
                placeBuyOrderTimePlusOneMin = placeStopMarketBuyOrder["PlaceOrderTimePlusOneMin"];
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSize), stopPrice, placeStopMarketBuyOrder["PlaceOrderTime"], placeStopMarketBuyOrder["PlaceOrderTimePlusOneMin"]));

                // Creating Buy and Sell Order to match Stop Price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPriceEqualsStop, timeInForce, Const.USER10, Const.USER11);

                // Create a seller to execute the Stop order
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                string sellOrderPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPriceEqualsStop, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(sellOrderPrice, limitPrice, driver);
                placeSellOrderTime = GenericUtils.GetCurrentTime();
                placeSellOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPriceEqualsStop));

                // Verify that the Stop order is executed

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(orderSize), feeValue, placeBuyOrderTime, placeBuyOrderTimePlusOneMin));
                TestProgressLogger.LogCheckPoint("Verfiy Market Order type Advance Buy Order passed successfully.");
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // TC 14 Testing Done
        public void VerifyStopMarketSellOrder()
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
                string buyOrderPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC14_SellOrderSize");
                limitPrice = TestData.GetData("TC14_LimitPrice");
                timeInForce = TestData.GetData("TC14_TimeInForce");
                stopPrice = TestData.GetData("TC14_StopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("StopMarketOrder");
                limitPriceEqualsStop = TestData.GetData("TC14_LimitPriceEqualsStop");

                type = Const.StopMarket;
                feeValue = GenericUtils.SellFeeAmount(orderSize, stopPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);

                // Setting up market with User 8 - Buy Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));

                // Creating Advance Stop Market Order with User 9 - Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeStopMarketSellOrder = advanceOrder.PlaceStopMarketSellOrder(orderSize, stopPrice);
                placeSellOrderTime = placeStopMarketSellOrder["PlaceOrderTime"];
                placeSellOrderTimePlusOneMin = placeStopMarketSellOrder["PlaceOrderTimePlusOneMin"];
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSize), stopPrice, placeStopMarketSellOrder["PlaceOrderTime"], placeStopMarketSellOrder["PlaceOrderTimePlusOneMin"]));

                // Creating Buy and Sell Order to match Stop Price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPriceEqualsStop, timeInForce, Const.USER10, Const.USER11);

                // Create a buyer to execute the Stop order
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                buyOrderPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPriceEqualsStop, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(buyOrderPrice, limitPrice, driver);
                placeBuyOrderTime = GenericUtils.GetCurrentTime();
                placeBuyOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPriceEqualsStop));

                // Verify that the Stop order is executed

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(orderSize), feeValue, placeBuyOrderTime, placeBuyOrderTimePlusOneMin));
                TestProgressLogger.Info("Verfiy Market Order type Advance Sell Order passed successfully.");
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // TC 17
        public void VerifyTrailingStopMarketBuyOrder()
        {
            try
            {
                string type;
                string feeValue;
                string successMsg;
                string askPrice;
                string trailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC17_BuyOrderSize");
                limitPrice = TestData.GetData("TC17_LimitPrice");
                timeInForce = TestData.GetData("TC17_TimeInForce");
                trailingAmount = TestData.GetData("TC17_TrailingAmount");
                pegPriceDropdown = TestData.GetData("TC17_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("TrailingStopMarket");

                type = Const.TrailingStopMarket;
                feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                // Creating Buy and Sell Order to get the last price
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Setting up market with User 8 - Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));

                // Place Trailing Stop Market BuyOrder
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeTrailingStopMarketBuyOrder = advanceOrder.PlaceTrailingStopMarketBuyOrder(orderSize, trailingAmount, pegPriceDropdown);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                trailingPrice = UserCommonFunctions.GetBuyOrderTrailingPrice(limitPrice, trailingAmount);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSize), trailingPrice, placeTrailingStopMarketBuyOrder["PlaceOrderTime"], placeTrailingStopMarketBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint("Verify Trailing Stop Market Buy Order passed successfully.");
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // TC 18 Testing Done
        public void VerifyTrailingStopMarketSellOrder()
        {
            try
            {
                string type;
                string successMsg;
                string askPrice;
                string trailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC18_BuyOrderSize");
                limitPrice = TestData.GetData("TC18_LimitPrice");
                timeInForce = TestData.GetData("TC18_TimeInForce");
                trailingAmount = TestData.GetData("TC18_TrailingAmount");
                pegPriceDropdown = TestData.GetData("TC18_PegPrice");
                orderTypeDropdown = TestData.GetData("TrailingStopMarket");

                type = Const.TrailingStopMarket;

                // Creating Buy and Sell Order to get the last price
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Setting up market with User 8 - Buy Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));

                // Place Trailing Stop Market Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeTrailingStopMarketSellOrder = advanceOrder.PlaceTrailingStopMarketSellOrder(orderSize, trailingAmount, pegPriceDropdown);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                trailingPrice = UserCommonFunctions.GetSellOrderTrailingPrice(limitPrice, trailingAmount);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSize), trailingPrice, placeTrailingStopMarketSellOrder["PlaceOrderTime"], placeTrailingStopMarketSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint("Verify Trailing Stop Market Sell Order passed successfully.");
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderSuccessMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();

            }
        }

        [Fact]
        public void Test()
        {
            instrument = TestData.GetData("Instrument");
            orderType = TestData.GetData("OrderType");
            menuTab = TestData.GetData("MenuTab");
            buyTab = TestData.GetData("BuyTab");
            sellTab = TestData.GetData("SellTab");
            orderSize = TestData.GetData("SetMarketOrderSize");
            limitPrice = TestData.GetData("SetMarketLimitPrice");
            timeInForce = TestData.GetData("TimeInForce");
            UserCommonFunctions userCommonFunctions = new UserCommonFunctions(TestProgressLogger);
            userCommonFunctions.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
        }
    }
}
