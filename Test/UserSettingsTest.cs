using System;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class UserSettingsTest:TestBase
    {

        public UserSettingsTest(ITestOutputHelper output):base(output)
        {

        }

        [Fact]
        public void VerifyAffiliateProgram()
        {
            try
            {
                // Admin login
                // Verify Trader has affiliate tag set up inadmin UI
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
                Assert.True(userSettingsPage.VerifyAffiliateProgramFunctionality(driver), Const.AffiliateProgramSuccessMsg);
            }
            catch (Exception e)
            {
                // add snapshot, logger then throw error
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.AffiliateProgramFailureMsg + e);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC44_VerifyCreateAPIKey()
        {
            try
            {
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
                Assert.True((userSettingsPage.SelectAPIKey()), Const.CreateAPIKeyBtnIsPresent);
                Assert.True((userSettingsPage.VerifyAPIKeyCheckboxesArePresent()), Const.APIKeyCheckboxesArePresent);
                Assert.True((userSettingsPage.CreateAndVerifyAPIKey()), Const.APIKeyCreatedSuccessMsg);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.CreateAPIKeyFailed, e);
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
        public void TC46_VerifyDeleteAPIKey()
        {
            try
            {
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER2);
                UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
                Assert.True(userSettingsPage.DeleteAPIKey(driver), Const.DeleteAPIKeySuccessMsg);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.DeleteAPIKeyFailureMsg , e);
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
