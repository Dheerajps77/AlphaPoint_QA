using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    class VerifyOrdersTab
    {
        IWebDriver driver;

        ProgressLogger logger;

        public VerifyOrdersTab(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
        }

        By orderRows = By.XPath("//div[@class='flex-table__body order-history-table__body']/div");
        By amountCurrencyName = By.XPath("//input[@data-test='Buy Amount']//preceding::span[@class='label-in-input ap-input__label-in-input order-entry__label-in-input']");
        By priceCurrencyName = By.XPath("//label[@class='ap--label ap-input__label order-entry__label' and text()='Limit Price']//following::div//span[@class='label-in-input ap-input__label-in-input order-entry__label-in-input']");

        public int CountOfOrderRows()
        {
            return driver.FindElements(orderRows).Count;
        }

        public string AmountCurrencyNameText()
        {
            return driver.FindElement(amountCurrencyName).Text;
        }

        public string PriceCurrencyNameText()
        {
            return driver.FindElement(priceCurrencyName).Text;
        }

        ///This method will verify the order placed in Filled orders tab through Order Entry 
        public bool VerifyFilledOrdersTab(string instrument, string side, double size, string fee, string placeOrderTime, string placeOrderTimePlusOneMin)
        {
            try
            {
                var flag = false;
                string currencyText;
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                string buyAmountValue = GenericUtils.ConvertToDoubleFormat(size);
                double feeValueInDouble = Double.Parse(fee);
                string feeValue = GenericUtils.ConvertToDoubleFormat(feeValueInDouble);
                if (side.Equals(TestData.GetData("BuyTab")))
                {
                    orderEntryPage.SelectBuyLimitButton();
                    currencyText = AmountCurrencyNameText();
                }
                else
                {
                    orderEntryPage.SelectSellLimitButton();
                    currencyText = PriceCurrencyNameText();
                }

                string lastPrice = orderEntryPage.GetLastPrice();
                double doubleLastPrice = Convert.ToDouble(lastPrice);
                string totalAmountCalculated = GenericUtils.FilledOrdersTotalAmount(size, doubleLastPrice);
                Thread.Sleep(2000);
                UserCommonFunctions.FilledOrderTab(driver);
                string expectedRow_1 = instrument + " || " + side + " || " + buyAmountValue + " || " + lastPrice + " || " + totalAmountCalculated + " || " + feeValue + " " + currencyText + " || " + placeOrderTime;
                string expectedRow_2 = instrument + " || " + side + " || " + buyAmountValue + " || " + lastPrice + " || " + totalAmountCalculated + " || " + feeValue + " " + currencyText + " || " + placeOrderTimePlusOneMin;
                var filledOrdersList = GetListOfFilledOrders();
                if (filledOrdersList.Contains(expectedRow_1) || filledOrdersList.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInFilledOrdersTab, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderNotFoundInFilledOrdersTab, side));
                }
                return flag;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //This method will verify the order placed in Open orders tab through Order Entry 
        public bool VerifyOpenOrdersTab(string instrument, string side, string type, double size, string price, string placeOrderTime, string placeOrderTimePlusOneMin)
        {
            try
            {
                var flag = false;
                string orderSize = GenericUtils.ConvertToDoubleFormat(size);
                string priceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(price));

                UserCommonFunctions.OpenOrderTab(driver);
                string expectedRow_1 = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + priceValue + " || " + placeOrderTime;
                string expectedRow_2 = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + priceValue + " || " + placeOrderTimePlusOneMin;


                var listOfFilledOrders = GetListOfOpenOrders();
                if (listOfFilledOrders.Contains(expectedRow_1) || listOfFilledOrders.Contains(expectedRow_2))
                {
                    logger.LogCheckPoint(string.Format(LogMessage.OpenOrdersVerifiedSuccessfully, side));
                    flag = true;
                }
                return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method returns the list of all filled orders 
        public ArrayList GetListOfFilledOrders()
        {
            ArrayList filledOrderList = new ArrayList();
            int countOfFilledOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfFilledOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        if (j == 8)
                        {
                            continue;
                        }
                        textFinal = textFinal + " || " + text;
                    }

                }
                filledOrderList.Add(textFinal);
            }
            return filledOrderList;
        }

        //This method returns the list of all open orders 
        public ArrayList GetListOfOpenOrders()
        {
            ArrayList openOrderList = new ArrayList();
            int countOfOpenOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfOpenOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= (countItems - 2); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                openOrderList.Add(textFinal);
            }
            return openOrderList;
        }

        //This method will verify the order placed in Open orders tab through Order Entry 
        public bool VerifyInactiveOrdersTab(string instrument, string side, string type, double size, string price, string placeOrderTime, string placeOrderTimePlusOneMin, string status)
        {
            try
            {
                var flag = false;
                string orderSize = GenericUtils.ConvertToDoubleFormat(size);
                string priceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(price));

                UserCommonFunctions.InactiveTab(driver);
                string expectedRow_1 = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + priceValue + " || " + placeOrderTime + " || " + status;
                string expectedRow_2 = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + priceValue + " || " + placeOrderTimePlusOneMin + " || " + status;

                var listOfInactiveOrders = GetListOfInactiveOrders();
                if (listOfInactiveOrders.Contains(expectedRow_1) || listOfInactiveOrders.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInInactiveOrdersTab, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderNotFoundInInactiveOrdersTab, side));
                }
                return flag;
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        //This method returns the list of all Inactive orders 
        public ArrayList GetListOfInactiveOrders()
        {
            ArrayList inactiveOrderList = new ArrayList();
            int countOfInactiveOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfInactiveOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= (countItems); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                inactiveOrderList.Add(textFinal);
            }
            return inactiveOrderList;
        }



        //This method is used to wait for disabled button.
        public ArrayList WaitForButtonDisable(String buttonTitle)
        {

            ArrayList dateTimeList = new ArrayList();

            String dateTime = "";
            String dateTimeMinusOne = "";
            for (int i = 0; i <= 100; i++)
            {

                String cssCursorValue = driver.FindElement(By.XPath("//button[text()='" + buttonTitle + "']")).GetCssValue("cursor");
                if (cssCursorValue.Equals("not-allowed"))
                {
                    dateTimeList.Add(dateTime);
                    dateTimeList.Add(dateTimeMinusOne);
                    break;
                }
                Thread.Sleep(100);
            }
            return dateTimeList;
        }

        //This method will verify the order placed in Trade Reports tab through Report Block Trade
        public bool VerifyTradeReportsTab(string instrument, string side, string size, string price, string fee, string placeOrderTime, string placeOrderTimePlusOneMin, string status)
        {
            try
            {
                Thread.Sleep(2000);
                var flag = false;
                string orderSize = GenericUtils.ConvertToDoubleFormat(Double.Parse(size));
                string priceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(price));
                string feeValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(fee));
                Thread.Sleep(2000);
                UserCommonFunctions.TradeTab(driver);
                string expectedRow_1 = instrument + " || " + side +  " || " + orderSize + " || " + priceValue + " || " + feeValue + " || " + placeOrderTime + " || " + status;
                string expectedRow_2 = instrument + " || " + side +  " || " + orderSize + " || " + priceValue + " || " + feeValue + " || " + placeOrderTimePlusOneMin + " || " + status;

                var listOfInactiveOrders = GetListOfTradeReports();
                if (listOfInactiveOrders.Contains(expectedRow_1) || listOfInactiveOrders.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInTradeReportsTab, side));

                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderNotFoundInTradeReportsTab, side));
                }
                return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method returns the list of all Trade Reports
        public ArrayList GetListOfTradeReports()
        {
            ArrayList inactiveOrderList = new ArrayList();
            int countOfInactiveOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfInactiveOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= (countItems); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                inactiveOrderList.Add(textFinal);
            }
            return inactiveOrderList;
        }

        public void CancelOpenOrderFromOrderList(string instrument, string side, double size, string timeStamp)
        {

        }

        public void VerifyCancelledOrderFromOpenOrderList(string instrument, string side, double size, string timeStamp)
        {

        }
    }
}
