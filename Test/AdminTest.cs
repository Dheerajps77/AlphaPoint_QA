using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    public class AdminTest : TestBase
    {
        private readonly ITestOutputHelper output;
        static ILog logger;

        public AdminTest(ITestOutputHelper output) : base(output)
        {
            this.output = output;
            logger = APLogger.GetLog();
        }

        string usernam = "User_2";
        [Fact]
        public void AdminLogin()
        {
            try
            {
                AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        string permission1 = "SubmitBlockTrade";
        string permission2 = "GetOpenTradeReports";
        string userID = "185";

        [Fact]
        public void AddPermission()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {

                objAdminFunctions.AdminLogIn(TestProgressLogger);
                Thread.Sleep(2000);                
                //objAdminCommonFunctions.SelectUserFromUsersList(usernam);
                objAdminCommonFunctions.UserByIDText(userID);
                objAdminCommonFunctions.OpenUserButton();
                objAdminCommonFunctions.UserPermissionButton();
                objAdminCommonFunctions.AddSubmitBlockTradePermissions(permission1);
                objAdminCommonFunctions.ClearTextBox();
                objAdminCommonFunctions.AddGetOpenTradeReportsPermissions(permission2);
                objAdminCommonFunctions.ClosePermissionWindow();
            }
            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }
        [Fact]
        public void AddNewUserFunction()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {

                objAdminFunctions.AdminLogIn(TestProgressLogger);
                Thread.Sleep(2000);
                objAdminCommonFunctions.AddNewUser("Alpha5", "Alpha5@Alpha.com", "1234", "1234");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }

       
        [Fact]
        public void AffiliateTageCreation()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                Thread.Sleep(2000);
                objAdminCommonFunctions.SelectUserFromUsersList(usernam);
                objAdminCommonFunctions.AffiliateTagCreations("2");
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }

        string ticketID = "347";
        [Fact]
        public void ClickOnTicket()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {

                objAdminFunctions.AdminLogIn(TestProgressLogger);
                Thread.Sleep(2000);
                objAdminCommonFunctions.SelectTicketsMenu();
                Thread.Sleep(4000);
                objAdminCommonFunctions.ClickOnTicketFromWithdrawTicketList(ticketID);
                objAdminCommonFunctions.CloseWithdrawTicketWindow();
                Thread.Sleep(3000);
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }

        string depID = "540";
        [Fact]
        public void ClickOnDeposits()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {

                objAdminFunctions.AdminLogIn(TestProgressLogger);
                Thread.Sleep(2000);
                objAdminCommonFunctions.SelectTicketsMenu();
                Thread.Sleep(3000);
                objAdminCommonFunctions.NavigateToDepositTicketsTab();
                Thread.Sleep(3000);
                objAdminCommonFunctions.ClickOnTicketFromDepositTicketList(depID);
                Thread.Sleep(2000);
                objAdminCommonFunctions.ClickOnAcceptButtonFromDepositsTicketModal();
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }

        [Fact]
        public void LoyalityFeeEnabledCheck()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);

            try
            {
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminCommonFunctions.SelectOMSAdminOption();
                Thread.Sleep(2000);
                objAdminCommonFunctions.OMSAdminstrationLoyalityFee();
                Thread.Sleep(2000);
                objAdminCommonFunctions.CloseLoyaltyTokenWindow();
                Thread.Sleep(2000);
                objAdminCommonFunctions.CloseEditOMSWindow();
                Thread.Sleep(2000);
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }


        string badgeNumber = "14";
        string badgeUserName = "User_1";
        string userAccountID = "194";
        [Fact]
        public void BadgeCreationForUser()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.SelectAccountsMenu();
                objAdminCommonFunctions.OpenAccountByIDText(userAccountID);
                objAdminCommonFunctions.OpenAccountBtn();
                objAdminCommonFunctions.OpenAddNewBadgeButtonForUser();
                objAdminCommonFunctions.SubmitCreateAccountBadgeButton();
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserBadgeIDValue(badgeNumber);
                objAdminCommonFunctions.CreateBadgeAccount();
                Thread.Sleep(2000);
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }

        string executionID = "5966";        
        [Fact]
        public void TradeOrderHistory()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.SelectTradeMenu();
                //objAdminCommonFunctions.BlockTradeBtn();
                objAdminCommonFunctions.TradeUserDetails(executionID);
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }

        string counterID = "5966";
        string instrument = "BTCUSD";
        [Fact]
        public void BlockTradeOrderHistory()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.SelectTradeMenu();
                objAdminCommonFunctions.BlockTradeBtn();
                //objAdminCommonFunctions.BlockTradeUserDetails(counterID, instrument);
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }

        string username2 = "sujayg9";
        [Fact]
        public void UserVerificationTest()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.UsersVerificationMenuBtn();
                objAdminCommonFunctions.UserNameTabUnderUserVerification(username2);
                
                //Commenting below code as its related to user config.
                //objAdminCommonFunctions.RejectUserConfig();
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserMenuButton();
                Thread.Sleep(2000);
                objAdminFunctions.AdminLogOut();
            }
        }
    }
}