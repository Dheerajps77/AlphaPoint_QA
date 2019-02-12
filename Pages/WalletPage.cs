using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    public class WalletPage
    {

        public string AvailableBalanceDetailsPage;
        public string HoldBalanceDetailsPage;
        public string TotalBalanceDetailsPage;
        public string AmountToBeDeducted;



        By sendTab = By.XPath("//section[@class='send-receive__main-form']/header//div[contains(@class,'send')]//span[text()='Send']");
        By receiveTab = By.XPath("//section[@class='send-receive__main-form']/header//div[contains(@class,'send')]//span[text()='Receive']");
        By requestByEmailTab = By.CssSelector("section.receive > header > div:nth-of-type(2)");

        By externalWalletTab = By.XPath("//section[@class='send-form__send-to']/header/div[1]");
        By toEmailAddressTab = By.XPath("//section[@class='send-form__send-to']/header/div[2]");
        By comment = By.CssSelector("div.ap-input__input-box.send-address__input-box > input[name=Comment]");
        By externalAddress = By.CssSelector("div.ap-input__input-box.send-address__input-box > input[name=ExternalAddress]");
        By amountOfBitCoin = By.CssSelector("div.ap-input__input-box.send-form__input-box > input[name=Amount]");
        By amountOfBitCoinRequest = By.XPath("//section[@class='receive-form__amounts']//input[@name='Amount']");
        By sendBitCoin = By.CssSelector("button[type=submit]");
        By recipientsEmailAddress = By.XPath("//div[@class='ap-input__input-box send-form__input-box']/input[@name='ReceiverUsername']");
        By amountOfBitcoinToSend = By.XPath("//div[@class='ap-input__input-box send-form__input-box']/input[@name='Amount']");
        // By addNoteOfSend = By.XPath("//div[@class='ap-input__input-box send-form__input-box']/textarea");
        By addNoteOfSend = By.XPath("//textarea[@name='Notes']");
        By emailAddressToRequestForm = By.XPath("//input[@name='ReceiverUsername']");
        By amountOfBitcoinToRequest = By.XPath("//div[@class='ap-input__input-box receive-form__input-box']//input[@name='Amount']");
        By addNoteOfRec = By.XPath("//div[@class='ap-input__input-box receive-form__input-box']/textarea");
        By copyLinkIcon = By.XPath("//span[@class='isvg loaded ap-icon ap-icon--copy receive-address__copy-icon receive-address__copy-icon--copy']");
        By addressLink = By.CssSelector("section.receive-address div:nth-of-type(2) > span.receive-address__address");
        By closeIcon = By.CssSelector("div.ap-sidepane__close-button.retail-sidepane-with-details__close-button > span");

        By btcAmount = By.XPath("//span[@data-test='BTC Amount']");
        By minerFees = By.XPath("//span[@data-test='Miner Fees']");
        By btcTotalAmount = By.XPath("//div[@class='ap-label-with-text send-receive-confirm-modal__lwt-container']//span[@data-test='BTC Total Amount']");
        By externalAdd = By.XPath("//span[@data-test='External Address']");
        By confirmButton = By.CssSelector("div.ap-modal__footer.send-receive-confirm-modal__footer > button");
        By sendIconOnDetailsPage = By.CssSelector("div[tooltip=Send]");
        By receiveIconOnDetailsPage = By.CssSelector("div[tooltip=Receive]");
        By holdBalanceOnDetailsPage = By.CssSelector("div.wallet-details > div:nth-of-type(2) > div:nth-of-type(2) > div:nth-of-type(2)");
        By availableBalanceOnDetailsPage = By.CssSelector("div.wallet-details > div:nth-of-type(2) > div:nth-of-type(1) > div:nth-of-type(2)");
        By totalBalanceOnDetailsPage = By.CssSelector("div.wallet-details > div:nth-of-type(2) > div:nth-of-type(4) > div:nth-of-type(2)");

        By statusIdOnDetailsPage = By.CssSelector("div.activity__status-id");
        By amountToSend = By.XPath("//span[@data-test='Amount to Send']");
        By yourCurrentBtcBalance = By.XPath("//span[@data-test='Your current BTC Balance']");
        By remainingBalance = By.XPath("//span[@data-test='Remaining Balance']");
        By btcAmountOnConfirm = By.XPath("//span[@data-test='BTC Amount']");
        By recipientsEmail = By.XPath("//span[@data-test='Recipient’s Email']");
        By requesteesEmail = By.XPath("//span[@data-test='Requestee’s Email']");
        By refreshTransfer = By.CssSelector("button.ap-inline-btn__btn.ap-inline-btn__btn--general.transfers__refresh-transfers__btn.transfers__refresh-transfers__btn--general");
        By sentRequestTab = By.XPath("//label[@data-test='Sent Requests']");
        By approveButton = By.XPath("//button[text()='Approve']");
        By rejectButton = By.XPath("//button[text()='Reject']");

        By amountOfUsdToWithdraw = By.XPath("//input[@data-test='Amount of USD to Withdraw']");
        By fullName = By.XPath("//input[@data-test='Full Name']");
        By language = By.XPath("//input[@data-test='Language']");
        By withdrawComment = By.CssSelector("input[name=Comment]");
        By bankAddress = By.XPath("//input[@name='BankAddress']");
        By bankAccountNumber = By.CssSelector("input[name=BankAccountNumber]");
        By bankName = By.CssSelector("input[name=BankAccountName]");
        By swiftCode = By.CssSelector("input[name=SwiftCode]");
        By withdrawUSD = By.CssSelector("button[type=submit]");
        By withdrawButtonOnDetails = By.CssSelector("div[tooltip=Withdraw]");
        By depositButtonOnDetails = By.CssSelector("div[tooltip=Deposit]");

        By currentUsdBalance = By.XPath("//span[@data-test='Your current USD Balance']");
        By amountToWithdraw = By.XPath("//span[@data-test='Amount to Withdraw']");
        By fee = By.XPath("//span[@data-test='Fee']");
      















        public void ClickOnInstrumentSendButton(IWebDriver driver, string instrumentname)
        {
            try
            {
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"));
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement sendicon = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[3]/div[2]/div/span"));
                        UserSetFunctions.Click(sendicon);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        
        public string GetStatusID(IWebDriver driver)
        {
           return driver.FindElement(statusIdOnDetailsPage).Text;
        }

        public string GetInstrumentCurrentBalance(IWebDriver driver, string instrumentname)
        {
            string currentbalance = null;
            try
            {
                Thread.Sleep(4000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"));
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement sendicon = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[2]/div/div/span"));
                        currentbalance=sendicon.Text;
                        break;

                    }
                }
                return currentbalance;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public string ClickInstrumentDetails(IWebDriver driver, string instrumentname)
        {
            string currentbalance = null;
            try
            {
                Thread.Sleep(2000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"));
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement details = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[3]/div/a"));
                        details.Click();
                        break;
                    }
                }
                return currentbalance;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public void SendBitCoinExternalWallet(IWebDriver driver,string commenttext,string amtbitcoin)
        {
            try
            {
                driver.FindElement(comment).SendKeys(commenttext);
                driver.FindElement(externalAddress).SendKeys(OpenQA.Selenium.Keys.Control + "v");
                driver.FindElement(amountOfBitCoin).SendKeys(amtbitcoin);
                driver.FindElement(sendBitCoin).Click();
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SendBitCoinToEmailAddress(IWebDriver driver, string commenttext,string emailAddress, string amtbitcoin)
        {
            try
            {
                driver.FindElement(emailAddressToRequestForm).SendKeys(emailAddress);
                driver.FindElement(amountOfBitCoin).SendKeys(amtbitcoin);
                driver.FindElement(addNoteOfSend).SendKeys(commenttext);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void WithdrawUSD(IWebDriver driver,string usd,string fullname,string languag,string comments,string bankaddres,string bankaccountNum,string bankname,string swiftcode)
        {
            try
            {
                driver.FindElement(amountOfUsdToWithdraw).SendKeys(usd);
                driver.FindElement(fullName).SendKeys(fullname);
                driver.FindElement(language).SendKeys(languag);
                driver.FindElement(withdrawComment).SendKeys(comments);
                driver.FindElement(bankAddress).SendKeys(bankaddres);
                driver.FindElement(bankAccountNumber).SendKeys(bankaccountNum);
                driver.FindElement(bankName).SendKeys(bankname);
                driver.FindElement(swiftCode).SendKeys(swiftcode);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SendBitCoinRequestByEmail(IWebDriver driver, string commenttext, string emailAddress, string amtbitcoin)
        {
            try
            {
                driver.FindElement(emailAddressToRequestForm).SendKeys(emailAddress);
                driver.FindElement(amountOfBitCoinRequest).SendKeys(amtbitcoin);
                driver.FindElement(addNoteOfSend).SendKeys(commenttext);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public void VerifySendDetailsBalances(IWebDriver driver)
        {
            try
            {
            string currentbalance= driver.FindElement(yourCurrentBtcBalance).Text.Split(" ")[0];
            string amounttosend = driver.FindElement(amountToSend).Text.Split(" ")[0];
            string remaingbalance = driver.FindElement(remainingBalance).Text.Split(" ")[0];
            string expectedremaingbalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(currentbalance, amounttosend);
            Assert.Equal(expectedremaingbalance, remaingbalance.Replace(@",", string.Empty));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void VerifyConfirmationModal(IWebDriver driver,string email,string sendBtcAmount)
        {
            try
            {
                string btcAmount = driver.FindElement(btcAmountOnConfirm).Text.Split(" ")[0];
                string expectedEmail = driver.FindElement(requesteesEmail).Text;
                Assert.Equal(expectedEmail, email);
                string expectedBtc=GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(sendBtcAmount));
                Assert.Equal(expectedBtc, btcAmount);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string VerifyAvailableBalanceOnDetailsPage(string holdAmt,string totalBalance)
        {
            double hold = GenericUtils.ConvertStringToDouble(holdAmt);
            double btctotal = GenericUtils.ConvertStringToDouble(totalBalance);
            double availableBalance = btctotal - hold;
            return Convert.ToString(availableBalance);
        }

        public void GetHoldAvailableTotalBalanceOnDetailsPage(IWebDriver driver)
        {
            Thread.Sleep(20000);
            HoldBalanceDetailsPage =driver.FindElement(holdBalanceOnDetailsPage).Text;
            AvailableBalanceDetailsPage=driver.FindElement(availableBalanceOnDetailsPage).Text;
            TotalBalanceDetailsPage = driver.FindElement(totalBalanceOnDetailsPage).Text;
        }

        public string IncreseHoldOnDetailsPage(string holdAmt,string btcTotalAmount)
        {
           double hold= GenericUtils.ConvertStringToDouble(holdAmt);
           double btctotal = GenericUtils.ConvertStringToDouble(btcTotalAmount);
           double holdwithbtctotal = hold + btctotal;
           return Convert.ToString(holdwithbtctotal);
        }

        public string ReducedAvailableBalanceOnDetailsPage(string holdAmt, string btcTotlaAmount)
        {
            double hold = GenericUtils.ConvertStringToDouble(holdAmt);
            double btctotal = GenericUtils.ConvertStringToDouble(btcTotlaAmount);
            double holdwithbtctotal = hold + btctotal;
            return Convert.ToString(holdwithbtctotal);

        }

        public void ClickApproveButton(IWebDriver driver)
        {
            IWebElement we = GenericUtils.WaitForElementPresence(driver, approveButton, 15);
            we.Click();
        }

        public bool VerifyApproveButton(IWebDriver driver)
        {
            bool val;
            try
            {
                Thread.Sleep(1000);
                driver.FindElement(approveButton);
                val=true;
                return val;
            }
            catch (NoSuchElementException )
            {
               val = false;
                return val;
            }
        }

        public bool VerifyRejectButton(IWebDriver driver)
        {
            bool val;
            try
            {
                Thread.Sleep(1000);
                driver.FindElement(rejectButton);
                val = true;
                return val;
            }
            catch (NoSuchElementException)
            {
                val = false;
                return val;
            }
        }
        public void ClickOnSendBitCoin(IWebDriver driver)
        {
            driver.FindElement(sendBitCoin).Click();
        }


        public void ClickConfirmButton(IWebDriver driver)
        {
            driver.FindElement(confirmButton).Click();
        }

        public void ClickSendButtonOnDetailsPage(IWebDriver driver)
        {
            driver.FindElement(sendIconOnDetailsPage).Click();
        }

        public void ClickReceiveButtonOnDetailsPage(IWebDriver driver)
        {
            driver.FindElement(receiveIconOnDetailsPage).Click();
        }

        public string GetBtcAmountOnConfirmation(IWebDriver driver)
        {
            string btcAmtWithCurrency=driver.FindElement(btcAmount).Text;
            return btcAmtWithCurrency.Split()[0];
        }

        public string GetMinerFeesOnConfirmation(IWebDriver driver)
        {
            string minerFeesWithCurrency = driver.FindElement(minerFees).Text;
            return minerFeesWithCurrency.Split()[0];
        }

        public string GetBtcTotalAmountOnConfirmation(IWebDriver driver)
        {
            string btcTotalAmountWithCurrency = driver.FindElement(btcTotalAmount).Text;
            return btcTotalAmountWithCurrency.Split()[0];
        }

        public string GetExternalAddressOnConfirmation(IWebDriver driver)
        {
            string externalAddress = driver.FindElement(externalAdd).Text;
            return externalAddress;
        }

        public string GetCurrentUSDBalance(IWebDriver driver)
        {
            string currentusdbalance = driver.FindElement(currentUsdBalance).Text.Split()[0];
            return currentusdbalance;
        }

        public string GetFee(IWebDriver driver)
        {
            string fees = driver.FindElement(fee).Text.Split()[0];
            return fees;
        }

        public string GetRemainingBalance(IWebDriver driver)
        {
            string remainingbalance = driver.FindElement(remainingBalance).Text.Split()[0];
            return remainingbalance;
        }

        public string GetAmountToWithdraw(IWebDriver driver)
        {
            string amounttowithdraw = driver.FindElement(amountToWithdraw).Text.Split()[0];
            return amounttowithdraw;
        }

        public void ClickRefreshTransfers(IWebDriver driver)
        {
            IWebElement we = GenericUtils.WaitForElementPresence(driver, refreshTransfer, 15);
            we.Click();
        }


        public void VerifyAmountInTransferSection(IWebDriver driver,string username,string recivedamount)
        {
            string actualrecivedamt = null;
            try
            {
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='flex-table__body transfers__body']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[1]/div/div[2]"));
                    string instrument = div.Text;
                    if (instrument.Contains(username))
                    {
                        IWebElement amount = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[2]/div/div[1]"));
                        actualrecivedamt=amount.Text;
                        break;
                    }
                }
                string expectedrecivedamount=GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(recivedamount));
                Assert.Equal(expectedrecivedamount, actualrecivedamt);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void VerifyAmountInTransferSentRequestsSection(IWebDriver driver, string username, string recivedamount)
        {
            string actualrecivedamt = null;
            try
            {
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='flex-table__body transfers__body']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[1]/div/div[2]"));
                    string instrument = div.Text;
                    if (instrument.Contains(username))
                    {
                        IWebElement amount = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[2]/div/div[1]"));
                        actualrecivedamt = amount.Text;
                        break;
                    }
                }
                string expectedrecivedamount = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(recivedamount));
                Assert.Equal(expectedrecivedamount, actualrecivedamt);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void ClickOnInstrumentReceiveButton(IWebDriver driver, string instrumentname)
        {
            try
            {
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"));
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement sendicon = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[3]/div[2]/div[2]/span"));
                        UserSetFunctions.Click(sendicon);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SelectSentRequests(IWebDriver driver)
        {
            driver.FindElement(sentRequestTab).Click();
        }

        public void ClickOnWithdrawUSDButton(IWebDriver driver)
        {
            driver.FindElement(withdrawUSD).Click();
        }

        public void ClickWithdrawButtonOnDetails(IWebDriver driver)
        {
            driver.FindElement(withdrawButtonOnDetails).Click();
        }









        public void SelectSendFundedAccount(string currency)
        {

        }

        public void SendCryptoWidgetIsPresent( )
        {
            //Verify Widget for sending Crypto is displayed
        }

        public void VerifyExternalWalletOptionIsPresent()
        {
            //Verify External Wallet option is present
        }

        public void VerifyEmailAddressOptionIsPresent()
        {
            //Verify To Email Address option is present
        }

        public void VerifyExternalWalletOption( )
        {
            //Select External Wallet option
        }
        public void EnterExternalAddress( )
        {
            //Enter External Address
        }

        public void EnterBitcoinAmount( )
        {
            //Enter Amount of Bitcoin to Send
        }


        public void VerifySendDetailsBalances(string amount)
        {
            //Verify To Email Address option is present
            // check calc
        }

        

        public void ClickOnEmailAddressTab(IWebDriver driver)
        {
            IWebElement we= GenericUtils.WaitForElementPresence(driver, toEmailAddressTab,15);
            we.Click();
        }

        public void ClickOnReceiveRequestByEmail(IWebDriver driver)
        {
            IWebElement we = GenericUtils.WaitForElementPresence(driver, requestByEmailTab, 15);
            we.Click();
        }

        public void ClickOnConfirmSendBTCBitcoinModalButton()
        {
            //ClickOnConfirmSendBTCBitcoinModalButton
        }

        public void VerifySendBTCBitcoinModal(string amount)
        {
            //Verify if this can be customised with other currency
            // Verify all the details inside the modal
            ClickOnConfirmSendBTCBitcoinModalButton();
        }

        public void VerifySendBTCBitcoinSuccesMessage()
        {

        }

        public void EnterEmailAddress()
        {
           //Enter Recipient Email Address
        }


        public void EnterBitcoinAmountForEmail()
        {
            //Enter Amount of Bitcoin to Send
        }

        public void VerifyCurrentBTCBalance()
        {
            //Verify Current BTC balance

        }

        public void VerifyAmountToSend()
        {
            //Verify Amount to send
        }

        public void VerifyRemainingBTCBalance()
        {
            //Verify Remaining balance
        }

        public void ClickOnConfirmSendBTCEmailModalButton()
        {
            //ClickOnConfirmSendBTCBitcoinModalButton
        }

        public void VerifySendBTCEmailModal(string amount)
        {
            //Verify if this can be customised with other currency
            // Verify all the details inside the modal
            ClickOnConfirmSendBTCBitcoinModalButton();
        }

        public void VerifySendBTCEmailSuccesMessage()
        {

        }


        /// <summary>
        /// Details
        /// </summary>


        public void ClickOnDetailsBitcoinButton()
        {
            //ClickOnDetailsBitcoinButton
        }


        public void VerifyAmountDetails()
        {

        }

        public void VerifyPendingAmount()
        {

        }

        public void VerifyAvailableBalance(string userName)
        {

        }

        public void VerifyTotalBalance(string userName)
        {

        }

       

        public void SelectSentTransfers()
        {

        }

        public void VerifySentTransfers(string userEmail)
        {

        }

        public void SelectReceivedTransfers(IWebDriver driver)
        {
            driver.FindElement(receiveTab).Click();
        }

        public void VerifyReceivedTransfers(string userEmail)
        {

        }

        
      

        public void SelectReceivedRequests()
        {

        }

        public void VerifyReceivedRequests(string userEmail)
        {

        }

        public void SelectAcceptReceivedRequests(string userEmail)
        {

        }

        public void SelectRejectReceivedRequests(string userEmail)
        {

        }

        public void VerifyAcceptReceivedRequests(string userEmail)
        {

        }

        public void VerifyRejectReceivedRequests(string userEmail)
        {

        }



        /// <summary>
        /// Receive
        /// </summary>

       


        public void VerifyReceiveCryptoWidget()
        {

        }

        public void VerifyReceiveWalletAddress(string userName)
        {

        }

        
        public void CloseSendOrReciveSection(IWebDriver driver)
        {
            driver.FindElement(closeIcon).Click();
        }

        public void CopyAddressToReceiveBTC(IWebDriver driver)
        {
            driver.FindElement(copyLinkIcon).Click();
        }

        public void VerifyCopiedAddressToReceiveBTC()
        {

        }

        public void EnterRequestEmailAddress()
        {

        }

        public void EnterRequestAmount()
        {

        }

        public void VerifyInstructionsOnReceiveModal()
        {
            //Verify Instructions on the modal on the right
        }
        public void VerifyReceiveRequestByEmailModal(string userName)
        {

        }

        public void ConfirmReceiveRequestByEmailModal(string userName)
        {

        }

        public void VerifyReceiveRequestByEmailSuccessMessage(string userName)
        {

        }

        /// <summary>
        /// USD
        /// </summary>

        
        public void VerifyWithdrawUSDWidgetIsPresent( )
        {
            // verify the widget
        }

        public void VerifyWithdrawUSDWidgetElements()
        {
            //"Verify below fields are present: name, bank address, bank account, swift code"
        }

        public ArrayList EnterWithdrawUSDWidgetDetails()
        {
            //"Verify below fields are present: name, bank address, bank account, swift code"
            return new ArrayList();
        }

        public void ClickOnSendUSDButton()
        {
            
        }

        public void VerifyWithdrawUSDInstructionsModal()
        {

        }

        public void VerifyWithdrawUSDConfirmationModal()
        {

        }

        public void ClickOnConfirmUSDModalButton()
        {

        }

        public void VerifyWithdrawUSDSuccessMessage()
        {

        }

        public void ClickOnUSDDetailsButton()
        {

        }

        /////////////////////////////////////////
        ///

        public void SelectDepositUSDButton()
        {

        }

        public void VerifyDepositUSDWidgetIsPresent()
        {
            // verify the widget
        }

        public void VerifyDepositUSDWidgetElements()
        {
            //"Verify below fields are present: name, bank address, bank account, swift code"
        }

        public ArrayList EnterDepositUSDWidgetDetails()
        {
            //"Verify below fields are present: name, bank address, bank account, swift code"
            return new ArrayList();
        }

        public void ClickOnPlaceDepositTicketButton()
        {

        }

        
        public void VerifyDepositUSDConfirmationModal()
        {

        }

        public void ClickOnConfirmDepositUSDModalButton()
        {

        }

        public void VerifyDepositUSDSuccessMessage()
        {

        }

        public void ClickOnDepositUSDSuccessButton()
        {
            // this is displayed after success msg/ not metioned in TC
        }

    }
}
