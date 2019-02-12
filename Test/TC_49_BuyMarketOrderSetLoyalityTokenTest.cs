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

namespace AlphaPoint_QA.Test
{
    public class TC_49_BuyMarketOrderSetLoyalityTokenTest : TestBase
    {
        private readonly ITestOutputHelper output;
        static ILog logger;

        private string userByID;

        public TC_49_BuyMarketOrderSetLoyalityTokenTest(ITestOutputHelper output) : base(output)
        {

            this.output = output;
            logger = APLogger.GetLog();
        }

        [Fact]
        public void VerifyLoyalityFeeOfBuyMarketOrder()
        {
            userByID = TestData.GetData("TC49_UserByID");

            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.UserByIDText(userByID);
                userfuntionality.LogIn(TestProgressLogger, Const.USER6);
                Thread.Sleep(2000);
                UserCommonFunctions.DashBoardMenuButton(driver); 
                UserCommonFunctions.NavigateToUserSetting(driver);

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(e.Message, e);
                throw e;
            }
            finally
            {
                objAdminCommonFunctions.UserMenuButton();
                objAdminFunctions.AdminLogOut();
            }
        }
    }
}
