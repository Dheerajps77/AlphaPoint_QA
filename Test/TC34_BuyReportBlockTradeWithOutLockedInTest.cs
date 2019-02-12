using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    public class TC34_BuyReportBlockTradeWithOutLockedInTest : TestBase
    {
        private readonly ITestOutputHelper output;
        static ILog logger;

        private string instrument;
        private string orderType;
        private string menuTab;
        private string buyTab;
        private string sellTab;
        private string orderSize;
        private string limitPrice;
        private string timeInForce;
        private string counterPartyPrice;
        private string wrongCounterParty;
        private string productBoughtPrice;
        private string productSoldPrice;
        private string blocktradeReportStatus;
        private string userWithPermissions;
        private string userWithBadge;
        private string counterParty;
        private string submitBlockTradePermission;
        private string getOpenTradeReportsPermission;
        private string userByID;
        private string counterPartyAccountID;
        private string badgeIdNumber;
        private string buyerAccountID;
        private string state;


        public TC34_BuyReportBlockTradeWithOutLockedInTest(ITestOutputHelper output) : base(output)
        {
            this.output = output;
            logger = APLogger.GetLog();

        }

        [Fact]
        public void VerifyBuyBlockTradeWithOutLockedInTest()
        {
            instrument = TestData.GetData("Instrument");
            orderType = TestData.GetData("OrderType");
            menuTab = TestData.GetData("MenuTab");
            buyTab = TestData.GetData("BuyTab");
            sellTab = TestData.GetData("SellTab");
            orderSize = TestData.GetData("OrderSize");
            limitPrice = TestData.GetData("LimitPrice");
            timeInForce = TestData.GetData("TimeInForce");
            counterParty = TestData.GetData("TC34_CounterPartyPrice");
            counterPartyPrice = TestData.GetData("TC34_CounterPartyPrice");
            productBoughtPrice = TestData.GetData("TC34_ProductBoughtPrice");
            productSoldPrice = TestData.GetData("TC34_ProductSoldPrice");            
            blocktradeReportStatus = TestData.GetData("TC34_TradeReportStatus");
            userWithBadge = TestData.GetData("TC34_UserWithBadge");
            userWithPermissions = TestData.GetData("TC34_UserWithPermissions");
            submitBlockTradePermission = TestData.GetData("TC34_SubmitBlockTradePermission");
            getOpenTradeReportsPermission = TestData.GetData("TC34_GetOpenTradeReportsPermission");
            userByID = TestData.GetData("TC34_UserByID");
            counterPartyAccountID = TestData.GetData("TC34_CounterPartyAccountID");
            buyerAccountID = TestData.GetData("TC34_BuyerAccountID");
            badgeIdNumber = TestData.GetData("TC34_BadgeNumber");           

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            ReportBlockTradePage objReportBlockTradePage = new ReportBlockTradePage(TestProgressLogger, output);
            UserFunctions objUserFunctions = new UserFunctions(TestProgressLogger);
            

            try
            {
                TestProgressLogger.StartTest();
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.UserByIDText(userByID);
                objAdminCommonFunctions.OpenUserButton();
                objAdminCommonFunctions.UserPermissionButton();
                objAdminCommonFunctions.AddSubmitBlockTradePermissions(submitBlockTradePermission);
                objAdminCommonFunctions.ClearTextBox();
                objAdminCommonFunctions.AddGetOpenTradeReportsPermissions(getOpenTradeReportsPermission);
                objAdminCommonFunctions.ClosePermissionWindow();
                Thread.Sleep(2000);
                objAdminCommonFunctions.SelectAccountsMenu();
                objAdminCommonFunctions.OpenAccountByIDText(counterPartyAccountID);
                objAdminCommonFunctions.OpenAccountBtn();
                objAdminCommonFunctions.OpenAddNewBadgeButtonForUser();
                objAdminCommonFunctions.SubmitCreateAccountBadgeButton();
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserBadgeIDValue(badgeIdNumber);
                objAdminCommonFunctions.CreateBadgeAccount();
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                objAdminFunctions.AdminLogOut();
                userfuntionality.LogIn(TestProgressLogger, Const.USER6);
                Thread.Sleep(2000);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Thread.Sleep(3000);
                UserCommonFunctions.ScrollingDownVertical(driver);
                objReportBlockTradePage.ReportBlockTradeButton();
                objReportBlockTradePage.VerifyReportBlockTradeWindow();
                objReportBlockTradePage.VerifyDropdownInstrument();
                objReportBlockTradePage.VerifyCounterParty();               
                objReportBlockTradePage.VerifyProductBought();
                objReportBlockTradePage.VerifyProductSold();
                objReportBlockTradePage.VerifyFees();
                objReportBlockTradePage.VerifyBalances();
                objReportBlockTradePage.SubmitBlockTradeReport(instrument, counterPartyPrice, productBoughtPrice, productSoldPrice, counterParty, buyTab, blocktradeReportStatus);
                var otherPartyBlockTradeData = objReportBlockTradePage.SubmitBuyTradeReport(instrument, buyTab, counterPartyPrice, productBoughtPrice, productSoldPrice, blocktradeReportStatus);
                objUserFunctions.LogOut();
                userfuntionality.LogIn(TestProgressLogger, Const.USER5);
                Thread.Sleep(2000);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Thread.Sleep(3000);
                UserCommonFunctions.ScrollingDownVertical(driver);
                objReportBlockTradePage.SubmitBlockTradeReportForAnotherUser(counterPartyPrice, productBoughtPrice, productSoldPrice);
                var otherPartyBlockTradeData1 = objReportBlockTradePage.SubmitBuyTradeReport(instrument, buyTab, counterPartyPrice, productBoughtPrice, productSoldPrice, blocktradeReportStatus);
                objUserFunctions.LogOut();
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objReportBlockTradePage.VerifyBlockTradeInAdmin(buyerAccountID, counterPartyAccountID, instrument, productBoughtPrice, productBoughtPrice);
                objAdminCommonFunctions.UserMenuButton();
                objAdminFunctions.AdminLogOut();
                TestProgressLogger.EndTest();
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error("", e);
                throw e;
            }
        }
    }
}
