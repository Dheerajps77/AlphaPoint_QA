using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace AlphaPoint_QA.Pages
{
    public class ReportBlockTrade
    {
        By boughtTab = By.XPath("//div[@class='report-block-trade-sidepane__tab-container']/div[1]");
        By soldTab = By.XPath("//div[@class='report-block-trade-sidepane__tab-container']/div[2]");
        By instrument = By.XPath("//select[@name='instrument']");
        By counterparty = By.XPath("//input[@data-test='Counterparty:']");
        By lockedIn = By.XPath("//div[@class='report-block-trade-form__checkbox-container']/div/label");
        By productBought = By.XPath("//input[@data-test='Product Bought:']");
        By productSold = By.XPath("//input[@data-test='Product Sold:']");
        By fees = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[contains(@data-test,'Fees')]");
        By Received = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[contains(@data-test,'Received')]");
        By OrderTotal = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[contains(@data-test,'Order Total')]");
        By btcBalance = By.XPath("//div[@class='ap-label-with-text user-balance__lwt-container']//span[contains(@data-test,'BTC Balance')]");
        By usdBalance = By.XPath("//div[@class='ap-label-with-text user-balance__lwt-container']//span[contains(@data-test,'USD Balance')]");
        By submitReport = By.XPath("//button[text()='Submit Report']");

        public void VerifyReportBlockTradeWindow()
        {

        }

        public void VerifyLabelsOnReportBlockTradeWindow()
        {
            // Check and Assert for all labels present on this page
        }

        public void VerifyCounterparty()
        {
            //Verify Error message 'Counterparty not found' is displayed
           

        }

        // used to check the counterparty checkbox
        // TC 33, 34 will be using this
        public bool SelectLockedInCheckbox()
        {
            return false;
        }

        public void VerifyProductBought()
        {
            // Verify Error message 'Product Bought', 'Product Sold' can not be zero is displayed
        }

        public void SubmitReport()
        {

        }

        public void VerifyConfirmBlockTrade()
        {
            //need to verify with previous filed values
        }

        public void VerifyConfirmationMessage()
        {
            //Verify Message 'Your order submitted successfully' should be displayed

        }

        public void VerifyTradeReportInBalances()
        {
            //Go to Balances menu and check 'Trade Report' for any of the instrument product

        }

        public void VerifyTradeReportInTradeMenu()
        {
            //Go to trade menu menu and check 'Trade Report' for any of the instrument product

        }
    }
}
