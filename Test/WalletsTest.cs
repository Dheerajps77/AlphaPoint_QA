using System;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{

    [Collection("Alphapoint_QA_USER")]
    public class WalletsTest : TestBase
    {

        private string instrument;
        private string currencyName;
        private string comment;
        private string amountOfBtcToSend;
        private string withdrawStatus;
        private string user12_EmailAddress;
        private string user13_EmailAddress;

        private string amountOfUSDToWithdraw;
        private string fullName;
        private string language;
        private string bankAddress;
        private string bankAccountNumber;
        private string bankName;
        private string swiftCode;


        public WalletsTest(ITestOutputHelper output) : base(output)
        {

        }


        [Fact]
        public void TC36_SendExternalWallets()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER7));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                string currentBalanceOfUser2 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                walletpage.ClickOnInstrumentReceiveButton(driver, currencyName);
                walletpage.CopyAddressToReceiveBTC(driver);
                string successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.CopyAddressSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.ExternalAddressCopied));
                walletpage.CloseSendOrReciveSection(driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER7));

                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER1));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                string currentBalanceOfUser3 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailableTotalBalanceOnDetailsPage(driver);
                string holdBalance = walletpage.HoldBalanceDetailsPage;
                string availableBalance = walletpage.AvailableBalanceDetailsPage;
                walletpage.ClickSendButtonOnDetailsPage(driver);
                walletpage.SendBitCoinExternalWallet(driver, comment, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SendBitCoinSuccessfully, amountOfBtcToSend));
                string btcAmount = walletpage.GetBtcAmountOnConfirmation(driver);
                string minerFees = walletpage.GetMinerFeesOnConfirmation(driver);
                string btcTotlaAmount = GenericUtils.GetDifferenceFromStringAfterAddition(btcAmount, minerFees);
                walletpage.ClickConfirmButton(driver);
                string withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.WithdrawSuccessMsg, withdrawSuccessMsg);
                string increasedHoldAmount = GenericUtils.GetDifferenceFromStringAfterAddition(holdBalance, btcTotlaAmount);
                walletpage.GetHoldAvailableTotalBalanceOnDetailsPage(driver);
                string incresedHoldBalance = walletpage.HoldBalanceDetailsPage;
                string TotalBalance = walletpage.TotalBalanceDetailsPage;
                string reducedAvailableBalance = walletpage.AvailableBalanceDetailsPage;
                String hold = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(increasedHoldAmount));
                Assert.Equal(hold, incresedHoldBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldAmountIncreasedSuccessfully);

                string expectedReducedAvailableBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(availableBalance, btcTotlaAmount);
                Assert.Equal(expectedReducedAvailableBalance, reducedAvailableBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.AvailableAmountReducedSuccessfully);
                string statusID = walletpage.GetStatusID(driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER1));

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.VerifyStatus(driver, statusID, withdrawStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreatedTicketStatusVerified, statusID));
                TestProgressLogger.LogCheckPoint(LogMessage.TC36_SendExternalWalletsTestPassed);
                admincommonfunctions.UserMenuButton();
                adminfunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(LogMessage.TC36_AdminUserLogoutSuccessfully);
                TestProgressLogger.EndTest();
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.TC36_SendExternalWallets, e);
                throw e;
            }
        }


        [Fact]
        public void TC37_WalletsSendToEmailAddress()
        {
            try
            {
                instrument = TestData.GetData("TC40_FiatCurrency");
                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                user12_EmailAddress = TestData.GetData("User_12EmailAddress");
                user13_EmailAddress = TestData.GetData("User_13EmailAddress");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                string username = userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                string currentBalanceOfUser7 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));

                userFunctions.LogIn(TestProgressLogger, Const.USER13);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER13));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                string currentBalanceOfUser6 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                walletpage.ClickOnInstrumentSendButton(driver, currencyName);
                walletpage.ClickOnEmailAddressTab(driver);
                walletpage.SendBitCoinToEmailAddress(driver, comment, user12_EmailAddress, amountOfBtcToSend);
                walletpage.VerifySendDetailsBalances(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.TC37_RemainingBalanceVerified);
                walletpage.ClickOnSendBitCoin(driver);
                walletpage.VerifyConfirmationModal(driver, user12_EmailAddress, amountOfBtcToSend);
                walletpage.ClickConfirmButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.TC37_ConfirmationModalVerified);
                string withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.TransferSuccessMsg, withdrawSuccessMsg);
                walletpage.CloseSendOrReciveSection(driver);

                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.ClickRefreshTransfers(driver);
                walletpage.VerifyAmountInTransferSection(driver, username, amountOfBtcToSend);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER13));
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                string updatedCurrentBalanceOfUser5 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                string expectedupdateBalance = GenericUtils.GetDifferenceFromStringAfterAddition(currentBalanceOfUser7, amountOfBtcToSend);
                Assert.Equal(expectedupdateBalance, updatedCurrentBalanceOfUser5.Replace(@",", string.Empty));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.TC37_BalanceUpdatedSuccessfully, Const.USER12));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));
                TestProgressLogger.EndTest();
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.TC37_WalletsSendToEmailAddressTestFailed, e);
                throw e;
            }
        }

        [Fact]
        public void TC39_WalletsReceiveRequestbyEmail()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                user12_EmailAddress = TestData.GetData("User_12EmailAddress");
                user13_EmailAddress = TestData.GetData("User_13EmailAddress");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                string username = userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                string currentBalanceOfUser7 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));

                userFunctions.LogIn(TestProgressLogger, Const.USER13);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER13));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                string currentBalanceOfUser6 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                //walletpage.ClickOnInstrumentReceiveButton(driver, currencyName);
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailableTotalBalanceOnDetailsPage(driver);
                string availablebalance = walletpage.AvailableBalanceDetailsPage;
                string totalbalance = walletpage.AvailableBalanceDetailsPage;
                walletpage.ClickReceiveButtonOnDetailsPage(driver);
                walletpage.ClickOnReceiveRequestByEmail(driver);
                walletpage.SendBitCoinRequestByEmail(driver, comment, user12_EmailAddress, amountOfBtcToSend);
                walletpage.ClickOnSendBitCoin(driver);
                walletpage.VerifyConfirmationModal(driver, user12_EmailAddress, amountOfBtcToSend);
                walletpage.ClickConfirmButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.TC37_ConfirmationModalVerified);
                string withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.RequestTransferSuccessMsg, withdrawSuccessMsg);
                walletpage.CloseSendOrReciveSection(driver);
                walletpage.GetHoldAvailableTotalBalanceOnDetailsPage(driver);
                Assert.Equal(availablebalance, walletpage.AvailableBalanceDetailsPage);
                TestProgressLogger.LogCheckPoint(LogMessage.TC39_VerifiedAvailableBalance);
                Assert.Equal(totalbalance, walletpage.AvailableBalanceDetailsPage);
                TestProgressLogger.LogCheckPoint(LogMessage.TC39_VerifiedTotalBalance);
                walletpage.ClickRefreshTransfers(driver);
                walletpage.SelectSentRequests(driver);
                walletpage.VerifyAmountInTransferSentRequestsSection(driver, username, amountOfBtcToSend);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER13));
                string user = userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));
                walletpage.ClickApproveButton(driver);
                Assert.False(walletpage.VerifyApproveButton(driver));
                Assert.False(walletpage.VerifyRejectButton(driver));
                TestProgressLogger.LogCheckPoint(LogMessage.TC39_VerifiedApproveAndRejectButton);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                string updatedCurrentBalanceOfUser5 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                string expectedupdateBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(currentBalanceOfUser7, amountOfBtcToSend);
                Assert.Equal(expectedupdateBalance, updatedCurrentBalanceOfUser5.Replace(@",", string.Empty));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.TC39_BalanceReducedSuccessfully, Const.USER12));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));

                userFunctions.LogIn(TestProgressLogger, Const.USER13);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER13));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                string currentBalance = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                string expupdateBalance = GenericUtils.GetDifferenceFromStringAfterAddition(currentBalanceOfUser6, amountOfBtcToSend);
                Assert.Equal(expupdateBalance, currentBalance.Replace(@",", string.Empty));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.TC39_BalanceIncreasedSuccessfully, Const.USER13));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER13));
                TestProgressLogger.EndTest();
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.TC39_WalletsSendToEmailAddress, e);
                throw e;
            }
        }


        [Fact]
        public void TC40_WalletsDepositFiatcurrency()
        {
            try
            {
                currencyName = TestData.GetData("USDCurrency");
                comment = TestData.GetData("TC40_Comment");
                amountOfUSDToWithdraw = TestData.GetData("TC40_USDAmount");
                fullName = TestData.GetData("TC40_FullName");
                language = TestData.GetData("TC40_Language");
                bankAddress = TestData.GetData("TC40_BankAddress");
                bankAccountNumber = TestData.GetData("TC40_BankAccountNumber");
                bankName = TestData.GetData("TC40_BankName");
                swiftCode = TestData.GetData("TC40_SwiftCode");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER7));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                string currentBalanceOfUser2 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);

                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailableTotalBalanceOnDetailsPage(driver);
                string holdBalance = walletpage.HoldBalanceDetailsPage;
                string availableBalance = walletpage.AvailableBalanceDetailsPage;
                walletpage.ClickWithdrawButtonOnDetails(driver);

                walletpage.WithdrawUSD(driver, amountOfUSDToWithdraw, fullName, language, comment, bankAddress, bankAccountNumber, bankName, swiftCode);
                string amounttowithdraw = walletpage.GetAmountToWithdraw(driver);
                string currentusdbalance = walletpage.GetCurrentUSDBalance(driver);
                string fee = walletpage.GetFee(driver);
                string remaingbalance = walletpage.GetRemainingBalance(driver);
                walletpage.ClickOnWithdrawUSDButton(driver);


                /*
                walletpage.ClickConfirmButton(driver);
                string withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.WithdrawSuccessMsg, withdrawSuccessMsg);
                string increasedHoldAmount = GenericUtils.GetDifferenceFromStringAfterAddition(holdBalance, btcTotlaAmount);
                walletpage.GetHoldAvailableTotalBalanceOnDetailsPage(driver);
                string incresedHoldBalance = walletpage.HoldBalanceDetailsPage;
                string TotalBalance = walletpage.TotalBalanceDetailsPage;
                string reducedAvailableBalance = walletpage.AvailableBalanceDetailsPage;
                String hold = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(increasedHoldAmount));
                Assert.Equal(hold, incresedHoldBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldAmountIncreasedSuccessfully);

                string expectedReducedAvailableBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(availableBalance, btcTotlaAmount);
                Assert.Equal(expectedReducedAvailableBalance, reducedAvailableBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.AvailableAmountReducedSuccessfully);
                string statusID = walletpage.GetStatusID(driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER13));

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.VerifyStatus(driver, statusID, withdrawStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreatedTicketStatusVerified, statusID));
                TestProgressLogger.LogCheckPoint(LogMessage.TC36_SendExternalWalletsTestPassed);
                admincommonfunctions.UserMenuButton();
                adminfunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(LogMessage.TC36_AdminUserLogoutSuccessfully);
                TestProgressLogger.EndTest();*/
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(Const.TC36_SendExternalWallets, e);
                throw e;
            }
        }
    }
}