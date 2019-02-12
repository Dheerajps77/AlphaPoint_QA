using System;
using System.Drawing;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class TradeReportsTest:TestBase
    {

        public TradeReportsTest(ITestOutputHelper output):base(output)
        {

        }

        [Fact]
        public void VerifySingleReportTradeActivities()
        {
            try
            {
                // Create 1 Trade
                // Create 1 Deposits
                // Create 1 Withdraw
                string reportTypeValue = "Trade Activity";
                driver.Navigate().GoToUrl("https://apexwebqa.azurewebsites.net/exchange");
                driver.Manage().Window.Size = new Size(1366, 768);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                //Login as a User say XYZ
                UserFunctions objUserFunctionality = new UserFunctions(TestProgressLogger);
                objUserFunctionality.LogIn(TestProgressLogger);

                // Note the number of affiliate programs
                TradeReportsPage usp = new TradeReportsPage(driver, TestProgressLogger);
                Assert.True(usp.VerifySingleReportData(reportTypeValue, "01/13/2019"));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                //error add to log check point. 
                throw e;
            }
            finally
            {
                //Need to work here.
            }
        }

        [Fact]
        public void VerifyCyclicReportTradeActivities()
        {
            //Need to work further
            TestProgressLogger.LogCheckPoint("" + GenericUtils.GetOnlyDateFromDateString("11/12/2019"));

        }

    }
}
